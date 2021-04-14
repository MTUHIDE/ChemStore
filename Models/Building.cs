using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Buildings
{
    [Display(Name = "Administration Building")]
    AdminBuilding,
    [Display(Name = "Lakeshore Center")]
    LakeshoreCenter,
    [Display(Name = "ROTC Building")]
    ROTC,
    [Display(Name = "Academic Office Building")]
    AOB,
    [Display(Name = "Electrical Engineering Resources Center")]
    EERC,
    [Display(Name = "Dow Environmental Sciences and Engineering Building")]
    Dow,
    [Display(Name = "Alumni House")]
    AlumniHouse,
    [Display(Name = "Rozsa Center for the Performing Arts")]
    Rozsa,
    [Display(Name = "Walker Arts and Humanities Center")]
    Walker,
    [Display(Name = "Minerals and Materials Engineering Building")]
    MandM,
    [Display(Name = "Hamar House")]
    Hamar,
    [Display(Name = "Grover C Dillman Hall")]
    Dillman,
    [Display(Name = "Fisher Hall")]
    Fisher,
    [Display(Name = "Widmaier House")]
    WidmaierHouse,
    [Display(Name = "J. R. Van Pelt and John and Ruanne Opie Library")]
    Library,
    [Display(Name = "U. J. Noblet Forestry Building")]
    Forestry,
    [Display(Name = "Chemical Sciences and Engineering Building")]
    ChemSci,
    [Display(Name = "R. L. Smith Building")]
    MEEM,
    [Display(Name = "Student Development Complex")]
    SDC,
    [Display(Name = "Sherman Field Press Box")]
    ShermanFieldPressBox,
    [Display(Name = "Ford Center")]
    FordCenter,
    [Display(Name = "Kanwal and Ann Rekhi Hall")]
    Rekhi,
    [Display(Name = "Little Huskies Child Development Center")]
    LittleHuskies,
    [Display(Name = "Douglass Houghton Hall")]
    DHH,
    [Display(Name = "Daniell Heights Apartments")]
    DaniellHeights,
    [Display(Name = "Memorial Union Building")]
    MUB,
    [Display(Name = "Wadsworth Hall")]
    Wads,
    [Display(Name = "West McNair Hall")]
    WestMcNair,
    [Display(Name = "East McNair Hall")]
    EastMcNair,
    [Display(Name = "Central Heating Plant")]
    CentralHeatingPlant,
    [Display(Name = "Lakeside Labratory")]
    LakesideLabratory,
    [Display(Name = "Storage-Service")]
    Facilities,
    [Display(Name = "Hillside Place")]
    Hillside,
    [Display(Name = "Gates Tennis Center")]
    Gates,
    [Display(Name = "Keweenaw Research Center")]
    KRC,
    [Display(Name = "Harold Meese Center")]
    Meese,
    [Display(Name = "Advanced Technology Development Complex")]
    ATDC,
    [Display(Name = "A. E. Seaman Mineral Museum")]
    Museum,
    [Display(Name = "Great Lakes Research Center")]
    GLRC
}

namespace ChemStoreWebApp.Models
{
    public partial class Building
    {
        public int BuildingId { get; set; }
        public Buildings BuildingName { get; set; }
    }
}
