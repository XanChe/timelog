using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelog.Core.ViewModels
{
    public class TotalStatisticsVewModel
    {
        public long ActivityCount { get; set; } = 0;
        public long DurationInSecondsTotal { get; set; } = 0;
        public long DurationInSecondsAvarage { get; set; } = 0;
        public long DurationInSecondsMin { get; set; } = 0;
        public long DurationInSecondsMax { get; set; } = 0;
        public DateTime FirstActivity { get; set; } = DateTime.MinValue;
        public DateTime LastActivity { get; set; } = DateTime.MinValue;
    }
}
