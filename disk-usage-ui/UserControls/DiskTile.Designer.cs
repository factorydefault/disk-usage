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
            this.nameLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.usageBar = new System.Windows.Forms.ProgressBar();
            this.tileContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tileAddPathButton = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.clipboardButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tileContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoEllipsis = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(70, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(150, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Admin";
            // 
            // detailLabel
            // 
            this.detailLabel.AutoSize = true;
            this.detailLabel.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.detailLabel.Location = new System.Drawing.Point(71, 41);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(58, 13);
            this.detailLabel.TabIndex = 2;
            this.detailLabel.Text = "Loading...";
            // 
            // usageBar
            // 
            this.usageBar.Location = new System.Drawing.Point(70, 20);
            this.usageBar.Name = "usageBar";
            this.usageBar.Size = new System.Drawing.Size(150, 16);
            this.usageBar.TabIndex = 3;
            // 
            // tileContext
            // 
            this.tileContext.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tileContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderButton,
            this.clipboardButton,
            this.tileAddPathButton,
            this.removeItemButton});
            this.tileContext.Name = "tileContext";
            this.tileContext.Size = new System.Drawing.Size(227, 100);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolderButton.Image = global::disk_usage_ui.Properties.Resources.OpenFolder_16x;
            this.openFolderButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(226, 24);
            this.openFolderButton.Text = "&Open Folder";
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // tileAddPathButton
            // 
            this.tileAddPathButton.Image = global::disk_usage_ui.Properties.Resources.action_add_16xLG;
            this.tileAddPathButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileAddPathButton.Name = "tileAddPathButton";
            this.tileAddPathButton.Size = new System.Drawing.Size(226, 24);
            this.tileAddPathButton.Text = "&Add New Path";
            this.tileAddPathButton.Click += new System.EventHandler(this.tileAddPathButton_Click);
            // 
            // removeItemButton
            // 
            this.removeItemButton.Image = global::disk_usage_ui.Properties.Resources.action_Cancel_16xLG;
            this.removeItemButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.removeItemButton.Name = "removeItemButton";
            this.removeItemButton.Size = new System.Drawing.Size(226, 24);
            this.removeItemButton.Text = "&Remove";
            this.removeItemButton.Click += new System.EventHandler(this.removeItemButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::disk_usage_ui.Properties.Resources.networkdrive7;
            this.pictureBox.Location = new System.Drawing.Point(13, 2);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.MaximumSize = new System.Drawing.Size(64, 64);
            this.pictureBox.MinimumSize = new System.Drawing.Size(48, 48);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(50, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // clipboardButton
            // 
            this.clipboardButton.Name = "clipboardButton";
            this.clipboardButton.Size = new System.Drawing.Size(226, 24);
            this.clipboardButton.Text = "&Copy Path to Clipboard";
            this.clipboardButton.Click += new System.EventHandler(this.clipboardButton_Click);
            // 
            // DiskTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.tileContext;
            this.Controls.Add(this.usageBar);
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DiskTile";
            this.Size = new System.Drawing.Size(237, 64);
            this.DoubleClick += new System.EventHandler(this.DiskTile_DoubleClick);
            this.tileContext.ResumeLayout(false);
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
    }
}
