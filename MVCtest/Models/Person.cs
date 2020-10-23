using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCtest.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Person_Name_Required", ErrorMessageResourceType =typeof(Resource1))]
        public string Name { get; set; }
        [Required]
        [Range(18,99,ErrorMessageResourceName = "Person_Age_Required",ErrorMessageResourceType =typeof(Resource1))]
        public int Age { get; set; }
    }
}