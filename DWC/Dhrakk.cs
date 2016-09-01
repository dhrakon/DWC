using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DWC
{
    public static class Dhrakk
    {
        public static class Game
        {
            public static class Constants
            {
                
            }

            public class DWRank
            {
                public int maxXP { get; set; }
                public int minXP { get; set; }
                public int rank { get; set; }
            }

            public static class CurrentInstance
            {
                public static void Initialize() { }

                public static DWKillTeam CurrentKillTeam;

                public static List<DWChapter> Chapters;

                public static DWAgent CreateNewAgent(string AgentName)
                {
                    DWAgent a = new DWAgent(AgentName);
                    CurrentKillTeam.Members.Add(a);
                    return a;
                }

                private static List<DWChapter> LoadChapters()
                {
                    return Dhrakk.Utility.LoadCustomObject<List<DWChapter>>(Dhrakk.Utility.GetDataPath() + "chapters.json");
                }

                static CurrentInstance()
                {
                    Chapters = LoadChapters();
                }
            }
            
            public class DWChapter
            {
                public string Name;
                public DWDemeanour Demeanour;

                public override string ToString()
                {
                    return Name;
                }
            }

            public class DWDemeanour
            {
                public string Name;
            }

            public class DWSkillAdvance
            {
                public enum DWAdvanceSource
                {
                    GeneralSpaceMarine = 1,
                    Deathwatch = 2,
                    Chapter = 3,
                    Specialty = 4
                }

                public enum DWAdvanceType
                {
                    Skill = 1,
                    Talent = 2
                }

                //This is how many times the skill advance can be taken.
                public int Multipier;
                //Where is the skill advance defined?
                public DWAdvanceSource AdvanceSource;
                //What type of skill advance
                public DWAdvanceType AdvanceType;
                //What rank you have to be to have this skill advance
                public DWRank RankRequirement;
                //Any skill advancements that are pre-requisites.
                public List<DWSkillAdvance> AdvancePrerequisites;
            }

            public class DWAgent
            {
                [JsonIgnoreAttribute]
                public DWAgentCheckbox AgentControl;

                public DWChapter Chapter;
                public DWDemeanour Demeanour;

                public int currentXP;

                private string _Name;

                private Guid uid;

                public DWAgent(string AgentName)
                {
                    // Create a new UID for the agent.
                    uid = Guid.NewGuid();
                    GenerateAgentControl();

                    Name = AgentName;

                    //AgentControl.Text = _Name;
                }

                public string Name
                {
                    get { return _Name; }
                    set
                    {
                        _Name = value;
                        AgentControl.Text = _Name;
                    }
                }

                public Guid UID { get { return this.uid; } }

                public override bool Equals(object SomeAgent)
                {
                    return ((DWAgent)SomeAgent).UID.Equals(this.UID);
                }

                public void GenerateAgentControl()
                {
                    // Create a new button for the agent. This will live below the details for the user. It will be an easy way to switch agents.
                    AgentControl = new DWAgentCheckbox();
                    AgentControl.Appearance = Appearance.Button;
                    AgentControl.Checked = true;
                    AgentControl.ParentAgent = this;
                    AgentControl.AutoCheck = false;
                }

                public override int GetHashCode()
                {
                    return int.Parse(UID.ToString());
                }

                public override string ToString()
                {
                    return _Name;
                }

                public class DWAgentCheckbox : CheckBox
                {
                    public DWAgent ParentAgent { get; set; }
                }
            }

            public class DWKillTeam
            {
                public List<DWAgent> Members;
                public string Name, FullName;
                public string SaveLocation;
                public DWAgent SquadLeader;
                public string Version = "0.0.0";
                public DWKillTeam(string Name)
                {
                    this.Name = Name;
                    Members = new List<DWAgent>();
                }

                public override string ToString()
                {
                    return Name;
                }
            }
        }

        public static class Utility
        { 

            public static UserResponse GetUserInput(string Prompt)
            {
                return new UserResponse(Prompt);
            }

            public static void Load(string FilePath)
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader sr = new StreamReader(FilePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    // TODO write logic to verify changes for versions.
                    Dhrakk.Game.DWKillTeam LoadedKT = serializer.Deserialize<Dhrakk.Game.DWKillTeam>(reader);

                    //Need to regenerate the checkbox controls for each newly loaded Agent.
                    foreach (Dhrakk.Game.DWAgent dw in LoadedKT.Members)
                    {
                        dw.GenerateAgentControl();
                    }

                    Dhrakk.Game.CurrentInstance.CurrentKillTeam = LoadedKT;
                }
            }

            public static void Save()
            {
                JsonSerializer serializer = new JsonSerializer();

                //ITraceWriter traceWriter = new MemoryTraceWriter();

                //var json = new JavaScriptSerializer().Serialize(Dhrakk.Game.Instance.CurrentKillTeam);

                //var json = JsonConvert.SerializeObject(Dhrakk.Game.Instance.CurrentKillTeam, new JsonSerializerSettings { TraceWriter = traceWriter });

                using (StreamWriter sw = new StreamWriter(Dhrakk.Game.CurrentInstance.CurrentKillTeam.SaveLocation))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, Dhrakk.Game.CurrentInstance.CurrentKillTeam);
                }
            }

            public static void SaveCustomObject<SomeObjectType>(SomeObjectType SomeObject, string FilePath)
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(FilePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, SomeObject);
                }
            }

            public static SomeObjectType LoadCustomObject<SomeObjectType>(string FilePath)
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader sr = new StreamReader(FilePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    // TODO write logic to verify changes for versions.
                    return serializer.Deserialize<SomeObjectType>(reader);
                }
            }

            public static string GetDataPath()
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"data\";
            }

            public class UserResponse
            {
                public bool Canceled = false;
                public string UserInput = "";

                private Form responseForm;

                public UserResponse(string Prompt)
                {
                    System.Drawing.Point pt;

                    responseForm = new Form();
                    responseForm.Height = 200;
                    responseForm.Width = 500;
                    responseForm.Text = Prompt;
                    responseForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    responseForm.MaximizeBox = false;
                    responseForm.MinimizeBox = false;

                    TextBox txtUserResponse = new TextBox();
                    txtUserResponse.TabIndex = 1;
                    pt = new System.Drawing.Point(14, (int)(responseForm.Height / 3));
                    txtUserResponse.Location = pt;
                    txtUserResponse.Width = responseForm.Width - 42;
                    txtUserResponse.TextChanged += new EventHandler(txtUserResponse_Changed);
                    txtUserResponse.KeyUp += new KeyEventHandler(txtUserResponsed_KeyPress);

                    Button btnCancel = new Button();
                    btnCancel.Text = "Cancel";
                    btnCancel.TabIndex = 3;
                    pt = new System.Drawing.Point(responseForm.Width - btnCancel.Width - 28, responseForm.Height - btnCancel.Height - 51);
                    btnCancel.Location = pt;
                    btnCancel.Click += new EventHandler(btnCancel_Click);

                    Button btnOK = new Button();
                    btnOK.Text = "OK";
                    btnOK.TabIndex = 2;
                    pt = new System.Drawing.Point(btnCancel.Left - btnOK.Width - 6, responseForm.Height - btnOK.Height - 51);
                    btnOK.Location = pt;
                    btnOK.Click += new EventHandler(btnOK_Click);
                    responseForm.AcceptButton = btnOK;

                    responseForm.Controls.Add(txtUserResponse);
                    responseForm.Controls.Add(btnOK);
                    responseForm.Controls.Add(btnCancel);

                    responseForm.ShowDialog();

                    if (responseForm.DialogResult == DialogResult.Cancel)
                        Canceled = true;
                }

                public override string ToString()
                {
                    return UserInput;
                }
                private void btnCancel_Click(object sender, EventArgs e)
                {
                    responseForm.DialogResult = DialogResult.Cancel;
                }

                private void btnOK_Click(object sender, EventArgs e)
                {
                    responseForm.DialogResult = DialogResult.OK;
                }

                private void txtUserResponse_Changed(object sender, EventArgs e)
                {
                    UserInput = ((TextBox)(sender)).Text;
                }
                private void txtUserResponsed_KeyPress(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        responseForm.DialogResult = DialogResult.OK;
                    }
                }
            }
        }
    }
}