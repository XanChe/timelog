using System;
using System.ComponentModel.DataAnnotations;

namespace Timelog.Entities
{
  
    public class Project : Entity
    {
        [Display(Name = "��������")]
        public string Name { get; set; }
        [Display(Name = "��������")]
        public string Description {get; set; }  

    }

}  

