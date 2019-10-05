using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGUI.Services
{
    public class PokeTypeRegistry
    {
        public PokeType None = new PokeType("none");
        public PokeType Normal = new PokeType("normal");
        public PokeType Fighting = new PokeType("fighting");
        public PokeType Flying = new PokeType("flying");
        public PokeType Poison = new PokeType("poison");
        public PokeType Ground = new PokeType("ground");
        public PokeType Rock = new PokeType("rock");
        public PokeType Bug = new PokeType("bug");
        public PokeType Ghost = new PokeType("ghost");
        public PokeType Steel = new PokeType("steel");
        public PokeType Fire = new PokeType("fire");
        public PokeType Water = new PokeType("water");
        public PokeType Grass = new PokeType("grass");
        public PokeType Electric = new PokeType("electric");
        public PokeType Psychic = new PokeType("psychic");
        public PokeType Ice = new PokeType("ice");
        public PokeType Dragon = new PokeType("dragon");
        public PokeType Fairy = new PokeType("fairy");
        public PokeType Unkown = new PokeType("unknown");
        public PokeType Shadow = new PokeType("shadow");
        public PokeType Dark = new PokeType("dark");
       
        public List<PokeType> All()
        {
            var types = new List<PokeType>();
            types.Add(None);
            types.Add(Normal);
            types.Add(Fighting);
            types.Add(Flying);
            types.Add(Poison);
            types.Add(Ground);
            types.Add(Rock );
            types.Add(Bug);
            types.Add(Ghost);
            types.Add(Steel);
            types.Add(Fire );
            types.Add(Water);
            types.Add(Grass);
            types.Add(Electric);
            types.Add(Psychic);
            types.Add(Ice);
            types.Add(Dragon);
            types.Add(Fairy);
            types.Add(Unkown);
            types.Add(Shadow);
            types.Add(Dark );
            return types;
        }
    }
}
