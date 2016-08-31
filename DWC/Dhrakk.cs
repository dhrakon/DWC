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
                private static List<Chapter> Chapters = new List<Chapter>
                {
                    new Chapter() { Name =  "Black Templars" },
                    new Chapter() { Name =  "Blood Angels" },
                    new Chapter() { Name =  "Dark Angels" },
                    new Chapter() { Name =  "Space Wolves" },
                    new Chapter() { Name =  "Storm Wardens" },
                    new Chapter() { Name =  "Ultramarines" }
                };

                private static List<Rank> Ranks = new List<Rank>()
                {
                    new Rank() { rank = 1, minXP = 13000, maxXP = 16999 },
                    new Rank() { rank = 2, minXP = 17000, maxXP = 20999 },
                    new Rank() { rank = 3, minXP = 21000, maxXP = 24999 },
                    new Rank() { rank = 4, minXP = 25000, maxXP = 29999 },
                    new Rank() { rank = 5, minXP = 30000, maxXP = 34999 },
                    new Rank() { rank = 6, minXP = 35000, maxXP = 39999 },
                    new Rank() { rank = 7, minXP = 40000, maxXP = 44999 },
                    new Rank() { rank = 8, minXP = 45000, maxXP = 49999 }
                };

                public static Rank GetRankFromXP(int xp)
                {
                    foreach (Rank r in Ranks)
                    {
                        if (r.minXP <= xp && xp <= r.maxXP)
                        {
                            return r;
                        }
                    }
                    Exception RankNotFound = new Exception(string.Format("Error in GetRankFromXP, Rank does not exist for provided XP: {0}", xp));
                    throw RankNotFound;
                }

                public class Chapter
                {
                    public string Name;
                }
                public class Rank
                {
                    public int maxXP { get; set; }
                    public int minXP { get; set; }
                    public int rank { get; set; }
                }
            }

            public static class Instance
            {
                public static KillTeam CurrentKillTeam;

                public static DWAgent CreateNewAgent(string AgentName)
                {
                    DWAgent a = new DWAgent(AgentName);
                    CurrentKillTeam.Members.Add(a);
                    return a;
                }
            }

            public class DWAgent
            {
                [JsonIgnoreAttribute]
                public AgentCheckbox AgentControl;

                public Constants.Chapter Chapter;

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
                    AgentControl = new AgentCheckbox();
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

                public class AgentCheckbox : CheckBox
                {
                    public DWAgent ParentAgent { get; set; }
                }
            }

            public class KillTeam
            {
                public List<DWAgent> Members;
                public string Name, FullName;
                public string SaveLocation;
                public DWAgent SquadLeader;
                public string Version = "0.0.0";
                public KillTeam(string Name)
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
                    Dhrakk.Game.KillTeam LoadedKT = serializer.Deserialize<Dhrakk.Game.KillTeam>(reader);

                    //Need to regenerate the checkbox controls for each newly loaded Agent.
                    foreach (Dhrakk.Game.DWAgent dw in LoadedKT.Members)
                    {
                        dw.GenerateAgentControl();
                    }

                    Dhrakk.Game.Instance.CurrentKillTeam = LoadedKT;
                }
            }

            public static void Save()
            {
                JsonSerializer serializer = new JsonSerializer();

                //ITraceWriter traceWriter = new MemoryTraceWriter();

                //var json = new JavaScriptSerializer().Serialize(Dhrakk.Game.Instance.CurrentKillTeam);

                //var json = JsonConvert.SerializeObject(Dhrakk.Game.Instance.CurrentKillTeam, new JsonSerializerSettings { TraceWriter = traceWriter });

                using (StreamWriter sw = new StreamWriter(Dhrakk.Game.Instance.CurrentKillTeam.SaveLocation))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, Dhrakk.Game.Instance.CurrentKillTeam);
                }
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