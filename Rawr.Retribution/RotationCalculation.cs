﻿using System;
using System.Collections.Generic;

namespace Rawr.Retribution
{
    public class RotationCalculation
    {
        public Skill CS { get { return skills[DamageAbility.CrusaderStrike]; } }
        public Skill HoLCS { get { return skills[DamageAbility.HandOfLightCS]; } }
        public Skill TV { get { return skills[DamageAbility.TemplarsVerdict]; } }
        public Skill HoLTV { get { return skills[DamageAbility.HandOfLightTV]; } }
        public Skill Judge { get { return skills[DamageAbility.Judgement]; } }
        public Skill Exo { get { return skills[DamageAbility.Exorcism]; } }
        public Skill HW { get { return skills[DamageAbility.HolyWrath]; } }
        public Skill HoW { get { return skills[DamageAbility.HammerOfWrath]; } }
        public Skill Cons { get { return skills[DamageAbility.Consecration]; } }
        public Skill Seal { get { return skills[DamageAbility.Seal]; } }
        public Skill SealDot { get { return skills[DamageAbility.SealDot]; } }
        public Skill SoC { get { return skills[DamageAbility.SoC]; } }
        public White White { get { return (White)skills[DamageAbility.White]; } }

        private Dictionary<Ability, float> remainingCd = new Dictionary<Ability, float>();
        private Dictionary<DamageAbility, float> casts = new Dictionary<DamageAbility, float>();
        private Dictionary<DamageAbility, Skill> skills = new Dictionary<DamageAbility, Skill>();

        public Stats Stats { get; private set; }
        public Character Character { get; private set; }
        public CalculationOptionsRetribution CalcOpts { get; private set; }

        public RotationCalculation(Character character, Stats stats)
        {
            Character = character;
            Stats = stats;
            CalcOpts = character.CalculationOptions as CalculationOptionsRetribution;
            dpChance = character.PaladinTalents.DivinePurpose * PaladinConstants.DP_CHANCE;
            fightlength = Character.BossOptions.BerserkTimer;

            #region Initialization
            casts[DamageAbility.Consecration] = 0f;
            casts[DamageAbility.CrusaderStrike] = 0f;
            casts[DamageAbility.Exorcism] = 0f;
            casts[DamageAbility.Inquisition] = 0f;
            casts[DamageAbility.HammerOfWrath] = 0f;
            casts[DamageAbility.HolyWrath] = 0f;
            casts[DamageAbility.Judgement] = 0f;
            casts[DamageAbility.TemplarsVerdict] = 0f;

            remainingCd[Ability.CrusaderStrike] = -1f;
            remainingCd[Ability.TemplarsVerdict] = -1f;
            remainingCd[Ability.Exorcism] = -1f;
            remainingCd[Ability.Inquisition] = -1f;
            remainingCd[Ability.HolyWrath] = -1f;
            remainingCd[Ability.HammerOfWrath] = -1f;
            remainingCd[Ability.Consecration] = -1f;
            remainingCd[Ability.Judgement] = -1f;

            skills[DamageAbility.CrusaderStrike] = new CrusaderStrike(Character, Stats);
            skills[DamageAbility.HandOfLightCS] = new HandofLight(Character, Stats, CS.AverageDamage);
            skills[DamageAbility.TemplarsVerdict] = new TemplarsVerdict(Character, Stats);
            skills[DamageAbility.HandOfLightTV] = new HandofLight(Character, Stats, TV.AverageDamage);
            skills[DamageAbility.White] = new White(Character, Stats);
            skills[DamageAbility.Exorcism] = new Exorcism(Character, Stats, White.CT.ChanceToLand);
            skills[DamageAbility.Inquisition] = new Inquisition(Character, Stats, CalcOpts.HPperInq);
            skills[DamageAbility.HolyWrath] = new HolyWrath(Character, Stats);
            skills[DamageAbility.HammerOfWrath] = new HammerOfWrath(Character, Stats);
            skills[DamageAbility.Consecration] = new Consecration(Character, Stats);
            skills[DamageAbility.SoC] = new SealOfCommand(Character, Stats);

            switch (CalcOpts.Seal)
            {
                case SealOf.Righteousness:
                    skills[DamageAbility.Seal] = new SealOfRighteousness(Character, Stats);
                    skills[DamageAbility.SealDot] = new NullSealDoT(Character, Stats);
                    skills[DamageAbility.Judgement] = new JudgementOfRighteousness(Character, Stats);
                    break;

                case SealOf.Truth:
                    skills[DamageAbility.Seal] = new SealOfTruth(Character, Stats);
                    /*skills[DamageAbility.SealDot] = new SealOfTruthDoT(combats, 0f);
                    skills[DamageAbility.Judgement] = new JudgementOfTruth(combats, 0f);*/
                    float stack = 5f;// AverageSoTStackSize();
                    skills[DamageAbility.SealDot] = new SealOfTruthDoT(Character, Stats, stack);
                    skills[DamageAbility.Judgement] = new JudgementOfTruth(Character, Stats, stack);
                    break;

                default:
                    skills[DamageAbility.Seal] = new NullSeal(Character, Stats);
                    skills[DamageAbility.SealDot] = new NullSealDoT(Character, Stats);
                    skills[DamageAbility.Judgement] = new NullJudgement(Character, Stats);
                    break;
            }

            inqRefresh = CalcOpts.InqRefresh;
            skipToCrusader = CalcOpts.SkipToCrusader;
            #endregion
            CalcRotation();
        }

