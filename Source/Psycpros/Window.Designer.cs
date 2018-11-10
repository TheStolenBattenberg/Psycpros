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
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sonyPlaystationTMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ArchiveTool = new System.Windows.Forms.TabPage();
            this.ArchiveList = new System.Windows.Forms.ListView();
            this.ToolBar = new System.Windows.Forms.TabControl();
            this.ModelTool = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.ImageList = new System.Windows.Forms.ListBox();
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ArchiveTool.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.ModelTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.psycprosToolStripMenuItem,
            this.archiveToolStripMenuItem,
            this.modelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // psycprosToolStripMenuItem
            // 
            this.psycprosToolStripMenuItem.Name = "psycprosToolStripMenuItem";
            this.psycprosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.psycprosToolStripMenuItem.Text = "Psycpros";
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
            this.extractToolStripMenuItem});
            this.tArchiveToolStripMenuItem.Name = "tArchiveToolStripMenuItem";
            this.tArchiveToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.tArchiveToolStripMenuItem.Text = "From Software Archive (*.T)";
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.extractToolStripMenuItem.Text = "Extract";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sonyPlaystationTMDToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // sonyPlaystationTMDToolStripMenuItem
            // 
            this.sonyPlaystationTMDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem});
            this.sonyPlaystationTMDToolStripMenuItem.Name = "sonyPlaystationTMDToolStripMenuItem";
            this.sonyPlaystationTMDToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.sonyPlaystationTMDToolStripMenuItem.Text = "Sony Playstation Model (*.TMD)";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
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
            // ArchiveList
            // 
            this.ArchiveList.Location = new System.Drawing.Point(6, 7);
            this.ArchiveList.Name = "ArchiveList";
            this.ArchiveList.Size = new System.Drawing.Size(207, 376);
            this.ArchiveList.TabIndex = 2;
            this.ArchiveList.UseCompatibleStateImageBehavior = false;
            // 
            // ToolBar
            // 
            this.ToolBar.Controls.Add(this.ArchiveTool);
            this.ToolBar.Controls.Add(this.ModelTool);
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolBar.Location = new System.Drawing.Point(0, 24);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.SelectedIndex = 0;
            this.ToolBar.Size = new System.Drawing.Size(860, 415);
            this.ToolBar.TabIndex = 2;
            // 
            // ModelTool
            // 
            this.ModelTool.Controls.Add(this.button1);
            this.ModelTool.Controls.Add(this.ImageList);
            this.ModelTool.Controls.Add(this.glControl1);
            this.ModelTool.Location = new System.Drawing.Point(4, 22);
            this.ModelTool.Name = "ModelTool";
            this.ModelTool.Padding = new System.Windows.Forms.Padding(3);
            this.ModelTool.Size = new System.Drawing.Size(852, 389);
            this.ModelTool.TabIndex = 1;
            this.ModelTool.Text = "Model";
            this.ModelTool.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(3, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ImageList
            // 
            this.ImageList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ImageList.FormattingEnabled = true;
            this.ImageList.Location = new System.Drawing.Point(3, 3);
            this.ImageList.Name = "ImageList";
            this.ImageList.Size = new System.Drawing.Size(201, 277);
            this.ImageList.TabIndex = 3;
            this.ImageList.SelectedIndexChanged += new System.EventHandler(this.ImageList_SelectedIndexChanged);
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.AutoSize = true;
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.glControl1.Location = new System.Drawing.Point(210, 3);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(639, 383);
            this.glControl1.TabIndex = 2;
            this.glControl1.VSync = true;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ArchiveTool.ResumeLayout(false);
            this.ToolBar.ResumeLayout(false);
            this.ModelTool.ResumeLayout(false);
            this.ModelTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem psycprosToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.TabPage ArchiveTool;
        private System.Windows.Forms.TabControl ToolBar;
        private System.Windows.Forms.TabPage ModelTool;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ListView ArchiveList;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sonyPlaystationTMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ListBox ImageList;
        private System.Windows.Forms.Button button1;
    }
}

