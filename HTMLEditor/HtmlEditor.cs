using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLEditor
{
    public partial class HtmlEditor : UserControl
    {
        public string AllHTML
        {
            get
            {
                return (string)webMain.Document.InvokeScript("getHtmlOfEditor");
            }
            set
            {
                webMain.Document.InvokeScript("setHtmlOfEditor", new object[] { Clean(value) });
            }
        }

        public HtmlEditor()
        {
            InitializeComponent();
            webMain.Navigate(@"file:///C:\Users\Anthony\Documents\Visual Studio 2013\Projects\ProgressFormsGenerator\HTMLEditor\bin\Debug\editor_html\editor_cke.html");
        }

        private string Clean(string value)
        {
            // Removes new lines (10 = LF, 13 = CR)
            return value == null ? "" : value.Replace("'", "\'").Replace((char)10, ' ').Replace((char)13, ' ');
        }

        private void webMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
    }
}
