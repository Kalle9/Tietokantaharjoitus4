using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Taulun pääavain määrittelyä varten
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Varastonhallinta
{
    public class Tuote
    {
        [Key]
        public int Id { get; set; }
        public string? Tuotenimi { get; set; }
        public int Tuotteenhinta { get; set; }
        public int Varastosaldo { get; set; }
    }
}
