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

            public static class CurrentInstance
            {
                public static List<DWChapter> Chapters;

                public static List<DWCharacteristic> Characteristics;

                public static DWKillTeam CurrentKillTeam;

                static CurrentInstance()
                {
                    Chapters = LoadChapters();
                    Characteristics = LoadCharacteristics();
                }

                public static DWAgent CreateNewAgent(string AgentName)
                {
                    DWAgent a = new DWAgent(AgentName);
                    CurrentKillTeam.Members.Add(a);
                    return a;
                }

                public static void Initialize() { }
                private static List<DWChapter> LoadChapters()
                {
                    return Dhrakk.Utility.LoadCustomObject<List<DWChapter>>(Dhrakk.Utility.GetDataPath() + "chapters.json");
                }
                private static List<DWCharacteristic> LoadCharacteristics()
                {
                    return Dhrakk.Utility.LoadCustomObject<List<DWCharacteristic>>(Dhrakk.Utility.GetDataPath() + "characteristics.json");
                }
            }

            public class DWAgent
            {
                [JsonIgnore]
                public DWAgentCheckbox AgentControl;

                public DWChapter Chapter;

                public List<DWAgentCharacteristic> Characteristics;

                public int currentXP;

                public DWDemeanour Demeanour;

                private string _Name;

                private Guid uid;

                public DWAgent()
                {
                    this.Characteristics = new List<DWAgentCharacteristic>();

                    foreach (DWCharacteristic characteristic in Dhrakk.Game.CurrentInstance.Characteristics)
                    {
                        DWAgent.DWAgentCharacteristic agentCharacteristic = new DWAgentCharacteristic(characteristic, 0);
                        this.Characteristics.Add(agentCharacteristic);
                    }
                }

                public DWAgent(string AgentName) : this()
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

                public class DWAgentCharacteristic
                {
                    DWCharacteristic Characteristic;
                    public DWAgentCharacteristic(DWCharacteristic Characteristic, int Value)
                    {
                        this.Characteristic = Characteristic;
                        this.Value = Value;
                    }

                    int _value;
                    int Value
                    {
                        get { return _value; }
                        set
                        {
                            if (value > 100)
                            {
                                _value = 100;
                            }
                            else if (value < 0)
                            {
                                _value = 0;
                            }
                            else
                            {
                                _value = value;
                            }
                        }
                    }
                }
                public class DWAgentCheckbox : CheckBox
                {
                    public DWAgent ParentAgent { get; set; }
                }
            }

            public class DWAttribute
            {
                public string Name;
                public int Value;
            }

            public class DWChapter
            {
                public DWDemeanour Demeanour;
                public string Name;
                public override string ToString()
                {
                    return Name;
                }
            }

            [JsonObject(IsReference = true)]
            public class DWCharacteristic
            {
                public string Name;
                public string ShortName;

                public DWCharacteristic() { }

                public DWCharacteristic(string Name, string ShortName) : this()
                {
                    this.Name = Name;
                    this.ShortName = ShortName;
                }

                public override string ToString()
                {
                    return Name;
                }
            }

            public class DWDemeanour
            {
                public string Name;
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

            public class DWPrerequisite
            {
                public List<object> Prerequisites;

                public DWPrerequisite()
                {
                    Prerequisites = new List<object>();
                }

                public DWPrerequisite(params object[] Prerequisites) : this()
                {
                    for (int i = 0; i < Prerequisites.Length; i++)
                    {
                        this.Prerequisites.Add(Prerequisites[i]);
                    }
                }
            }

            [JsonObject(IsReference = true)]
            public class DWRank
            {
                public int maxXP { get; set; }
                public int minXP { get; set; }
                public int rank { get; set; }
            }
            public class DWSkill
            {
                string Name;
            }

            [JsonObject(IsReference = true)]
            public class DWSkillAdvance
            {
                //Where is the skill advance defined?
                public DWAdvanceSource AdvanceSource;

                //What type of skill advance
                public DWAdvanceType AdvanceType;

                //This is how many times the skill advance can be taken.
                public int Multipier;

                //A list of objects containing the prerequisites for this skill advance.
                public List<object> Prerequisites;

                [JsonIgnore]
                private Guid uid;

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
                public Guid UID
                {
                    get { return uid; }
                }
            }
            public class DWSpecialty
            {
                public string Name;

                public List<DWSkillAdvance> SpecialtySkillAdvancements;

                public override string ToString()
                {
                    return Name;
                }
            }

            public class DWTalent
            {
                public string Name;
                
            }
        }

        public static class Utility
        {

            public static string GetDataPath()
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"data\";
            }

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