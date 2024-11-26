using System.ComponentModel.DataAnnotations;

namespace ChemStoreWebApp.Models.Enums
{
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
}
