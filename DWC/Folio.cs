using System;
using System.IO;
using System.Windows.Forms;

namespace DWC
{
    public partial class Folio : Form
    {
        private Dhrakk.Game.DWAgent CurrentAgent;
        private bool dirty;
        private enum FormState
        {
            Fresh = 1,
            Stale = 2,
            Debug = 3
        }

        public Folio()
        {
            InitializeComponent();

            SetWelcome();

            SetFormState(FormState.Fresh);
        }

        public bool IsDirty()
        {
            return dirty;
        }

        public void SetDirty()
        {
            dirty = true;
        }

        private void AgentButton_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void AgentButton_Click(object sender, EventArgs e)
        {
            if (((Dhrakk.Game.DWAgent.AgentCheckbox)sender).ParentAgent.Equals(CurrentAgent))
            {
                //Do nothing.
            }
            else
            {
                CurrentAgent.AgentControl.Checked = false;
                LoadAgentToControls(((Dhrakk.Game.DWAgent.AgentCheckbox)sender).ParentAgent);
            }
        }

        private void CreateNewAgent()
        {
            Dhrakk.Game.DWAgent newAgent = Dhrakk.Game.Instance.CreateNewAgent("New Agent");
            flowLayoutAgents.Controls.Add(newAgent.AgentControl);
            LoadAgentToControls(newAgent);
        }

        private void flowLayoutAgents_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is Dhrakk.Game.DWAgent.AgentCheckbox)
            {
                ((Dhrakk.Game.DWAgent.AgentCheckbox)e.Control).CheckedChanged += AgentButton_CheckedChanged;
                ((Dhrakk.Game.DWAgent.AgentCheckbox)e.Control).Click += AgentButton_Click;
            }
        }

        private void LoadAgentToControls(Dhrakk.Game.DWAgent Agent)
        {
            CurrentAgent = Agent;

            foreach (object FlowAgent in flowLayoutAgents.Controls)
            {
                if (FlowAgent is Dhrakk.Game.DWAgent.AgentCheckbox)
                {
                    if (!((Dhrakk.Game.DWAgent.AgentCheckbox)FlowAgent).ParentAgent.Equals(CurrentAgent))
                    {
                        ((Dhrakk.Game.DWAgent.AgentCheckbox)FlowAgent).Checked = false;
                    }
                }
            }

            Agent.AgentControl.Checked = true;

            txtAgentName.Text = Agent.Name;

            UpdateStatusBar();
        }

        private void SetFormState(FormState FS)
        {
            switch (FS)
            {
                case FormState.Fresh:

                    flowLayoutAgents.Controls.Clear();

                    CurrentAgent = null;
                    
                    newAgentToolStripMenuItem.Enabled = false;
                    saveKillTeamToolStripMenuItem.Enabled = false;

                    ClearControls();

                    break;
                case FormState.Stale:
                    UnloadWelcome();
                    newAgentToolStripMenuItem.Enabled = true;
                    saveKillTeamToolStripMenuItem.Enabled = true;
                    break;

                case FormState.Debug:

                    break;
            }
        }

        private void ClearControls()
        {
            CurrentAgent = new Dhrakk.Game.DWAgent("Cleaning up...");
            
            //Controls on the Agent Tab.
            txtAgentName.Text = "";

            CurrentAgent = null;
        }
        
        private void loadKillTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Deathwatch Companion Files (*.dwc)|*.dwc";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SetFormState(FormState.Fresh);

                Dhrakk.Utility.Load(ofd.FileName);

                foreach (Dhrakk.Game.DWAgent dw in Dhrakk.Game.Instance.CurrentKillTeam.Members)
                {
                    flowLayoutAgents.Controls.Add(dw.AgentControl);
                }

                LoadAgentToControls(Dhrakk.Game.Instance.CurrentKillTeam.Members[0]);

                SetFormState(FormState.Stale);
            }
        }

        private void newAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO Check for dirty, already loaded agent.

            CreateNewAgent();
        }

        private void newKillTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO Check for dirty form.
            Dhrakk.Utility.UserResponse response = Dhrakk.Utility.GetUserInput("Please enter a name for your new Kill Team:");

            if (!response.Canceled)
            {
                SetFormState(FormState.Fresh);
                UpdateKillTeam(new Dhrakk.Game.KillTeam(response.ToString()));

                CreateNewAgent();
                SetFormState(FormState.Stale);
            }
        }

        private void saveKillTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Dhrakk.Game.Instance.CurrentKillTeam.SaveLocation))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Deathwatch Companion Files (*.dwc)|*.dwc";
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Dhrakk.Game.Instance.CurrentKillTeam.SaveLocation = sfd.FileName;
                    Dhrakk.Utility.Save();
                }
            }
        }

        private void SetWelcome()
        {
            string welcomeText;

            try
            {
                welcomeText = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\data\WelcomeMessage.txt");
            }
            catch
            {
                welcomeText = "Failed to load welcome text file.";
            }

            txtWelcome.Text = welcomeText;

            txtWelcome.TextAlign = HorizontalAlignment.Center;
        }

        private void txtAgentName_TextChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
            CurrentAgent.Name = txtAgentName.Text;
        }

        private void UnloadWelcome()
        {
            //Hide the welcome tab.
            if (tabControl.TabPages.Contains(tabWelcome))
                tabControl.TabPages.Remove(tabWelcome);
        }

        private void UpdateKillTeam(Dhrakk.Game.KillTeam KillTeam)
        {
            Dhrakk.Game.Instance.CurrentKillTeam = KillTeam;
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            string statusText;
            statusText = "Deathwatch Companion v" + Dhrakk.Game.Instance.CurrentKillTeam.Version;
            statusText += " - " + Dhrakk.Game.Instance.CurrentKillTeam.ToString();

            if (CurrentAgent != null)
            {
                statusText += " - " + CurrentAgent.ToString();
            }

            toolStripStatusKillTeam.Text = statusText;
        }
    }
}