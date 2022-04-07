﻿using System.ComponentModel.DataAnnotations;

namespace Timelog.Core.ViewModels
{
    public class ProjectStatisticsViewModel: TotalStatisticsVewModel
    {
        [Display(Name = "Проект")]
        public string ProjectName { get; set; } = String.Empty;
        public long ProjectId { get; set; }
    }
}
