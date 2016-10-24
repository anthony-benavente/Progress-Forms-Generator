using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator
{
    public class FieldEventArgs : EventArgs
    {
        private string label;
        private string name;
        private string type;

        public FieldEventArgs(string label, string name, string type)
        {
            this.label = label;
            this.name = name;
            this.type = type;
        }

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>()
            {
                { "label", label },
                { "name", name },
                { "fieldType", type }
            };
        }
    }
}
