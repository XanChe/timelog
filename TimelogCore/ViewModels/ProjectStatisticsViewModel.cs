using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Entities;

namespace Timelog.Core.ViewModels
{
    public class ProjectStatisticsViewModel: TotalStatisticsVewModel
    {
        [Display(Name = "Проект")]
        public string ProjectName { get; set; }
        public long ProjectId { get; set; }
    }
}
