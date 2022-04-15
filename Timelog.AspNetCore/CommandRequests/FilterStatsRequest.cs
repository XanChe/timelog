using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Timelog.AspNetCore.CommandRequests
{
    public class FilterStatsRequest
    {
        [BindProperty, DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
    }
}
