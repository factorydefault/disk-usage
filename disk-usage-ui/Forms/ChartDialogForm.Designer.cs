namespace disk_usage_ui.Forms
{
    partial class ChartDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartDialogForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortingCombo = new System.Windows.Forms.ToolStripComboBox();
            this.displayModeCombo = new System.Windows.Forms.ToolStripComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.seriesMI = new System.Windows.Forms.ToolStripMenuItem();
            this.diskChart = new disk_usage_ui.UserControls.DiskChart();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sortingCombo,
            this.displayModeCombo,
            this.seriesMI});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(532, 29);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 25);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Image = global::disk_usage_ui.Properties.Resources.resource_16xLG;
            this.saveImageToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.saveImageToolStripMenuItem.Text = "&Save Image...";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.exitToolStripMenuItem.Text = "&Close";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sortingCombo
            // 
            this.sortingCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortingCombo.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.sortingCombo.Font = new System.Drawing.Font("Segoe UI", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sortingCombo.Name = "sortingCombo";
            this.sortingCombo.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sortingCombo.Size = new System.Drawing.Size(220, 25);
            this.sortingCombo.SelectedIndexChanged += new System.EventHandler(this.sortingCombo_SelectedIndexChanged);
            // 
            // displayModeCombo
            // 
            this.displayModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayModeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.displayModeCombo.Font = new System.Drawing.Font("Segoe UI", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayModeCombo.Items.AddRange(new object[] {
            "Capacity",
            "Percentage Fill"});
            this.displayModeCombo.Name = "displayModeCombo";
            this.displayModeCombo.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.displayModeCombo.Size = new System.Drawing.Size(112, 25);
            this.displayModeCombo.SelectedIndexChanged += new System.EventHandler(this.displayModeCombo_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "png";
            this.saveFileDialog.Filter = "PNG Image|*.png";
            this.saveFileDialog.Title = "Save Image";
            // 
            // seriesMI
            // 
            this.seriesMI.Name = "seriesMI";
            this.seriesMI.Size = new System.Drawing.Size(87, 25);
            this.seriesMI.Text = "Show/Hide";
            this.seriesMI.DropDownOpening += new System.EventHandler(this.seriesMI_DropDownOpening);
            // 
            // diskChart
            // 
            this.diskChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskChart.Location = new System.Drawing.Point(0, 29);
            this.diskChart.Name = "diskChart";
            this.diskChart.Size = new System.Drawing.Size(532, 228);
            this.diskChart.TabIndex = 0;
            // 
            // ChartDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 257);
            this.Controls.Add(this.diskChart);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "ChartDialogForm";
            this.Text = "Disk Usage Chart";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.DiskChart diskChart;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox displayModeCombo;
        private System.Windows.Forms.ToolStripComboBox sortingCombo;
        private System.Windows.Forms.ToolStripMenuItem seriesMI;
    }
}