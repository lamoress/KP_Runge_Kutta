using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP
{
    public class ExampleSystem
    {
        public string[] Equations { get; set; }
        public double[] InitialConditions { get; set; }
        public double T0 { get; set; }
        public double T1 { get; set; }
        public double H { get; set; }
    }
}
