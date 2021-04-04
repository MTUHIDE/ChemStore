using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Departments
{
    [Display(Name = "Biomedical Engineering")]
    BiomedicalEng,
    [Display(Name = "Chemical Engineering")]
    ChemEng,
    [Display(Name = "Civil and Environmental Engineering")]
    CivilAndEnviromentalEng,
    [Display(Name = "Electrical and Computer Engineering")]
    ElectricalAndComputerEng,
    [Display(Name = "Engineering Fundamentals")]
    EngFundamentals,
    [Display(Name = "Geological and Mining Engineering and Sciences")]
    GeoMiningEngAndSciences,
    [Display(Name = "Manufacturing and Mechanical Engineering Technology")]
    MandMEngTech,
    [Display(Name = "Materials Science and Engineering")]
    MaterialsScienceEng,
    [Display(Name = "Mechanical Engineering-Engineering Mechanics")]
    MEEM,
    [Display(Name = "Biological Sciences")]
    BiologicalSciences,

    Chemistry,
    [Display(Name = "Cognitive and Learning Sciences")]
    CognitiveAndLearning,
    [Display(Name = "Kinesiology and Integrative Physiology")]
    KinesiologyAndIntegrativePhysiology,

    Humanities,
    [Display(Name = "Mathematical Sciences")]
    Math,

    Physics,
    [Display(Name = "Social Sciences")]
    SocialSciences,
    [Display(Name = "Visual and Performing Arts")]
    VPA,
    [Display(Name = "Air Force ROTC")]
    AirForceROTC,
    [Display(Name = "Army ROTC")]
    ArmyROTC,
    [Display(Name = "Applied Computing")]
    AppliedComputing,
    [Display(Name = "Computer Science")]
    ComputerScience,
    [Display(Name = "College of Business")]
    Business,
    [Display(Name = "College of Forest Resources and Environmental Science")]
    ForestResourcesAndEnvironmentalScience,
    [Display(Name = "Pavlis Honors College")]  
    Pavlis,
    [Display(Name = "Graduate School")]
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
