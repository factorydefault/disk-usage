﻿namespace disk_usage_ui
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.usageBar = new System.Windows.Forms.ProgressBar();
            this.tileContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeItemButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tileAddPathButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderButton = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tileContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(64, 64);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(64, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoEllipsis = true;
            this.nameLabel.Location = new System.Drawing.Point(70, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(150, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Admin";
            // 
            // detailLabel
            // 
            this.detailLabel.AutoSize = true;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.detailLabel.Location = new System.Drawing.Point(71, 41);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(118, 13);
            this.detailLabel.TabIndex = 2;
            this.detailLabel.Text = "25.8 GB free of 599 GB";
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
            this.tileAddPathButton,
            this.removeItemButton});
            this.tileContext.Name = "tileContext";
            this.tileContext.Size = new System.Drawing.Size(171, 101);
            // 
            // removeItemButton
            // 
            this.removeItemButton.Name = "removeItemButton";
            this.removeItemButton.Size = new System.Drawing.Size(170, 24);
            this.removeItemButton.Text = "Remove";
            this.removeItemButton.Click += new System.EventHandler(this.removeItemButton_Click);
            // 
            // tileAddPathButton
            // 
            this.tileAddPathButton.Name = "tileAddPathButton";
            this.tileAddPathButton.Size = new System.Drawing.Size(170, 24);
            this.tileAddPathButton.Text = "Add New Path";
            this.tileAddPathButton.Click += new System.EventHandler(this.tileAddPathButton_Click);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Enabled = false;
            this.openFolderButton.Font = new System.Drawing.Font("Segoe UI", 9.163636F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(170, 24);
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.Visible = false;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
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
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DiskTile";
            this.Size = new System.Drawing.Size(237, 64);
            this.DoubleClick += new System.EventHandler(this.DiskTile_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tileContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label detailLabel;
        private System.Windows.Forms.ProgressBar usageBar;
        private System.Windows.Forms.ContextMenuStrip tileContext;
        private System.Windows.Forms.ToolStripMenuItem removeItemButton;
        private System.Windows.Forms.ToolStripMenuItem tileAddPathButton;
        private System.Windows.Forms.ToolStripMenuItem openFolderButton;
    }
}