namespace ProgressFormsGenerator.Forms.Modals
{
    partial class ExportedHtmlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExported = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtExported
            // 
            this.txtExported.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExported.Location = new System.Drawing.Point(0, 0);
            this.txtExported.Multiline = true;
            this.txtExported.Name = "txtExported";
            this.txtExported.ReadOnly = true;
            this.txtExported.Size = new System.Drawing.Size(879, 420);
            this.txtExported.TabIndex = 0;
            // 
            // ExportedHtmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 420);
            this.Controls.Add(this.txtExported);
            this.Name = "ExportedHtmlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExportedHtmlForm";
            this.Load += new System.EventHandler(this.ExportedHtmlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExported;
    }
}