using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Authorization
{
    public class Authorization
    {
        public string Username { get; set; }
        public string App { get; set; }
        public string Controller { get; set; }
        public List<object> PermittedMethods { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
    }
}
