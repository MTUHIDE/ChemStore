using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum Buildings
{
    [Display(Name = "Administration Building")]
    AdminBuilding = 0,
    [Display(Name = "Lakeshore Center")]
    LakeshoreCenter = 1,
    [Display(Name = "ROTC Building")]
    ROTC = 2,
    [Display(Name = "Academic Office Building")]
    AOB = 3,
    [Display(Name = "Electrical Engineering Resources Center")]
    EERC = 4,
    [Display(Name = "Dow Environmental Sciences and Engineering Building")]
    Dow = 5,
    [Display(Name = "Alumni House")]
    AlumniHouse = 5,
    [Display(Name = "Rozsa Center for the Performing Arts")]
    Rozsa = 6,
    [Display(Name = "Walker Arts and Humanities Center")]
    Walker = 7,
    [Display(Name = "Minerals and Materials Engineering Building")]
    MandM = 8,
    [Display(Name = "Hamar House")]
    Hamar = 9,
    [Display(Name = "Grover C Dillman Hall")]
    Dillman = 10,
    [Display(Name = "Fisher Hall")]
    Fisher = 11,
    [Display(Name = "Widmaier House")]
    WidmaierHouse = 12,
    [Display(Name = "J. R. Van Pelt and John and Ruanne Opie Library")]
    Library = 13,
    [Display(Name = "U. J. Noblet Forestry Building")]
    Forestry = 14,
    [Display(Name = "Chemical Sciences and Engineering Building")]
    ChemSci = 15,
    [Display(Name = "R. L. Smith Building")]
    MEEM = 16,
    [Display(Name = "Student Development Complex")]
    SDC = 17,
    [Display(Name = "Sherman Field Press Box")]
    ShermanFieldPressBox = 18,
    [Display(Name = "Ford Center")]
    FordCenter = 19,
    [Display(Name = "Kanwal and Ann Rekhi Hall")]
    Rekhi = 20,
    [Display(Name = "Little Huskies Child Development Center")]
    LittleHuskies = 21,
    [Display(Name = "Douglass Houghton Hall")]
    DHH = 22,
    [Display(Name = "Daniell Heights Apartments")]
    DaniellHeights = 23,
    [Display(Name = "Memorial Union Building")]
    MUB = 24,
    [Display(Name = "Wadsworth Hall")]
    Wads = 25,
    [Display(Name = "West McNair Hall")]
    WestMcNair = 26,
    [Display(Name = "East McNair Hall")]
    EastMcNair = 27,
    [Display(Name = "Central Heating Plant")]
    CentralHeatingPlant = 28,
    [Display(Name = "Lakeside Labratory")]
    LakesideLabratory = 29,
    [Display(Name = "Storage-Service")]
    Facilities = 30,
    [Display(Name = "Hillside Place")]
    Hillside = 31,
    [Display(Name = "Gates Tennis Center")]
    Gates = 32,
    [Display(Name = "Keweenaw Research Center")]
    KRC = 33,
    [Display(Name = "Harold Meese Center")]
    Meese = 34,
    [Display(Name = "Advanced Technology Development Complex")]
    ATDC = 35,
    [Display(Name = "A. E. Seaman Mineral Museum")]
    Museum = 36,
    [Display(Name = "Great Lakes Research Center")]
    GLRC = 37
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
