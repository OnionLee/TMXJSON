using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMXJson
{
    public sealed class TMXObject
    {
        public string Name { get; private set; }

        public Dictionary<string, string> Properties { get; private set; }
 
        public TMXObject(string name, Dictionary<string, string> properties)
        {
            Name = name;

            Properties = properties;
        }
    }
}
