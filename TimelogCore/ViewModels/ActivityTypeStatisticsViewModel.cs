using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Entities;

namespace Timelog.Core.ViewModels
{
    public class ActivityTypeStatisticsViewModel: TotalStatisticsVewModel
    {
        ActivityType ActivityType { get; set; }
       
    }
}
