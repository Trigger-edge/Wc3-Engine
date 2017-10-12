using System.Collections.Generic;

namespace Wc3Engine
{
    public class CompilerTypes
    {
        public static List<string> List = new List<string>
        {
            /*
             Jass basic types
            */

            "boolean",
            "integer",
            "real",
            "string",

            /*
             Jass common handles
            */

            "destructable",
            "effect",
            "item",
            "player",
            "unit",
            
            /*
             Wc3 Engine types
            */
            
            "damage",
            "damagebonus",
            "vector2",
            "vertor3",
        };
    }
}