        #region Rotation things
        private Ability[] allAb = { Ability.Consecration, Ability.CrusaderStrike, Ability.Exorcism, Ability.HammerOfWrath, Ability.HolyWrath, Ability.Inquisition, Ability.Judgement, Ability.TemplarsVerdict };

        private float fightlength;
        private float fightcorrVal = 5f;
        private float latency = .1f;
        private float inqRefresh;
        private float skipToCrusader;

        private float inquptime = 0f;
        private float holyPower = 0f;
        private float time = 0f;
        private bool below20 = false;
        private float dpChance;
        private float holyPowerDP = 0f;
        
        public void CalcRotation()
        {
            RotState state = RotState.CS;
            float tempFightlength = fightlength * fightcorrVal;

            while (time < tempFightlength)
            {
                DoInq();
                switch (state)
                {
                    case RotState.CS:
                        DoCS();
                        state = RotState.FillerOne;
                        break;
                    case RotState.FillerOne:
                        DoFiller();
                        state = RotState.FillerTwo;
                        break;
                    case RotState.FillerTwo:
                        if (remainingCd[Ability.CrusaderStrike] >= skipToCrusader)
                        {
                            if (DoFiller())
                                state = RotState.CS;
                        }
                        else
                        {
                            if (remainingCd[Ability.CrusaderStrike] > 0f)
                                TriggerCD(remainingCd[Ability.CrusaderStrike]);
                            state = RotState.CS;
                        }
                        break;
                }
            }

            //Correct to float values
            foreach (KeyValuePair<DamageAbility, Skill> kvp in skills)
            {
                if (casts.ContainsKey(kvp.Key))
                    casts[kvp.Key] = casts[kvp.Key] / fightcorrVal;
            }

            casts[DamageAbility.HandOfLightCS] = casts[DamageAbility.CrusaderStrike];
            casts[DamageAbility.HandOfLightTV] = casts[DamageAbility.TemplarsVerdict];
            casts[DamageAbility.White] = fightlength / AbilityHelper.WeaponSpeed(Character, Stats.PhysicalHaste);
            casts[DamageAbility.SoC] = casts[DamageAbility.Seal] = (float)(fightlength * SealProcsPerSec(Seal));
            casts[DamageAbility.SealDot] = (float)(fightlength * SealDotProcPerSec(Seal));

            //Inq only last until end of fight not longer => prevent > 100% uptime
            inquptime = (inquptime - remainingCd[Ability.Inquisition]) / tempFightlength;

            //UsagePerSecCalc
            foreach (KeyValuePair<DamageAbility, Skill> kvp in skills)
            {
                kvp.Value.UsagePerSec = casts[kvp.Key] / (double)fightlength;
                kvp.Value.InqUptime = inquptime;
            }
            //Seals
            casts[DamageAbility.SoC] = casts[DamageAbility.Seal] = (float)(fightlength * SealProcsPerSec(Seal));
            skills[DamageAbility.SoC].UsagePerSec = skills[DamageAbility.Seal].UsagePerSec = casts[DamageAbility.Seal] / (double)fightlength;
        }

