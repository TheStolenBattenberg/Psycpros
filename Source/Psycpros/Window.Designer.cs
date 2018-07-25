namespace Psycpros
{
    partial class Psycpros
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.psycprosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ArchiveTool = new System.Windows.Forms.TabPage();
            this.ToolBar = new System.Windows.Forms.TabControl();
            this.ImageTool = new System.Windows.Forms.TabPage();
            this.glControl1 = new OpenTK.GLControl();
            this.ImageList = new System.Windows.Forms.ListView();
            this.ArchiveList = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ArchiveTool.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.ImageTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.psycprosToolStripMenuItem,
            this.archiveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // psycprosToolStripMenuItem
            // 
            this.psycprosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.psycprosToolStripMenuItem.Name = "psycprosToolStripMenuItem";
            this.psycprosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.psycprosToolStripMenuItem.Text = "Psycpros";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tArchiveToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // tArchiveToolStripMenuItem
            // 
            this.tArchiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.extractToolStripMenuItem});
            this.tArchiveToolStripMenuItem.Name = "tArchiveToolStripMenuItem";
            this.tArchiveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.tArchiveToolStripMenuItem.Text = "T Archive";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.extractToolStripMenuItem.Text = "Extract";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel1.Text = "FPS:";
            // 
            // ArchiveTool
            // 
            this.ArchiveTool.Controls.Add(this.ArchiveList);
            this.ArchiveTool.Location = new System.Drawing.Point(4, 22);
            this.ArchiveTool.Name = "ArchiveTool";
            this.ArchiveTool.Padding = new System.Windows.Forms.Padding(3);
            this.ArchiveTool.Size = new System.Drawing.Size(852, 389);
            this.ArchiveTool.TabIndex = 0;
            this.ArchiveTool.Text = "Archive";
            this.ArchiveTool.UseVisualStyleBackColor = true;
            // 
            // ToolBar
            // 
            this.ToolBar.Controls.Add(this.ArchiveTool);
            this.ToolBar.Controls.Add(this.ImageTool);
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolBar.Location = new System.Drawing.Point(0, 24);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.SelectedIndex = 0;
            this.ToolBar.Size = new System.Drawing.Size(860, 415);
            this.ToolBar.TabIndex = 2;
            // 
            // ImageTool
            // 
            this.ImageTool.Controls.Add(this.glControl1);
            this.ImageTool.Controls.Add(this.ImageList);
            this.ImageTool.Location = new System.Drawing.Point(4, 22);
            this.ImageTool.Name = "ImageTool";
            this.ImageTool.Padding = new System.Windows.Forms.Padding(3);
            this.ImageTool.Size = new System.Drawing.Size(852, 389);
            this.ImageTool.TabIndex = 1;
            this.ImageTool.Text = "Image";
            this.ImageTool.UseVisualStyleBackColor = true;
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(220, 7);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(624, 376);
            this.glControl1.TabIndex = 2;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // ImageList
            // 
            this.ImageList.Location = new System.Drawing.Point(6, 7);
            this.ImageList.Name = "ImageList";
            this.ImageList.Size = new System.Drawing.Size(207, 376);
            this.ImageList.TabIndex = 1;
            this.ImageList.UseCompatibleStateImageBehavior = false;
            // 
            // ArchiveList
            // 
            this.ArchiveList.Location = new System.Drawing.Point(6, 7);
            this.ArchiveList.Name = "ArchiveList";
            this.ArchiveList.Size = new System.Drawing.Size(207, 376);
            this.ArchiveList.TabIndex = 2;
            this.ArchiveList.UseCompatibleStateImageBehavior = false;
            // 
            // Psycpros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(860, 461);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Psycpros";
            this.Text = "Psycpros";
            this.Load += new System.EventHandler(this.Psycpros_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ArchiveTool.ResumeLayout(false);
            this.ToolBar.ResumeLayout(false);
            this.ImageTool.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem psycprosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.TabPage ArchiveTool;
        private System.Windows.Forms.TabControl ToolBar;
        private System.Windows.Forms.TabPage ImageTool;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ListView ImageList;
        private System.Windows.Forms.ListView ArchiveList;
    }
}

