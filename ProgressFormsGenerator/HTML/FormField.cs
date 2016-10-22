using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class FormField
    {
        private HtmlElement fieldGroup;
        private HtmlElement labelElement;
        private HtmlElement inputElement;

        private string _label;
        private FieldType _fieldType;
        private string _name;

        public string Label
        {
            get { return _label; }
            set { _label = value; labelElement.InnerHTML = value; }
        }
        
        public FieldType FieldType
        {
            get { return _fieldType; }
            set
            {
                _fieldType = value;
                switch (_fieldType)
                {
                    case FieldType.Input:
                    default:
                        fieldGroup.Children[1] = new HtmlElement("input");
                        break;
                    case FieldType.Separator:
                        fieldGroup.Remove(x => x.TagName == "label");
                        fieldGroup.Children[0] = new HtmlElement("hr", true);
                        break;
                }
            }
        }

        public string ControlName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                var input = fieldGroup.Children[1];

                if (input.Attributes.ContainsKey("name"))
                {
                    input.Attributes["name"] = _name;
                    input.Attributes["id"] = _name;
                }
                else
                {
                    input.AddAttribute("name", _name);
                    input.AddAttribute("id", _name);
                }
            }
        }

        public FormField() : this("", "", FieldType.Input)
        {
        }

        public FormField(string label, string controlName, FieldType type)
        {
            fieldGroup = new HtmlElement("div");
            fieldGroup.AddClass("fieldgroup");
            labelElement = new HtmlElement("label");
            inputElement = new HtmlElement("input");

            fieldGroup.AddChildren(labelElement, inputElement);

            Label = label;
            FieldType = type;
            ControlName = controlName;


        }

        public override string ToString()
        {
            return Label + ": " + FieldType;
        }

        public HtmlElement GetHtmlElement()
        {
            return fieldGroup;
        }
    }
}
