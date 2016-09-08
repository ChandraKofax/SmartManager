using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFS.Model;
using TFS.Reporting;

namespace SmartManagerWindows
{
    public partial class Home : Form
    {
        private TimeSpan reportRunningTime;
        private BurnReport burnReport;
        BurnRetrievalOptions filter;
        TreeNode[] selectedNodes;
        int currentIndex;

        public Home()
        {
            InitializeComponent();
        }
        private void ResetBurnReport()
        {
            lblBurnSummary.Text = "Report running.";
            treeViewBurnDetails.Nodes.Clear();
        }
        private void ResetProcessIssuesReport()
        {
            lstBugFixIssues.Items.Clear();
        }
        private void ResetTaskUpdateIssuesReport()
        {
            treeViewTaskUpdateDetails.Nodes.Clear();
        }
        private void ResetBugProgressReport()
        {
            lblBugProgressSummary.Text = String.Empty;
            treeViewBugProgress.Nodes.Clear();
        }
        private void btnGetBurn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SmartManager.SmartManagerSDK smartManagerSDK = new SmartManager.SmartManagerSDK();

                //ChildItemCollection childReport = smartManagerSDK.GetChildTasksDetails("http://tfs.kofax.com:8080/tfs/products", "KTA", "742678");
                FieldFilter fieldFilter = new FieldFilter();
                fieldFilter.AssignedTo.Name = "Tiger Team";
                burnReport = smartManagerSDK.GetBurnDetails("7A2D26351789904A8993CC2455163262", "http://tfs.kofax.com:8080/tfs/products", "KTA", fieldFilter);

                //burnReport = smartManagerSDK.GetAssignedWorkItems("7A2D26351789904A8993CC2455163262", "http://tfs.kofax.com:8080/tfs/products", "KTA", fieldFilter);

                this.ResetBurnReport();
                filter = PopulateFilterDetails();
                //ViewType selectedViewType = ViewType.Resource;
                //if (radStoryView.Checked)
                //{
                //    selectedViewType = ViewType.Story;
                //}

                ClientApplication.Manager.Connect("http://tfs.kofax.com:8080/tfs/products", "KTA");
                burnReport = ClientApplication.Manager.GetBurnDetails(filter);

                //Report report = ClientApplication.Manager.GetChildTasksDetails(filter);
                //burnReport.SetView(selectedViewType, filter.Team.Members);
                this.PopulateBurnView();

