using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class JsonObject
    {
        public List<string> Exclude { get; set; }
        public List<OperatorItem> Operator { get; set; }
    }
}
