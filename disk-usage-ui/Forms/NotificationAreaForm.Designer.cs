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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.diskStack = new System.Windows.Forms.FlowLayoutPanel();
            this.emptySpaceContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.emptySpaceAddPathButton = new System.Windows.Forms.ToolStripMenuItem();
            this.diskTile5 = new disk_usage_ui.DiskTile();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.orderByCombo = new System.Windows.Forms.ComboBox();
            this.taskbarContext.SuspendLayout();
            this.diskStack.SuspendLayout();
            this.emptySpaceContext.SuspendLayout();
            this.tableLayout.SuspendLayout();
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
            this.toolStripSeparator2,
            this.exitButton});
            this.taskbarContext.Name = "taskbarMenu";
            this.taskbarContext.Size = new System.Drawing.Size(285, 130);
            this.taskbarContext.Opening += new System.ComponentModel.CancelEventHandler(this.taskbarContext_Opening);
            // 
            // openButton
            // 
            this.openButton.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openButton.Image = global::disk_usage_ui.Properties.Resources.Bubble_16xLG;
            this.openButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(284, 24);
            this.openButton.Text = "&Open Disk Usage";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // addPathTaskbarButton
            // 
            this.addPathTaskbarButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.addPathTaskbarButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addPathTaskbarButton.Name = "addPathTaskbarButton";
            this.addPathTaskbarButton.Size = new System.Drawing.Size(284, 24);
            this.addPathTaskbarButton.Text = "Add New Path";
            this.addPathTaskbarButton.Click += new System.EventHandler(this.AddNewPath);
            // 
            // editJsonButton
            // 
            this.editJsonButton.Name = "editJsonButton";
            this.editJsonButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.editJsonButton.Size = new System.Drawing.Size(284, 24);
            this.editJsonButton.Text = "Edit JSON Path List";
            this.editJsonButton.Visible = false;
            this.editJsonButton.Click += new System.EventHandler(this.editJsonButton_Click);
            // 
            // viewChartButton
            // 
            this.viewChartButton.Image = global::disk_usage_ui.Properties.Resources.KPI_16xLG;
            this.viewChartButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewChartButton.Name = "viewChartButton";
            this.viewChartButton.Size = new System.Drawing.Size(284, 24);
            this.viewChartButton.Text = "View Chart";
            this.viewChartButton.Click += new System.EventHandler(this.viewChartButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(281, 6);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(284, 24);
            this.exitButton.Text = "E&xit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // diskStack
            // 
            this.diskStack.AutoScroll = true;
            this.diskStack.BackColor = System.Drawing.Color.White;
            this.diskStack.ContextMenuStrip = this.emptySpaceContext;
            this.diskStack.Controls.Add(this.diskTile5);
            this.diskStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskStack.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.diskStack.Location = new System.Drawing.Point(0, 0);
            this.diskStack.Margin = new System.Windows.Forms.Padding(0);
            this.diskStack.Name = "diskStack";
            this.diskStack.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
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
            this.emptySpaceContext.Size = new System.Drawing.Size(171, 53);
            // 
            // emptySpaceAddPathButton
            // 
            this.emptySpaceAddPathButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.emptySpaceAddPathButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.emptySpaceAddPathButton.Name = "emptySpaceAddPathButton";
            this.emptySpaceAddPathButton.Size = new System.Drawing.Size(170, 24);
            this.emptySpaceAddPathButton.Text = "Add New Path";
            this.emptySpaceAddPathButton.Click += new System.EventHandler(this.AddNewPath);
            // 
            // diskTile5
            // 
            this.diskTile5.AutoSize = true;
            this.diskTile5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diskTile5.BackColor = System.Drawing.Color.White;
            this.diskTile5.Location = new System.Drawing.Point(3, 3);
            this.diskTile5.Margin = new System.Windows.Forms.Padding(0);
            this.diskTile5.Name = "diskTile5";
            this.diskTile5.Size = new System.Drawing.Size(223, 64);
            this.diskTile5.TabIndex = 5;
            this.diskTile5.RemoveRequested += new System.EventHandler<System.EventArgs>(this.RemovePathUsingTileObject);
            // 
            // tableLayout
            // 
            this.tableLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.diskStack, 0, 0);
            this.tableLayout.Controls.Add(this.orderByCombo, 0, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayout.Size = new System.Drawing.Size(257, 497);
            this.tableLayout.TabIndex = 4;
            // 
            // orderByCombo
            // 
            this.orderByCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.orderByCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderByCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orderByCombo.FormattingEnabled = true;
            this.orderByCombo.Items.AddRange(new object[] {
            "Sort by Name (A-Z)",
            "Sort by Name (Z-A)",
            "Sort by Free Space",
            "Sort by Free Space (Descending)",
            "Sort by Percentage Fill",
            "Sort by Percentage Fill (Descending)",
            "Sort by Volume Size",
            "Sort by Volume Size (Descending)"});
            this.orderByCombo.Location = new System.Drawing.Point(3, 468);
            this.orderByCombo.Name = "orderByCombo";
            this.orderByCombo.Size = new System.Drawing.Size(251, 21);
            this.orderByCombo.TabIndex = 4;
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(275, 515);
            this.MinimumSize = new System.Drawing.Size(275, 515);
            this.Name = "NotificationAreaForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FormDeactivate);
            this.Resize += new System.EventHandler(this.NotificationAreaForm_Resize);
            this.taskbarContext.ResumeLayout(false);
            this.diskStack.ResumeLayout(false);
            this.diskStack.PerformLayout();
            this.emptySpaceContext.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
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
    }
}

