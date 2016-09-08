namespace SmartManagerWindows
{
    partial class Home
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
            this.tabReport = new System.Windows.Forms.TabControl();
            this.tabPageBurn = new System.Windows.Forms.TabPage();
            this.btnGetBurn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBurnSummary = new System.Windows.Forms.Label();
            this.treeViewBurnDetails = new System.Windows.Forms.TreeView();
            this.tabPageBugs = new System.Windows.Forms.TabPage();
            this.treeViewBugProgress = new System.Windows.Forms.TreeView();
            this.lblBugProgressSummary = new System.Windows.Forms.Label();
            this.lblTextBugProgress = new System.Windows.Forms.Label();
            this.btnBugProgress = new System.Windows.Forms.Button();
            this.tabPageBugUpdateIssue = new System.Windows.Forms.TabPage();
            this.lblBugUpdatesIssueText = new System.Windows.Forms.Label();
            this.btnBugUpdateIssues = new System.Windows.Forms.Button();
            this.lstBugFixIssues = new System.Windows.Forms.ListBox();
            this.tabPageTaskUpdateIssue = new System.Windows.Forms.TabPage();
            this.btnTaskUpdateIssues = new System.Windows.Forms.Button();
            this.treeViewTaskUpdateDetails = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReportRunTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSprint = new System.Windows.Forms.CheckBox();
            this.chkIncludeRemoved = new System.Windows.Forms.CheckBox();
            this.cmbSprint = new System.Windows.Forms.ComboBox();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbDuration = new System.Windows.Forms.ComboBox();
            this.radStoryView = new System.Windows.Forms.RadioButton();
            this.radResourceView = new System.Windows.Forms.RadioButton();
            this.btnBurnViewRefresh = new System.Windows.Forms.Button();
            this.tabReport.SuspendLayout();
            this.tabPageBurn.SuspendLayout();
            this.tabPageBugs.SuspendLayout();
            this.tabPageBugUpdateIssue.SuspendLayout();
            this.tabPageTaskUpdateIssue.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabReport
            // 
            this.tabReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabReport.Controls.Add(this.tabPageBurn);
            this.tabReport.Controls.Add(this.tabPageBugs);
            this.tabReport.Controls.Add(this.tabPageBugUpdateIssue);
            this.tabReport.Controls.Add(this.tabPageTaskUpdateIssue);
            this.tabReport.Location = new System.Drawing.Point(12, 116);
            this.tabReport.Name = "tabReport";
            this.tabReport.SelectedIndex = 0;
            this.tabReport.Size = new System.Drawing.Size(1068, 585);
            this.tabReport.TabIndex = 1;
            // 
            // tabPageBurn
            // 
            this.tabPageBurn.Controls.Add(this.btnBurnViewRefresh);
            this.tabPageBurn.Controls.Add(this.radResourceView);
            this.tabPageBurn.Controls.Add(this.radStoryView);
            this.tabPageBurn.Controls.Add(this.btnGetBurn);
            this.tabPageBurn.Controls.Add(this.label2);
            this.tabPageBurn.Controls.Add(this.txtFind);
            this.tabPageBurn.Controls.Add(this.label6);
            this.tabPageBurn.Controls.Add(this.lblBurnSummary);
            this.tabPageBurn.Controls.Add(this.treeViewBurnDetails);
            this.tabPageBurn.Location = new System.Drawing.Point(4, 22);
            this.tabPageBurn.Name = "tabPageBurn";
            this.tabPageBurn.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBurn.Size = new System.Drawing.Size(1060, 559);
            this.tabPageBurn.TabIndex = 1;
            this.tabPageBurn.Text = "Burn";
            this.tabPageBurn.UseVisualStyleBackColor = true;
            // 
            // btnGetBurn
            // 
            this.btnGetBurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetBurn.Location = new System.Drawing.Point(924, 13);
            this.btnGetBurn.Name = "btnGetBurn";
            this.btnGetBurn.Size = new System.Drawing.Size(120, 57);
            this.btnGetBurn.TabIndex = 19;
            this.btnGetBurn.Text = "Get burn details";
            this.btnGetBurn.UseVisualStyleBackColor = true;
            this.btnGetBurn.Click += new System.EventHandler(this.btnGetBurn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(933, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Find:";
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(967, 111);
            this.txtFind.MaxLength = 6;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(77, 20);
            this.txtFind.TabIndex = 3;
            this.txtFind.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFind_MouseClick);
            this.txtFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Summary:";
            // 
            // lblBurnSummary
            // 
            this.lblBurnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBurnSummary.AutoSize = true;
            this.lblBurnSummary.Location = new System.Drawing.Point(83, 13);
            this.lblBurnSummary.Name = "lblBurnSummary";
            this.lblBurnSummary.Size = new System.Drawing.Size(87, 13);
            this.lblBurnSummary.TabIndex = 1;
            this.lblBurnSummary.Text = "Please run report";
            // 
            // treeViewBurnDetails
            // 
            this.treeViewBurnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewBurnDetails.Location = new System.Drawing.Point(6, 137);
            this.treeViewBurnDetails.Name = "treeViewBurnDetails";
            this.treeViewBurnDetails.Size = new System.Drawing.Size(1039, 416);
            this.treeViewBurnDetails.TabIndex = 0;
            // 
            // tabPageBugs
            // 
            this.tabPageBugs.Controls.Add(this.treeViewBugProgress);
            this.tabPageBugs.Controls.Add(this.lblBugProgressSummary);
            this.tabPageBugs.Controls.Add(this.lblTextBugProgress);
            this.tabPageBugs.Controls.Add(this.btnBugProgress);
            this.tabPageBugs.Location = new System.Drawing.Point(4, 22);
            this.tabPageBugs.Name = "tabPageBugs";
            this.tabPageBugs.Size = new System.Drawing.Size(1060, 559);
            this.tabPageBugs.TabIndex = 4;
            this.tabPageBugs.Text = "Bug Progress";
            this.tabPageBugs.UseVisualStyleBackColor = true;
            // 
            // treeViewBugProgress
            // 
            this.treeViewBugProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewBugProgress.Location = new System.Drawing.Point(9, 102);
            this.treeViewBugProgress.Name = "treeViewBugProgress";
            this.treeViewBugProgress.Size = new System.Drawing.Size(1039, 449);
            this.treeViewBugProgress.TabIndex = 5;
            // 
            // lblBugProgressSummary
            // 
            this.lblBugProgressSummary.AutoSize = true;
            this.lblBugProgressSummary.Location = new System.Drawing.Point(95, 12);
            this.lblBugProgressSummary.Name = "lblBugProgressSummary";
            this.lblBugProgressSummary.Size = new System.Drawing.Size(0, 13);
            this.lblBugProgressSummary.TabIndex = 4;
            // 
            // lblTextBugProgress
            // 
            this.lblTextBugProgress.AutoSize = true;
            this.lblTextBugProgress.Location = new System.Drawing.Point(9, 12);
            this.lblTextBugProgress.Name = "lblTextBugProgress";
            this.lblTextBugProgress.Size = new System.Drawing.Size(79, 13);
            this.lblTextBugProgress.TabIndex = 3;
            this.lblTextBugProgress.Text = "Bug Progress : ";
            // 
            // btnBugProgress
            // 
            this.btnBugProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBugProgress.Location = new System.Drawing.Point(858, 12);
            this.btnBugProgress.Name = "btnBugProgress";
            this.btnBugProgress.Size = new System.Drawing.Size(190, 42);
            this.btnBugProgress.TabIndex = 2;
            this.btnBugProgress.Text = "Get details";
            this.btnBugProgress.UseVisualStyleBackColor = true;
            this.btnBugProgress.Click += new System.EventHandler(this.btnBugProgress_Click);
            // 
            // tabPageBugUpdateIssue
            // 
            this.tabPageBugUpdateIssue.Controls.Add(this.lblBugUpdatesIssueText);
            this.tabPageBugUpdateIssue.Controls.Add(this.btnBugUpdateIssues);
            this.tabPageBugUpdateIssue.Controls.Add(this.lstBugFixIssues);
            this.tabPageBugUpdateIssue.Location = new System.Drawing.Point(4, 22);
            this.tabPageBugUpdateIssue.Name = "tabPageBugUpdateIssue";
            this.tabPageBugUpdateIssue.Size = new System.Drawing.Size(1060, 559);
            this.tabPageBugUpdateIssue.TabIndex = 2;
            this.tabPageBugUpdateIssue.Text = "Bug Updates";
            this.tabPageBugUpdateIssue.UseVisualStyleBackColor = true;
            // 
            // lblBugUpdatesIssueText
            // 
            this.lblBugUpdatesIssueText.AutoSize = true;
            this.lblBugUpdatesIssueText.Location = new System.Drawing.Point(12, 72);
            this.lblBugUpdatesIssueText.Name = "lblBugUpdatesIssueText";
            this.lblBugUpdatesIssueText.Size = new System.Drawing.Size(324, 13);
            this.lblBugUpdatesIssueText.TabIndex = 21;
            this.lblBugUpdatesIssueText.Text = "Following bugs are not updated with their Cause/ How Fixed details";
            // 
            // btnBugUpdateIssues
            // 
            this.btnBugUpdateIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBugUpdateIssues.Location = new System.Drawing.Point(887, 3);
            this.btnBugUpdateIssues.Name = "btnBugUpdateIssues";
            this.btnBugUpdateIssues.Size = new System.Drawing.Size(160, 57);
            this.btnBugUpdateIssues.TabIndex = 20;
            this.btnBugUpdateIssues.Text = "Get bug update issues";
            this.btnBugUpdateIssues.UseVisualStyleBackColor = true;
            this.btnBugUpdateIssues.Click += new System.EventHandler(this.btnBugUpdateIssues_Click);
            // 
            // lstBugFixIssues
            // 
            this.lstBugFixIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBugFixIssues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBugFixIssues.FormattingEnabled = true;
            this.lstBugFixIssues.Location = new System.Drawing.Point(12, 91);
            this.lstBugFixIssues.Name = "lstBugFixIssues";
            this.lstBugFixIssues.Size = new System.Drawing.Size(1036, 457);
            this.lstBugFixIssues.TabIndex = 0;
            // 
            // tabPageTaskUpdateIssue
            // 
            this.tabPageTaskUpdateIssue.Controls.Add(this.btnTaskUpdateIssues);
            this.tabPageTaskUpdateIssue.Controls.Add(this.treeViewTaskUpdateDetails);
            this.tabPageTaskUpdateIssue.Location = new System.Drawing.Point(4, 22);
            this.tabPageTaskUpdateIssue.Name = "tabPageTaskUpdateIssue";
            this.tabPageTaskUpdateIssue.Size = new System.Drawing.Size(1060, 559);
            this.tabPageTaskUpdateIssue.TabIndex = 3;
            this.tabPageTaskUpdateIssue.Text = "Task Updates";
            this.tabPageTaskUpdateIssue.UseVisualStyleBackColor = true;
            // 
            // btnTaskUpdateIssues
            // 
            this.btnTaskUpdateIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaskUpdateIssues.Location = new System.Drawing.Point(887, 12);
            this.btnTaskUpdateIssues.Name = "btnTaskUpdateIssues";
            this.btnTaskUpdateIssues.Size = new System.Drawing.Size(160, 57);
            this.btnTaskUpdateIssues.TabIndex = 21;
            this.btnTaskUpdateIssues.Text = "Get task update issues";
            this.btnTaskUpdateIssues.UseVisualStyleBackColor = true;
            this.btnTaskUpdateIssues.Click += new System.EventHandler(this.btnTaskUpdateIssues_Click);
            // 
            // treeViewTaskUpdateDetails
            // 
            this.treeViewTaskUpdateDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewTaskUpdateDetails.Location = new System.Drawing.Point(12, 91);
            this.treeViewTaskUpdateDetails.Name = "treeViewTaskUpdateDetails";
            this.treeViewTaskUpdateDetails.Size = new System.Drawing.Size(1045, 456);
            this.treeViewTaskUpdateDetails.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblReportRunTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkSprint);
            this.groupBox1.Controls.Add(this.chkIncludeRemoved);
            this.groupBox1.Controls.Add(this.cmbSprint);
            this.groupBox1.Controls.Add(this.cmbTeam);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.cmbDuration);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1064, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // lblReportRunTime
            // 
            this.lblReportRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReportRunTime.AutoSize = true;
            this.lblReportRunTime.Location = new System.Drawing.Point(1038, 82);
            this.lblReportRunTime.Name = "lblReportRunTime";
            this.lblReportRunTime.Size = new System.Drawing.Size(13, 13);
            this.lblReportRunTime.TabIndex = 23;
            this.lblReportRunTime.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(888, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Time taken to run report(Sec):";
            // 
            // chkSprint
            // 
            this.chkSprint.AutoSize = true;
            this.chkSprint.Location = new System.Drawing.Point(630, 23);
            this.chkSprint.Name = "chkSprint";
            this.chkSprint.Size = new System.Drawing.Size(53, 17);
            this.chkSprint.TabIndex = 20;
            this.chkSprint.Text = "Sprint";
            this.chkSprint.UseVisualStyleBackColor = true;
            this.chkSprint.Visible = false;
            // 
            // chkIncludeRemoved
            // 
            this.chkIncludeRemoved.AutoSize = true;
            this.chkIncludeRemoved.Checked = true;
            this.chkIncludeRemoved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeRemoved.Location = new System.Drawing.Point(32, 63);
            this.chkIncludeRemoved.Name = "chkIncludeRemoved";
            this.chkIncludeRemoved.Size = new System.Drawing.Size(142, 17);
            this.chkIncludeRemoved.TabIndex = 19;
            this.chkIncludeRemoved.Text = "Include Removed Tasks";
            this.chkIncludeRemoved.UseVisualStyleBackColor = true;
            // 
            // cmbSprint
            // 
            this.cmbSprint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSprint.FormattingEnabled = true;
            this.cmbSprint.Location = new System.Drawing.Point(688, 20);
            this.cmbSprint.Name = "cmbSprint";
            this.cmbSprint.Size = new System.Drawing.Size(121, 21);
            this.cmbSprint.TabIndex = 16;
            this.cmbSprint.Visible = false;
            // 
            // cmbTeam
            // 
            this.cmbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(73, 20);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(187, 21);
            this.cmbTeam.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Team";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Duration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(535, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "To";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(570, 59);
            this.dateTimePickerTo.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerTo.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerTo.TabIndex = 8;
            this.dateTimePickerTo.Value = new System.DateTime(2014, 11, 24, 14, 35, 58, 0);
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.dateTimePickerTo_ValueChanged);
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(320, 59);
            this.dateTimePickerFrom.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerFrom.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFrom.TabIndex = 7;
            this.dateTimePickerFrom.Value = new System.DateTime(2015, 7, 31, 0, 0, 0, 0);
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.dateTimePickerFrom_ValueChanged);
            // 
            // cmbDuration
            // 
            this.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuration.FormattingEnabled = true;
            this.cmbDuration.Location = new System.Drawing.Point(388, 20);
            this.cmbDuration.Name = "cmbDuration";
            this.cmbDuration.Size = new System.Drawing.Size(187, 21);
            this.cmbDuration.TabIndex = 5;
            // 
            // radStoryView
            // 
            this.radStoryView.AutoSize = true;
            this.radStoryView.Location = new System.Drawing.Point(9, 110);
            this.radStoryView.Name = "radStoryView";
            this.radStoryView.Size = new System.Drawing.Size(75, 17);
            this.radStoryView.TabIndex = 20;
            this.radStoryView.TabStop = true;
            this.radStoryView.Text = "Story View";
            this.radStoryView.UseVisualStyleBackColor = true;
            // 
            // radResourceView
            // 
            this.radResourceView.AutoSize = true;
            this.radResourceView.Checked = true;
            this.radResourceView.Location = new System.Drawing.Point(86, 111);
            this.radResourceView.Name = "radResourceView";
            this.radResourceView.Size = new System.Drawing.Size(97, 17);
            this.radResourceView.TabIndex = 21;
            this.radResourceView.TabStop = true;
            this.radResourceView.Text = "Resource View";
            this.radResourceView.UseVisualStyleBackColor = true;
            // 
            // btnBurnViewRefresh
            // 
            this.btnBurnViewRefresh.Location = new System.Drawing.Point(203, 109);
            this.btnBurnViewRefresh.Name = "btnBurnViewRefresh";
            this.btnBurnViewRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnBurnViewRefresh.TabIndex = 22;
            this.btnBurnViewRefresh.Text = "Refresh";
            this.btnBurnViewRefresh.UseVisualStyleBackColor = true;
            this.btnBurnViewRefresh.Click += new System.EventHandler(this.btnBurnViewRefresh_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 713);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabReport);
            this.Name = "Home";
            this.Text = "SmartManger";
            this.Load += new System.EventHandler(this.Home_Load);
            this.tabReport.ResumeLayout(false);
            this.tabPageBurn.ResumeLayout(false);
            this.tabPageBurn.PerformLayout();
            this.tabPageBugs.ResumeLayout(false);
            this.tabPageBugs.PerformLayout();
            this.tabPageBugUpdateIssue.ResumeLayout(false);
            this.tabPageBugUpdateIssue.PerformLayout();
            this.tabPageTaskUpdateIssue.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabReport;
        private System.Windows.Forms.TabPage tabPageBurn;
        private System.Windows.Forms.TreeView treeViewBurnDetails;
        private System.Windows.Forms.Label lblBurnSummary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageBugUpdateIssue;
        private System.Windows.Forms.ListBox lstBugFixIssues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSprint;
        private System.Windows.Forms.CheckBox chkIncludeRemoved;
        private System.Windows.Forms.ComboBox cmbSprint;
        private System.Windows.Forms.ComboBox cmbTeam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.ComboBox cmbDuration;
        private System.Windows.Forms.Button btnGetBurn;
        private System.Windows.Forms.Label lblReportRunTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBugUpdateIssues;
        private System.Windows.Forms.Label lblBugUpdatesIssueText;
        private System.Windows.Forms.TabPage tabPageTaskUpdateIssue;
        private System.Windows.Forms.TreeView treeViewTaskUpdateDetails;
        private System.Windows.Forms.Button btnTaskUpdateIssues;
        private System.Windows.Forms.TabPage tabPageBugs;
        private System.Windows.Forms.Label lblBugProgressSummary;
        private System.Windows.Forms.Label lblTextBugProgress;
        private System.Windows.Forms.Button btnBugProgress;
        private System.Windows.Forms.TreeView treeViewBugProgress;
        private System.Windows.Forms.RadioButton radResourceView;
        private System.Windows.Forms.RadioButton radStoryView;
        private System.Windows.Forms.Button btnBurnViewRefresh;

    }
}

