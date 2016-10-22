using ProgressFormsGenerator.Forms.Modals;
using ProgressFormsGenerator.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace ProgressFormsGenerator
{
    public partial class FormMain : System.Windows.Forms.Form
    {

        private List<ProgressFormsTab> tabs;
        private ProgressFormsTab selectedTab;

        // This is what gets exported when the export button is pressed
        private HtmlDocument mainDoc;


        public FormMain()
        {
            InitializeComponent();
            mainDoc = new HtmlDocument("form");
            mainDoc.RootElement.AddAttribute("id", "formWrapper");
            tabs = new List<ProgressFormsTab>();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            FormNewTab newTabDialog = new FormNewTab();
            if (newTabDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var newTab = new ProgressFormsTab() 
                { 
                    Index = lstTabs.Items.Count, 
                    Label = newTabDialog.Result
                };
                newTab.DocumentChanged += HandleProgressFormsTabChanged;
                lstTabs.Items.Add(newTab);

                var fieldset = new HtmlElement("fieldset");
                {
                    var legend = new HtmlElement("legend");
                    legend.InnerHTML = newTabDialog.Result;
                    fieldset.AddChildren(legend);
                }
                // This is our first tab. Prepare the HTML stuff
                selectedTab = newTab;
                newTab.Document.RootElement.AddChildren(fieldset);
                HandleProgressFormsTabChanged(newTab, null);

                lstTabs.SelectedIndex = lstTabs.Items.Count - 1;
            }

            btnAddField.Enabled = true;
            btnRemoveField.Enabled = true;
        }

        private void HandleProgressFormsTabChanged(object sender, EventArgs e)
        {
            var tabSender = (ProgressFormsTab)sender;
            if (tabSender == selectedTab)
            {
                // We have to update the HTML control
                htmlEditor.AllHTML = tabSender.GetHtml();
            }
        }

        private void lstTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTabs.SelectedIndex >= 0)
            {
                selectedTab = (ProgressFormsTab)lstTabs.SelectedItem;
                HandleProgressFormsTabChanged(selectedTab, e);

                lstFields.Items.Clear();
                foreach (FormField f in selectedTab.Fields)
                {
                    lstFields.Items.Add(f);
                }
            }
        }

        private void tabBuilder_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            frmNewField newField = new frmNewField();
            if (newField.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FormField field = new FormField();
                field.FieldType = newField.type;
                if (field.FieldType != FieldType.Separator)
                {
                    field.Label = newField.label;
                    field.ControlName = newField.name;
                }
                selectedTab.AddField(field);
                lstFields.Items.Add(field);
            }
            HandleProgressFormsTabChanged(selectedTab, null);
        }

        private void btnRemoveTab_Click(object sender, EventArgs e)
        {
            if (lstTabs.Items.Count == 0)
            {
                btnAddField.Enabled = false;
                btnRemoveField.Enabled = false;
            }
        }
    }
}
