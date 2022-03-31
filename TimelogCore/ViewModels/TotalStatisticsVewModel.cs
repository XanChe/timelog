using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelog.Core.ViewModels
{
    public class TotalStatisticsVewModel
    {
        [Display(Name = "Количство подходов")]
        public long ActivityCount { get; set; } = 0;
        [Display(Name = "Суммарная длительность")]
        public long DurationInSecondsTotal { get; set; } = 0;
        [Display(Name = "Средняя длительность")]
        public long DurationInSecondsAvarage { get; set; } = 0;
        [Display(Name = "Минимальная длительность")]
        public long DurationInSecondsMin { get; set; } = 0;
        [Display(Name = "Максимальная длительность")]
        public long DurationInSecondsMax { get; set; } = 0;
        [Display(Name = "Самый ранний старт")]
        public DateTime FirstActivity { get; set; } = DateTime.MinValue;
        [Display(Name = "Самый поздний финиш")]
        public DateTime LastActivity { get; set; } = DateTime.MinValue;
    }
}
