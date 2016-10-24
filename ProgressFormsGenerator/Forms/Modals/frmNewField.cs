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
        private string lastLabel;
        private string lastName;

        public frmNewField() : this(null) { }

        public frmNewField(Dictionary<string, string> data)
        {
            InitializeComponent();
            
            if (data != null)
            {
                txtLabel.Text = data["label"];
                txtName.Text = data["name"];
                cboFieldType.Text = data["fieldType"];

                txtLabel_TextChanged(null, null);
            }
        }

        /// <summary>
        /// Closes the dialog and sets the result to be Ok.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            label = txtLabel.Text;
            name = txtName.Text;
            type = FieldTypeUtil.Parse(cboFieldType.SelectedItem.ToString());
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Closes the dialog and doesn't save anything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Called when the field type changes. This is used to change the text in the name/label to be
        /// <hr> whenever separator is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbo = (ComboBox)sender;

            txtName.Enabled = txtLabel.Enabled = cbo.SelectedItem.ToString().ToLower() != "separator";
            if (!txtName.Enabled)
            {
                txtLabel.Text = "<hr>";
                txtName.Text = "<hr>";
            }
            else
            {
                txtLabel.Text = lastLabel;
                txtName.Text = lastName;
            }
        }

        /// <summary>
        /// This event is called when either the label or name of the field changes. This is so we
        /// can save the text before when changing to different field types (makes it easier to use
        /// :)).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            if (cboFieldType.SelectedItem.ToString() != "Separator")
            {
                if (sender == txtLabel)
                {
                    lastLabel = txtLabel.Text;
                }
                else
                {
                    lastName = txtName.Text;
                }
            }

        }
    }
}
