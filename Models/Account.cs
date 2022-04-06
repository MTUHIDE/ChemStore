using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Departments
{
    [Display(Name = "Air Force ROTC")]
    AirForceROTC,
    [Display(Name = "Applied Computing")]
    AppliedComputing,
    [Display(Name = "Army ROTC")]
    ArmyROTC,
    [Display(Name = "Biological Sciences")]
    BiologicalSciences,
    [Display(Name = "Biomedical Engineering")]
    BiomedicalEng,
    [Display(Name = "Chemical Engineering")]
    ChemEng,
    Chemistry,
    [Display(Name = "Civil and Environmental Engineering")]
    CivilAndEnviromentalEng,
    [Display(Name = "Cognitive and Learning Sciences")]
    CognitiveAndLearning,
    [Display(Name = "College of Business")]
    Business,
    [Display(Name = "College of Forest Resources and Environmental Science")]
    ForestResourcesAndEnvironmentalScience,
    [Display(Name = "Computer Science")]
    ComputerScience,
    [Display(Name = "Electrical and Computer Engineering")]
    ElectricalAndComputerEng,
    [Display(Name = "Engineering Fundamentals")]
    EngFundamentals,
    [Display(Name = "Geological and Mining Engineering and Sciences")]
    GeoMiningEngAndSciences,
    [Display(Name = "Graduate School")]
    GradSchool,
    Humanities,
    [Display(Name = "Kinesiology and Integrative Physiology")]
    KinesiologyAndIntegrativePhysiology,
    [Display(Name = "Manufacturing and Mechanical Engineering Technology")]
    MandMEngTech,
    [Display(Name = "Materials Science and Engineering")]
    MaterialsScienceEng,
    [Display(Name = "Mathematical Sciences")]
    Math,
    [Display(Name = "Mechanical Engineering-Engineering Mechanics")]
    MEEM,
    [Display(Name = "Pavlis Honors College")]
    Pavlis,
    Physics,
    [Display(Name = "Social Sciences")]
    SocialSciences,
    [Display(Name = "Visual and Performing Arts")]
    VPA,
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
