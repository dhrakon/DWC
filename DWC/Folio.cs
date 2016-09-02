using System;
using System.Collections.Generic;
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
        private FormState CurrentFormState;

        public Folio()
        {
            InitializeComponent();
            Dhrakk.Game.CurrentInstance.Initialize();

            LoadToControls();

            SetWelcome();

            SetFormState(FormState.Fresh);
        }

        private void LoadToControls()
        {
            //Load the Chapters Combo Box.
            foreach(Dhrakk.Game.DWChapter Chapter in Dhrakk.Game.CurrentInstance.Chapters)
            {
                cboChapters.Items.Add(Chapter);
            }
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
            if (((Dhrakk.Game.DWAgent.DWAgentCheckbox)sender).ParentAgent.Equals(CurrentAgent))
            {
                //Do nothing.
            }
            else
            {
                CurrentAgent.AgentControl.Checked = false;
                LoadAgentToControls(((Dhrakk.Game.DWAgent.DWAgentCheckbox)sender).ParentAgent);
            }
        }

        private void CreateNewAgent()
        {
            Dhrakk.Game.DWAgent newAgent = Dhrakk.Game.CurrentInstance.CreateNewAgent("New Agent");
            flowLayoutAgents.Controls.Add(newAgent.AgentControl);
            LoadAgentToControls(newAgent);
        }

        private void flowLayoutAgents_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is Dhrakk.Game.DWAgent.DWAgentCheckbox)
            {
                ((Dhrakk.Game.DWAgent.DWAgentCheckbox)e.Control).CheckedChanged += AgentButton_CheckedChanged;
                ((Dhrakk.Game.DWAgent.DWAgentCheckbox)e.Control).Click += AgentButton_Click;
            }
        }

        private void LoadAgentToControls(Dhrakk.Game.DWAgent Agent)
        {
            CurrentAgent = Agent;

            foreach (object FlowAgent in flowLayoutAgents.Controls)
            {
                if (FlowAgent is Dhrakk.Game.DWAgent.DWAgentCheckbox)
                {
                    if (!((Dhrakk.Game.DWAgent.DWAgentCheckbox)FlowAgent).ParentAgent.Equals(CurrentAgent))
                    {
                        ((Dhrakk.Game.DWAgent.DWAgentCheckbox)FlowAgent).Checked = false;
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

                    if (!tabControl.TabPages.Contains(tabWelcome))
                        tabControl.TabPages.Insert(0, tabWelcome);

                    tabWelcome.Focus();

                    CurrentFormState = FormState.Fresh;

                    ClearControls();

                    break;
                case FormState.Stale:
                    UnloadWelcome();
                    newAgentToolStripMenuItem.Enabled = true;
                    saveKillTeamToolStripMenuItem.Enabled = true;

                    CurrentFormState = FormState.Stale;
                    break;

                case FormState.Debug:

                    CurrentFormState = FormState.Debug;
                    break;
            }
        }

        private void ClearControls()
        {
            CurrentAgent = new Dhrakk.Game.DWAgent("Cleaning up...");
            
            //Controls on the Agent Tab.
            txtAgentName.Text = "";
            lblRank.Text = "";

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

                foreach (Dhrakk.Game.DWAgent dw in Dhrakk.Game.CurrentInstance.CurrentKillTeam.Members)
                {
                    flowLayoutAgents.Controls.Add(dw.AgentControl);
                }

                LoadAgentToControls(Dhrakk.Game.CurrentInstance.CurrentKillTeam.Members[0]);

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
                UpdateKillTeam(new Dhrakk.Game.DWKillTeam(response.ToString()));

                CreateNewAgent();
                SetFormState(FormState.Stale);
            }
        }

        private void saveKillTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Dhrakk.Game.CurrentInstance.CurrentKillTeam.SaveLocation))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Deathwatch Companion Files (*.dwc)|*.dwc";
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Dhrakk.Game.CurrentInstance.CurrentKillTeam.SaveLocation = sfd.FileName;
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
            CurrentAgent.Name = txtAgentName.Text;
            UpdateStatusBar();
        }

        private void UnloadWelcome()
        {
            //Hide the welcome tab.
            if (tabControl.TabPages.Contains(tabWelcome))
                tabControl.TabPages.Remove(tabWelcome);
        }

        private void UpdateKillTeam(Dhrakk.Game.DWKillTeam KillTeam)
        {
            Dhrakk.Game.CurrentInstance.CurrentKillTeam = KillTeam;
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            string statusText;
            statusText = "Deathwatch Companion v" + Dhrakk.Game.CurrentInstance.CurrentKillTeam.Version;
            statusText += " - " + Dhrakk.Game.CurrentInstance.CurrentKillTeam.ToString();

            if (CurrentAgent != null)
            {
                statusText += " - " + CurrentAgent.ToString();
            }

            toolStripStatusKillTeam.Text = statusText;
        }

        private void btnGenerateJSON_Click(object sender, EventArgs e)
        {
            string SaveLocation = @"c:\users\dhrakon\Desktop\characteristics.json";

            List<Dhrakk.Game.DWCharacteristic> dwcs = new List<Dhrakk.Game.DWCharacteristic>();
            Dhrakk.Game.DWCharacteristic WS = new Dhrakk.Game.DWCharacteristic() { Name = "Weapon Skill", ShortName = "WS" };
            Dhrakk.Game.DWCharacteristic BS = new Dhrakk.Game.DWCharacteristic() { Name = "Ballistic Skill", ShortName = "BS" };

            dwcs.Add(WS);
            dwcs.Add(BS);

            Dhrakk.Utility.SaveCustomObject<List<Dhrakk.Game.DWCharacteristic>>(dwcs, SaveLocation);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentFormState == FormState.Fresh)
            {
                if (tabControl.SelectedIndex != 0)
                {
                    tabControl.SelectedIndex = 0;
                }
            }
        }
    }
}