        private void DoInq()
        {
            if ((remainingCd[Ability.Inquisition] <= inqRefresh) && (HasHolyPower(CalcOpts.HPperInq)))
            {
                inquptime += skills[DamageAbility.Inquisition].Cooldown - (remainingCd[Ability.Inquisition] > 0f ? remainingCd[Ability.Inquisition] : 0f);
                DoCast(Ability.Inquisition);
                UseHolyPower(CalcOpts.HPperInq);
                holyPowerDP += dpChance;
            }
        }

        private void DoCS()
        {
            //Cast Crusaderstrike
            if (remainingCd[Ability.CrusaderStrike] <= 0f && holyPower < 3f)
            {
                DoCast(Ability.CrusaderStrike);
                holyPower += 1;
            }
        }

        private bool DoFiller()
        {
            //Cast Templar's Verdict
            if (HasHolyPower(3))
            {
                DoCast(Ability.TemplarsVerdict);
                UseHolyPower(3);
                holyPowerDP += dpChance;
            } else
            //Cast Hammer of Wrath
            if (below20 && remainingCd[Ability.HammerOfWrath] <= 0f)
            {
                DoCast(Ability.HammerOfWrath);
                holyPowerDP += dpChance;
            } else
            //Cast Exo
            if (remainingCd[Ability.Exorcism] <= 0f)
            {
                DoCast(Ability.Exorcism);
                holyPowerDP += dpChance;
            } else
            //Cast Judge
            if (remainingCd[Ability.Judgement] <= 0f)
            {
                DoCast(Ability.Judgement);
                holyPowerDP += dpChance;
            } else
            //Cast Holy Wrath
            if (remainingCd[Ability.HolyWrath] <= 0f)
            {
                DoCast(Ability.HolyWrath);
                holyPowerDP += dpChance;
            } else
            //Cast Cons
            if (remainingCd[Ability.Consecration] <= 0f)
            {
                DoCast(Ability.Consecration);
            } else
            {
                //Wait till one cd is ready
                DoNothing();
                return false;
            }
            return true;
        }

        private void DoNothing()
        {
            TriggerCD(GetLowestCd());
        }

        private float GetLowestCd()
        {
            float lCd = 100;
            foreach (KeyValuePair<Ability, float> kvp in remainingCd)
            {
                if (kvp.Value < lCd)
                    if (kvp.Key == Ability.CrusaderStrike ||
                        kvp.Key == Ability.Consecration ||
                        kvp.Key == Ability.Exorcism ||
                        kvp.Key == Ability.HolyWrath ||
                        kvp.Key == Ability.Judgement ||
                        (kvp.Key == Ability.HammerOfWrath && below20)
                       )
                        lCd = kvp.Value;
            }
            return (lCd < 0f ? 0f : lCd);
        }

        private void DoCast(Ability ability)
        {
            casts[(DamageAbility)ability] += 1f;
            remainingCd[ability] = skills[(DamageAbility)ability].Cooldown;
            TriggerCD(skills[(DamageAbility)ability].GCD + latency);
        }

        private void TriggerCD(float CD)
        {
            time += CD;
            foreach (Ability abi in allAb)
                remainingCd[abi] -= CD;
        }

        private bool HasHolyPower(int ReqHP)
        {
            if (holyPowerDP > 1f)
                return true;
            else
                return holyPower >= ReqHP;
        }

        private void UseHolyPower(int ReqHP)
        {
            if (holyPowerDP > 1f)
                holyPowerDP -= 1f;
            else
                holyPower -= ReqHP;
        }
        #endregion

