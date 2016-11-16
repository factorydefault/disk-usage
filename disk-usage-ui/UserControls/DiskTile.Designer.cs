namespace disk_usage_ui
{
    partial class DiskTile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiskTile));
            this.nameLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.usageBar = new System.Windows.Forms.ProgressBar();
            this.tileContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderButton = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboardButton = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tileAddPathButton = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.notifyMI = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesButton = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationPicture = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tileContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.notificationPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoEllipsis = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(61, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(186, 18);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "12345678901234567890123";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.nameLabel.MouseEnter += new System.EventHandler(this.nameLabel_MouseEnter);
            this.nameLabel.MouseLeave += new System.EventHandler(this.nameLabel_MouseLeave);
            // 
            // detailLabel
            // 
            this.detailLabel.AutoSize = true;
            this.detailLabel.Font = new System.Drawing.Font("Segoe UI", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.detailLabel.Location = new System.Drawing.Point(60, 43);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(64, 17);
            this.detailLabel.TabIndex = 2;
            this.detailLabel.Text = "Loading...";
            // 
            // usageBar
            // 
            this.usageBar.Location = new System.Drawing.Point(62, 27);
            this.usageBar.Name = "usageBar";
            this.usageBar.Size = new System.Drawing.Size(200, 16);
            this.usageBar.TabIndex = 3;
            // 
            // tileContext
            // 
            this.tileContext.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tileContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderButton,
            this.clipboardButton,
            this.shortcutButton,
            this.toolStripSeparator2,
            this.tileAddPathButton,
            this.removeItemButton,
            this.toolStripSeparator1,
            this.notifyMI,
            this.propertiesButton});
            this.tileContext.Name = "tileContext";
            this.tileContext.Size = new System.Drawing.Size(224, 184);
            this.tileContext.Opening += new System.ComponentModel.CancelEventHandler(this.tileContext_Opening);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolderButton.Image = global::disk_usage_ui.Properties.Resources.OpenFolder_16x;
            this.openFolderButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(223, 24);
            this.openFolderButton.Text = "&Open";
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // clipboardButton
            // 
            this.clipboardButton.Name = "clipboardButton";
            this.clipboardButton.Size = new System.Drawing.Size(223, 24);
            this.clipboardButton.Text = "&Copy path to clipboard";
            this.clipboardButton.Click += new System.EventHandler(this.clipboardButton_Click);
            // 
            // shortcutButton
            // 
            this.shortcutButton.Name = "shortcutButton";
            this.shortcutButton.Size = new System.Drawing.Size(223, 24);
            this.shortcutButton.Text = "Create &shortcut";
            this.shortcutButton.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // tileAddPathButton
            // 
            this.tileAddPathButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.tileAddPathButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileAddPathButton.Name = "tileAddPathButton";
            this.tileAddPathButton.Size = new System.Drawing.Size(223, 24);
            this.tileAddPathButton.Text = "&Add new path";
            this.tileAddPathButton.Click += new System.EventHandler(this.tileAddPathButton_Click);
            // 
            // removeItemButton
            // 
            this.removeItemButton.Image = global::disk_usage_ui.Properties.Resources.action_Cancel_16xLG;
            this.removeItemButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.removeItemButton.Name = "removeItemButton";
            this.removeItemButton.Size = new System.Drawing.Size(223, 24);
            this.removeItemButton.Text = "&Remove";
            this.removeItemButton.Click += new System.EventHandler(this.removeItemButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // notifyMI
            // 
            this.notifyMI.Name = "notifyMI";
            this.notifyMI.Size = new System.Drawing.Size(223, 24);
            this.notifyMI.Text = "Notifications";
            this.notifyMI.Click += new System.EventHandler(this.notifyMI_Click);
            // 
            // propertiesButton
            // 
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(223, 24);
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // notificationPicture
            // 
            this.notificationPicture.BackColor = System.Drawing.Color.Transparent;
            this.notificationPicture.Enabled = false;
            this.notificationPicture.Image = ((System.Drawing.Image)(resources.GetObject("notificationPicture.Image")));
            this.notificationPicture.Location = new System.Drawing.Point(244, 46);
            this.notificationPicture.Name = "notificationPicture";
            this.notificationPicture.Size = new System.Drawing.Size(16, 16);
            this.notificationPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.notificationPicture.TabIndex = 4;
            this.notificationPicture.TabStop = false;
            this.notificationPicture.Click += new System.EventHandler(this.notificationPicture_Click);
            this.notificationPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notificationPicture_MouseDown);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::disk_usage_ui.Properties.Resources.networkdrive7;
            this.pictureBox.Location = new System.Drawing.Point(7, 7);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.MaximumSize = new System.Drawing.Size(64, 64);
            this.pictureBox.MinimumSize = new System.Drawing.Size(48, 48);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(50, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // DiskTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.tileContext;
            this.Controls.Add(this.notificationPicture);
            this.Controls.Add(this.usageBar);
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(270, 64);
            this.MinimumSize = new System.Drawing.Size(270, 64);
            this.Name = "DiskTile";
            this.Size = new System.Drawing.Size(270, 64);
            this.DoubleClick += new System.EventHandler(this.DiskTile_DoubleClick);
            this.tileContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.notificationPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label detailLabel;
        private System.Windows.Forms.ProgressBar usageBar;
        private System.Windows.Forms.ContextMenuStrip tileContext;
        private System.Windows.Forms.ToolStripMenuItem removeItemButton;
        private System.Windows.Forms.ToolStripMenuItem tileAddPathButton;
        private System.Windows.Forms.ToolStripMenuItem openFolderButton;
        private System.Windows.Forms.ToolStripMenuItem clipboardButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem propertiesButton;
        private System.Windows.Forms.ToolStripMenuItem shortcutButton;
        private System.Windows.Forms.PictureBox notificationPicture;
        private System.Windows.Forms.ToolStripMenuItem notifyMI;
    }
}
