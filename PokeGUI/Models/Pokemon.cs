using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokeGUI.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int PokeId { get; set; }
        public PokeType Type1 { get; set; }
        public PokeType Type2 { get; set; }
        public string Image { get; set; }
    }
}
