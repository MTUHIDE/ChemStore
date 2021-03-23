using System;
using System.Collections.Generic;

public enum Departments
{
    BiomedicalEng,
    ChemEng,
    CivilAndEnviromentalEng,
    ElectricalAndComputerEng,
    EngFundamentals,
    GeoMiningEngAndSciences,
    MandMEngTech,
    MaterialsScienceEng,
    MEEM,
    BiologicalSciences,
    Chemistry,
    CognitiveAndLearning,
    KinesiologyAndIntegrativePhysiology,
    Humanities,
    Math,
    Physics,
    SocialSciences,
    VPA,
    AirForceROTC,
    ArmyROTC,
    AppliedComputing,
    ComputerScience,
    Business,
    ForestResourcesAndEnvironmentalScience,
    Pavlis,
    GradSchool
}

namespace ChemStoreWebApp.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public Departments Department { get; set; }
        public int? Room { get; set; }
        public int? Building { get; set; }
        public int LocationFid { get; set; }

        public virtual HasLocation LocationF { get; set; }
    }
}
