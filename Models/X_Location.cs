using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ChemStoreWebApp.Models
{
    public partial class X_Location
    {
        [Key]
        public int LocationID { get; set; } //similar to RoomID (this says smallint on database, but we're likely going to refactor with guid/uuid)
        public short ParentID { get; set; } //similar to RoomNumber, BuildingName
        public byte Level { get; set; } //similar to RoomNumber, BuildingName
        public string Name { get; set; } //similar to RoomNumber, BuildingName

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; } //this says tinyInt on database, left as int for now
        public Department Department { get; init; } 

        [ForeignKey(nameof(User))]
        public int SupervisorID { get; set; } //this says mediumint?, left as int for now
        public User User { get; init; }

        public bool IsHidden { get; set; }

    }
}