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

        public int Index { get; set; }
        public string Label { get; set; }
        public HtmlDocument Document { get { return document; } set { document = value; } }

        public ProgressFormsTab()
        {
            document = new HtmlDocument("div");
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
    }
}
