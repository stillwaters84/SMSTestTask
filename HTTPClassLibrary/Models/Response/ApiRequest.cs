using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClassLib.Models.Response
{
    public class ApiRequest
    {
        public string Command { get; set; }
        
        public Dictionary<string, bool> CommandParameters { get; set; }

    }
}
