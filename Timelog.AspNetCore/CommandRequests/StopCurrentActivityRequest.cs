using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelog.AspNetCore.CommandRequests
{
    public class StopCurrentActivityRequest
    {
        public string Comment { get; set; } = string.Empty;
    }
}
