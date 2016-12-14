using disk_usage_ui.UserControls;

namespace disk_usage_ui.Forms
{
    partial class AddPathDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPathDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.notificationsCheck = new System.Windows.Forms.CheckBox();
            this.exampleTile = new DiskTile();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Label (optional):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Path:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(92, 6);
            this.labelTextBox.MaxLength = 128;
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(214, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextChanged += new System.EventHandler(this.DialogTextChanged);
            // 
            // pathTextBox
            // 
            this.pathTextBox.AllowDrop = true;
            this.pathTextBox.Location = new System.Drawing.Point(92, 32);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(214, 20);
            this.pathTextBox.TabIndex = 3;
            this.pathTextBox.TextChanged += new System.EventHandler(this.DialogTextChanged);
            this.pathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropEvent);
            this.pathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEvent);
            this.pathTextBox.DoubleClick += new System.EventHandler(this.pathTextBox_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(4, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(302, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Valid path formats are local disk (C:\\) or UNC (\\\\server\\share\\)";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(169, 195);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Add";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(250, 195);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select Folder";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // mainPanel
            // 
            this.mainPanel.AllowDrop = true;
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.notificationsCheck);
            this.mainPanel.Controls.Add(this.exampleTile);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.labelTextBox);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.pathTextBox);
            this.mainPanel.Location = new System.Drawing.Point(7, 7);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(316, 182);
            this.mainPanel.TabIndex = 9;
            this.mainPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropEvent);
            this.mainPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEvent);
            // 
            // notificationsCheck
            // 
            this.notificationsCheck.AutoSize = true;
            this.notificationsCheck.Location = new System.Drawing.Point(9, 84);
            this.notificationsCheck.Name = "notificationsCheck";
            this.notificationsCheck.Size = new System.Drawing.Size(270, 17);
            this.notificationsCheck.TabIndex = 11;
            this.notificationsCheck.Text = "Include this path in desktop notifications (if enabled)";
            this.notificationsCheck.UseVisualStyleBackColor = true;
            this.notificationsCheck.CheckedChanged += new System.EventHandler(this.notificationsCheck_CheckedChanged);
            // 
            // exampleTile
            // 
            this.exampleTile.BackColor = System.Drawing.Color.White;
            this.exampleTile.Interactive = true;
            this.exampleTile.Location = new System.Drawing.Point(20, 104);
            this.exampleTile.Margin = new System.Windows.Forms.Padding(0);
            this.exampleTile.MaximumSize = new System.Drawing.Size(270, 64);
            this.exampleTile.MinimumSize = new System.Drawing.Size(270, 64);
            this.exampleTile.Name = "exampleTile";
            this.exampleTile.ShowPathOnHover = false;
            this.exampleTile.Size = new System.Drawing.Size(270, 64);
            this.exampleTile.TabIndex = 8;
            // 
            // AddPathDialog
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(330, 224);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPathDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Path";
            this.Load += new System.EventHandler(this.OnLoad);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private DiskTile exampleTile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.CheckBox notificationsCheck;
    }
}