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

public enum Roles
{
    Admin,
    Employee,
    Associate,
    Member
}

namespace ChemStoreWebApp.Models
{
    public partial class Account
    {
        public Account()
        {
            Container = new HashSet<Container>();
        }

        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Role { get; set; }
        public bool Supervisor { get; set; }
        public int? Department { get; set; }

        public virtual ICollection<Container> Container { get; set; }
    }
}