                reportRunningTime = DateTime.Now.TimeOfDay - reportRunningTime;
                lblReportRunTime.Text = reportRunningTime.TotalSeconds.ToString("F0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnBugProgress_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ResetBugProgressReport();
                BurnRetrievalOptions filter = PopulateFilterDetails();

                Report bugsReport = ClientApplication.Manager.GetBugsResolvedOrClosedDetails(filter);
                bugsReport.SetView(ViewType.Resource, filter.Team.Members);
                lblBugProgressSummary.Text = CreateBugSummary(bugsReport);
                PopulateTree(treeViewBugProgress, bugsReport, false);

                reportRunningTime = DateTime.Now.TimeOfDay - reportRunningTime;
                lblReportRunTime.Text = reportRunningTime.TotalSeconds.ToString("F0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnBugUpdateIssues_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ResetProcessIssuesReport();
                BurnRetrievalOptions filter = PopulateFilterDetails(); 
                Report bugUpdatesIssueReport = ClientApplication.Manager.GetCauseAndResolutionIssueDetails(filter);
                PopulateUpdateIssuesList(bugUpdatesIssueReport);
                reportRunningTime = DateTime.Now.TimeOfDay - reportRunningTime;
                lblReportRunTime.Text = reportRunningTime.TotalSeconds.ToString("F0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnTaskUpdateIssues_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ResetTaskUpdateIssuesReport();
                BurnRetrievalOptions filter = PopulateFilterDetails();

                Report taskUpdateReport = ClientApplication.Manager.GetTaskUpdateDetails(filter);

                PopulateTree(treeViewTaskUpdateDetails, taskUpdateReport, false);

                reportRunningTime = DateTime.Now.TimeOfDay - reportRunningTime;
                lblReportRunTime.Text = reportRunningTime.TotalSeconds.ToString("F0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PopulateUpdateIssuesList(Report bugUpdatesIssueReport)
        {
            string formattingString = "{0} : {1} [{2}]. Assigned To [{3}]";
            foreach(Item bug in bugUpdatesIssueReport.AllItems)
            {
                lstBugFixIssues.Items.Add(string.Format(formattingString, bug.Id, bug.Title, bug.State, bug.AssignedTo.Name));
            }
        }

        private BurnRetrievalOptions PopulateFilterDetails()
        {
            reportRunningTime = DateTime.Now.TimeOfDay;
            Team selectedTeam = (Team)cmbTeam.SelectedItem;
            BurnDuration selectedDuration = (BurnDuration)cmbDuration.SelectedItem;

            BurnRetrievalOptions filter = new BurnRetrievalOptions
            {
                DateRange = GetDateRangeForDuration(selectedDuration),
                Team = (Team)cmbTeam.SelectedItem,
                BurnDuration = (BurnDuration)cmbDuration.SelectedItem,
                IncludeRemovedTasks = chkIncludeRemoved.Checked,
                Sprint = chkSprint.Checked ? (Sprint)cmbSprint.SelectedItem : null,
            };
            return filter;
        }
        private Duration GetDateRangeForDuration(BurnDuration duration)
        {
            Duration durationFilter = new Duration
            {
                From = dateTimePickerFrom.Value.Date.Add(Constants.ClientTimeConstant),
                To = dateTimePickerTo.Value.Date.Add(Constants.ClientTimeConstant)
            };
            switch (duration)
            {
                case BurnDuration.PreviousDay:
                    durationFilter.From = DateTime.Now.Date.AddDays(-1).Add(Constants.ClientTimeConstant);
                    durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                    break;
                case BurnDuration.Last3Days:
                    durationFilter.From = DateTime.Now.Date.AddDays(-3).Add(Constants.ClientTimeConstant);
                    durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                    break;
                case BurnDuration.Last7Days:
                    durationFilter.From = DateTime.Now.Date.AddDays(-7).Add(Constants.ClientTimeConstant);
                    durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                    break;
                case BurnDuration.Last15Days:
                    durationFilter.From = DateTime.Now.Date.AddDays(-15).Add(Constants.ClientTimeConstant);
                    durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                    break;
                case BurnDuration.Last30Days:
                    durationFilter.From = DateTime.Now.Date.AddDays(-30).Add(Constants.ClientTimeConstant);
                    durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                    break;
                case BurnDuration.Duration:
                    durationFilter.From = dateTimePickerFrom.Value.Date.Add(Constants.ClientTimeConstant);
                    durationFilter.To = dateTimePickerTo.Value.Date.Add(Constants.ClientTimeConstant);
                    break;
            }

            return durationFilter;
        }

        private void PopulateTree(TreeView tree, Report report, bool includeBurnDetails)
        {
            tree.Nodes.Clear();
            foreach (Item item in report.AllItems)
            {
                string titleString = includeBurnDetails ? item.GetBDTString() : item.ToString();
                TreeNode rootNode = new TreeNode(titleString);
                rootNode.Name = item.Id.ToString();
                BuildTree(rootNode, item, includeBurnDetails);
                tree.Nodes.Add(rootNode);
            }
        }

        private string CreateBugSummary(Report report)
        {
            string summaryString = String.Format("{0} (R:{1}, C:{2})", report.NumberOfBugsResolved + report.NumberOfBugsClosed, report.NumberOfBugsResolved, report.NumberOfBugsClosed);
            return summaryString;
        }

        private string CreateBurnSummary(BurnReport report)
        {
            string summaryString = string.Format("Burn: {0}\n Burn on Bugs: {1}\n Story Burn: {2}\n Deviation: {3}\n Progress on plan: {4}\n Time spent: {5}",
                        report.Burn.ToString(),
                        report.BugBurn.ToString(),
                        report.StoryBurn.ToString(), 
                        report.Deviation.ToString(),
                        report.Progress.ToString(),
                        report.TimeSpent);
            return summaryString;
        }

        private void BuildTree(TreeNode node, Item item, bool includeBurnDetails)
        {
            item.Children.ForEach(i => 
                {
                    string titleString = includeBurnDetails ? i.GetBDTString() : i.ToString();
                    TreeNode newNode = new TreeNode(titleString);
                    newNode.Name = i.Id.ToString();
                    node.Nodes.Add(newNode);
                    BuildTree(newNode, i, includeBurnDetails);
                });
        }

        private void Home_Load(object sender, EventArgs e)
        {
            cmbTeam.DataSource = Constants.Teams;
            cmbTeam.DisplayMember = "Name";

            cmbDuration.DataSource = Enum.GetValues(typeof(BurnDuration));

            cmbSprint.DataSource = Constants.AvailableSprints;
            cmbSprint.DisplayMember = "Name";

            this.dateTimePickerFrom.Value = DateTime.Today;
            this.dateTimePickerTo.Value = DateTime.Today;
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(selectedNodes != null)
                {
                    currentIndex++;
                }
                else if (txtFind.Text.Length == 6)
                {
                    selectedNodes = this.treeViewBurnDetails.Nodes.Find(txtFind.Text, true);
                    currentIndex = 0;
                }

                SelectTreeNode();
            }
            else
            {
                selectedNodes = null;
                currentIndex = -1;
            }
        }

        private void SelectTreeNode()
        {
            if (selectedNodes.Count() > 0)
            {
                if (selectedNodes.Count() <= currentIndex)
                {
                    currentIndex = 0;
                }
                treeViewBurnDetails.Select();
                treeViewBurnDetails.SelectedNode = selectedNodes[currentIndex];
                selectedNodes[currentIndex].EnsureVisible();
            }
        }

        private void txtFind_MouseClick(object sender, MouseEventArgs e)
        {
            txtFind.SelectAll();
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerFrom.Value > this.dateTimePickerTo.Value)
            {
                this.dateTimePickerTo.Value = this.dateTimePickerFrom.Value.AddDays(1);
            }
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerFrom.Value > this.dateTimePickerTo.Value)
            {
                this.dateTimePickerTo.Value = this.dateTimePickerFrom.Value.AddDays(1);
            }
        }

        private void btnBurnViewRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateBurnView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PopulateBurnView()
        {
            ViewType currentViewType = ViewType.Resource;
            if (radStoryView.Checked)
            {
                currentViewType = ViewType.Story;
            }

            if (burnReport != null)
            {
                burnReport.SetView(currentViewType, filter.Team.Members);
                lblBurnSummary.Text = CreateBurnSummary(burnReport);

                PopulateTree(treeViewBurnDetails, burnReport, true);
            }
        }
    }
}
