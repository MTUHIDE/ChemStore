using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum Buildings
{
    [Display(Name = "A. E. Seaman Mineral Museum")] // 0
    Museum,
    [Display(Name = "Academic Office Building")] // 1
    AOB,
    [Display(Name = "Administration Building")] // 2
    AdminBuilding, 
    [Display(Name = "Advanced Technology Development Complex")] // 3
    ATDC,
    [Display(Name = "Alumni House")] // 4
    AlumniHouse,
    [Display(Name = "Central Heating Plant")] // 5
    CentralHeatingPlant,
    [Display(Name = "Chemical Sciences and Engineering Building")] // 6
    ChemSci,
    [Display(Name = "Daniell Heights Apartments")] // 7
    DaniellHeights,
    [Display(Name = "Douglass Houghton Hall")] // 8
    DHH,
    [Display(Name = "Dow Environmental Sciences and Engineering Building")] // 9
    Dow,
    [Display(Name = "East McNair Hall")] // 10
    EastMcNair,
    [Display(Name = "Electrical Engineering Resources Center")] // 11
    EERC,
    [Display(Name = "Fisher Hall")] // 12
    Fisher,
    [Display(Name = "Ford Center")] // 13
    FordCenter,
    [Display(Name = "Gates Tennis Center")] // 14
    Gates,
    [Display(Name = "Great Lakes Research Center")] // 15
    GLRC,
    [Display(Name = "Grover C Dillman Hall")] // 16
    Dillman,
    [Display(Name = "Hamar House")] // 17
    Hamar,
    [Display(Name = "Harold Meese Center")] // 18
    Meese,
    [Display(Name = "Hillside Place")] // 19
    Hillside,
    [Display(Name = "J. R. Van Pelt and John and Ruanne Opie Library")] // 20
    Library,
    [Display(Name = "Kanwal and Ann Rekhi Hall")] // 21
    Rekhi,
    [Display(Name = "Keweenaw Research Center")] // 22
    KRC,
    [Display(Name = "Lakeshore Center")] // 23
    LakeshoreCenter,
    [Display(Name = "Lakeside Labratory")] // 24
    LakesideLabratory,
    [Display(Name = "Little Huskies Child Development Center")] // 25
    LittleHuskies,
    [Display(Name = "Memorial Union Building")] // 26
    MUB,
    [Display(Name = "Minerals and Materials Engineering Building")] // 27
    MandM,
    [Display(Name = "R. L. Smith Building")] // 28
    MEEM,
    [Display(Name = "ROTC Building")] // 29
    ROTC,
    [Display(Name = "Rozsa Center for the Performing Arts")] // 30
    Rozsa,
    [Display(Name = "Sherman Field Press Box")] // 31
    ShermanFieldPressBox,
    [Display(Name = "Storage-Service")] // 32
    Facilities,
    [Display(Name = "Student Development Complex")] // 33
    SDC,
    [Display(Name = "U. J. Noblet Forestry Building")] // 34
    Forestry,
    [Display(Name = "Wadsworth Hall")] // 35
    Wads,
    [Display(Name = "Walker Arts and Humanities Center")] // 36
    Walker,
    [Display(Name = "West McNair Hall")] // 37
    WestMcNair,
    [Display(Name = "Widmaier House")] // 38
    WidmaierHouse   
}

namespace ChemStoreWebApp.Models
{
    public partial class Location
    {
        public Location()
        {
            Container = new HashSet<Container>();
        }

        [Key]
        public string RoomId { get; set; }
        [Required]
        [DisplayName("Room Number")]
        public string RoomNumber { get; set; }
        [DisplayName("Building Name")]
        public int BuildingName { get; set; }

        public virtual ICollection<Container> Container { get; set; }
    }
}
