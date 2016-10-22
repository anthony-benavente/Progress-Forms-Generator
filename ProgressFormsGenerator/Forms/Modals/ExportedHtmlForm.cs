using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressFormsGenerator.Forms.Modals
{
    public partial class ExportedHtmlForm : Form
    {
        private HTML.HtmlDocument exported;

        public ExportedHtmlForm(HTML.HtmlDocument doc)
        {
            InitializeComponent();

            this.exported = doc;
        }

        private void ExportedHtmlForm_Load(object sender, EventArgs e)
        {
            txtExported.Text = exported.ToString();

            txtExported.SelectAll();
        }
    }
}
