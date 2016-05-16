using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class HtmlDocument
    {
        private HtmlElement rootHtml;

        public HtmlDocument()
        {
            rootHtml = new HtmlElement("html");
        }
    }
}
