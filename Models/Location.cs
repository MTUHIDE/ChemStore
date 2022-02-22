using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Buildings
{
    [Display(Name = "A. E. Seaman Mineral Museum")]
    Museum,
    [Display(Name = "Academic Office Building")]
    AOB,
    [Display(Name = "Administration Building")]
    AdminBuilding,
    [Display(Name = "Advanced Technology Development Complex")]
    ATDC,
    [Display(Name = "Alumni House")]
    AlumniHouse,
    [Display(Name = "Central Heating Plant")]
    CentralHeatingPlant,
    [Display(Name = "Chemical Sciences and Engineering Building")]
    ChemSci,
    [Display(Name = "Daniell Heights Apartments")]
    DaniellHeights,
    [Display(Name = "Douglass Houghton Hall")]
    DHH,
    [Display(Name = "Dow Environmental Sciences and Engineering Building")]
    Dow,
    [Display(Name = "East McNair Hall")]
    EastMcNair,
    [Display(Name = "Electrical Engineering Resources Center")]
    EERC,
    [Display(Name = "Fisher Hall")]
    Fisher,
    [Display(Name = "Ford Center")]
    FordCenter,
    [Display(Name = "Gates Tennis Center")]
    Gates,
    [Display(Name = "Great Lakes Research Center")]
    GLRC,
    [Display(Name = "Grover C Dillman Hall")]
    Dillman,
    [Display(Name = "Hamar House")]
    Hamar,
    [Display(Name = "Harold Meese Center")]
    Meese,
    [Display(Name = "Hillside Place")]
    Hillside,
    [Display(Name = "J. R. Van Pelt and John and Ruanne Opie Library")]
    Library,
    [Display(Name = "Kanwal and Ann Rekhi Hall")]
    Rekhi,
    [Display(Name = "Keweenaw Research Center")]
    KRC,
    [Display(Name = "Lakeshore Center")]
    LakeshoreCenter,
    [Display(Name = "Lakeside Labratory")]
    LakesideLabratory,
    [Display(Name = "Little Huskies Child Development Center")]
    LittleHuskies,
    [Display(Name = "Memorial Union Building")]
    MUB,
    [Display(Name = "Minerals and Materials Engineering Building")]
    MandM,
    [Display(Name = "R. L. Smith Building")]
    MEEM,
    [Display(Name = "ROTC Building")]
    ROTC,
    [Display(Name = "Rozsa Center for the Performing Arts")]
    Rozsa,
    [Display(Name = "Sherman Field Press Box")]
    ShermanFieldPressBox,
    [Display(Name = "Storage-Service")]
    Facilities,
    [Display(Name = "Student Development Complex")]
    SDC,
    [Display(Name = "U. J. Noblet Forestry Building")]
    Forestry,
    [Display(Name = "Wadsworth Hall")]
    Wads,
    [Display(Name = "Walker Arts and Humanities Center")]
    Walker,
    [Display(Name = "West McNair Hall")]
    WestMcNair,
    [Display(Name = "Widmaier House")]
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

        public string RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int BuildingName { get; set; }

        public virtual ICollection<Container> Container { get; set; }
    }
}
