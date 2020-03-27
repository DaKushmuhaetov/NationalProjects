using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.Model
{
    public sealed class Counter
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
