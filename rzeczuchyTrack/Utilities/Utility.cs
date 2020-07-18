using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.Utilities
{
    static class Utility
    {
        public static int Clamp(int number, int max, int min)
        {
            return (number < min) ? min : (number > max) ? max : number;
        }
    }
}