        public void SetDPS(CharacterCalculationsRetribution calc)
        {
            calc.WhiteSkill = White;
            calc.SealSkill = Seal;
            calc.SealDotSkill = SealDot;
            calc.CommandSkill = SoC;
            calc.JudgementSkill = Judge;
            calc.TemplarsVerdictSkill = TV;
            calc.HandOfLightTVSkill = HoLTV;
            calc.CrusaderStrikeSkill = CS;
            calc.HandOfLightCSSkill = HoLCS;
            calc.ConsecrationSkill = Cons;
            calc.ExorcismSkill = Exo;
            calc.HolyWrathSkill = HW;
            calc.HammerOfWrathSkill = HoW;

            calc.DPSPoints = White.GetDPS() +
                Seal.GetDPS() +
                SealDot.GetDPS() +
                SoC.GetDPS() +
                Judge.GetDPS() +
                CS.GetDPS() +
                HoLCS.GetDPS() +
                TV.GetDPS() +
                HoLTV.GetDPS() +
                Exo.GetDPS() +
                HW.GetDPS() +
                Cons.GetDPS() +
                HoW.GetDPS() +
                calc.OtherDPS;
        }

        #region Ability per second
        public double SealProcsPerSec(Skill seal)
        {
            if (seal.GetType() == typeof(SealOfTruth))
                return GetMeleeAttacksPerSec() + GetRangedAttacksPerSec() + GetAbilityHitsPerSecond(Exo);
            if (seal.GetType() == typeof(SealOfRighteousness))
                return GetMeleeAttacksPerSec() + GetAbilityHitsPerSecond(HoW);
            else
                return 0d;
        }

        public double SealDotProcPerSec(Skill seal)
        {
            if (seal.GetType() == typeof(SealOfTruth))
                return 1 / (3d / (1 + Stats.PhysicalHaste));
            else
                return 0d;
        }

        public double GetAbilityHitsPerSecond(Skill skill)
        {
            return
                skill.UsagePerSec *
                skill.CT.ChanceToLand *
                skill.Targets() *
                skill.TickCount();
        }

        public double GetAbilityCritsPerSecond(Skill skill)
        {
            return
                skill.UsagePerSec *
                skill.CT.ChanceToCrit *
                skill.Targets() *
                skill.TickCount();
        }

        public double GetMeleeAttacksPerSec()
        {
            return
                GetAbilityHitsPerSecond(CS) +
                GetAbilityHitsPerSecond(White) + 
                GetAbilityHitsPerSecond(TV);
        }

        private double GetRangedAttacksPerSec()
        {
            return
                GetAbilityHitsPerSecond(Judge) +
                GetAbilityHitsPerSecond(HoW);
        }

        public double GetSpellAttacksPerSec()
        {
            return
                GetAbilityHitsPerSecond(Exo) +
                GetAbilityHitsPerSecond(HW) +
                GetAbilityHitsPerSecond(SealDot);
        }

        public double GetPhysicalAttacksPerSec()
        {
            return
                GetMeleeAttacksPerSec() +
                GetRangedAttacksPerSec();
        }

        public double GetMeleeCritsPerSec()
        {
            return
                GetAbilityCritsPerSecond(CS) +
                GetAbilityCritsPerSecond(White) + 
                GetAbilityCritsPerSecond(TV);
        }

        public double GetRangeCritsPerSec()
        {
            return
                GetAbilityCritsPerSecond(Judge) +
                GetAbilityCritsPerSecond(HoW);
        }

        public double GetSpellCritsPerSec()
        {
            return
                GetAbilityCritsPerSecond(Exo) +
                GetAbilityCritsPerSecond(HW) +
                GetAbilityCritsPerSecond(SealDot);
        }

        public double GetPhysicalCritsPerSec()
        {
            return
                GetMeleeCritsPerSec() +
                GetRangeCritsPerSec();
        }

        public double GetAttacksPerSec()
        {
            return
                GetMeleeAttacksPerSec() +
                GetRangedAttacksPerSec() +
                GetSpellAttacksPerSec();
        }
        #endregion
    }
}