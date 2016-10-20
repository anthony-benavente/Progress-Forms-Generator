using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public enum FieldType
    {
        Input,
        Checkbox,
        Select,
        Textarea,
        Radio,
        Separator
    }

    public class FieldTypeUtil
    {
        public static FieldType Parse(string val)
        {
            switch (val.ToLower())
            {
                case "input":
                default:
                    return FieldType.Input;
                case "separator":
                    return FieldType.Separator;
            }
        }
    }
}
