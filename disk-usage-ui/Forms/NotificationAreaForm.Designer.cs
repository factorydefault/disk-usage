namespace disk_usage_ui
{
    partial class NotificationAreaForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationAreaForm));
            this.taskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbarContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openButton = new System.Windows.Forms.ToolStripMenuItem();
            this.addPathTaskbarButton = new System.Windows.Forms.ToolStripMenuItem();
            this.editJsonButton = new System.Windows.Forms.ToolStripMenuItem();
            this.viewChartButton = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.diskStack = new System.Windows.Forms.FlowLayoutPanel();
            this.emptySpaceContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.emptySpaceAddPathButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chartButton = new System.Windows.Forms.Button();
            this.orderByCombo = new System.Windows.Forms.ComboBox();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.taskbarContext.SuspendLayout();
            this.emptySpaceContext.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskbarIcon
            // 
            this.taskbarIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.taskbarIcon.BalloonTipText = "Click the taskbar icon to see disk usage information";
            this.taskbarIcon.BalloonTipTitle = "Disk Usage";
            this.taskbarIcon.ContextMenuStrip = this.taskbarContext;
            this.taskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIcon.Icon")));
            this.taskbarIcon.Text = "Disk Usage";
            this.taskbarIcon.Visible = true;
            this.taskbarIcon.Click += new System.EventHandler(this.taskbarIcon_Click);
            // 
            // taskbarContext
            // 
            this.taskbarContext.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.taskbarContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.addPathTaskbarButton,
            this.editJsonButton,
            this.viewChartButton,
            this.aboutButton,
            this.toolStripSeparator2,
            this.exitButton});
            this.taskbarContext.Name = "taskbarMenu";
            this.taskbarContext.Size = new System.Drawing.Size(281, 154);
            this.taskbarContext.Opening += new System.ComponentModel.CancelEventHandler(this.taskbarContext_Opening);
            // 
            // openButton
            // 
            this.openButton.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(280, 24);
            this.openButton.Text = "&Open Disk Usage";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // addPathTaskbarButton
            // 
            this.addPathTaskbarButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.addPathTaskbarButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addPathTaskbarButton.Name = "addPathTaskbarButton";
            this.addPathTaskbarButton.Size = new System.Drawing.Size(280, 24);
            this.addPathTaskbarButton.Text = "Add new path";
            this.addPathTaskbarButton.Click += new System.EventHandler(this.AddNewPath);
            // 
            // editJsonButton
            // 
            this.editJsonButton.Name = "editJsonButton";
            this.editJsonButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.editJsonButton.Size = new System.Drawing.Size(280, 24);
            this.editJsonButton.Text = "Edit JSON path list";
            this.editJsonButton.Visible = false;
            this.editJsonButton.Click += new System.EventHandler(this.editJsonButton_Click);
            // 
            // viewChartButton
            // 
            this.viewChartButton.Image = global::disk_usage_ui.Properties.Resources.KPI_16xLG;
            this.viewChartButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewChartButton.Name = "viewChartButton";
            this.viewChartButton.Size = new System.Drawing.Size(280, 24);
            this.viewChartButton.Text = "View bar chart";
            this.viewChartButton.Click += new System.EventHandler(this.viewChartButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(280, 24);
            this.aboutButton.Text = "About";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(280, 24);
            this.exitButton.Text = "E&xit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // diskStack
            // 
            this.diskStack.AutoScroll = true;
            this.diskStack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diskStack.BackColor = System.Drawing.Color.White;
            this.diskStack.ContextMenuStrip = this.emptySpaceContext;
            this.diskStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskStack.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.diskStack.Location = new System.Drawing.Point(0, 0);
            this.diskStack.Margin = new System.Windows.Forms.Padding(0);
            this.diskStack.MinimumSize = new System.Drawing.Size(257, 70);
            this.diskStack.Name = "diskStack";
            this.diskStack.Padding = new System.Windows.Forms.Padding(3);
            this.diskStack.Size = new System.Drawing.Size(257, 465);
            this.diskStack.TabIndex = 3;
            this.diskStack.WrapContents = false;
            // 
            // emptySpaceContext
            // 
            this.emptySpaceContext.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.emptySpaceContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptySpaceAddPathButton});
            this.emptySpaceContext.Name = "contextMenuStrip1";
            this.emptySpaceContext.Size = new System.Drawing.Size(169, 28);
            // 
            // emptySpaceAddPathButton
            // 
            this.emptySpaceAddPathButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.emptySpaceAddPathButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.emptySpaceAddPathButton.Name = "emptySpaceAddPathButton";
            this.emptySpaceAddPathButton.Size = new System.Drawing.Size(168, 24);
            this.emptySpaceAddPathButton.Text = "Add new path";
            this.emptySpaceAddPathButton.Click += new System.EventHandler(this.AddNewPath);
            // 
            // tableLayout
            // 
            this.tableLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.Controls.Add(this.diskStack, 0, 0);
            this.tableLayout.Controls.Add(this.panel1, 0, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayout.MinimumSize = new System.Drawing.Size(257, 96);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayout.Size = new System.Drawing.Size(257, 497);
            this.tableLayout.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chartButton);
            this.panel1.Controls.Add(this.orderByCombo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 465);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(257, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 32);
            this.panel1.TabIndex = 5;
            // 
            // chartButton
            // 
            this.chartButton.Image = global::disk_usage_ui.Properties.Resources.KPI_16xLG;
            this.chartButton.Location = new System.Drawing.Point(215, 4);
            this.chartButton.Margin = new System.Windows.Forms.Padding(0);
            this.chartButton.Name = "chartButton";
            this.chartButton.Size = new System.Drawing.Size(39, 23);
            this.chartButton.TabIndex = 5;
            this.toolTipProvider.SetToolTip(this.chartButton, "View list as a Chart");
            this.chartButton.UseVisualStyleBackColor = true;
            this.chartButton.Click += new System.EventHandler(this.chartButton_Click);
            // 
            // orderByCombo
            // 
            this.orderByCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.orderByCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orderByCombo.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderByCombo.FormattingEnabled = true;
            this.orderByCombo.Items.AddRange(new object[] {
            "Sort by Name (A-Z)"});
            this.orderByCombo.Location = new System.Drawing.Point(3, 5);
            this.orderByCombo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.orderByCombo.Name = "orderByCombo";
            this.orderByCombo.Size = new System.Drawing.Size(209, 21);
            this.orderByCombo.TabIndex = 4;
            this.toolTipProvider.SetToolTip(this.orderByCombo, "Select Sorting Method");
            this.orderByCombo.SelectedIndexChanged += new System.EventHandler(this.orderByCombo_SelectedIndexChanged);
            // 
            // NotificationAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(257, 497);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(275, 515);
            this.MinimumSize = new System.Drawing.Size(275, 96);
            this.Name = "NotificationAreaForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FormDeactivate);
            this.Resize += new System.EventHandler(this.NotificationAreaForm_Resize);
            this.taskbarContext.ResumeLayout(false);
            this.emptySpaceContext.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon taskbarIcon;
        private System.Windows.Forms.ContextMenuStrip taskbarContext;
        private System.Windows.Forms.ToolStripMenuItem exitButton;
        private System.Windows.Forms.ToolStripMenuItem openButton;
        private System.Windows.Forms.FlowLayoutPanel diskStack;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private DiskTile diskTile5;
        private System.Windows.Forms.ContextMenuStrip emptySpaceContext;
        private System.Windows.Forms.ToolStripMenuItem emptySpaceAddPathButton;
        private System.Windows.Forms.ToolStripMenuItem addPathTaskbarButton;
        private System.Windows.Forms.ToolStripMenuItem editJsonButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox orderByCombo;
        private System.Windows.Forms.ToolStripMenuItem viewChartButton;
        private System.Windows.Forms.ToolStripMenuItem aboutButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button chartButton;
        private System.Windows.Forms.ToolTip toolTipProvider;
    }
}

