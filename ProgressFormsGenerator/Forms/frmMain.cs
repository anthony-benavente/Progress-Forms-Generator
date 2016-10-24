using ProgressFormsGenerator.Forms.Modals;
using ProgressFormsGenerator.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace ProgressFormsGenerator
{
    public partial class FormMain : System.Windows.Forms.Form
    {

        private BindingList<ProgressFormsTab> tabs;

        private ProgressFormsTab selectedTab;

        // This is what gets exported when the export button is pressed
        private HtmlDocument mainDoc;


        public FormMain()
        {
            InitializeComponent();
            mainDoc = new HtmlDocument("form");
            mainDoc.RootElement.AddAttribute("id", "formWrapper");
            tabs = new BindingList<ProgressFormsTab>();
            tabs.AllowEdit = true;
        }
        

        private void FormMain_Load(object sender, EventArgs e)
        {
            lstTabs.DisplayMember = "Label";
            lstTabs.DataSource = tabs;
            lstTabs.SelectedIndexChanged += LstTabs_SelectedIndexChanged;

            lstFields.DisplayMember = "Label";
            lstFields.DataSource = null;
            lstFields.ControlRemoved += LstTabs_SelectedIndexChanged;
        }

        private void LstTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTabs.SelectedItem != null)
            {
                HandleProgressFormsTabChanged(lstTabs.SelectedItem, e);
            }

            if (tabs.Count == 0)
            {
                lstFields.DataSource = null;
            }
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
                    Index = tabs.Count, 
                    Label = newTabDialog.Result
                };
                newTab.DocumentChanged += HandleProgressFormsTabChanged;
                tabs.Add(newTab);

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
            btnPreviewFinal.Enabled = true;
            btnExport.Enabled = true;
            btnRemoveTab.Enabled = true;
        }

        private void HandleProgressFormsTabChanged(object sender, EventArgs e)
        {
            var tabSender = (ProgressFormsTab)sender;
            if (tabSender == selectedTab)   
            {
                lstFields.DataSource = tabSender.Fields;

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
            }
        }

        private void tabBuilder_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            bool editMode = e is FieldEventArgs;
            frmNewField newField = new frmNewField(editMode ? ((FieldEventArgs) e).ToDict() : null);
            FormField field = new FormField();
            if (newField.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (editMode) field = (FormField)lstFields.SelectedItem;

                field.FieldType = newField.type;
                if (field.FieldType != FieldType.Separator)
                {
                    field.Label = newField.label;
                    field.ControlName = newField.name;
                }

                if (!editMode) selectedTab.AddField(field);
            }
            HandleProgressFormsTabChanged(selectedTab, null);
        }

        private void btnRemoveTab_Click(object sender, EventArgs e)
        {
            var selected = (ProgressFormsTab)lstTabs.SelectedItem;
            var index = tabs.IndexOf(selected);

            tabs.RemoveAt(index);
            if (tabs.Count == 0)
            {
                btnRemoveTab.Enabled = false;
                btnAddField.Enabled = false;
                btnRemoveField.Enabled = false;
                btnPreviewFinal.Enabled = false;
                btnExport.Enabled = false;

                htmlEditor.AllHTML = "";
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            HtmlDocument result = new HtmlDocument();
            foreach (ProgressFormsTab tab in tabs)
            {
                result.RootElement.Append(tab.Document.RootElement);
            }
            ExportedHtmlForm form = new ExportedHtmlForm(result);
            form.ShowDialog();
        }

        /// <summary>
        /// This event is called when the list of tabs is double clicked. If there is a selected
        /// tab, we open up a dialog to change its name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTabs_DoubleClick(object sender, EventArgs e)
        {
            var tab = (ProgressFormsTab)lstTabs.SelectedItem;

            if (tab != null)
            {
                EditTabForm form = new EditTabForm(tab);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var tabIndex = tabs.IndexOf(tab);
                    tabs[tabIndex].Label = form.NewTabLabel;
                    tabs.ResetItem(tabIndex);
                    HandleProgressFormsTabChanged(tab, null);
                }
            }
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            var selectedTab = (ProgressFormsTab)lstTabs.SelectedItem;
            var selected = (FormField)lstFields.SelectedItem;
            var index = selectedTab.Fields.IndexOf(selected);

            selectedTab.RemoveField(index);
            LstTabs_SelectedIndexChanged(selectedTab, null);
        }

        /// <summary>
        /// This event is used to check when a field has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            var field = (FormField)lstFields.SelectedItem;

            if (field != null)
            {
                btnAddField_Click(null, new FieldEventArgs(field.Label,
                    field.ControlName,
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(field.FieldType.ToString())));
            }
        }
    }
}
