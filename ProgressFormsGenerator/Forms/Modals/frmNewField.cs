using ProgressFormsGenerator.HTML;
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
    public partial class frmNewField : Form
    {
        public string label;
        public string name;
        public FieldType type;

        public frmNewField()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            label = txtLabel.Text;
            name = txtName.Text;
            type = FieldTypeUtil.Parse(cboFieldType.SelectedText);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbo = (ComboBox)sender;

            txtName.Enabled = txtLabel.Enabled = cbo.SelectedText.ToLower() == "separator";
            if (!txtName.Enabled)
            {
                txtLabel.Text = "<hr>";
                txtName.Text = "<hr>";
            }
        }
    }
}
