using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr.Tankadin
{
    [System.ComponentModel.DisplayName("Tankadin|Spell_Holy_AvengersShield")]
    public class CalculationsTankadin : CalculationsBase
    {
        //my insides all turned to ash / so slow
        //and blew away as i collapsed / so cold
        private CalculationOptionsPanelBase _calculationOptionsPanel = null;
        public override CalculationOptionsPanelBase CalculationOptionsPanel
        {
            get
            {
                if (_calculationOptionsPanel == null)
                {
                    _calculationOptionsPanel = new CalculationOptionsPanelTankadin();
                }
                return _calculationOptionsPanel;
            }
        }

        private string[] _characterDisplayCalculationLabels = null;
        public override string[] CharacterDisplayCalculationLabels
        {
            get
            {
                if (_characterDisplayCalculationLabels == null)
                    _characterDisplayCalculationLabels = new string[] {
					"Basic Stats:Health",
					"Basic Stats:Armor",
					"Basic Stats:Stamina",
					"Basic Stats:Agility",
					"Basic Stats:Defense",
					"Basic Stats:Miss",
					"Basic Stats:Dodge",
					"Basic Stats:Parry",
					"Basic Stats:Block",
					"Basic Stats:Block Value",
					"Basic Stats:Spell Damage",
					"Complex Stats:Avoidance",
					"Complex Stats:Mitigation",
					"Complex Stats:Total Mitigation",
					"Complex Stats:Chance to be Crit",
                    "Complex Stats:Chance to be Crush",
                    @"Complex Stats:Overall Points*Overall Points are a sum of Mitigation and Survival Points.
Overall is typically, but not always, the best way to rate gear.
For specific encounters, closer attention to Mitigation and
Survival Points individually may be important.",
					@"Complex Stats:Mitigation Points*Effective health with correct avoidances",
					@"Complex Stats:Survival Points*Effective health without avoidances, crits, crushes",
                     "Threat:Overall",
                     "Threat:Holy Shield",
                     "Threat:Seal of Right",
                     "Threat:Judgement of Right",
                     "Threat:Consecrate",
                     "Threat:Misc"
				};
                return _characterDisplayCalculationLabels;
            }
        }

        private string[] _customChartNames = null;
        public override string[] CustomChartNames
        {
            get
            {
                if (_customChartNames == null)
                    _customChartNames = new string[] {
					//"Combat Table",
					//"Relative Stat Values",
					//"Agi Test"
					};
                return _customChartNames;
            }
        }

        private Dictionary<string, System.Drawing.Color> _subPointNameColors = null;
        public override Dictionary<string, System.Drawing.Color> SubPointNameColors
        {
            get
            {
                if (_subPointNameColors == null)
                {
                    _subPointNameColors = new Dictionary<string, System.Drawing.Color>();
                    _subPointNameColors.Add("Mitigation", System.Drawing.Color.Red);
                    _subPointNameColors.Add("Survival", System.Drawing.Color.Blue);
                    _subPointNameColors.Add("Threat", System.Drawing.Color.DarkOliveGreen);
                }
                return _subPointNameColors;
            }
        }

        private List<Item.ItemType> _relevantItemTypes = null;
        public override List<Item.ItemType> RelevantItemTypes
        {
            get
            {
                if (_relevantItemTypes == null)
                {
                    _relevantItemTypes = new List<Item.ItemType>(new Item.ItemType[]
					{
                        Item.ItemType.Plate,
                        Item.ItemType.None,
						Item.ItemType.Shield,
						Item.ItemType.Libram,
						Item.ItemType.OneHandAxe,
						Item.ItemType.OneHandMace,
						Item.ItemType.OneHandSword,
						Item.ItemType.TwoHandAxe,
						Item.ItemType.TwoHandMace,
						Item.ItemType.TwoHandSword
					});
                }
                return _relevantItemTypes;
            }
        }

        public override Character.CharacterClass TargetClass { get { return Character.CharacterClass.Paladin; } }
        public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationTankadin(); }
        public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsTankadin(); }

        public override ICalculationOptionBase DeserializeDataObject(string xml)
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(CalculationOptionsTankadin));
            System.IO.StringReader reader = new System.IO.StringReader(xml);
            CalculationOptionsTankadin calcOpts = serializer.Deserialize(reader) as CalculationOptionsTankadin;
            return calcOpts;
        }

        public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem)
        {
            CalculationOptionsTankadin calcOpts = character.CalculationOptions as CalculationOptionsTankadin;
            //_cachedCharacter = character;
            int targetLevel = calcOpts.TargetLevel;
            Stats stats = GetCharacterStats(character, additionalItem);
            Talents talents = new Talents();
            float targetDefense = targetLevel * 5;
            CharacterCalculationsTankadin calculatedStats = new CharacterCalculationsTankadin();
            calculatedStats.BasicStats = stats;
            calculatedStats.TargetLevel = targetLevel;


            //Avoidance calculations
            calculatedStats.Defense = 350 + (float)Math.Floor(stats.DefenseRating / (123f / 52f)) + talents.Anticipation * 4;
            calculatedStats.Miss = 5 + (calculatedStats.Defense - targetDefense) * .04f + stats.Miss;
            calculatedStats.Parry = 5 + (calculatedStats.Defense - targetDefense) * .04f + stats.ParryRating / 23.6538461538462f + talents.Deflection;
            calculatedStats.Dodge = (calculatedStats.Defense - targetDefense) * .04f + stats.Agility / 25f + (stats.DodgeRating / (984f / 52f));
            calculatedStats.Avoidance = calculatedStats.Dodge + calculatedStats.Miss + calculatedStats.Parry;

            calculatedStats.Block = 5 + (calculatedStats.Defense - targetDefense) * .04f + stats.BlockRating / 7.884614944458f;
            calculatedStats.BlockValue = (float)Math.Round(stats.BlockValue * (1 + 0.1f * talents.ShieldSpecialization)) + (float)Math.Floor(stats.Strength / 20f);
            calculatedStats.CrushAvoidance = calculatedStats.Avoidance + calculatedStats.Block + 30 + (character?.Ranged?.Id == 29388 ? 42f / 7.884614944458f : 0);
            calculatedStats.CritAvoidance = (calculatedStats.Defense - targetDefense) * .04f + stats.Resilience / 39.423f;
            calculatedStats.Mitigation = Math.Min(75f, (stats.Armor / (stats.Armor - 22167.5f + (467.5f * targetLevel))) * 100f);

            float reduction = (1f - (calculatedStats.Mitigation * .01f)) * (1 - 0.02f * talents.ImprovedRighteousFury);
            float attacks = calcOpts.NumberAttackers / calcOpts.AttackSpeed * 10;
            //Apply armor and multipliers for each attack type...
            float miss = Math.Min(0.01f * attacks * calculatedStats.Avoidance, attacks);

            float block = Math.Min(Math.Min(8, attacks * (.3f + (character?.Ranged?.Id == 29388 ? 0.01f * 42f / 7.884614944458f : 0) + 0.01f*calculatedStats.Block)), attacks - miss);
            if (block > 8) block += Math.Min((attacks - block) * .01f * calculatedStats.Block, attacks - miss - block);
            float crit = Math.Min(0.01f * Math.Max(5 - calculatedStats.CritAvoidance, 0) * attacks, attacks - miss - block);
            float crush = Math.Min((targetLevel == 73 ? .15f : 0f) * attacks, attacks - miss - block - crit);
            float hit = attacks - miss - block - crit - crush;

            float threatModifier = 1 + 0.01f * talents.OneHandSpec;
            switch (talents.ImprovedRighteousFury)
            {
                case 1:
                    threatModifier *= 1 + 0.06f * 1.16f;
                    break;
                case 2:
                    threatModifier *= 1 + 0.06f * 1.33f;
                    break;
                case 3:
                    threatModifier *= 1 + 0.06f * 1.5f;
                    break;
                default:
                    threatModifier *= 1 + 0.06f;
                    break;
            }
            var spellDamage = stats.SpellDamageRating + stats.SpellHolyDamageRating;
            calculatedStats.HolyShieldTPS = threatModifier * Math.Min(block, 8f) * 1.2f * 1.35f * (155 + .05f * spellDamage) / 10f;

            crit *= calcOpts.AverageHit * reduction * 2f;
            crush *= calcOpts.AverageHit * reduction * 1.5f;
            hit *= calcOpts.AverageHit * reduction;
            block *= calcOpts.AverageHit * reduction * Math.Max(1f - (calculatedStats.BlockValue / calcOpts.AverageHit / reduction), 0);
            calculatedStats.DamageTaken = (hit + crush + crit + block) / (attacks * calcOpts.AverageHit) * 100;
            calculatedStats.TotalMitigation = 100f - calculatedStats.DamageTaken;

            calculatedStats.SurvivalPoints = stats.Health / reduction * calcOpts.SurvivalScale;
            calculatedStats.MitigationPoints = stats.Health / calculatedStats.DamageTaken * 100;
            float ws = character.MainHand == null ? 0 : character.MainHand.Speed;
            float wd = character.MainHand == null ? 0 : ((character.MainHand.MinDamage + character.MainHand.MaxDamage) / 2f);
            calculatedStats.SoRTPS = ws == 0 ? 0 : ((0.85f * (2610.43f * ws / 100f) + 0.03f * wd + 6f + (0.102f * ws * spellDamage)) / ws * threatModifier);
            calculatedStats.ConsecrateTPS = calcOpts.NumberAttackers * (512 + .9524f * spellDamage) / 8f * threatModifier;
            calculatedStats.JoRTPS = (235.5f + spellDamage * .7143f) / (10 - 2 * talents.ImprovedJudgement) * threatModifier;
            calculatedStats.OverallTPS = calculatedStats.SoRTPS + calculatedStats.JoRTPS +
                calculatedStats.HolyShieldTPS + calculatedStats.ConsecrateTPS + calculatedStats.MiscTPS;
            calculatedStats.ThreatPoints = calculatedStats.OverallTPS * calcOpts.ThreatScale;

            calculatedStats.OverallPoints = calculatedStats.MitigationPoints + calculatedStats.SurvivalPoints + calculatedStats.ThreatPoints;

            return calculatedStats;
        }

        public override Stats GetCharacterStats(Character character, Item additionalItem)
        {
            Stats statsRace = new Stats() { Health = 3197, Mana = 2673, Stamina = 120, Intellect = 83, Spirit = 89, Agility = 77, DodgeRating = 12.3f, BlockValue = 4 };
            Stats statsBaseGear = GetItemStats(character, additionalItem);
            Stats statsEnchants = GetEnchantsStats(character);
            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);
            Talents talents = new Talents();

            Stats statsTotal = statsBaseGear + statsEnchants + statsBuffs + statsRace;
            statsTotal.Agility = (float)Math.Floor(statsTotal.Agility * (1 + statsBuffs.BonusAgilityMultiplier));
            statsTotal.Stamina = (float)Math.Floor(statsTotal.Stamina * (1 + statsBuffs.BonusStaminaMultiplier) * (1 + 0.03f * talents.SacredDuty) * (1 + 0.02f * talents.CombatExpertise));
            statsTotal.Health = (float)Math.Round(statsTotal.Health + (statsTotal.Stamina * 10));
            statsTotal.Armor = (float)Math.Round(statsTotal.Armor * (1 + statsBuffs.BonusArmorMultiplier) * (1 + talents.Thoughness * 0.02f) + statsTotal.Agility * 2f);
            return statsTotal;
        }

        public override ComparisonCalculationBase[] GetCustomChartData(Character character, string chartName)
        {
            return new ComparisonCalculationBase[0];
        }

        public override Stats GetRelevantStats(Stats stats)
        {
            return new Stats()
            {
                Armor = stats.Armor,
                Stamina = stats.Stamina,
                Agility = stats.Agility,
                DodgeRating = stats.DodgeRating,
                DefenseRating = stats.DefenseRating,
                Resilience = stats.Resilience,
                ParryRating = stats.ParryRating,
                BlockRating = stats.BlockRating,
                BlockValue = stats.BlockValue,
                BonusAgilityMultiplier = stats.BonusAgilityMultiplier,
                BonusArmorMultiplier = stats.BonusArmorMultiplier,
                BonusStaminaMultiplier = stats.BonusStaminaMultiplier,
                Health = stats.Health,
                Miss = stats.Miss,
                SpellDamageRating = stats.SpellDamageRating,
                SpellHolyDamageRating = stats.SpellHolyDamageRating,
                HitRating = stats.HitRating,
                SpellHitRating = stats.SpellHitRating,
                ArmorPenetration = stats.ArmorPenetration,
            };
        }

        public override bool HasRelevantStats(Stats stats)
        {
            return (stats.Agility + stats.Armor + stats.BonusAgilityMultiplier + stats.BonusArmorMultiplier +
                stats.BonusStaminaMultiplier + stats.DefenseRating + stats.DodgeRating + stats.Health +
                stats.Miss + stats.Resilience + stats.Stamina + stats.ParryRating + stats.BlockRating + stats.BlockValue +
                stats.SpellHitRating + stats.SpellDamageRating + stats.SpellHolyDamageRating + stats.HitRating + stats.ArmorPenetration) > 0;
        }
    }

}