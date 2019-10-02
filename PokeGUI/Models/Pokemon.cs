using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGUI.Models
{
    public class Pokemon
    {
        public string Name { get; }
        public int Number { get; }
        public PokeType Type1 { get; }
        public PokeType Type2 { get; }
        public Pokemon(string name, int number, PokeType type1, PokeType type2 = null)
        {
            Name = name;
            Number = number;
            Type1 = type1;
            Type2 = type2;
        }

    }
}
