﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Rawr.Warlock {

    /// <summary>
    /// Calculates a Warlock's DPS and Spell Stats.
    /// </summary>
    public class CharacterCalculationsWarlock : CharacterCalculationsBase {

        #region overridden properties
        public override float OverallPoints { get; set; }
        public override float[] SubPoints { get; set; }
        #endregion


        #region subclass specific properties

        public float PersonalDps { get { return SubPoints[0]; } }
        public float PetDps { get { return SubPoints[1]; } }

        public Character Character { get; private set; }
        public Pet Pet { get; private set; }
        public Stats PreProcStats { get; private set; }
        public Stats Stats { get; private set; }
        public CalculationOptionsWarlock Options { get; private set; }
        public WarlockTalents Talents { get; private set; }
        public SpellModifiers SpellModifiers { get; private set; }
        public List<WeightedStat> Haste { get; private set; }

        public float BaseMana { get; private set; }
        public float HitChance { get; private set; }
        public float AvgTimeUsed { get; private set; }
        public float MaxCritChance { get; private set; }

        public List<Spell> Priorities { get; private set; }
        public Dictionary<string, Spell> Spells { get; private set; }
        public Dictionary<string, Spell> CastSpells { get; private set; }

        #endregion


        #region constructors

        public CharacterCalculationsWarlock() { }

        public CharacterCalculationsWarlock(Character character, Stats stats) {

            Character = character;
            Options = (CalculationOptionsWarlock) character.CalculationOptions;
            if (Options == null) {
                Options = CalculationOptionsWarlock.MakeDefaultOptions();
            }
            Talents = character.WarlockTalents;
            Stats = stats;
            PreProcStats = Stats.Clone();
            BaseMana = BaseStats.GetBaseStats(character).Mana;
            Spells = new Dictionary<string, Spell>();
            CastSpells = new Dictionary<string, Spell>();
            HitChance
                = Math.Min(
                    1f,
                    Options.GetBaseHitRate() / 100f + CalcSpellHit());

            if (!Options.Pet.Equals("None")
                && (!Options.Pet.Equals("Felguard")
                    || Talents.SummonFelguard > 0)) {

                Type type = Type.GetType("Rawr.Warlock." + Options.Pet);
                Pet = (Pet) Activator.CreateInstance(
                        type, new object[] { this });
                Stats.SpellPower
                    += Talents.DemonicKnowledge
                        * .04f
                        * (Pet.CalcStamina() + Pet.CalcIntellect());
            }

            float personalDps = CalcPersonalDps();
            float petDps = CalcPetDps();
            SubPoints = new float[] { personalDps, petDps };
            OverallPoints = personalDps + petDps;
        }

        #endregion


        #region Stat accessors

        public float CalcStamina() { return GetStamina(Stats); }
        public float GetStamina(Stats stats) {

            return stats.Stamina * (1f + stats.BonusStaminaMultiplier);
        }

        public float CalcIntellect() { return GetIntellect(Stats); }
        public float GetIntellect(Stats stats) {

            return stats.Intellect * (1f + stats.BonusIntellectMultiplier);
        }

        public float CalcSpirit() { return GetSpirit(Stats); }
        public float GetSpirit(Stats stats) {

            return stats.Spirit * (1f + stats.BonusSpiritMultiplier);
        }

        public float CalcHealth() { return GetHealth(Stats); }
        public float GetHealth(Stats stats) {

            return (stats.Health
                    + StatConversion.GetHealthFromStamina(GetStamina(stats)))
                * (1 + stats.BonusHealthMultiplier);
        }

        public float CalcMana() { return GetMana(Stats); }
        public float GetMana(Stats stats) {

            return (1 + stats.BonusManaMultiplier)
                * (stats.Mana
                    + StatConversion.GetManaFromIntellect(GetIntellect(stats)));
        }

        public float CalcUsableMana(float fightLen) {
            return GetUsableMana(Stats, fightLen);
        }
        public float GetUsableMana(Stats stats, float fightLen) {

            float mps
                = stats.Mp5 / 5f
                    + stats.Mana
                        * Math.Max(
                            stats.ManaRestoreFromMaxManaPerSecond,
                            .002f
                                * Spell.CalcUprate(
                                    Talents.ImprovedSoulLeech * .5f,
                                    15f,
                                    AvgTimeUsed * 1.1f));
            return GetMana(stats) + stats.ManaRestore + mps * fightLen;
        }

        public float CalcSpellCrit() { return GetSpellCrit(Stats); }
        public float GetSpellCrit(Stats stats) {

            return stats.SpellCrit
                + StatConversion.GetSpellCritFromIntellect(GetIntellect(stats))
                + StatConversion.GetSpellCritFromRating(
                    stats.WarlockFirestoneSpellCritRating
                            * (1f + Talents.MasterConjuror * 1.5f)
                        + stats.CritRating)
                + stats.BonusCritChance
                + stats.SpellCritOnTarget;
        }

        public float CalcHasteRating() { return GetHasteRating(Stats); }
        public float GetHasteRating(Stats stats) {

            return stats.HasteRating
                + stats.WarlockSpellstoneHasteRating
                    * (1f + Talents.MasterConjuror * 1.5f);
        }

        public float CalcSpellHit() { return GetSpellHit(Stats); }
        public float GetSpellHit(Stats stats) {

            return stats.SpellHit
                + StatConversion.GetSpellHitFromRating(stats.HitRating);
        }

        public float CalcSpellPower() { return GetSpellPower(Stats); }
        public float GetSpellPower(Stats stats) {

            float aegis = 1 + Talents.DemonicAegis * 0.10f;
            return (stats.SpellPower
                    + (stats.WarlockFelArmor > 0
                        ? aegis
                            * (.3f * GetSpirit(stats) + stats.WarlockFelArmor)
                        : 0f))
                * (1f + stats.BonusSpellPowerMultiplier);
        }

        public float CalcBonusDamageMultiplier() {
            return GetBonusDamageMultiplier(Stats);
        }
        public float GetBonusDamageMultiplier(Stats stats) {

            return stats.BonusDamageMultiplier;
        }

        public float CalcBonusCritMultiplier() {
            return GetBonusCritMultiplier(Stats);
        }
        public float GetBonusCritMultiplier(Stats stats) {

            return stats.BonusCritMultiplier;
        }

        public float CalcWarlockFirestoneDirectDamageMultiplier() {
            return GetWarlockFirestoneDirectDamageMultiplier(Stats);
        }
        public float GetWarlockFirestoneDirectDamageMultiplier(Stats stats) {

            return stats.WarlockFirestoneDirectDamageMultiplier;
        }

        public float CalcWarlockSpellstoneDotDamageMultiplier() {
            return GetWarlockSpellstoneDotDamageMultiplier(Stats);
        }
        public float GetWarlockSpellstoneDotDamageMultiplier(Stats stats) {

            return stats.WarlockSpellstoneDotDamageMultiplier;
        }

        public float CalcBonusShadowDamageMultiplier() {
            return GetBonusShadowDamageMultiplier(Stats);
        }
        public float GetBonusShadowDamageMultiplier(Stats stats) {

            return stats.BonusShadowDamageMultiplier;
        }

        public float CalcBonusFireDamageMultiplier() {
            return GetBonusFireDamageMultiplier(Stats);
        }
        public float GetBonusFireDamageMultiplier(Stats stats) {

            return stats.BonusFireDamageMultiplier;
        }

        public float CalcSpellHaste() { return GetSpellHaste(Stats); }
        public float GetSpellHaste(Stats stats) {

            return 1
                + stats.SpellHaste
                + StatConversion.GetSpellHasteFromRating(GetHasteRating(stats));
        }

        #endregion


        #region the overridden method (GetCharacterDisplayCalculationValues)
        /// <summary>
        /// Builds a dictionary containing the values to display for each of the
        /// calculations defined in CharacterDisplayCalculationLabels. The key
        /// should be the Label of each display calculation, and the value
        /// should be the value to display, optionally appended with '*'
        /// followed by any string you'd like displayed as a tooltip on the
        /// value.
        /// </summary>
        /// <returns>
        /// A Dictionary<string, string> containing the values to display for
        /// each of the calculations defined in
        /// CharacterDisplayCalculationLabels.
        /// </returns>
        public override Dictionary<string, string>
            GetCharacterDisplayCalculationValues() {

            Dictionary<string, string> dictValues
                = new Dictionary<string, string>();

            dictValues.Add("Personal DPS", string.Format("{0:0}", PersonalDps));
            dictValues.Add("Pet DPS", string.Format("{0:0}", PetDps));
            dictValues.Add("Total DPS", string.Format("{0:0}", OverallPoints));

            dictValues.Add(
                "Health",
                string.Format(
                    "{0:0.0}*{1:0.0} stamina",
                    CalcHealth(),
                    CalcStamina()));
            dictValues.Add(
                "Mana",
                string.Format(
                    "{0:0.0}*{1:0.0} intellect",
                    CalcMana(),
                    CalcIntellect()));
            dictValues.Add(
                "Spirit", string.Format("{0:0.0}", CalcSpirit()));

            dictValues.Add(
                "Bonus Damage",
                string.Format(
                    "{0:0.0}*{1:0.0}\tBefore Procs",
                    CalcSpellPower(),
                    GetSpellPower(PreProcStats)));

            #region Hit Rating
            float onePercentOfHitRating
                = (1 / StatConversion.GetSpellHitFromRating(1));
            float hitFromRating
                = StatConversion.GetSpellHitFromRating(Stats.HitRating);
            float hitFromTalents = Talents.Suppression * 0.01f;
            float hitFromBuffs
                = (CalcSpellHit() - hitFromRating - hitFromTalents);
            float targetHit = Options.GetBaseHitRate() / 100f;
            float totalHit = targetHit + CalcSpellHit();
            float missChance = totalHit > 1 ? 0 : (1 - totalHit);
            dictValues.Add(
                "Hit Rating",
                string.Format(
                    "{0}*{1:0.00%} Hit Chance (max 100%) | {2:0.00%} Miss Chance \r\n\r\n"
                        + "{3:0.00%}\t Base Hit Chance on a Level {4:0} target\r\n"
                        + "{5:0.00%}\t from {6:0} Hit Rating [gear, food and/or flasks]\r\n"
                        + "{7:0.00%}\t from Talent: Suppression\r\n"
                        + "{8:0.00%}\t from Buffs: Racial and/or Spell Hit Chance Taken\r\n\r\n"
                        + "You are {9} hit rating {10} the 446 hard cap [no hit from gear, talents or buffs]\r\n\r\n"
                        + "Hit Rating soft caps:\r\n"
                        + "420 - Heroic Presence\r\n"
                        + "368 - Suppression\r\n"
                        + "342 - Suppression and Heroic Presence\r\n"
                        + "289 - Suppression, Improved Faerie Fire / Misery\r\n"
                        + "263 - Suppression, Improved Faerie Fire / Misery and  Heroic Presence",
                    Stats.HitRating,
                    totalHit,
                    missChance,
                    targetHit,
                    Options.TargetLevel,
                    hitFromRating,
                    Stats.HitRating,
                    hitFromTalents,
                    hitFromBuffs,
                    Math.Ceiling(
                        Math.Abs((totalHit - 1) * onePercentOfHitRating)),
                    (totalHit > 1) ? "above" : "below"));
            #endregion

            dictValues.Add(
                "Crit Chance",
                string.Format(
                    "{0:0.00%}*{1:0.00%}\tBefore Procs",
                    CalcSpellCrit(),
                    GetSpellCrit(PreProcStats)));

            dictValues.Add(
                "Haste Rating",
                string.Format(
                    "{0:0.00}%*"
                        + "{1:0.00}s\tGlobal Cooldown\n"
                        + "{2:0.00}%\tBefore Procs",
                    (CalcSpellHaste() - 1) * 100f,
                    Math.Max(1.0f, 1.5f / CalcSpellHaste()),
                    (GetSpellHaste(PreProcStats) - 1) * 100f));

            // Pet Stats
            if (Pet == null) {
                dictValues.Add("Pet Stamina", "-");
                dictValues.Add("Pet Intellect", "-");
                dictValues.Add("Pet Health", "-");
            } else {
                dictValues.Add(
                    "Pet Stamina",
                    string.Format("{0:0.0}", Pet.CalcStamina()));
                dictValues.Add(
                    "Pet Intellect",
                    string.Format("{0:0.0}", Pet.CalcIntellect()));
                dictValues.Add(
                    "Pet Health",
                    string.Format("{0:0.0}", Pet.CalcHealth()));
            }


            // Spell Stats
            foreach (string spellName in Spell.ALL_SPELLS) {
                if (CastSpells.ContainsKey(spellName)) {
                    dictValues.Add(
                        spellName, CastSpells[spellName].GetToolTip());
                } else {
                    dictValues.Add(spellName, "-");
                }
            }

            return dictValues;
        }
        #endregion


        #region dps calculations

        private float CalcPersonalDps() {

            if (Options.GetActiveRotation().GetError() != null) {
                return 0f;
            }

            CalcHasteAndManaProcs();
            AvgTimeUsed
                = Spell.GetTimeUsed(
                    CalculationsWarlock.AVG_UNHASTED_CAST_TIME,
                    0f,
                    Haste,
                    Options.Latency);

            float timeRemaining = Options.Duration;
            float manaRemaining = CalcUsableMana(timeRemaining);

            #region Calculate NumCasts for each spell
            Priorities = new List<Spell>();
            foreach (
                string spellName
                in Options.GetActiveRotation().GetPrioritiesForCalcs(Talents)) {

                Spell spell = GetSpell(spellName);
                if (spell.IsCastable()) {
                    Priorities.Add(spell);
                    CastSpells.Add(spellName, spell);
                }
            }
            Spell filler = GetSpell(Options.GetActiveRotation().Filler);
            RecordCollisionDelays(new CastingState(this, filler));
            foreach (Spell spell in Priorities) {
                float numCasts = spell.GetNumCasts();
                timeRemaining -= spell.GetAvgTimeUsed() * numCasts;
                manaRemaining -= spell.ManaCost * numCasts;
            }
            LifeTap lifeTap = (LifeTap) GetSpell("Life Tap");
            timeRemaining
                -= lifeTap.GetAvgTimeUsed()
                    * lifeTap.AddCastsForRegen(
                        timeRemaining, manaRemaining, filler);
            filler.Spam(timeRemaining);
            CastSpells.Add(Options.GetActiveRotation().Filler, filler);
            #endregion

            #region Calculate spell modifiers

            // add procs to RawStats
            if (CastSpells.ContainsKey("Curse Of The Elements")) {

                // If the raid is already providing this debuff, the curse will
                // not actually end up casting, so this will not double-count
                // the debuff.
                Stats.BonusFireDamageMultiplier
                    = Stats.BonusShadowDamageMultiplier
                    = Stats.BonusHolyDamageMultiplier 
                    = Stats.BonusFrostDamageMultiplier
                    = Stats.BonusNatureDamageMultiplier
                    = .13f;
            }
            if (Talents.ImprovedShadowBolt > 0
                && Stats.SpellCritOnTarget < .05f) {

                // TODO this should somehow affect Pyroclasm

                // If the 5% crit debuff is not already being maintained by
                // somebody else (i.e. it's not selected in the buffs tab), we
                // may supply it via Improved Shadow Bolt.
                float casts = 0f;
                if (CastSpells.ContainsKey("Shadow Bolt")) {
                    casts += CastSpells["Shadow Bolt"].GetNumCasts();
                }
                if (CastSpells.ContainsKey("Shadow Bolt (Instant)")) {
                    casts += CastSpells["Shadow Bolt (Instant)"].GetNumCasts();
                }
                float uprate = Spell.CalcUprate(
                    Talents.ImprovedShadowBolt * .2f, // proc rate
                    30f, // duration
                    Options.Duration / casts); // trigger period
                float benefit = .05f - Stats.SpellCritOnTarget;
                Stats.SpellCritOnTarget += benefit * uprate;
            }
            Stats.SpellPower += lifeTap.GetAvgBonusSpellPower();

            // create the SpellModifiers object
            SpellModifiers = new SpellModifiers();
            SpellModifiers.AddMultiplicativeMultiplier(
                Stats.BonusDamageMultiplier);
            SpellModifiers.AddMultiplicativeMultiplier(
                Talents.Malediction * .01f);
            SpellModifiers.AddMultiplicativeMultiplier(
                Talents.DemonicPact * .02f);
            SpellModifiers.AddCritChance(CalcSpellCrit());
            SpellModifiers.AddCritOverallMultiplier(
                Stats.BonusCritMultiplier);
            if (Talents.Metamorphosis > 0) {
                SpellModifiers.AddMultiplicativeMultiplier(
                    GetMetamorphosisBonus());
            }
            if (Pet is Felguard) {
                SpellModifiers.AddMultiplicativeMultiplier(
                    Talents.MasterDemonologist * .01f);
            }
            if (Stats.Warlock4T10 > 0) {
                Spell trigger = null;
                if (CastSpells.ContainsKey("Immolate")) {
                    trigger = CastSpells["Immolate"];
                } else if (CastSpells.ContainsKey("Unstable Affliction")) {
                    trigger = CastSpells["Unstable Affliction"];
                }
                if (trigger != null) {
                    float numTicks = trigger.GetNumCasts() * trigger.NumTicks;
                    float uprate
                        = Spell.CalcUprate(
                            .15f, 10f, Options.Duration / numTicks);
                    SpellModifiers.AddMultiplicativeMultiplier(.1f * uprate);
                }
            }

            // finilize each spell's modifiers.
            // Start with Conflagrate, since pyroclasm depends on its results.
            if (CastSpells.ContainsKey("Conflagrate")) {
                CastSpells["Conflagrate"].FinalizeSpellModifiers();
            }
            foreach (Spell spell in CastSpells.Values) {
                if (!(spell is Conflagrate)) {
                    spell.FinalizeSpellModifiers();
                }
            }
            #endregion

            float damageDone = CalcRemainingProcs();

            #region Calculate damage done for each spell
            Spell conflagrate = null;
            float spellPower = CalcSpellPower();
            foreach (KeyValuePair<string, Spell> pair in CastSpells) {
                Spell spell = pair.Value;
                if (pair.Key.Equals("Conflagrate")) {
                    conflagrate = spell;
                    continue; // save until we're sure immolate is done
                }
                spell.SetDamageStats(spellPower);
                damageDone += spell.GetNumCasts() * spell.AvgDamagePerCast;
            }
            if (conflagrate != null) {
                conflagrate.SetDamageStats(spellPower);
                damageDone
                    += conflagrate.GetNumCasts() * conflagrate.AvgDamagePerCast;
            }
            #endregion

            return damageDone / Options.Duration;
        }

        private float CalcPetDps() {

            return 0f;
        }

        private void CalcHasteAndManaProcs() {

            if (Options.NoProcs) {
                WeightedStat staticHaste = new WeightedStat();
                staticHaste.Chance = 1f;
                staticHaste.Value = GetSpellHaste(PreProcStats);
                Haste = new List<WeightedStat> { staticHaste };
                return;
            }

            // the trigger rates are all guestimates at this point, since the
            // real values depend on haste (which obviously has not been
            // finalized yet)

            Dictionary<int, float> periods
                = new Dictionary<int, float>();
            Dictionary<int, float> chances
                = new Dictionary<int, float>();
            PopulateTriggers(periods, chances);

            List<SpecialEffect> hasteEffects = new List<SpecialEffect>();
            List<float> hasteIntervals = new List<float>();
            List<float> hasteChances = new List<float>();
            List<float> hasteOffsets = new List<float>();
            List<float> hasteScales = new List<float>();
            List<float> hasteValues = new List<float>();
            List<SpecialEffect> hasteRatingEffects = new List<SpecialEffect>();
            List<float> hasteRatingIntervals = new List<float>();
            List<float> hasteRatingChances = new List<float>();
            List<float> hasteRatingOffsets = new List<float>();
            List<float> hasteRatingScales = new List<float>();
            List<float> hasteRatingValues = new List<float>();
            Stats procStats = new Stats();
            foreach (SpecialEffect effect in Stats.SpecialEffects()) {
                if (!periods.ContainsKey((int) effect.Trigger)) {
                    continue;
                }

                if (effect.Stats.HasteRating > 0) {
                    hasteRatingEffects.Add(effect);
                    hasteRatingIntervals.Add(periods[(int) effect.Trigger]);
                    hasteRatingChances.Add(chances[(int) effect.Trigger]);
                    if (IsDoublePot(effect)) {
                        hasteRatingOffsets.Add(.75f * Options.Duration);
                    } else {
                        hasteRatingOffsets.Add(0f);
                    }
                    hasteRatingScales.Add(1f);
                    hasteRatingValues.Add(effect.Stats.HasteRating);
                }
                if (effect.Stats.SpellHaste > 0) {
                    hasteEffects.Add(effect);
                    hasteIntervals.Add(periods[(int) effect.Trigger]);
                    hasteChances.Add(chances[(int) effect.Trigger]);
                    hasteOffsets.Add(0f);
                    hasteScales.Add(1f);
                    hasteValues.Add(effect.Stats.SpellHaste);
                }
            }
            WeightedStat[] ratings
                = SpecialEffect.GetAverageCombinedUptimeCombinations(
                    hasteRatingEffects.ToArray(),
                    hasteRatingIntervals.ToArray(),
                    hasteRatingChances.ToArray(),
                    hasteRatingOffsets.ToArray(),
                    hasteRatingScales.ToArray(),
                    CalculationsWarlock.AVG_UNHASTED_CAST_TIME,
                    Options.Duration,
                    hasteRatingValues.ToArray());
            WeightedStat[] percentages
                = SpecialEffect
                        .GetAverageCombinedUptimeCombinationsMultiplicative(
                    hasteEffects.ToArray(),
                    hasteIntervals.ToArray(),
                    hasteChances.ToArray(),
                    hasteOffsets.ToArray(),
                    hasteScales.ToArray(),
                    CalculationsWarlock.AVG_UNHASTED_CAST_TIME,
                    Options.Duration,
                    hasteValues.ToArray());
            float staticRating = CalcHasteRating();
            Haste = new List<WeightedStat>();
            for (int p = percentages.Length, f = 0; --p >= 0; ) {
                if (percentages[p].Chance == 0) {
                    continue;
                }
                for (int r = ratings.Length; --r >= 0; ++f) {
                    if (ratings[r].Chance == 0) {
                        continue;
                    }
                    WeightedStat s = new WeightedStat();
                    s.Chance = percentages[p].Chance * ratings[r].Chance;
                    s.Value
                        = (1 + percentages[p].Value)
                            * (1 + StatConversion.GetSpellHasteFromRating(
                                    ratings[r].Value + staticRating))
                            * (1 + Stats.SpellHaste);
                    Haste.Add(s);
                }
            }
        }

        private float CalcRemainingProcs() {

            float procdDamage = 0f;
            MaxCritChance = CalcSpellCrit();

            if (Options.NoProcs) {
                return procdDamage;
            }

            Dictionary<int, float> periods
                = new Dictionary<int, float>();
            Dictionary<int, float> chances
                = new Dictionary<int, float>();
            PopulateTriggers(periods, chances);
            Stats procStats = new Stats();
            foreach (SpecialEffect effect in Stats.SpecialEffects()) {
                if (!periods.ContainsKey((int) effect.Trigger)) {
                    continue;
                }

                float interval = periods[(int) effect.Trigger];
                float chance = chances[(int) effect.Trigger];

                Stats effectStats = effect.Stats;
                if (effectStats.ValkyrDamage > 0) {
                    SpellModifiers mods = new SpellModifiers();
                    mods.AddCritChance(.05f + Stats.SpellCritOnTarget);
                    mods.AddMultiplicativeMultiplier(
                        Stats.BonusHolyDamageMultiplier);
                    procdDamage
                        += CalcDamageProc(
                            effect,
                            effect.Stats.ValkyrDamage,
                            periods[(int) Trigger.DamageDone],
                            chance,
                            mods);
                } else if (effectStats.ShadowDamage > 0) {
                    SpellModifiers mods = new SpellModifiers();
                    mods.Accumulate(SpellModifiers);
                    mods.AddAdditiveDirectMultiplier(
                        CalcWarlockFirestoneDirectDamageMultiplier());
                    AddShadowModifiers(mods);
                    procdDamage
                        += CalcDamageProc(
                            effect,
                            effect.Stats.ShadowDamage,
                            interval,
                            chance,
                            mods);
                } else if (effectStats.FireDamage > 0) {
                    SpellModifiers mods = new SpellModifiers();
                    mods.Accumulate(SpellModifiers);
                    mods.AddAdditiveDirectMultiplier(
                        CalcWarlockFirestoneDirectDamageMultiplier());
                    AddShadowModifiers(mods);
                    AddFireModifiers(mods);
                    procdDamage
                        += CalcDamageProc(
                            effect,
                            effect.Stats.FireDamage,
                            interval,
                            chance,
                            mods);
                } else if (
                    effectStats.NatureDamage > 0
                        || effectStats.HolyDamage > 0
                        || effectStats.FrostDamage > 0) {
                    SpellModifiers mods = new SpellModifiers();
                    mods.Accumulate(SpellModifiers);
                    mods.AddAdditiveDirectMultiplier(
                        CalcWarlockFirestoneDirectDamageMultiplier());
                    AddShadowModifiers(mods);
                    procdDamage
                        += CalcDamageProc(
                            effect,
                            effectStats.NatureDamage
                                + effectStats.HolyDamage
                                + effectStats.FrostDamage,
                            interval,
                            chance,
                            mods);
                } else {
                    Stats proc = effect.GetAverageStats(
                        interval,
                        chance,
                        CalculationsWarlock.AVG_UNHASTED_CAST_TIME,
                        Options.Duration);
                    procStats.Accumulate(proc);

                    if (effect.Trigger != Trigger.Use || IsDoublePot(effect)) {
                        MaxCritChance += GetSpellCrit(proc);
                    } else {
                        MaxCritChance += GetSpellCrit(effect.Stats);
                    }

                    // Handle "recursive effects" - i.e. those that *enable* a
                    // proc during a short window.
                    if (effect.Stats._rawSpecialEffectDataSize == 1
                        && periods.ContainsKey(
                            (int) effect.Stats._rawSpecialEffectData[0].Trigger)) {

                        SpecialEffect inner
                            = effect.Stats._rawSpecialEffectData[0];
                        Stats innerStats
                            = inner.GetAverageStats(
                                periods[(int) inner.Trigger],
                                chances[(int) inner.Trigger],
                                1f,
                                effect.Duration);
                        float upTime
                            = effect.GetAverageUptime(
                                periods[(int) effect.Trigger],
                                chances[(int) effect.Trigger],
                                1f,
                                Options.Duration);
                        procStats.Accumulate(innerStats, upTime);
                    }
                }
            }

            procStats.HasteRating
                = procStats.SpellHaste
                = procStats.Mana
                = procStats.ManaCostPerc
                = procStats.ManacostReduceWithin15OnHealingCast
                = procStats.ManaGainOnGreaterHealOverheal
                = procStats.ManaorEquivRestore
                = procStats.ManaRestore
                = procStats.ManaRestoreFromBaseManaPPM
                = procStats.ManaRestoreFromMaxManaPerSecond
                = procStats.ManaRestoreOnCast_5_15
                = procStats.ManaSpringMp5Increase
                = 0;
            Stats.Accumulate(procStats);

            return procdDamage;
        }

        private float CalcDamageProc(
            SpecialEffect effect,
            float damagePerProc,
            float interval,
            float chance,
            SpellModifiers modifiers) {

            damagePerProc *=
                (1  + (modifiers.GetFinalCritMultiplier() - 1)
                        * modifiers.CritChance)
                    * modifiers.GetFinalDirectMultiplier()
                    * (1
                        - StatConversion.GetAverageResistance(
                            80, Options.TargetLevel, 0f, 0f));
            float numProcs
                = Options.Duration
                    * effect.GetAverageProcsPerSecond(
                        interval,
                        chance,
                        CalculationsWarlock.AVG_UNHASTED_CAST_TIME,
                        Options.Duration);
            return numProcs * damagePerProc;
        }

        private bool IsDoublePot(SpecialEffect effect) {

            return effect.Cooldown == 1200f && effect.Duration == 14f;
        }

        private void PopulateTriggers(
            Dictionary<int, float> periods,
            Dictionary<int, float> chances) {

            // this is a temporary method until non-guestimate triggers are
            // implemented
            float nonProcHaste = GetSpellHaste(PreProcStats);
            float corruptionPeriod = 0f;
            if (Options.GetActiveRotation().Contains("Corruption")) {
                corruptionPeriod = 3.1f;
                if (Talents.GlyphQuickDecay) {
                    corruptionPeriod /= nonProcHaste;
                }
            }
            PopulateTriggers(
                periods,
                chances,
                CalculationsWarlock.AVG_UNHASTED_CAST_TIME / nonProcHaste
                    + Options.Latency,
                1 / 1.5f,
                corruptionPeriod);
        }

        private void PopulateTriggers(
            Dictionary<int, float> periods,
            Dictionary<int, float> chances,
            float castPeriod,
            float dotFrequency,
            float corruptionPeriod) {

            periods[(int) Trigger.Use] = 0f;
            periods[(int) Trigger.SpellHit]
                = periods[(int) Trigger.SpellCrit]
                = periods[(int) Trigger.SpellCast]
                = periods[(int) Trigger.SpellMiss]
                = periods[(int) Trigger.DamageSpellHit]
                = periods[(int) Trigger.DamageSpellCrit]
                = periods[(int) Trigger.DamageSpellCast]
                = castPeriod;
            periods[(int) Trigger.DoTTick] = 1 / dotFrequency;
            periods[(int) Trigger.DamageDone]
                = periods[(int) Trigger.DamageOrHealingDone]
                = 1f / (dotFrequency + 1f / periods[(int) Trigger.SpellHit]);
            periods[(int) Trigger.CorruptionTick] = corruptionPeriod;

            chances[(int) Trigger.Use] = 1f;
            chances[(int) Trigger.SpellHit]
                = chances[(int) Trigger.DamageSpellHit]
                = chances[(int) Trigger.DamageDone]
                = chances[(int) Trigger.DamageOrHealingDone]
                = HitChance;
            chances[(int) Trigger.SpellCrit]
                = chances[(int) Trigger.DamageSpellCrit]
                = CalcSpellCrit() * chances[(int) Trigger.SpellHit];
            chances[(int) Trigger.SpellCast]
                = chances[(int) Trigger.DamageSpellCast] = 1f;
            chances[(int) Trigger.SpellMiss]
                = 1 - chances[(int) Trigger.SpellHit];
            chances[(int) Trigger.DoTTick] = 1f;
            chances[(int) Trigger.CorruptionTick]
                = corruptionPeriod == 0f ? 0f : 1f;
        }

        // This technique assumes that if you pick a random time during filler
        // spell(s) or downtime, the "cooldowns" remaining on the rest of your
        // spells are all equally likely to be at any value. This is unrealistic
        // (e.g. it's impossible for them all to be at their full value), but
        // for some classes is a reasonable approximation.
        private void RecordCollisionDelays(CastingState state) {

            float pRemaining = 1f;
            foreach (Spell spell in Priorities) {
                float p = spell.GetQueueProbability(state);
                if (p == 0f) {
                    continue;
                }

                List<CastingState> nextStates =
                    spell.SimulateCast(state, p * pRemaining);
                foreach (CastingState nextState in nextStates) {
                    if (nextState.Probability > .0001f) {

                        // Only calculate if the probabilty of the state is
                        // large enough to make any difference at all.
                        RecordCollisionDelays(nextState);
                    }
                }
                if (p == 1f) {
                    return;
                }

                pRemaining *= 1f - p;
            }

            //System.Console.WriteLine(state.ToString());
        }

        public float GetMetamorphosisBonus() {

            if (Talents.Metamorphosis == 0) {
                return 0;
            }

            float cooldown = 180f * (1f - Talents.Nemesis * .1f);
            float duration = 30f;
            if (Talents.GlyphMetamorphosis) {
                duration += 6f;
            }
            return .2f * duration / cooldown;
        }

        public void AddShadowModifiers(SpellModifiers modifiers) {

            modifiers.AddMultiplicativeMultiplier(
                CalcBonusShadowDamageMultiplier());
            modifiers.AddAdditiveMultiplier(
                Talents.ShadowMastery * .03f);
            if (Options.GetActiveRotation().Contains("Shadow Bolt")
                || (Options.GetActiveRotation().Contains("Haunt")
                    && Talents.Haunt > 0)) {

                modifiers.AddMultiplicativeTickMultiplier(
                    Talents.ShadowEmbrace * .01f * 3f);
            }
            if (CastSpells.ContainsKey("Haunt")) {
                modifiers.AddMultiplicativeTickMultiplier(
                    ((Haunt) CastSpells["Haunt"]).GetAvgTickBonus());
            }
            if (Pet is Succubus) {
                float bonus = Talents.MasterDemonologist * .01f;
                modifiers.AddMultiplicativeMultiplier(bonus);
                modifiers.AddCritChance(bonus);
            }
        }

        public void AddFireModifiers(SpellModifiers modifiers) {

            modifiers.AddMultiplicativeMultiplier(
                CalcBonusFireDamageMultiplier());
            modifiers.AddAdditiveMultiplier(Talents.Emberstorm * .03f);
            if (Pet is Imp) {
                float bonus = Talents.MasterDemonologist * .01f;
                modifiers.AddMultiplicativeMultiplier(bonus);
                modifiers.AddCritChance(bonus);
            }
        }

        #endregion


        public Spell GetSpell(string spellName) {

            if (Spells.ContainsKey(spellName)) {
                return Spells[spellName];
            }

            string className = spellName.Replace(" ", "");
            className = className.Replace("(", "_");
            className = className.Replace(")", "");
            Type type = Type.GetType("Rawr.Warlock." + className);
            Spell spell
                = (Spell) Activator.CreateInstance(type, new object[] { this });
            Spells[spellName] = spell;
            return spell;
        }

        public bool IsPriorityOrdered(Spell s1, Spell s2) {

            int i1 = Priorities.IndexOf(s1);
            int i2 = Priorities.IndexOf(s2);
            return (i1 < i2 && i1 != -1) || (i1 != -1 && i2 == -1);
        }
    }
}
//3456789 223456789 323456789 423456789 523456789 623456789 723456789 8234567890