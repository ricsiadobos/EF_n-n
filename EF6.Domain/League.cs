using System;
using System.Collections.Generic;

namespace EF6.Domain
{
    public class League : BaseDomainObject
    {

        public string Name { get; set; }

        public List<Team> Teams { get; set; }
    }
}
