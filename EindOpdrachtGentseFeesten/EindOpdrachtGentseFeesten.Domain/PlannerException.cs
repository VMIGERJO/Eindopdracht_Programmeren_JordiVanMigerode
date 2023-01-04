using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain
{
    public class PlannerException : Exception
    {
        public PlannerException() : base() { }
        public PlannerException(string message) : base(message) { }
        public PlannerException(string message, Exception inner) : base(message, inner) { }

    }
}
