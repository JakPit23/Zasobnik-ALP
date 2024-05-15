using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zasobnil
{
    internal class Naboj
    {
        public string serialNumber;
        public string typRaze;

        public Naboj(string raze) {
            typRaze = raze;
            this.serialNumber = Guid.NewGuid().ToString();

        }

        public (string, string) infoNaboj()
        {
            return (this.serialNumber, this.typRaze);
        }
    }
}
