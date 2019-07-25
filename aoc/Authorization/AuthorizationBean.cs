using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc.Authorization
{
    public class AuthorizationBean
    {
        public string Username { get; set; }
        public string Controller { get; set; }
        public List<string> PermittedMethods { get; set; }
    }
}
