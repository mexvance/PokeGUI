using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGUI.Models
{
    public class PokeType
    {
        public string TypeName { get;}
        public PokeType(string typeName)
        {
            this.TypeName = typeName;
        }
    }
}
