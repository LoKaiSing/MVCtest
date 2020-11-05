using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCtest.Models
{
	[MetadataType(typeof(DepartmentMetadata))]
	public partial class Department
    {
        public class DepartmentMetadata
        {
            [Required]
            public int DepartmentID { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public decimal Budget { get; set; }
            [Required]
            public Nullable<System.DateTime> StartDate { get; set; }
            [Required]
            public Nullable<int> InstructorID { get; set; }
            public byte[] RowVersion { get; set; }
        }



    }
}