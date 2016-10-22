using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class ProgressFormsTab
    {
        public event EventHandler DocumentChanged;

        private HtmlDocument document;

        private List<FormField> fields;

        public List<FormField> Fields
        {
            get { return fields;  }
        }

        public int Index { get; set; }
        public string Label { get; set; }
        public HtmlDocument Document { get { return document; } set { document = value; } }

        public ProgressFormsTab()
        {
            document = new HtmlDocument("div");
            fields = new List<FormField>();
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
            document.RootElement.Append(f.GetHtmlElement());
        }

        public void removeField(string name)
        {
            int removed = fields.RemoveAll(x => x.ControlName == name);
            if (removed == 0)
            {
                throw new InvalidOperationException("This control does not exist.");
            }
            else
            {
                document.RootElement.Remove(x => x.GetAttribute("name") == name);
            }
        }
    }
}
