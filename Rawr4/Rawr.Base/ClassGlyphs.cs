﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr
{
    public enum GlyphType { Minor = 0, Major, Prime, }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GlyphDataAttribute : Attribute
    {
        public GlyphDataAttribute(int index, string name, GlyphType type, string description)
        {
            _index = index;
            _name = name;
            _type = type;
            _description = description;
        }

        private readonly int _index;
        private readonly string _name;
        private readonly GlyphType _type;
        private readonly string _description;

        public int Index { get { return _index; } }
        public string Name { get { return _name; } }
        public GlyphType Type { get { return _type; } }
        public string Description { get { return _description; } }
    }

    public partial class MageTalents
    {
        private bool[] _glyphData = new bool[19];
        public override bool[] GlyphData { get { return _glyphData; } }

        [GlyphData(0, "Glyph of Fireball", GlyphType.Major, @"Reduces the casting time of your Fireball spell by 0.15 sec, but removes the damage over time effect.")]
        public bool GlyphOfFireball { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Frostfire", GlyphType.Major, @"Increases the initial damage dealt by Frostfire Bolt by 2% and its critical strike chance by 2%.")]
        public bool GlyphOfFrostfire { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Frostbolt", GlyphType.Major, @"Increases the damage dealt by Frostbolt by 5%, but removes the slowing effect.")]
        public bool GlyphOfFrostbolt { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Ice Armor", GlyphType.Major, @"Your Ice Armor and Frost Armor spells grant an additional 50% armor and resistance.")]
        public bool GlyphOfIceArmor { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Improved Scorch", GlyphType.Major, @"Increases the damage of your Scorch spell by 20%.")]
        public bool GlyphOfImprovedScorch { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Mage Armor", GlyphType.Major, @"Your Mage Armor spell grants an additional 20% mana regeneration while casting.")]
        public bool GlyphOfMageArmor { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Mana Gem", GlyphType.Major, @"Increases the mana recieved from using a mana gem by 40%.")]
        public bool GlyphOfManaGem { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Molten Armor", GlyphType.Major, @"Your Molten Armor spell grants an additional 20% of your spirit as critical strike rating.")]
        public bool GlyphOfMoltenArmor { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Water Elemental", GlyphType.Major, @"Reduces the cooldown of your Summon Water Elemental spell by 30 sec.")]
        public bool GlyphOfWaterElemental { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Arcane Explosion", GlyphType.Major, @"Reduces mana cost of Arcane Explosion by 10%.")]
        public bool GlyphOfArcaneExplosion { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Arcane Power", GlyphType.Major, @"Increases the duration of Arcane Power by 3 sec.")]
        public bool GlyphOfArcanePower { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Arcane Blast", GlyphType.Major, @"Increases the damage from your Arcane Blast buff by 3%.")]
        public bool GlyphOfArcaneBlast { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Arcane Missiles", GlyphType.Major, @"Increases the critical strike damage bonus of Arcane Missiles by 25%.")]
        public bool GlyphOfArcaneMissiles { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Arcane Barrage", GlyphType.Major, @"Reduces the mana cost of Arcane Barrage by 20%.")]
        public bool GlyphOfArcaneBarrage { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Living Bomb", GlyphType.Major, @"The periodic damage from your Living Bomb can now be critical strikes.")]
        public bool GlyphOfLivingBomb { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Ice Lance", GlyphType.Major, @"Your Ice Lance now causes 4 times damage against frozen targets higher level than you instead of triple damage.")]
        public bool GlyphOfIceLance { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Mirror Image", GlyphType.Major, @"Your Mirror Image spell now creates a 4th copy.")]
        public bool GlyphOfMirrorImage { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Deep Freeze", GlyphType.Major, @"Increases the range of Deep Freeze by 10 yards.")]
        public bool GlyphOfDeepFreeze { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Eternal Water", GlyphType.Major, @"Your Summon Water Elemental now lasts indefinitely, but your Water Elemental can no longer cast Freeze.")]
        public bool GlyphOfEternalWater { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
    }

    public partial class DruidTalents
    {
        private bool[] _glyphData = new bool[25];
        public override bool[] GlyphData { get { return _glyphData; } }

        //Cat Glyphs
        [GlyphData(0, "Glyph of Mangle", GlyphType.Major, @"Increases the damage done by Mangle by 10%.")]
        public bool GlyphOfMangle { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Shred", GlyphType.Major, @"Each time you Shred, the duration of your Rip on the target is extended 2 sec, up to a maximum of 6 sec.")]
        public bool GlyphOfShred { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Rip", GlyphType.Major, @"Increases the duration of your Rip ability by 4 sec.")]
        public bool GlyphOfRip { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Berserk", GlyphType.Major, @"Increases the duration of Berserk by 5 sec.")]
        public bool GlyphOfBerserk { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Savage Roar", GlyphType.Major, @"Your Savage Roar ability grants an additional 3% bonnus damage done.")]
        public bool GlyphOfSavageRoar { get { return _glyphData[4]; } set { _glyphData[4] = value; } }

        //Bear Glyphs
        [GlyphData(5, "Glyph of Growl", GlyphType.Major, @"Increases the chance for your Growl ability to work successfully by 8%.")]
        public bool GlyphOfGrowl { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Maul", GlyphType.Major, @"Your Maul ability now hits 1 additional target.")]
        public bool GlyphOfMaul { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Frenzied Regeneration", GlyphType.Major, @"While Frenzied Regeneration is active, healing effects on you are 20% more powerful.")]
        public bool GlyphOfFrenziedRegeneration { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        
        //Moonkin Glyphs
        [GlyphData(8, "Glyph of Focus", GlyphType.Major, @"Increases the damage done by Starfall by 20%, but decreases its radius by 50%.")]
        public bool GlyphOfFocus { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Insect Swarm", GlyphType.Major, @"Increases the damage of your Insect Swarm ability by 20%, but it no longer affects your victim's chance to hit.")]
        public bool GlyphOfInsectSwarm { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Monsoon", GlyphType.Major, @"Reduces the cooldown of your Typhoon spell by 3 sec.")]
        public bool GlyphOfMonsoon { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Moonfire", GlyphType.Major, @"Increases the periodic damage of your Moonfire ability by 75%, but initial damage is decreased by 90%.")]
        public bool GlyphOfMoonfire { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Starfall", GlyphType.Major, @"Reduces the cooldown of Starfall by 30 sec.")]
        public bool GlyphOfStarfall { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Starfire", GlyphType.Major, @"Your Starfire ability increases the duration of your Moonfire effect on the target by 3 sec, up to a maximum of 9 additional seconds.")]
        public bool GlyphOfStarfire { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Wrath", GlyphType.Major, @"Reduces the pushback suffered from damaging attacks while casting your Wrath spell by 50%.")]
        public bool GlyphOfWrath { get { return _glyphData[14]; } set { _glyphData[14] = value; } }

        //Tree Glyphs
        [GlyphData(15, "Glyph of Healing Touch", GlyphType.Major, @"Decreases the cast time of Healing Touch by 1.5 sec., the mana cost by 25%, and the amount healed by 50%.")]
        public bool GlyphOfHealingTouch { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Innervate", GlyphType.Major, @"Innervate now grants the caster 90% of his base mana pool over 20 sec in addition to the normal effects of Innervate.")]
        public bool GlyphOfInnervate { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Lifebloom", GlyphType.Major, @"Increases the duration of Lifebloom by 1 sec.")]
        public bool GlyphOfLifebloom { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Nourish", GlyphType.Major, @"Your Nourish heals for an additional 6% for each of your heal over time effects present on the target.")]
        public bool GlyphOfNourish { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        [GlyphData(19, "Glyph of Rebirth", GlyphType.Major, @"Players resurrected by Rebirth are returned to life with 100% health.")]
        public bool GlyphOfRebirth { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        [GlyphData(20, "Glyph of Regrowth", GlyphType.Major, @"Increases the healing of your Regrowth spell by 20% if your Regrowth effect is still active on the target.")]
        public bool GlyphOfRegrowth { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        [GlyphData(21, "Glyph of Rejuvenation", GlyphType.Major, @"While your Rejuvenation targets are below 50% health, you will heal them for an additional 50% health.")]
        public bool GlyphOfRejuvenation { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        [GlyphData(22, "Glyph of Swiftmend", GlyphType.Major, @"Your Swiftmend ability no longer consumes a Rejuvenation or Regrowth effect from the target.")]
        public bool GlyphOfSwiftmend { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        [GlyphData(23, "Glyph of Wild Growth", GlyphType.Major, @"Wild Growth can affect 1 additional target.")]
        public bool GlyphOfWildGrowth { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        [GlyphData(24, "Glyph of Rapid Rejuvenation", GlyphType.Major, @"Your haste now reduces the time between the periodic healing ticks of your Rejuvenation spell.")]
        public bool GlyphOfRapidRejuvenation { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
    }

    public partial class PaladinTalents
    {
        private bool[] _glyphData = new bool[34];
        public override bool[] GlyphData { get { return _glyphData; } }
        
        [GlyphData(0, "Glyph of Avenging Wrath", GlyphType.Major, @"Reduces the cooldown of your Hammer of Wrath spell by 50% while Avenging Wrath is active.")]
        public bool GlyphOfAvengingWrath { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Avenger's Shield", GlyphType.Major, @"Your Avenger's Shield hits 2 fewer targets, but for 100% more damage.")]
        public bool GlyphOfAvengersShield { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Seal of Blood", GlyphType.Major, @"When your Seal of Blood, Seal of the Martyr, Judgement of Blood, or Judgement of the Martyr deals damage to you, you gain 11% of the damage done as mana.")]
        public bool GlyphOfSealOfBlood { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Seal of Righteousness", GlyphType.Major, @"Increases the damage done by Seal of Righteousness by 10%.")]
        public bool GlyphOfSealOfRighteousness { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Seal of Vengeance", GlyphType.Major, @"Your Seal of Vengeance or Seal of Corruption also grants 10 expertise while active.")]
        public bool GlyphOfSealOfVengeance { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Hammer of Wrath", GlyphType.Major, @"Reduces the mana cost of Hammer of Wrath by 100%.")]
        public bool GlyphOfHammerOfWrath { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Beacon of Light", GlyphType.Major, @"Increases the duration of Beacon of Light by 30 sec.")]
        public bool GlyphOfBeaconOfLight { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Divine Plea", GlyphType.Major, @"While Divine Plea is active, you take 3% reduced damage from all sources.")]
        public bool GlyphOfDivinePlea { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Divine Storm", GlyphType.Major, @"Your Divine Storm now heals for an additional 15% of the damage it causes.")]
        public bool GlyphOfDivineStorm { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Hammer of the Righteous", GlyphType.Major, @"Your Hammer of the Righteous hits 1 additional target.")]
        public bool GlyphOfHammerOfTheRighteous { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Holy Shock", GlyphType.Major, @"Reduces the cooldown of Holy Shock by 1 sec.")]
        public bool GlyphOfHolyShock { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Salvation", GlyphType.Major, @"When you cast Hand of Salvation on yourself, it also reduces damage taken by 20%.")]
        public bool GlyphOfSalvation  { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Shield of Righteousness", GlyphType.Major, @"Reduces the mana cost of Shield of Righteousness by 80%.")]
        public bool GlyphOfShieldOfRighteousness { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Seal of Wisdom", GlyphType.Major, @"While Seal of Wisdom is active, the cost of your healing spells is reduced by 5%.")]
        public bool GlyphOfSealOfWisdom { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Cleansing", GlyphType.Major, @"Reduces the mana cost of your Cleanse and Purify spells by 20%.")]
        public bool GlyphOfCleansing { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Seal of Light", GlyphType.Major, @"While Seal of Light is active, the effect of your healing spells is increased by 5%.")]
        public bool GlyphOfSealOfLight { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of the Wise", GlyphType.Minor, @"Reduces the mana cost of your Seal of Wisdom spell by 50%.")]
        public bool GlyphOftheWise { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Turn Evil", GlyphType.Major, @"Reduces the casting time of your Turn Evil spell by 100%, but increases the cooldown by 8 sec.")]
        public bool GlyphOfTurnEvil { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Consecration", GlyphType.Major, @"Increases the duration and cooldown of Consecration by 2 sec.")]
        public bool GlyphOfConsecration { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        [GlyphData(19, "Glyph of Crusader Strike", GlyphType.Major, @"Reduces the mana cost of your Crusader Strike ability by 20%.")]
        public bool GlyphOfCrusaderStrike { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        [GlyphData(20, "Glyph of Exorcism", GlyphType.Major, @"Increases damage done by Exorcism by 20%.")]
        public bool GlyphOfExorcism { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        [GlyphData(21, "Glyph of Flash of Light", GlyphType.Major, @"Your Flash of Light has an additional 5% critical strike chance.")]
        public bool GlyphOfFlashOfLight { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        [GlyphData(22, "Glyph of Seal of Command", GlyphType.Major, @"Increases the chance of dealing Seal of Command damage by 20%.")]
        public bool GlyphOfSealOfCommand { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        [GlyphData(23, "Glyph of Blessing of Kings", GlyphType.Minor, @"Reduces the mana cost of your Blessing of Kings and Greater Blessing of Kings spells by 50%.")]
        public bool GlyphOfBlessingOfKings { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        [GlyphData(24, "Glyph of Sense Undead", GlyphType.Minor, @"Damage against Undead increased by 1% while your Sense Undead ability is active.")]
        public bool GlyphOfSenseUndead { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        [GlyphData(25, "Glyph of Righteous Defense", GlyphType.Major, @"Increases the chance for your Righteous Defense ability to work successfully by 8% on each target.")]
        public bool GlyphOfRighteousDefense { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        [GlyphData(26, "Glyph of Spiritual Attunement", GlyphType.Major, @"Increases the amount of mana gained from your Spiritual Attunement spell by an additional 2%.")]
        public bool GlyphOfSpiritualAttunement { get { return _glyphData[26]; } set { _glyphData[26] = value; } }
        [GlyphData(27, "Glyph of Divinity", GlyphType.Major, @"Your Lay on Hands grants twice as much mana as normal and also grants you as much mana as it grants your target.")]
        public bool GlyphOfDivinity { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        [GlyphData(28, "Glyph of Blessing of Wisdom", GlyphType.Minor, @"Increases the duration of your Blessing of Wisdom spell by 20 min when cast on yourself.")]
        public bool GlyphOfBlessingOfWisdom { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
        [GlyphData(29, "Glyph of Hammer of Justice", GlyphType.Major, @"Increases your Hammer of Justice range by 5 yards.")]
        public bool GlyphOfHammerOfJustice { get { return _glyphData[29]; } set { _glyphData[29] = value; } }
        [GlyphData(30, "Glyph of Lay on Hands", GlyphType.Minor, @"Reduces the cooldown of your Lay on Hands spell by 5 min.")]
        public bool GlyphOfLayonHands { get { return _glyphData[30]; } set { _glyphData[30] = value; } }
        [GlyphData(31, "Glyph of Judgement", GlyphType.Major, @"Your Judgements deal 10% more damage.")]
        public bool GlyphOfJudgement { get { return _glyphData[31]; } set { _glyphData[31] = value; } }
        [GlyphData(32, "Glyph of Holy Light", GlyphType.Major, @"Your Holy Light grants 10% of its heal amount to up to 5 friendly targets within 8 yards of the initial target.")]
        public bool GlyphOfHolyLight { get { return _glyphData[32]; } set { _glyphData[32] = value; } }
        [GlyphData(33, "Glyph of Blessing of Might", GlyphType.Minor, @"Increases the duration of your Blessing of Might spell by 20 min when cast on yourself.")]
        public bool GlyphOfBlessingOfMight { get { return _glyphData[33]; } set { _glyphData[33] = value; } }
    }

    public partial class WarriorTalents
    {
        private bool[] _glyphData = new bool[30];
        public override bool[] GlyphData { get { return _glyphData; } }

        #region Prime
        /// <summary>Increases the number of targets your Cleave hits by 1.</summary>
        [GlyphData(0, "Glyph of Cleaving", GlyphType.Prime, @"Increases the number of targets your Cleave hits by 1.")]
        public bool GlyphOfCleaving { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        /// <summary>Gives your Hamstring ability a 10% chance to immobilize the target for 5 sec.</summary>
        [GlyphData(1, "Glyph of Hamstring", GlyphType.Prime, @"Gives your Hamstring ability a 10% chance to immobilize the target for 5 sec.")]
        public bool GlyphOfHamstring { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        /// <summary>Your Heroic Throw applies a stack of Sunder Armor.</summary>
        [GlyphData(2, "Glyph of Heroic Throw", GlyphType.Prime, @"Your Heroic Throw applies a stack of Sunder Armor.")]
        public bool GlyphOfHeroicThrow { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        /// <summary>Increases the range of your Charge ability by 5 yards.</summary>
        [GlyphData(3, "Glyph of Long Charge", GlyphType.Prime, @"Increases the range of your Charge ability by 5 yards.")]
        public bool GlyphOfLongCharge { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        /// <summary>Reduces the cooldown of your Charge ability by 7%.</summary>
        [GlyphData(4, "Glyph of Rapid Charge", GlyphType.Prime, @"Reduces the cooldown of your Charge ability by 7%.")]
        public bool GlyphOfRapidCharge { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        /// <summary>Reduces the rage cost of your Thunder Clap ability by 5.</summary>
        [GlyphData(5, "Glyph of Resonating Power", GlyphType.Prime, @"Reduces the rage cost of your Thunder Clap ability by 5.")]
        public bool GlyphOfResonatingPower { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        /// <summary>Shield wall now reduces damage taken by 20%, but increases its cooldown by 2 min.</summary>
        [GlyphData(6, "Glyph of Shield Wall", GlyphType.Prime, @"Shield wall now reduces damage taken by 20%, but increases its cooldown by 2 min.")]
        public bool GlyphOfShieldWall { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        /// <summary>Reduces the cooldown on Shockwave by 3 sec.</summary>
        [GlyphData(7, "Glyph of Shockwave", GlyphType.Prime, @"Reduces the cooldown on Shockwave by 3 sec.")]
        public bool GlyphOfShockwave { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        /// <summary>Reduces the cooldown on Spell Reflection by 1 sec.</summary>
        [GlyphData(8, "Glyph of Spell Reflection", GlyphType.Prime, @"Reduces the cooldown on Spell Reflection by 1 sec.")]
        public bool GlyphOfSpellReflection { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        /// <summary>Your Sunder Armor ability effects a second nearby target.</summary>
        [GlyphData(9, "Glyph of Sunder Armor", GlyphType.Prime, @"Your Sunder Armor ability effects a second nearby target.")]
        public bool GlyphOfSunderArmor { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        /// <summary>Reduces the rage cost of Sweeping Strikes ability by 100%.</summary>
        [GlyphData(10, "Glyph of Sweeping Strikes", GlyphType.Prime, @"Reduces the rage cost of Sweeping Strikes ability by 100%.")]
        public bool GlyphOfSweepingStrikes { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        /// <summary>Increases the radius of your Thunder Clap ability by 2 yards.</summary>
        [GlyphData(11, "Glyph of Thunder Clap", GlyphType.Prime, @"Increases the radius of your Thunder Clap ability by 2 yards.")]
        public bool GlyphOfThunderClap { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        /// <summary>Increases the total healing provided by your Victory Rush by 50%.</summary>
        [GlyphData(12, "Glyph of Victory Rush", GlyphType.Prime, @"Increases the total healing provided by your Victory Rush by 50%.")]
        public bool GlyphOfVictoryRush { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        #endregion
        #region Major
        /// <summary>Reduces the cooldown on Bladestorm by 15 sec.</summary>
        [GlyphData(13, "Glyph of Bladestorm", GlyphType.Major, @"Reduces the cooldown on Bladestorm by 15 sec.")]
        public bool GlyphOfBladestorm { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        /// <summary>Increases the healing your recieve from Bloodthirst ability by 100%.</summary>
        [GlyphData(14, "Glyph of Bloodthirst", GlyphType.Major, @"Increases the healing your recieve from Bloodthirst ability by 100%.")]
        public bool GlyphOfBloodthirst { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        /// <summary>Your Devastate ability now applies two stacks of Sunder Armor.</summary>
        [GlyphData(15, "Glyph of Devastate", GlyphType.Major, @"Your Devastate ability now applies two stacks of Sunder Armor.")]
        public bool GlyphOfDevastate { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        /// <summary>Increases the damage of your Mortal Strike ability by 10%.</summary>
        [GlyphData(16, "Glyph of Mortal Strike", GlyphType.Major, @"Increases the damage of your Mortal Strike ability by 10%.")]
        public bool GlyphOfMortalStrike { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        /// <summary>Adds a 100% chance to enable your Overpower when your attacks are parried.</summary>
        [GlyphData(17, "Glyph of Overpower", GlyphType.Major, @"Adds a 100% chance to enable your Overpower when your attacks are parried.")]
        public bool GlyphOfOverpower { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        /// <summary>Increases the critical strike chance of Raging Blow by 5%.</summary>
        [GlyphData(18, "Glyph of Raging Blow", GlyphType.Major, @"Increases the critical strike chance of Raging Blow by 5%.")]
        public bool GlyphOfRagingBlow { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        /// <summary>After using Revenge, your next Heroic Strike costs no rage.</summary>
        [GlyphData(19, "Glyph of Revenge", GlyphType.Major, @"After using Revenge, your next Heroic Strike costs no rage.")]
        public bool GlyphOfRevenge { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        /// <summary>Increases the damage of your Shield Slam by 10%.</summary>
        [GlyphData(20, "Glyph of Shield Slam", GlyphType.Major, @"Increases the damage of your Shield Slam by 10%.")]
        public bool GlyphOfShieldSlam { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        /// <summary>Increases the critical strike chance of Slam by 5%.</summary>
        [GlyphData(21, "Glyph of Slam", GlyphType.Major, @"Increases the critical strike chance of Slam by 5%.")]
        public bool GlyphOfSlam { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        #endregion
        #region Minor
        /// <summary>Increases the duration of your Battle Shout by 2 min.</summary>
        [GlyphData(22, "Glyph of Battle", GlyphType.Minor, @"Increases the duration of your Battle Shout by 2 min.")]
        public bool GlyphOfBattle { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        /// <summary>Berserker Rage generates 5 rage when used.</summary>
        [GlyphData(23, "Glyph of Berserker Rage", GlyphType.Minor, @"Berserker Rage generates 5 rage when used.")]
        public bool GlyphOfBerserkerRage { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        /// <summary>Increases the healing your recieve from your Bloodthirst ability by 100%.</summary>
        [GlyphData(24, "Glyph of Bloody Healing", GlyphType.Minor, @"Increases the healing your recieve from your Bloodthirst ability by 100%.")]
        public bool GlyphOfBloodyHealing { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        /// <summary>Increases the duration by 2 min and the area of effect by 50% of your Commanding Shout.</summary>
        [GlyphData(25, "Glyph of Command", GlyphType.Minor, @"Increases the duration by 2 min and the area of effect by 50% of your Commanding Shout.")]
        public bool GlyphOfCommand { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        /// <summary>Increases the duration by 15 sec and area of effect 50% of your Demoralizing Shout.</summary>
        [GlyphData(26, "Glyph of Demoralizing Shout", GlyphType.Minor, @"Increases the duration by 15 sec and area of effect 50% of your Demoralizing Shout.")]
        public bool GlyphOfDemoralizingShout { get { return _glyphData[26]; } set { _glyphData[26] = value; } }
        /// <summary>Increases the window of opportunity in which you can use Victory Rush by 5 sec.</summary>
        [GlyphData(27, "Glyph of Enduring Victory", GlyphType.Minor, @"Increases the window of opportunity in which you can use Victory Rush by 5 sec.")]
        public bool GlyphOfEnduringVictory { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        /// <summary>Reduces the cost of Sunder Armor by 50%.</summary>
        [GlyphData(28, "Glyph of Furious Sundering", GlyphType.Minor, @"Reduces the cost of Sunder Armor by 50%.")]
        public bool GlyphOfFuriousSundering { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
        /// <summary>Targets of your Intimidating Shout no longer move faster when feared.</summary>
        [GlyphData(29, "Glyph of Intimidating Shout", GlyphType.Minor, @"Targets of your Intimidating Shout no longer move faster when feared.")]
        public bool GlyphOfIntimidatingShout { get { return _glyphData[29]; } set { _glyphData[29] = value; } }
        #endregion
    }

    public partial class ShamanTalents
    {
        private bool[] _glyphData = new bool[29];
        public override bool[] GlyphData { get { return _glyphData; } }

        [GlyphData(0, "Glyph of Healing Wave", GlyphType.Major, @"Your Healing Wave also heals you for 20% of the healing effect when you heal someone else.")]
        public bool GlyphofHealingWave { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Lightning Bolt", GlyphType.Major, @"Increases the damage dealt by Lightning Bolt by 4%.")]
        public bool GlyphofLightningBolt { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Shocking", GlyphType.Major, @"Reduces the global cooldown triggered by your shock spells by 0.5 sec.")]
        public bool GlyphofShocking { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Lightning Shield", GlyphType.Major, @"Increases the damage from Lightning Shield by 20%.")]
        public bool GlyphofLightningShield { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Flame Shock", GlyphType.Major, @"Increases the critical strike damage bonus of your Flame Shock damage by 60%.")]
        public bool GlyphofFlameShock { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Flametongue Weapon", GlyphType.Major, @"Increases spell critical strike chance by 2% while Flametongue Weapon is active.")]
        public bool GlyphofFlametongueWeapon { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Lava Lash", GlyphType.Major, @"Damage on your Lava Lash is increased by an additional 10% if your weapon is enchanted with Flametongue.")]
        public bool GlyphofLavaLash { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Fire Nova", GlyphType.Major, @"Reduces the cooldown of your Fire Nova by 3 seconds.")]
        public bool GlyphofFireNova { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Frost Shock", GlyphType.Major, @"Increases the duration of your Frost Shock by 2 sec.")]
        public bool GlyphofFrostShock { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Healing Stream Totem", GlyphType.Major, @"Your Healing Stream Totem heals for an additional 20%.")]
        public bool GlyphofHealingStreamTotem { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Lesser Healing Wave", GlyphType.Major, @"Your Lesser Healing Wave heals for 20% more if the target is also affected by your Earth Shield.")]
        public bool GlyphofLesserHealingWave { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Water Mastery", GlyphType.Major, @"Increases the passive mana regeneration of your Water Shield spell by 30%.")]
        public bool GlyphofWaterMastery { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Earthliving Weapon", GlyphType.Major, @"Increases the chance for your Earthliving weapon to trigger by 5%.")]
        public bool GlyphofEarthlivingWeapon { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Windfury Weapon", GlyphType.Major, @"Increases the chance per swing for Windfury Weapon to trigger by 2%.")]
        public bool GlyphofWindfuryWeapon { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Chain Lightning", GlyphType.Major, @"Your Chain Lightning strikes 1 additional target.")]
        public bool GlyphofChainLightning { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Chain Heal", GlyphType.Major, @"Your Chain Heal heals 1 additional target.")]
        public bool GlyphofChainHeal { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Earth Shield", GlyphType.Major, @"Increases the amount healed by your Earth Shield by 20%.")]
        public bool GlyphofEarthShield { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Feral Spirit", GlyphType.Major, @"Your spirit wolves gain an additional 30% of your attack power.")]
        public bool GlyphofFeralSpirit { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Hex", GlyphType.Major, @"Increases the damage your Hex target can take before the Hex effect is removed by 20%.")]
        public bool GlyphofHex { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        [GlyphData(19, "Glyph of Mana Tide Totem", GlyphType.Major, @"Your Mana Tide Totem grants an additional 1% of each target's maximum mana each time it pulses.")]
        public bool GlyphofManaTideTotem { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        [GlyphData(20, "Glyph of Riptide", GlyphType.Major, @"Increases the duration of Riptide by 6 sec.")]
        public bool GlyphofRiptide { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        [GlyphData(21, "Glyph of Stoneclaw Totem", GlyphType.Major, @"Your Stoneclaw Totem also places a damage absorb shield on you, equal to 4 times the strength of the shield it places on your totems.")]
        public bool GlyphofStoneclawTotem { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        [GlyphData(22, "Glyph of Stormstrike", GlyphType.Major, @"Increases the Nature damage bonus from your Stormstrike ability by an additional 8%.")]
        public bool GlyphofStormstrike { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        [GlyphData(23, "Glyph of Thunder", GlyphType.Major, @"Reduces the cooldown on Thunderstorm by 10 sec.")]
        public bool GlyphofThunder { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        [GlyphData(24, "Glyph of Totem of Wrath", GlyphType.Major, @"When you cast Totem of Wrath, you gain 30% of the totem's bonus spell power for 5 min.")]
        public bool GlyphofTotemofWrath { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        [GlyphData(25, "Glyph of Elemental Mastery", GlyphType.Major, @"Reduces the cooldown of your Elemental Mastery ability by 30 sec.")]
        public bool GlyphofElementalMastery { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        [GlyphData(26, "Glyph of Lava", GlyphType.Major, @"Your Lava Burst spell gains an additional 10% of your spellpower.")]
        public bool GlyphofLava { get { return _glyphData[26]; } set { _glyphData[26] = value; } }
        [GlyphData(27, "Glyph of Fire Elemental Totem", GlyphType.Major, @"Reduces the cooldown of your Fire Elemental Totem by 10 min.")]
        public bool GlyphofFireElementalTotem { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        [GlyphData(28, "Glyph of Thunderstorm", GlyphType.Minor, @"Increases the mana you recieve from your Thunderstorm spell by 2%, but it no longer knocks enemies back.")]
        public bool GlyphofThunderstorm { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
    }

    public partial class PriestTalents
    {
        private bool[] _glyphData = new bool[33];
        public override bool[] GlyphData { get { return _glyphData; } }

        // Major
        [GlyphData(0, "Glyph of Circle of Healing", GlyphType.Major, @"Your Circle of Healing spell heals 1 additional target.")]
        public bool GlyphofCircleOfHealing { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Dispel Magic", GlyphType.Major, @"Your Dispel Magic spell also heals your target for 3% maximum health.")]
        public bool GlyphofDispelMagic { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Dispersion", GlyphType.Major, @"Reduces the cooldown of your Dispersion by 45 sec.")]
        public bool GlyphofDispersion { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Fade", GlyphType.Major, @"Reduces the cooldown of your Fade spell by 9 sec.")]
        public bool GlyphofFade { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Fear Ward", GlyphType.Major, @"Reduces the cooldown and duration of Fear Ward by 60 sec.")]
        public bool GlyphofFearWard { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Flash Heal", GlyphType.Major, @"Reduces the mana cost of Flash Heal by 10%.")]
        public bool GlyphofFlashHeal { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Guardian Spirit", GlyphType.Major, @"If your Guardian Spirit lasts its entire duration without being triggered, the cooldown is reset to 1 min.")]
        public bool GlyphofGuardianSpirit { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Holy Nova", GlyphType.Major, @"Increases the damage and healing of your Holy Nova spell by an additional 20%.")]
        public bool GlyphofHolyNova { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Hymn of Hope", GlyphType.Major, @"Your Hymn of Hope lasts an additional 2 sec.")]
        public bool GlyphofHymnofHope { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Inner Fire", GlyphType.Major, @"Increases the armor from your Inner Fire spell by 50%.")]
        public bool GlyphofInnerFire { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Lightwell", GlyphType.Major, @"Increases the amount healed by your Lightwell by 20%.")]
        public bool GlyphofLightwell { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Mass Dispel", GlyphType.Major, @"Reduces the mana cost of Mass Dispel by 30%.")]
        public bool GlyphofMassDispel { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Mind Control", GlyphType.Major, @"Reduces the chance targets will resist or break Mind Control spell by an additional 17%.")]
        public bool GlyphofMindControl { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Mind Flay", GlyphType.Major, @"Increases the damage done by your Mind Flay spell by 10% when your target is afflicted with Shadow Word: Pain.")]
        public bool GlyphofMindFlay { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Mind Sear", GlyphType.Major, @"Increases the radius of effect on Mind Sear by 5 yards.")]
        public bool GlyphofMindSear { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Pain Suppression", GlyphType.Major, @"Allows Pain Suppression to be cast while stunned.")]
        public bool GlyphofPainSuppression { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Penance", GlyphType.Major, @"Reduces the cooldown of Penance by 2 sec.")]
        public bool GlyphofPenance { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Power Word: Shield", GlyphType.Major, @"Your Power Word: Shield also heals the target for 20% of absorption amount.")]
        public bool GlyphofPowerWordShield { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Prayer of Healing", GlyphType.Major, @"Your Prayer of Healing spell also heals an additional 20% of its initial heal over 6 sec.")]
        public bool GlyphofPrayerofHealing { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        [GlyphData(19, "Glyph of Psychic Scream", GlyphType.Major, @"Increases the duration of your Pscychic Scream by 2 sec. and increases its cooldown by 8 sec.")]
        public bool GlyphofPsychicScream { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        [GlyphData(20, "Glyph of Renew", GlyphType.Major, @"Reduces the duration of your Renew by 3 sec. but increases the amount healed each tick by 25%.")]
        public bool GlyphofRenew { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        [GlyphData(21, "Glyph of Scourge Imprisonment", GlyphType.Major, @"Reduces the cast time of your Shackle Undead by 0.5 sec.")]
        public bool GlyphofScourgeImprisonment { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        [GlyphData(22, "Glyph of Shadow", GlyphType.Major, @"While in Shadowform, your spell critical strikes increase your spell power by 30% of your Spirit for 10 sec.")]
        public bool GlyphofShadow { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        [GlyphData(23, "Glyph of Shadow Word: Death", GlyphType.Major, @"Targets below 35% health take an additional 10% damage from your Shadow Word: Death spell.")]
        public bool GlyphofShadowWordDeath { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        [GlyphData(24, "Glyph of Shadow Word: Pain", GlyphType.Major, @"The periodic damage ticks of your Shadow Word: Pain spell restore 1% of your base mana.")]
        public bool GlyphofShadowWordPain { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        [GlyphData(25, "Glyph of Smite", GlyphType.Major, @"Your Smite spell inflicts an additional 20% damage against targets afflicted by Holy Fire.")]
        public bool GlyphofSmite { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        [GlyphData(26, "Glyph of Spirit of Redemption", GlyphType.Major, @"Increases the duration of Spirit of Redemption by 6 sec.")]
        public bool GlyphofSpiritofRedemption { get { return _glyphData[26]; } set { _glyphData[26] = value; } }

        // Minor
        [GlyphData(27, "Glyph of Fading", GlyphType.Minor, @"Reduces the mana cost of your Fade spell by 30%.")]
        public bool GlyphofFading { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        [GlyphData(28, "Glyph of Fortitude", GlyphType.Minor, @"Reduces the mana cost of your Power Word: Fortitude and Prayer of Fortitude by 50%.")]
        public bool GlyphofFortitude { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
        [GlyphData(29, "Glyph of Levitate", GlyphType.Minor, @"Your Levitate spell no longer requires a reagent.")]
        public bool GlyphofLevitate { get { return _glyphData[29]; } set { _glyphData[29] = value; } }
        [GlyphData(30, "Glyph of Shackle Undead", GlyphType.Minor, @"Increases the range of your Shackle Undead spell by 5 yards.")]
        public bool GlyphofShackleUndead { get { return _glyphData[30]; } set { _glyphData[30] = value; } }
        [GlyphData(31, "Glyph of Shadow Protection", GlyphType.Minor, @"Increases the duration of your Shadow Protection and Prayer of Shadow Protection spells by 10 min.")]
        public bool GlyphofShadowProtection { get { return _glyphData[31]; } set { _glyphData[31] = value; } }
        [GlyphData(32, "Glyph of Shadowfiend", GlyphType.Minor, @"Receive 5% of your maximum mana if your Shadowfiend dies from damage.")]
        public bool GlyphofShadowfiend { get { return _glyphData[32]; } set { _glyphData[32] = value; } }


    }

    public partial class DeathKnightTalents
    {
        private bool[] _glyphData = new bool[11 + 12 + 6]; // Prime + Major + Minor
        public override bool[] GlyphData { get { return _glyphData; } }
        #region Prime
        //* Death Coil - Increases the damage or healing done by Death Coil by 15%.
        [GlyphData(0, "Glyph of Death Coil", GlyphType.Prime, @"Increases the damage or healing done by Death Coil by 15%.")]
        public bool GlyphofDeathCoil { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        //* Death Strike - Increases your Death Strike's damage by 2% for every 5 runic power you currently have (up to a maximum of 40%). The runic power is not consumed by this effect.
        [GlyphData(1, "Glyph of Death Strike", GlyphType.Prime, @"Increases your Death Strike's damage by 2% for every 5 runic power you currently have (up to a maximum of 40%). The runic power is not consumed by this effect.")]
        public bool GlyphofDeathStrike { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        //* Death and Decay - Increases the duration of your Death and Decay spell by 50%.
        [GlyphData(2, "Glyph of Death and Decay", GlyphType.Prime, @"Increases the duration of your Death and Decay spell by 50%.")]
        public bool GlyphofDeathandDecay { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        //* Frost Strike - Reduces the cost of your Frost Strike by 8 Runic Power.
        [GlyphData(3, "Glyph of Frost Strike", GlyphType.Prime, @"Reduces the cost of your Frost Strike by 8 Runic Power.")]
        public bool GlyphofFrostStrike { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        //* Heart Strike - Increases the damage of your Heart Strike ability by 30%.
        [GlyphData(4, "Glyph of Heart Strike", GlyphType.Prime, @"Increases the damage of your Heart Strike ability by 30%.")]
        public bool GlyphofHeartStrike { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        //* Howling Blast - Your Howling Blast ability now infects your targets with Frost Fever.
        [GlyphData(5, "Glyph of Howling Blast", GlyphType.Prime, @"Your Howling Blast ability now infects your targets with Frost Fever.")]
        public bool GlyphofHowlingBlast { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        //* Icy Touch - Your Frost Fever disease deals 20% additional damage.
        [GlyphData(6, "Glyph of Icy Touch", GlyphType.Prime, @"Your Frost Fever disease deals 20% additional damage.")]
        public bool GlyphofIcyTouch { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        //* Obliterate - Increases the damage of your Obliterate ability by 20%.
        [GlyphData(7, "Glyph of Obliterate", GlyphType.Prime, @"Increases the damage of your Obliterate ability by 20%.")]
        public bool GlyphofObliterate { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        //* Raise Dead - Your Ghoul receives an additional 40% of your Strength and 40% of your Stamina.
        [GlyphData(8, "Glyph of Raise Dead", GlyphType.Prime, @"Your Ghoul receives an additional 40% of your Strength and 40% of your Stamina.")]
        public bool GlyphofRaiseDead { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        //* Rune Strike - Increases the critical strike chance of your Rune Strike by 10%.
        [GlyphData(9, "Glyph of Rune Strike", GlyphType.Prime, @"Increases the critical strike chance of your Rune Strike by 10%.")]
        public bool GlyphofRuneStrike { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        //* Scourge Strike - Increases the Shadow damage portion of your Scourge Strike by 30%.
        [GlyphData(10, "Glyph of Scourge Strike", GlyphType.Prime, @"Increases the Shadow damage portion of your Scourge Strike by 30%.")]
        public bool GlyphofScourgeStrike { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        #endregion
        #region Major
        [GlyphData(11, "Glyph of Anti-Magic Shell", GlyphType.Major, @"Increases the duration of your Anti-Magic Shell by 2 sec.")]
        public bool GlyphofAntiMagicShell { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Blood Boil", GlyphType.Major, @"Increases the radius of your Blood Boil ability by 50%.")]
        public bool GlyphofBloodBoil { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Bone Shield", GlyphType.Major, @"Increases your movement speed by 15% while Bone Shield is active. This does not stack with other movement-speed increasing effects.")]
        public bool GlyphofBoneShield { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Chains of Ice", GlyphType.Major, @"Your Chains of Ice also causes 144 to 156 Frost damage, increased by your attack power.")]
        public bool GlyphofChainsofIce { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Dancing Rune Weapon", GlyphType.Major, @"Increases your threat generation by 50% while your Dancing Rune Weapon is active.")]
        public bool GlyphofDancingRuneWeapon { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Death Grip", GlyphType.Major, @"Increases the range of your Death Grip ability by 5 yards.")]
        public bool GlyphofDeathGrip { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Hungering Cold", GlyphType.Major, @"Your Hungering Cold ability no longer costs runic power.")]
        public bool GlyphofHungeringCold { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        [GlyphData(18, "Glyph of Pestilence", GlyphType.Major, @"Increases the radius of your Pestilence effect by 5 yards.")]
        public bool GlyphofPestilence { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        [GlyphData(19, "Glyph of Pillar of Frost", GlyphType.Major, @"Empowers your Pillar of Frost, making you immune to all effects that cause loss of control of your character, but also freezing you in place while the ability is active.")]
        public bool GlyphofPillarofFrost { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        [GlyphData(20, "Glyph of Rune Tap", GlyphType.Major, @"Your Rune Tap also heals your party for 10% of their maximum health.")]
        public bool GlyphofRuneTap { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        [GlyphData(21, "Glyph of Strangulate", GlyphType.Major, @"Increases the Silence duration of your Strangulate ability by 2 sec when used on a target who is casting a spell.")]
        public bool GlyphofStrangulate { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        [GlyphData(22, "Glyph of Vampiric Blood", GlyphType.Major, @"Increases the bonus healing received while your Vampiric Blood is active by an additional 15%, but your Vampiric Blood no longer grants you health.")]
        public bool GlyphofVampiricBlood { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        #endregion
        #region Minor
        [GlyphData(23, "Glyph of Blood Tap", GlyphType.Minor, @"Your Blood Tap no longer causes damage to you.")]
        public bool GlyphofBloodTap { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        [GlyphData(24, "Glyph of Death's Embrace", GlyphType.Minor, @"Your Death Coil refunds 20 runic power when used to heal.")]
        public bool GlyphofDeathsEmbrace { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        [GlyphData(25, "Glyph of Horn of Winter", GlyphType.Minor, @"Increases the duration of your Horn of Winter ability by 1 min.")]
        public bool GlyphofHornofWinter { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        [GlyphData(26, "Glyph of Path of Frost", GlyphType.Minor, @"Your Path of Frost ability allows you to fall from a greater distance without suffering damage.")]
        public bool GlyphofPathofFrost { get { return _glyphData[26]; } set { _glyphData[26] = value; } }
        [GlyphData(27, "Glyph of Raise Ally", GlyphType.Minor, @"Increases the health of your Risen Ally by 25% and its run speed by 15%.c")]
        public bool GlyphofRaiseAlly { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        [GlyphData(28, "Glyph of Resilient Grip", GlyphType.Minor, @"When your Death Grip ability fails because its target is immune, its cooldown is reset.")]
        public bool GlyphofResilientGrip { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
        #endregion
    } 

    public partial class WarlockTalents
    {
        private bool[] _glyphData = new bool[17];
        public override bool[] GlyphData { get { return _glyphData; } }
  
        [GlyphData(0, "Glyph of Chaos Bolt", GlyphType.Major, @"Reduces the cooldown on Chaos Bolt by 2 sec.")]
        public bool GlyphChaosBolt { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Conflagrate", GlyphType.Major, @"Your Conflagrate spell no longer consumes your Immolate or Shadowflame spell from" +
                    " the target.")]
        public bool GlyphConflag { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Corruption", GlyphType.Major, @"Your Corruption spell has a 4% chance to cause you to enter a Shadow Trance state" +
                    " after damaging the opponent.  The Shadow Trance state reduces the casting time " +
                    "of your next Shadow Bolt spell by 100%.")]
        public bool GlyphCorruption { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Curse of Agony", GlyphType.Major, @"Increases the duration of your Curse of Agony by 4 sec.")]
        public bool GlyphCoA { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Death Coil", GlyphType.Major, @" Increases the duration of your Death Coil by 0.5 sec.")]
        public bool GlyphDeathCoil { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Felguard", GlyphType.Major, @"Increases the Felguard\'s total attack power by 20%.")]
        public bool GlyphFelguard { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Haunt", GlyphType.Major, @"The bonus damage granted by your Haunt spell is increased by an additional 3%.")]
        public bool GlyphHaunt { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Immolate", GlyphType.Major, @"Increases the periodic damage of your Immolate by 10%.")]
        public bool GlyphImmolate { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Imp", GlyphType.Major, @"Increases the damage done by your Imp\'s Firebolt spell by 20%.")]
        public bool GlyphImp { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Incinerate", GlyphType.Major, @"Increases the damage done by Incinerate by 5%.")]
        public bool GlyphIncinerate { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Life Tap", GlyphType.Major, @"When you use Life Tap, you gain 20% of your Spirit as spell power for 40 sec.")]
        public bool GlyphLifeTap { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Metamorphosis", GlyphType.Major, @"Increases the duration of your Metamorphosis by 6 sec.")]
        public bool GlyphMetamorphosis { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Quick Decay", GlyphType.Major, @"Your haste now reduces the time between periodic damage ticks of your Corruption spell.")]
        public bool GlyphQuickDecay { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Searing Pain", GlyphType.Major, @"Increases the critical strike bonus of your Searing Pain by 20%.")]
        public bool GlyphSearingPain { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Shadowbolt", GlyphType.Major, @"Reduces the mana cost of your Shadow Bolt by 10%.")]
        public bool GlyphSB { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Shadowburn", GlyphType.Major, @"Increases the critical strike chance of Shadowburn by 20% when the target is belo" +
                    "w 35% health.")]
        public bool GlyphShadowburn { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        [GlyphData(16, "Glyph of Unstable Affliction", GlyphType.Major, @"Decreases the casting time of your Unstable Affliction by 0.2 sec.")]
        public bool GlyphUA { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
    }

    public partial class RogueTalents
    {
        private bool[] _glyphData = new bool[34];
        public override bool[] GlyphData { get { return _glyphData; } }

        [GlyphData(0, "Glyph of Backstab", GlyphType.Major, @"Your Backstab increases the duration of your Rupture effect on the target by 2 sec, up to a maximum of 6 additional sec.")]
        public bool GlyphOfBackstab { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        [GlyphData(1, "Glyph of Eviscerate", GlyphType.Major, @"Increases the critical strike chance of Eviscerate by 10%.")]
        public bool GlyphOfEviscerate { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        [GlyphData(2, "Glyph of Mutilate", GlyphType.Major, @"Reduces the cost of Mutilate by 5 energy.")]
        public bool GlyphOfMutilate { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        [GlyphData(3, "Glyph of Hunger For Blood", GlyphType.Major, @"Increases the bonus damage from Hunger For Blood by 3%.")]
        public bool GlyphOfHungerforBlood { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        [GlyphData(4, "Glyph of Killing Spree", GlyphType.Major, @"Reduces the cooldown on Killing Spree by 45 seconds.")]
        public bool GlyphOfKillingSpree { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        [GlyphData(5, "Glyph of Vigor", GlyphType.Major, @"Vigor grants an additional 10 maximum energy.")]
        public bool GlyphOfVigor { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        [GlyphData(6, "Glyph of Fan of Knives", GlyphType.Major, @"Increases the damage done by Fan of Knives by 20%.")]
        public bool GlyphOfFanOfKnives { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        [GlyphData(7, "Glyph of Expose Armor", GlyphType.Major, @"Increases the duration of Expose Armor by 10 sec.")]
        public bool GlyphOfExposeArmor { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        [GlyphData(8, "Glyph of Sinister Strike", GlyphType.Major, @"Your Sinister Strike critical strikes have a 50% chance to add an additional combo point.")]
        public bool GlyphOfSinisterStrike { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        [GlyphData(9, "Glyph of Slice and Dice", GlyphType.Major, @"Increases the duration of Slice and Dice by 3 sec.")]
        public bool GlyphOfSliceandDice { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        [GlyphData(10, "Glyph of Feint", GlyphType.Major, @"Reduces the energy cost of Feint by 10.")]
        public bool GlyphOfFeint { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        [GlyphData(11, "Glyph of Ghostly Strike", GlyphType.Major, @"Increases the damage dealt by Ghostly Strike by 40% and the duration of its effect by 4 sec., but increases its cooldown by 10 sec.")]
        public bool GlyphOfGhostlyStrike { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        [GlyphData(12, "Glyph of Rupture", GlyphType.Major, @"Increases the duration of Rupture by 4 sec.")]
        public bool GlyphOfRupture { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        [GlyphData(13, "Glyph of Blade Flurry", GlyphType.Major, @"Reduces the energy cost of Blade Flurry by 100%.")]
        public bool GlyphOfBladeFlurry { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        [GlyphData(14, "Glyph of Adrenaline Rush", GlyphType.Major, @"Increases the duration of Adrenaline Rush by 5 sec.")]
        public bool GlyphOfAdrenalineRush { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        [GlyphData(15, "Glyph of Evasion", GlyphType.Major, @"Increases the duration of Evasion by 5 sec.")]
        public bool GlyphOfEvasion { get { return _glyphData[15]; } set{ _glyphData[15] = value;} }
        [GlyphData(16, "Glyph of Garrote", GlyphType.Major, @"Increases periodic damage dealt by Garrote by 45%, but decreases the duration by 3 sec.")]
        public bool GlyphOfGarrote { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        [GlyphData(17, "Glyph of Gouge", GlyphType.Major, @"Reduces the energy cost of Gouge by 10.")]
        public bool GlyphOfGouge { get { return _glyphData[17]; } set{ _glyphData[17] = value;} }
        [GlyphData(18, "Glyph of Sap", GlyphType.Major, @"Increases the duration of Sap by 20 sec.")]
        public bool GlyphOfSap { get { return _glyphData[18]; } set{ _glyphData[18] = value;} }
        [GlyphData(19, "Glyph of Sprint", GlyphType.Major, @"Increases the movement speed of your Sprint ability by an additional 30%.")]
        public bool GlyphOfSprint { get { return _glyphData[19]; } set{ _glyphData[19] = value;} }
        [GlyphData(20, "Glyph of Ambush", GlyphType.Major, @"Increases the range on Ambush by 5 yards.")]
        public bool GlyphOfAmbush { get { return _glyphData[20]; } set{ _glyphData[20] = value;} }
        [GlyphData(21, "Glyph of Crippling Poison", GlyphType.Major, @"Increases the chance to trigger Crippling Poison by 20%.")]
        public bool GlyphOfCripplingPoison { get { return _glyphData[21]; } set{ _glyphData[21] = value;} }
        [GlyphData(22, "Glyph of Hemorrhage", GlyphType.Major, @"Increases the damage bonus against targets afflicted by Hemorrhage by 40%.")]
        public bool GlyphOfHemorrhage { get { return _glyphData[22]; } set{ _glyphData[22] = value;} }
        [GlyphData(23, "Glyph of Preparation", GlyphType.Major, @"Your Preparation ability also instantly resets the cooldown of Blade Furry, Dismantle, and Kick.")]
        public bool GlyphOfPreparation { get { return _glyphData[23]; } set{ _glyphData[23] = value;} }
        [GlyphData(24, "Glyph of Shadow Dance", GlyphType.Major, @"Increases the duration of Shadow Dance by 4 sec.")]
        public bool GlyphOfShadowDance { get { return _glyphData[24]; } set{ _glyphData[24] = value;} }
        [GlyphData(25, "Glyph of Deadly Throw", GlyphType.Major, @"Increases the slowing effect on Deadly Throw by 10%.")]
        public bool GlyphOfDeadlyThrow { get { return _glyphData[25]; } set{ _glyphData[25] = value;} }
        [GlyphData(26, "Glyph of Cloak of Shadows", GlyphType.Major, @"While Cloak of Shadows is active, you take 40% less physical damage.")]
        public bool GlyphOfCloakOfShadows { get { return _glyphData[26]; } set{ _glyphData[26] = value;} }
        [GlyphData(27, "Glyph of Tricks of the Trade", GlyphType.Major, @"The bonus damage and threat redirection granted by your Tricks of the Trade ability lasts an additional 4 sec.")]
        public bool GlyphOfTricksOfTheTrade { get { return _glyphData[27]; } set{ _glyphData[27] = value;} }

        //minor glyphs
        [GlyphData(28, "Glyph of Blurred Speed", GlyphType.Minor, @"You gain the ability to walk on water while your Sprint ability is active.")]
        public bool GlyphOfBlurredSpeed { get { return _glyphData[28]; } set{ _glyphData[28] = value;} }
        [GlyphData(29, "Glyph of Pick Pocket", GlyphType.Minor, @"Increases the range of your Pick Pocket ability by 5 yards.")]
        public bool GlyphOfPickPocket { get { return _glyphData[29]; } set{ _glyphData[29] = value;} }
        [GlyphData(30, "Glyph of Pick Lock", GlyphType.Minor, @"Reduces the cast time of your Pick Lock ability by 100%.")]
        public bool GlyphOfPickLock { get { return _glyphData[30]; } set{ _glyphData[30] = value;} }
        [GlyphData(31, "Glyph of Distract", GlyphType.Minor, @"Increases the range of your Distract ability by 5 yards.")]
        public bool GlyphOfDistrict { get { return _glyphData[31]; } set{ _glyphData[31] = value;} }
        [GlyphData(32, "Glyph of Vanish", GlyphType.Minor, @"Increases your movement speed by 30% while the Vanish effect is active.")]
        public bool GlyphOfVanish { get { return _glyphData[32]; } set{ _glyphData[32] = value;} }
        [GlyphData(33, "Glyph of Safe Fall", GlyphType.Minor, @"Increases the distance your Safe Fall ability allows you to fall without taking damage.")]
        public bool GlyphOfSafeFall { get { return _glyphData[33]; } set{ _glyphData[33] = value;}}
    }

    public partial class HunterTalents
    {
        private bool[] _glyphData = new bool[33]; // Set this to the final value of how many glyphs there end up being.
        public override bool[] GlyphData { get { return _glyphData; } }

        // ===== MAJOR GLYPHS =========================
        /// <summary>Reduces the cooldown of your Aimed Shot ability by 2 sec.</summary>
        [GlyphData( 0, "Glyph of Aimed Shot", GlyphType.Major, @"Reduces the cooldown of your Aimed Shot ability by 2 sec.")]
        public bool GlyphOfAimedShot { get { return _glyphData[0]; } set { _glyphData[0] = value; } }
        /// <summary>Your Arcane Shot refunds 20% of its mana cost if the target has one of your Stings active on it.</summary>
        [GlyphData( 1, "Glyph of Arcane Shot", GlyphType.Major, @"Your Arcane Shot refunds 20% of its mana cost if the target has one of your Stings active on it.")]
        public bool GlyphOfArcaneShot { get { return _glyphData[1]; } set { _glyphData[1] = value; } }
        /// <summary>Increases the amount of mana gained from attacks while Aspect of the Viper is active by 10%.</summary>
        [GlyphData( 2, "Glyph of Aspect of the Viper", GlyphType.Major, @"Increases the amount of mana gained from attacks while Aspect of the Viper is active by 10%.")]
        public bool GlyphOfAspectOfTheViper { get { return _glyphData[2]; } set { _glyphData[2] = value; } }
        /// <summary>Decreases the cooldown of Bestial Wrath by 20 sec.</summary>
        [GlyphData( 3, "Glyph of Bestial Wrath", GlyphType.Major, @"Decreases the cooldown of Bestial Wrath by 20 sec.")]
        public bool GlyphOfBestialWrath { get { return _glyphData[3]; } set { _glyphData[3] = value; } }
        /// <summary>Reduces the cooldown of Chimera Shot by 1 sec.</summary>
        [GlyphData( 4, "Glyph of Chimera Shot", GlyphType.Major, @"Reduces the cooldown of Chimera Shot by 1 sec.")]
        public bool GlyphOfChimeraShot { get { return _glyphData[4]; } set { _glyphData[4] = value; } }
        /// <summary>Decreases the cooldown of Deterrence by 10 sec.</summary>
        [GlyphData( 5, "Glyph of Deterrence", GlyphType.Major, @"Decreases the cooldown of Deterrence by 10 sec.")]
        public bool GlyphOfDeterrence { get { return _glyphData[5]; } set { _glyphData[5] = value; } }
        /// <summary>Decreases the cooldown of Disengage by 5 sec.</summary>
        [GlyphData( 6, "Glyph of Disengage", GlyphType.Major, @"Decreases the cooldown of Disengage by 5 sec.")]
        public bool GlyphOfDisengage { get { return _glyphData[6]; } set { _glyphData[6] = value; } }
        /// <summary>Increases the critical strike chance of Explosive Shot by 4%.</summary>
        [GlyphData( 7, "Glyph of Explosive Shot", GlyphType.Major, @"Increases the critical strike chance of Explosive Shot by 4%.")]
        public bool GlyphOfExplosiveShot { get { return _glyphData[7]; } set { _glyphData[7] = value; } }
        /// <summary>The periodic damage from your Explosive Trap can now be critical strikes.</summary>
        [GlyphData( 8, "Glyph of Explosive Trap", GlyphType.Major, @"The periodic damage from your Explosive Trap can now be critical strikes.")]
        public bool GlyphOfExplosiveTrap { get { return _glyphData[8]; } set { _glyphData[8] = value; } }
        /// <summary>When your Freezing Trap breaks, the victim's movement speed is reduced by 30% for 4 sec.</summary>
        [GlyphData( 9, "Glyph of Freezing Trap", GlyphType.Major, @"When your Freezing Trap breaks, the victim's movement speed is reduced by 30% for 4 sec.")]
        public bool GlyphOfFreezingTrap { get { return _glyphData[9]; } set { _glyphData[9] = value; } }
        /// <summary>Increases the radius of the effect from your Frost Trap by 2 yards.</summary>
        [GlyphData(10, "Glyph of Frost Trap", GlyphType.Major, @"Increases the radius of the effect from your Frost Trap by 2 yards.")]
        public bool GlyphOfFrostTrap { get { return _glyphData[10]; } set { _glyphData[10] = value; } }
        /// <summary>Increases the attack power bonus of your Hunter's Mark by 20%.</summary>
        [GlyphData(11, "Glyph of Hunter's Mark", GlyphType.Major, @"Increases the attack power bonus of your Hunter's Mark by 20%.")]
        public bool GlyphOfHuntersMark { get { return _glyphData[11]; } set { _glyphData[11] = value; } }
        /// <summary>Decreases the duration of the effect from your Immolation Trap by 6 sec., but damage while active is increased by 100%.</summary>
        [GlyphData(12, "Glyph of Immolation Trap", GlyphType.Major, @"Decreases the duration of the effect from your Immolation Trap by 6 sec., but damage while active is increased by 100%.")]
        public bool GlyphOfImmolationTrap { get { return _glyphData[12]; } set { _glyphData[12] = value; } }
        /// <summary>Reduces the cooldown of Kill Shot by 6 sec.</summary>
        [GlyphData(13, "Glyph of Kill Shot", GlyphType.Major, @"Reduces the cooldown of Kill Shot by 6 sec.")]
        public bool GlyphOfKillShot { get { return _glyphData[13]; } set { _glyphData[13] = value; } }
        /// <summary>Increases the healing done by your Mend Pet ability by 40%.</summary>
        [GlyphData(14, "Glyph of Mending", GlyphType.Major, @"Increases the healing done by your Mend Pet ability by 40%.")]
        public bool GlyphOfMending { get { return _glyphData[14]; } set { _glyphData[14] = value; } }
        /// <summary>Decreases the cooldown of Multi-Shot by 1 sec.</summary>
        [GlyphData(15, "Glyph of Multi-Shot", GlyphType.Major, @"Decreases the cooldown of Multi-Shot by 1 sec.")]
        public bool GlyphOfMultiShot { get { return _glyphData[15]; } set { _glyphData[15] = value; } }
        /// <summary>Increases the haste from Rapid Fire by an additional 8%.</summary>
        [GlyphData(16, "Glyph of Rapid Fire", GlyphType.Major, @"Increases the haste from Rapid Fire by an additional 8%.")]
        public bool GlyphOfRapidFire { get { return _glyphData[16]; } set { _glyphData[16] = value; } }
        /// <summary>Reduces damage taken by 20% for 3 sec after using Raptor Strike.</summary>
        [GlyphData(17, "Glyph of Raptor Strike", GlyphType.Major, @"Reduces damage taken by 20% for 3 sec after using Raptor Strike.")]
        public bool GlyphOfRaptorStrike { get { return _glyphData[17]; } set { _glyphData[17] = value; } }
        /// <summary>Increases the range of Scatter Shot by 3 yards.</summary>
        [GlyphData(18, "Glyph of Scatter Shot", GlyphType.Major, @"Increases the range of Scatter Shot by 3 yards.")]
        public bool GlyphOfScatterShot { get { return _glyphData[18]; } set { _glyphData[18] = value; } }
        /// <summary>Increases the duration of your Serpent Sting by 6 sec.</summary>
        [GlyphData(19, "Glyph of Serpent Sting", GlyphType.Major, @"Increases the duration of your Serpent Sting by 6 sec.")]
        public bool GlyphOfSerpentSting { get { return _glyphData[19]; } set { _glyphData[19] = value; } }
        /// <summary>Snakes generated by your Snake Trap take 90% reduced damge from area of effect spells.</summary>
        [GlyphData(20, "Glyph of Snake Trap", GlyphType.Major, @"Snakes generated by your Snake Trap take 90% reduced damge from area of effect spells.")]
        public bool GlyphOfSnakeTrap { get { return _glyphData[20]; } set { _glyphData[20] = value; } }
        /// <summary>Increases the damage dealt by Steady Shot by 10% when your target is afflicted with Serpent Sting.</summary>
        [GlyphData(21, "Glyph of Steady Shot", GlyphType.Major, @"Increases the damage dealt by Steady Shot by 10% when your target is afflicted with Serpent Sting.")]
        public bool GlyphOfSteadyShot { get { return _glyphData[21]; } set { _glyphData[21] = value; } }
        /// <summary>Increases the attack power bonus of Aspect of the Beast for you and your pet by an additional 2%.</summary>
        [GlyphData(22, "Glyph of the Beast", GlyphType.Major, @"Increases the attack power bonus of Aspect of the Beast for you and your pet by an additional 2%.")]
        public bool GlyphOfTheBeast { get { return _glyphData[22]; } set { _glyphData[22] = value; } }
        /// <summary>Increases the haste bonus of the Improved Aspect of the Hawk effect by an additional 6%.</summary>
        [GlyphData(23, "Glyph of the Hawk", GlyphType.Major, @"Increases the haste bonus of the Improved Aspect of the Hawk effect by an additional 6%.")]
        public bool GlyphOfTheHawk { get { return _glyphData[23]; } set { _glyphData[23] = value; } }
        /// <summary>While your Trueshot Aura is active, you have 10% increased critical strike chance on your Aimed Shot.</summary>
        [GlyphData(24, "Glyph of Trueshot Aura", GlyphType.Major, @"While your Trueshot Aura is active, you have 10% increased critical strike chance on your Aimed Shot.")]
        public bool GlyphOfTrueshotAura { get { return _glyphData[24]; } set { _glyphData[24] = value; } }
        /// <summary>Decreases the mana cost of Volley by 20%.</summary>
        [GlyphData(25, "Glyph of Volley", GlyphType.Major, @"Decreases the mana cost of Volley by 20%.")]
        public bool GlyphOfVolley { get { return _glyphData[25]; } set { _glyphData[25] = value; } }
        /// <summary>Decreases the cooldown of your Wyvern Sting by 6 sec.</summary>
        [GlyphData(26, "Glyph of Wyvern Sting", GlyphType.Major, @"Decreases the cooldown of your Wyvern Sting by 6 sec.")]
        public bool GlyphOfWyvernSting { get { return _glyphData[26]; } set { _glyphData[26] = value; } }
        // ===== MINOR GLYPHS =========================
        /// <summary>Reduces the cooldown of your Feign Death spell by 5 sec.</summary>
        [GlyphData(27, "Glyph of Feign Death", GlyphType.Minor, @"Reduces the cooldown of your Feign Death spell by 5 sec.")]
        public bool GlyphOfFeignDeath { get { return _glyphData[27]; } set { _glyphData[27] = value; } }
        /// <summary>Your Mend Pet spell increases your pet's happiness slightly.</summary>
        [GlyphData(28, "Glyph of Mend Pet", GlyphType.Minor, @"Your Mend Pet spell increases your pet's happiness slightly.")]
        public bool GlyphOfMendPet { get { return _glyphData[28]; } set { _glyphData[28] = value; } }
        /// <summary>Increases the damage your pet inflicts while using Eyes of the Beast by 50%.</summary>
        [GlyphData(29, "Glyph of Possessed Strength", GlyphType.Minor, @"Increases the damage your pet inflicts while using Eyes of the Beast by 50%.")]
        public bool GlyphOfPossessedStrength { get { return _glyphData[29]; } set { _glyphData[29] = value; } }
        /// <summary>Reduces the pushback suffered from damaging attacks while casting Revive Pet by 100%.</summary>
        [GlyphData(30, "Glyph of Revive Pet", GlyphType.Minor, @"Reduces the pushback suffered from damaging attacks while casting Revive Pet by 100%.")]
        public bool GlyphOfRevivePet { get { return _glyphData[30]; } set { _glyphData[30] = value; } }
        /// <summary>Reduces the pushback suffered from damaging attacks while casting Scare Beast by 75%.</summary>
        [GlyphData(31, "Glyph of Scare Beast", GlyphType.Minor, @"Reduces the pushback suffered from damaging attacks while casting Scare Beast by 75%.")]
        public bool GlyphOfScareBeast { get { return _glyphData[31]; } set { _glyphData[31] = value; } }
        /// <summary>Increases the range of your Aspect of the Pack ability by 15 yards.</summary>
        [GlyphData(32, "Glyph of the Pack", GlyphType.Minor, @"Increases the range of your Aspect of the Pack ability by 15 yards.")]
        public bool GlyphOfThePack { get { return _glyphData[32]; } set { _glyphData[32] = value; } }
    }

}