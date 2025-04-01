using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClassLib.Models
{
    public class Dish
    {
        public string Id { get; set; }

        public string Article { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public bool IsWeighted { get; set; }

        public string FullPath { get; set; }

        public List<string> Barcodes { get; set; }
    }
}
