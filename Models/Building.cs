using System;
using System.Collections.Generic;

public enum Buildings
{
    AdminBuilding,
    LakeshoreCenter,
    ROTC,
    AOB,
    EERC,
    Dow,
    AlumniHouse,
    Rozsa,
    Walker,
    MandM,
    Hamar,
    Dillman,
    Fisher,
    WidmaierHouse,
    Library,
    Forestry,
    ChemSci,
    MEEM,
    SDC,
    ShermanFieldPressBox,
    FordCenter,
    Rekhi,
    LittleHuskies,
    DHH,
    DaniellHeights,
    MUB,
    Wads,
    WestMcNair,
    EastMcNair,
    CentralHeatingPlant,
    LakesideLabratory,
    Facilities,
    Hillside,
    Gates,
    KRC,
    Meese,
    ATDC,
    Museum,
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
