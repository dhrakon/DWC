namespace DWC
{
    partial class Folio
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newKillTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveKillTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadKillTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusKillTeam = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabWelcome = new System.Windows.Forms.TabPage();
            this.txtWelcome = new System.Windows.Forms.TextBox();
            this.tabPageAgent = new System.Windows.Forms.TabPage();
            this.cboChapters = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtAgentName = new System.Windows.Forms.TextBox();
            this.txtChapter = new System.Windows.Forms.Label();
            this.lblXP = new System.Windows.Forms.Label();
            this.lblAgentName = new System.Windows.Forms.Label();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.tabSubInfo = new System.Windows.Forms.TabControl();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.btnGenerateChapters = new System.Windows.Forms.Button();
            this.flowLayoutAgents = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRank = new System.Windows.Forms.Label();
            this.grdCharacteristics = new System.Windows.Forms.DataGridView();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabWelcome.SuspendLayout();
            this.tabPageAgent.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tabSubInfo.SuspendLayout();
            this.tabDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCharacteristics)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(687, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveKillTeamToolStripMenuItem,
            this.loadKillTeamToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newKillTeamToolStripMenuItem,
            this.newAgentToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // newKillTeamToolStripMenuItem
            // 
            this.newKillTeamToolStripMenuItem.Name = "newKillTeamToolStripMenuItem";
            this.newKillTeamToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.newKillTeamToolStripMenuItem.Text = "Kill Team";
            this.newKillTeamToolStripMenuItem.Click += new System.EventHandler(this.newKillTeamToolStripMenuItem_Click);
            // 
            // newAgentToolStripMenuItem
            // 
            this.newAgentToolStripMenuItem.Enabled = false;
            this.newAgentToolStripMenuItem.Name = "newAgentToolStripMenuItem";
            this.newAgentToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.newAgentToolStripMenuItem.Text = "Agent";
            this.newAgentToolStripMenuItem.Click += new System.EventHandler(this.newAgentToolStripMenuItem_Click);
            // 
            // saveKillTeamToolStripMenuItem
            // 
            this.saveKillTeamToolStripMenuItem.Name = "saveKillTeamToolStripMenuItem";
            this.saveKillTeamToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveKillTeamToolStripMenuItem.Text = "Save Kill Team";
            this.saveKillTeamToolStripMenuItem.Click += new System.EventHandler(this.saveKillTeamToolStripMenuItem_Click);
            // 
            // loadKillTeamToolStripMenuItem
            // 
            this.loadKillTeamToolStripMenuItem.Name = "loadKillTeamToolStripMenuItem";
            this.loadKillTeamToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loadKillTeamToolStripMenuItem.Text = "Load Kill Team";
            this.loadKillTeamToolStripMenuItem.Click += new System.EventHandler(this.loadKillTeamToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusKillTeam});
            this.statusStrip.Location = new System.Drawing.Point(0, 534);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(687, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusKillTeam
            // 
            this.toolStripStatusKillTeam.Name = "toolStripStatusKillTeam";
            this.toolStripStatusKillTeam.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabWelcome);
            this.tabControl.Controls.Add(this.tabPageAgent);
            this.tabControl.Controls.Add(this.tabPageInfo);
            this.tabControl.Controls.Add(this.tabDebug);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(663, 469);
            this.tabControl.TabIndex = 2;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabWelcome
            // 
            this.tabWelcome.Controls.Add(this.txtWelcome);
            this.tabWelcome.Location = new System.Drawing.Point(4, 22);
            this.tabWelcome.Name = "tabWelcome";
            this.tabWelcome.Size = new System.Drawing.Size(655, 443);
            this.tabWelcome.TabIndex = 2;
            this.tabWelcome.Text = "Welcome";
            this.tabWelcome.UseVisualStyleBackColor = true;
            // 
            // txtWelcome
            // 
            this.txtWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWelcome.Location = new System.Drawing.Point(3, 3);
            this.txtWelcome.Multiline = true;
            this.txtWelcome.Name = "txtWelcome";
            this.txtWelcome.Size = new System.Drawing.Size(649, 437);
            this.txtWelcome.TabIndex = 0;
            // 
            // tabPageAgent
            // 
            this.tabPageAgent.Controls.Add(this.grdCharacteristics);
            this.tabPageAgent.Controls.Add(this.lblRank);
            this.tabPageAgent.Controls.Add(this.cboChapters);
            this.tabPageAgent.Controls.Add(this.textBox1);
            this.tabPageAgent.Controls.Add(this.txtAgentName);
            this.tabPageAgent.Controls.Add(this.txtChapter);
            this.tabPageAgent.Controls.Add(this.lblXP);
            this.tabPageAgent.Controls.Add(this.lblAgentName);
            this.tabPageAgent.Location = new System.Drawing.Point(4, 22);
            this.tabPageAgent.Name = "tabPageAgent";
            this.tabPageAgent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAgent.Size = new System.Drawing.Size(655, 443);
            this.tabPageAgent.TabIndex = 0;
            this.tabPageAgent.Text = "Agent";
            this.tabPageAgent.UseVisualStyleBackColor = true;
            // 
            // cboChapters
            // 
            this.cboChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChapters.FormattingEnabled = true;
            this.cboChapters.Location = new System.Drawing.Point(114, 58);
            this.cboChapters.Name = "cboChapters";
            this.cboChapters.Size = new System.Drawing.Size(121, 21);
            this.cboChapters.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 2;
            // 
            // txtAgentName
            // 
            this.txtAgentName.Location = new System.Drawing.Point(114, 6);
            this.txtAgentName.Name = "txtAgentName";
            this.txtAgentName.Size = new System.Drawing.Size(536, 20);
            this.txtAgentName.TabIndex = 1;
            this.txtAgentName.TextChanged += new System.EventHandler(this.txtAgentName_TextChanged);
            // 
            // txtChapter
            // 
            this.txtChapter.AutoSize = true;
            this.txtChapter.Location = new System.Drawing.Point(6, 61);
            this.txtChapter.Name = "txtChapter";
            this.txtChapter.Size = new System.Drawing.Size(44, 13);
            this.txtChapter.TabIndex = 0;
            this.txtChapter.Text = "Chapter";
            // 
            // lblXP
            // 
            this.lblXP.AutoSize = true;
            this.lblXP.Location = new System.Drawing.Point(6, 35);
            this.lblXP.Name = "lblXP";
            this.lblXP.Size = new System.Drawing.Size(21, 13);
            this.lblXP.TabIndex = 0;
            this.lblXP.Text = "XP";
            // 
            // lblAgentName
            // 
            this.lblAgentName.AutoSize = true;
            this.lblAgentName.Location = new System.Drawing.Point(6, 9);
            this.lblAgentName.Name = "lblAgentName";
            this.lblAgentName.Size = new System.Drawing.Size(66, 13);
            this.lblAgentName.TabIndex = 0;
            this.lblAgentName.Text = "Agent Name";
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.tabSubInfo);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(655, 443);
            this.tabPageInfo.TabIndex = 1;
            this.tabPageInfo.Text = "Detailed Information";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tabSubInfo
            // 
            this.tabSubInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSubInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSubInfo.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabSubInfo.Controls.Add(this.tabPageDetails);
            this.tabSubInfo.Controls.Add(this.tabPage2);
            this.tabSubInfo.Location = new System.Drawing.Point(6, 6);
            this.tabSubInfo.Name = "tabSubInfo";
            this.tabSubInfo.SelectedIndex = 0;
            this.tabSubInfo.Size = new System.Drawing.Size(643, 431);
            this.tabSubInfo.TabIndex = 0;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Location = new System.Drawing.Point(4, 4);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(635, 0);
            this.tabPageDetails.TabIndex = 0;
            this.tabPageDetails.Text = "Astartes Profile";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(635, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.btnGenerateChapters);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Size = new System.Drawing.Size(655, 443);
            this.tabDebug.TabIndex = 3;
            this.tabDebug.Text = "DEBUG";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // btnGenerateChapters
            // 
            this.btnGenerateChapters.Location = new System.Drawing.Point(3, 3);
            this.btnGenerateChapters.Name = "btnGenerateChapters";
            this.btnGenerateChapters.Size = new System.Drawing.Size(145, 23);
            this.btnGenerateChapters.TabIndex = 0;
            this.btnGenerateChapters.Text = "Generate JSON";
            this.btnGenerateChapters.UseVisualStyleBackColor = true;
            this.btnGenerateChapters.Click += new System.EventHandler(this.btnGenerateJSON_Click);
            // 
            // flowLayoutAgents
            // 
            this.flowLayoutAgents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutAgents.Location = new System.Drawing.Point(12, 502);
            this.flowLayoutAgents.Name = "flowLayoutAgents";
            this.flowLayoutAgents.Size = new System.Drawing.Size(663, 29);
            this.flowLayoutAgents.TabIndex = 3;
            this.flowLayoutAgents.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutAgents_ControlAdded);
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Location = new System.Drawing.Point(241, 35);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(49, 13);
            this.lblRank.TabIndex = 4;
            this.lblRank.Text = "[lblRank]";
            // 
            // grdCharacteristics
            // 
            this.grdCharacteristics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCharacteristics.Location = new System.Drawing.Point(471, 33);
            this.grdCharacteristics.Name = "grdCharacteristics";
            this.grdCharacteristics.Size = new System.Drawing.Size(178, 150);
            this.grdCharacteristics.TabIndex = 5;
            // 
            // Folio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 556);
            this.Controls.Add(this.flowLayoutAgents);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Folio";
            this.Text = "Folio";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabWelcome.ResumeLayout(false);
            this.tabWelcome.PerformLayout();
            this.tabPageAgent.ResumeLayout(false);
            this.tabPageAgent.PerformLayout();
            this.tabPageInfo.ResumeLayout(false);
            this.tabSubInfo.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCharacteristics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAgent;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.TabControl tabSubInfo;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newKillTeamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAgentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadKillTeamToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutAgents;
        private System.Windows.Forms.TextBox txtAgentName;
        private System.Windows.Forms.Label lblAgentName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusKillTeam;
        private System.Windows.Forms.TabPage tabWelcome;
        private System.Windows.Forms.TextBox txtWelcome;
        private System.Windows.Forms.ToolStripMenuItem saveKillTeamToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblXP;
        private System.Windows.Forms.ComboBox cboChapters;
        private System.Windows.Forms.Label txtChapter;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.Button btnGenerateChapters;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.DataGridView grdCharacteristics;
    }
}