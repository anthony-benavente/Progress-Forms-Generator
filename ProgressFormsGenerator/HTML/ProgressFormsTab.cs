using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class ProgressFormsTab
    {
        public event EventHandler DocumentChanged;

        private HtmlDocument document;

        private BindingList<FormField> fields;

        private string _label;

        public BindingList<FormField> Fields
        {
            get { return fields;  }
        }

        public int Index { get; set; }
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;

                var legend = Document.RootElement.Find(x => x.TagName == "legend");
                if (legend != null)
                {
                    legend.InnerHTML = _label;
                }
            }
        }
        public HtmlDocument Document { get { return document; } set { document = value; } }

        public ProgressFormsTab()
        {
            document = new HtmlDocument("div");
            fields = new BindingList<FormField>();
            document.DocumentChanged += (s, e) => DocumentChanged?.Invoke(this, e);
        }

        public override string ToString()
        {
            return Index + ": " + Label;
        }

        public string GetHtml()
        {
            return document.RootElement.ToString();
        }

        public void AddField(FormField f)
        {
            fields.Add(f);
            document.RootElement.Children[0].Append(f.GetHtmlElement());
        }

        public void removeField(string name)
        {
            var field = fields.FirstOrDefault(x => x.Id == name);
            if (field == null)
            {
                throw new InvalidOperationException("This control does not exist.");
            }
            else
            {
                fields.Remove(field);
                document.RootElement.Children[0].Remove(x => x.GetAttribute("id") == name);
            }
        }

        public void RemoveField(int index)
        {
            var field = Fields[index];
            if (field != null)
            {
                removeField(field.ControlName);
            }
        }
    }
}
