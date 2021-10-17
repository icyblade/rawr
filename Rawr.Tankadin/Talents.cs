using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr.Tankadin
{
    public class Talents
    {

        public Talents()
        {
            // Protection
            Thoughness = 5;
            ImprovedRighteousFury = 3;
            ShieldSpecialization = 3;
            Anticipation = 5;
            OneHandSpec = 5;
            SacredDuty = 2;
            CombatExpertise = 5;
            // Retribution
            Deflection = 4;
            ImprovedJudgement = 2;
        }

        public int Deflection { get; set; }
        public int Anticipation { get; set; }
        public int CombatExpertise { get; set; }
        public int SacredDuty { get; set; }
        public int ShieldSpecialization { get; set; }
        public int Thoughness { get; set; }
        public int OneHandSpec { get; set; }
        public int ImprovedRighteousFury { get; set; }
        public int ImprovedJudgement { get; set; }
    }
}
