using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LordsAndKnightsBot
{
    public partial class Main : Form
    {
        #region Variables
        CookieContainer cookieContainer = new CookieContainer();
        Form extraWindow;
        bool formVisible = true;
        int totalSilver = 0;
        public List<String> log = new List<String>();
        public int newMessages = 0;
        int getHtmlTries = 1;
        bool messageshowing = false;
        public String playerAllianceId;

        public List<Diplomacy> diplomacies = new List<Diplomacy>();

        List<Building> buildings = new List<Building>() {
            new Building(){ name="Bergfried",   primaryKey=1,    maxLevel=10, engName="Keep",           timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,9,30)}, {2, new TimeSpan(0,19,0)}, {3, new TimeSpan(0,37,3)}, {4, new TimeSpan(1,10,24)}, {5, new TimeSpan(2,10,14)}, {6, new TimeSpan(3,54,25)}, {7, new TimeSpan(6,50,14)}, {8, new TimeSpan(11,37,23)}, {9, new TimeSpan(19,10,41)}, {10, new TimeSpan(30,41,6)} } },
            new Building(){ name="Holzfaeller", primaryKey=100,  maxLevel=30, engName="Lumberjack",     timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0, 2, 30)}, {2, new TimeSpan(0,3,0)}, {3, new TimeSpan(0,3,36)}, {4, new TimeSpan(0,4,21)}, {5, new TimeSpan(0,5,16)}, {6, new TimeSpan(0,6,23)}, {7, new TimeSpan(0,7,47)}, {8, new TimeSpan(0,9,30)}, {9, new TimeSpan(0,11,35)}, {10, new TimeSpan(0,14,15)}, {11, new TimeSpan(0,17,31)}, {12, new TimeSpan(0,21,44)}, {13, new TimeSpan(0,26,56)}, {14, new TimeSpan(0,33,40)}, {15, new TimeSpan(0,42,6)}, {16, new TimeSpan(0,53,2)}, {17, new TimeSpan(1,6,50)}, {18, new TimeSpan(1,24,52)}, {19, new TimeSpan(1,47,47)}, {20, new TimeSpan(2,17,58)}, {21, new TimeSpan(2,56,36)}, {22, new TimeSpan(3,46,3)}, {23, new TimeSpan(4,51,36)}, {24, new TimeSpan(6,16,9)}, {25, new TimeSpan(8,5,15)}, {26, new TimeSpan(10,15,58)}, {27, new TimeSpan(13,33,45)}, {28, new TimeSpan(17,37,53)}, {29, new TimeSpan(22,55,15)}, {30, new TimeSpan(29,47,49)} } },
            new Building(){ name="Holzlager",   primaryKey=200,  maxLevel=20, engName="Woodstore",      timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0, 1, 30)}, {2, new TimeSpan(0,2, 6)}, {3, new TimeSpan(0,2,56)}, {4, new TimeSpan(0,4,7)}, {5, new TimeSpan(0,5,43)}, {6, new TimeSpan(0, 7,57)}, {7, new TimeSpan(0,10,58)}, {8, new TimeSpan(0,15,9)}, {9, new TimeSpan(0,20,45)}, {10, new TimeSpan(0,28,26)}, {11, new TimeSpan(0,38,40)}, {12, new TimeSpan(0,52,35)}, {13, new TimeSpan(1,10,29)}, {14, new TimeSpan(1,35,49)}, {15, new TimeSpan(2,8,24)}, {16, new TimeSpan(2,52,3)}, {17, new TimeSpan(3,48,50)}, {18, new TimeSpan(5,4,21)}, {19, new TimeSpan(6,41,44)}, {20, new TimeSpan(8,50,17)} } },
            new Building(){ name="Steinbruch",  primaryKey=300,  maxLevel=30, engName="Quarry",         timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,2,42)}, {2, new TimeSpan(0,3,14)}, {3, new TimeSpan(0,3,53)}, {4, new TimeSpan(0,4,42)}, {5, new TimeSpan(0,5,42)}, {6, new TimeSpan(0,6,53)}, {7, new TimeSpan(0,8,24)}, {8, new TimeSpan(0,10,15)}, {9, new TimeSpan(0,12,30)}, {10, new TimeSpan(0,15,23)}, {11, new TimeSpan(0,18,55)}, {12, new TimeSpan(0,23,28)}, {13, new TimeSpan(0,29,6)}, {14, new TimeSpan(0,36,22)}, {15, new TimeSpan(0,45,28)}, {16, new TimeSpan(0,57,17)}, {17, new TimeSpan(1,12,10)}, {18, new TimeSpan(1,31,40)}, {19, new TimeSpan(1,56,24)}, {20, new TimeSpan(2,29,0)}, {21, new TimeSpan(3,10,49)}, {22, new TimeSpan(4,4,8)}, {23, new TimeSpan(5,14,55)}, {24, new TimeSpan(6,46,15)}, {25, new TimeSpan(8,44,4)}, {26, new TimeSpan(11,16,2)}, {27, new TimeSpan(14,38,51)}, {28, new TimeSpan(19,2,31)}, {29, new TimeSpan(24,45,16)}, {30, new TimeSpan(32,10,50)} } },
            new Building(){ name="Steinlager",  primaryKey=400,  maxLevel=20, engName="Stonestore",     timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,1,30)}, {2, new TimeSpan(0,2,6)}, {3, new TimeSpan(0,2,56)}, {4, new TimeSpan(0,4,7)}, {5, new TimeSpan(0,5,43)}, {6, new TimeSpan(0,7,57)}, {7, new TimeSpan(0,10,58)}, {8, new TimeSpan(0,15,9)}, {9, new TimeSpan(0,20,45)}, {10, new TimeSpan(0,28,26)}, {11, new TimeSpan(0,38,40)}, {12, new TimeSpan(0,52,35)}, {13, new TimeSpan(1,10,29)}, {14, new TimeSpan(1,35,49)}, {15, new TimeSpan(2,8,24)}, {16, new TimeSpan(2,52,3)}, {17, new TimeSpan(3,48,50)}, {18, new TimeSpan(5,4,21)}, {19, new TimeSpan(6,41,44)}, {20, new TimeSpan(8,50,17)} } },
            new Building(){ name="Erzmine",     primaryKey=500,  maxLevel=30, engName="Oremine",        timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,3,0)}, {2, new TimeSpan(0,3,36)}, {3, new TimeSpan(0,4,19)}, {4, new TimeSpan(0,5,14)}, {5, new TimeSpan(0,6,19)}, {6, new TimeSpan(0,7,39)}, {7, new TimeSpan(0,9,20)}, {8, new TimeSpan(0,11,23)}, {9, new TimeSpan(0,13,54)}, {10, new TimeSpan(0,17,6)}, {11, new TimeSpan(0,21,1)}, {12, new TimeSpan(0,26,4)}, {13, new TimeSpan(0,32,20)}, {14, new TimeSpan(0,40,25)}, {15, new TimeSpan(0,50,31)}, {16, new TimeSpan(1,30,39)}, {17, new TimeSpan(1,20,12)}, {18, new TimeSpan(1,41,51)}, {19, new TimeSpan(2,9,21)}, {20, new TimeSpan(2,45,34)}, {21, new TimeSpan(3,31,55)}, {22, new TimeSpan(4,31,15)}, {23, new TimeSpan(5,49,55)}, {24, new TimeSpan(7,31,23)}, {25, new TimeSpan(9,42,17)}, {26, new TimeSpan(12,31,19)}, {27, new TimeSpan(16,16,30)}, {28, new TimeSpan(21,9,27)}, {29, new TimeSpan(27,30,17)}, {30, new TimeSpan(35,45,23)} } },
            new Building(){ name="Erzlager",    primaryKey=600,  maxLevel=20, engName="Orestore",       timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0, 1, 30)}, {2, new TimeSpan(0,2, 6)}, {3, new TimeSpan(0,2,56)}, {4, new TimeSpan(0,4,7)}, {5, new TimeSpan(0,5,43)}, {6, new TimeSpan(0, 7,57)}, {7, new TimeSpan(0,10,58)}, {8, new TimeSpan(0,15,9)}, {9, new TimeSpan(0,20,45)}, {10, new TimeSpan(0,28,26)}, {11, new TimeSpan(0,38,40)}, {12, new TimeSpan(0,52,35)}, {13, new TimeSpan(1,10,29)}, {14, new TimeSpan(1,35,49)}, {15, new TimeSpan(2,8,24)}, {16, new TimeSpan(2,52,3)}, {17, new TimeSpan(3,48,50)}, {18, new TimeSpan(5,4,21)}, {19, new TimeSpan(6,41,44)}, {20, new TimeSpan(8,50,17)} } },
            new Building(){ name="Bauernhof",   primaryKey=800,  maxLevel=30, engName="Farm",           timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,2,18)}, {2, new TimeSpan(0,2,39)}, {3, new TimeSpan(0,3,4)}, {4, new TimeSpan(0,3,34)}, {5, new TimeSpan(0,4,10)}, {6, new TimeSpan(0,4,52)}, {7, new TimeSpan(0,5,45)}, {8, new TimeSpan(0,6,50)}, {9, new TimeSpan(0,8,13)}, {10, new TimeSpan(0,9,56)}, {11, new TimeSpan(0,12,7)}, {12, new TimeSpan(0,14,54)}, {13, new TimeSpan(0,18,29)}, {14, new TimeSpan(0,23,6)}, {15, new TimeSpan(0,29,7)}, {16, new TimeSpan(0,36,58)}, {17, new TimeSpan(0,47,19)}, {18, new TimeSpan(1,1,3)}, {19, new TimeSpan(1,19,22)}, {20, new TimeSpan(1,43,58)}, {21, new TimeSpan(2,17,14)}, {22, new TimeSpan(3,2,31)}, {23, new TimeSpan(4,2,45)}, {24, new TimeSpan(5,25,18)}, {25, new TimeSpan(7,19,9)}, {26, new TimeSpan(9,57,14)}, {27, new TimeSpan(13,38,13)}, {28, new TimeSpan(18,49,9)}, {29, new TimeSpan(26,20,48)}, {30, new TimeSpan(39,31,12)} } },
            new Building(){ name="Markt",       primaryKey=900,  maxLevel=8,  engName="Market",         timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,6,0)}, {2, new TimeSpan(0,11,42)}, {3, new TimeSpan(0,23,24)}, {4, new TimeSpan(0,47,58)}, {5, new TimeSpan(1,40,44)}, {6, new TimeSpan(3,36,35)}, {7, new TimeSpan(7,56,29)}, {8, new TimeSpan(17,52,6)} } },
            new Building(){ name="Bibliothek",  primaryKey=1000, maxLevel=10, engName="Library",        timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,12,30)}, {2, new TimeSpan(0,19,23)}, {3, new TimeSpan(0,31,0)}, {4, new TimeSpan(0,51,9)}, {5, new TimeSpan(1,26,57)}, {6, new TimeSpan(2,32,10)}, {7, new TimeSpan(4,33,54)}, {8, new TimeSpan(8,26,44)}, {9, new TimeSpan(16,2,47)}, {10, new TimeSpan(31,17,26)} } },
            new Building(){ name="Taverne",     primaryKey=1100, maxLevel=10, engName="Tavern",         timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,8,0)}, {2, new TimeSpan(0,11,12)}, {3, new TimeSpan(0,16,48)}, {4, new TimeSpan(0,26,53)}, {5, new TimeSpan(0,45,42)}, {6, new TimeSpan(1,22,15)}, {7, new TimeSpan(2,36,17)}, {8, new TimeSpan(5,12,34)}, {9, new TimeSpan(10,15,7)}, {10, new TimeSpan(20,50,15)} } },
            new Building(){ name="Wehranlagen", primaryKey=1200, maxLevel=20, engName="Fortifications", timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,4,0)}, {2, new TimeSpan(0,5,14)}, {3, new TimeSpan(0,6,55)}, {4, new TimeSpan(0,9,12)}, {5, new TimeSpan(0,12,20)}, {6, new TimeSpan(0,16,38)}, {7, new TimeSpan(0,22,38)}, {8, new TimeSpan(0,31,0)}, {9, new TimeSpan(0,42,47)}, {10, new TimeSpan(0,59,29)}, {11, new TimeSpan(1,23,16)}, {12, new TimeSpan(1,57,24)}, {13, new TimeSpan(2,46,43)}, {14, new TimeSpan(3,58,24)}, {15, new TimeSpan(5,43,18)}, {16, new TimeSpan(8,17,48)}, {17, new TimeSpan(12,6,17)}, {18, new TimeSpan(17,48,22)}, {19, new TimeSpan(26,21,11)}, {20, new TimeSpan(39,15,58)} } },
            new Building(){ name="Zeughaus",    primaryKey=1300, maxLevel=30, engName="Arsenal",        timeForLevel= new Dictionary<int, TimeSpan>() { {1, new TimeSpan(0,7,0)}, {2, new TimeSpan(0,8,3)}, {3, new TimeSpan(0,9,20)}, {4, new TimeSpan(0,10,50)}, {5, new TimeSpan(0,12,40)}, {6, new TimeSpan(0,14,50)}, {7, new TimeSpan(0,17,30)}, {8, new TimeSpan(0,20,39)}, {9, new TimeSpan(0,24,34)}, {10, new TimeSpan(0,29,14)}, {11, new TimeSpan(0,35,5)}, {12, new TimeSpan(0,42,6)}, {13, new TimeSpan(0,50,57)}, {14, new TimeSpan(1,1,39)}, {15, new TimeSpan(1,15,12)}, {16, new TimeSpan(1,31,45)}, {17, new TimeSpan(1,52,51)}, {18, new TimeSpan(2,18,48)}, {19, new TimeSpan(2,52,7)}, {20, new TimeSpan(3,33,26)}, {21, new TimeSpan(4,26,47)}, {22, new TimeSpan(5,33,29)}, {23, new TimeSpan(7,0,11)}, {24, new TimeSpan(8,49,26)}, {25, new TimeSpan(11,12,23)}, {26, new TimeSpan(14,13,55)}, {27, new TimeSpan(18,13,1)}, {28, new TimeSpan(23,19,4)}, {29, new TimeSpan(30,4,47)}, {30, new TimeSpan(38,48,11)} } }
        };
        List<Knowledge> knowledges = new List<Knowledge>() { 
            new Knowledge(){ Name="Longbow",        PrimaryKey=0, RequiredLibraryLevel=1,   WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(1,0,0) },
            new Knowledge(){ Name="Crop Rotation",  PrimaryKey=0, RequiredLibraryLevel=1,   WoodCost=640, StoneCost=320, OreCost=640, PopulationCost=0, Time=new TimeSpan(8,0,0) },
            new Knowledge(){ Name="Yoke",           PrimaryKey=0, RequiredLibraryLevel=1,   WoodCost=840, StoneCost=840, OreCost=1120, PopulationCost=0, Time=new TimeSpan(12,0,0) },
            new Knowledge(){ Name="Cellar Storeroom", PrimaryKey=0, RequiredLibraryLevel=1, WoodCost=1200, StoneCost=2000, OreCost=800, PopulationCost=0, Time=new TimeSpan(16,0,0) },
            new Knowledge(){ Name="Stirrup",        PrimaryKey=2, RequiredLibraryLevel=2,   WoodCost=120, StoneCost=120, OreCost=160, PopulationCost=2, Time=new TimeSpan(2,0,0) },
            new Knowledge(){ Name="Weaponsmith",    PrimaryKey=0, RequiredLibraryLevel=3,   WoodCost=200, StoneCost=500, OreCost=300, PopulationCost=3, Time=new TimeSpan(3,0,0) },
            new Knowledge(){ Name="Armoursmith",    PrimaryKey=0, RequiredLibraryLevel=3,   WoodCost=400, StoneCost=100, OreCost=500, PopulationCost=3, Time=new TimeSpan(4,0,0) },
            new Knowledge(){ Name="Beer Tester",    PrimaryKey=0, RequiredLibraryLevel=3,   WoodCost=400, StoneCost=300, OreCost=400, PopulationCost=3, Time=new TimeSpan(5,0,0) },
            new Knowledge(){ Name="Swordsmith",     PrimaryKey=0, RequiredLibraryLevel=4,   WoodCost=420, StoneCost=280, OreCost=700, PopulationCost=4, Time=new TimeSpan(6,0,0) },
            new Knowledge(){ Name="Iron Hardening", PrimaryKey=0, RequiredLibraryLevel=4,   WoodCost=900, StoneCost=600, OreCost=1500, PopulationCost=4, Time=new TimeSpan(13,0,0) },
            new Knowledge(){ Name="Crossbow",       PrimaryKey=0, RequiredLibraryLevel=5,   WoodCost=1000, StoneCost=200, OreCost=800, PopulationCost=5, Time=new TimeSpan(10,0,0) },
            new Knowledge(){ Name="Poison Arrows",  PrimaryKey=0, RequiredLibraryLevel=6,   WoodCost=1100, StoneCost=220, OreCost=880, PopulationCost=6, Time=new TimeSpan(15,0,0) },
            new Knowledge(){ Name="Horse Breeding", PrimaryKey=0, RequiredLibraryLevel=6,   WoodCost=720, StoneCost=720, OreCost=960, PopulationCost=6, Time=new TimeSpan(18,0,0) },
            new Knowledge(){ Name="Weapons Manufacturing", PrimaryKey=0, RequiredLibraryLevel=7, WoodCost=1300, StoneCost=260, OreCost=1040, PopulationCost=7, Time=new TimeSpan(7,0,0) },
            new Knowledge(){ Name="Horse Armour",   PrimaryKey=0, RequiredLibraryLevel=7,   WoodCost=900, StoneCost=600, OreCost=1500, PopulationCost=7, Time=new TimeSpan(17,0,0) },
            new Knowledge(){ Name="Wheelbarrow",    PrimaryKey=0, RequiredLibraryLevel=8,   WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(14,0,0) },
            new Knowledge(){ Name="Flaming Arrows", PrimaryKey=0, RequiredLibraryLevel=8,   WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(9,0,0) },
            new Knowledge(){ Name="Blacksmith",     PrimaryKey=0, RequiredLibraryLevel=9,   WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(11,0,0) },
            new Knowledge(){ Name="Map of Area",    PrimaryKey=0, RequiredLibraryLevel=10,  WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(20,0,0) },
            new Knowledge(){ Name="Cistern",        PrimaryKey=0, RequiredLibraryLevel=10,  WoodCost=120, StoneCost=20, OreCost=60, PopulationCost=1, Time=new TimeSpan(21,0,0) }
        };
        List<Unit> units = new List<Unit>() { 
            new Unit(){ Name="Archer", PrimaryKey=101, WoodCost=27, StoneCost=12, OreCost=39, PopulationCost=1, TimeForField=TimeSpan.FromMinutes(8.3), TransportCapacity=16 },
            new Unit(){ Name="Armoured Horseman", PrimaryKey=201, WoodCost=25, StoneCost=15, OreCost=45, PopulationCost=2, TimeForField=TimeSpan.FromMinutes(5), TransportCapacity=22 },
            new Unit(){ Name="Crossbowman", PrimaryKey=102, WoodCost=50, StoneCost=28, OreCost=55, PopulationCost=13, TimeForField=TimeSpan.FromMinutes(10), TransportCapacity=13 },
            new Unit(){ Name="Handcart", PrimaryKey=10001, WoodCost=45, StoneCost=25, OreCost=30, PopulationCost=1, TimeForField=TimeSpan.FromMinutes(13.3), TransportCapacity=500 },
            new Unit(){ Name="Lancer Horseman", PrimaryKey=202, WoodCost=55, StoneCost=45, OreCost=110, PopulationCost=2, TimeForField=TimeSpan.FromMinutes(6.7), TransportCapacity=20 },
            new Unit(){ Name="Ox Cart", PrimaryKey=10002, WoodCost=95, StoneCost=40, OreCost=65, PopulationCost=3, TimeForField=TimeSpan.FromMinutes(16.7), TransportCapacity=2500 },
            new Unit(){ Name="Spearman", PrimaryKey=1, WoodCost=18, StoneCost=6, OreCost=30, PopulationCost=1, TimeForField=TimeSpan.FromMinutes(11.7), TransportCapacity=12 },
            new Unit(){ Name="Swordsman", PrimaryKey=2, WoodCost=43, StoneCost=20, OreCost=48, PopulationCost=1, TimeForField=TimeSpan.FromMinutes(13.3), TransportCapacity=10 }
        };
        //   Constellation the script should create: 
        //   1000 1200 1200 200 100 100 0 22
        const String KEEP = "Bergfried";
        const String LUMBERJACK = "Holzfaeller";
        const String WOODSTORE = "Holzlager";
        const String QUARRY = "Steinbruch";
        const String STONESTORE = "Steinlager";
        const String OREMINE = "Erzmine";
        const String ORESTORE = "Erzlager";
        const String FARM = "Bauernhof";
        const String MARKET = "Markt";
        const String LIBRARY = "Bibliothek";
        const String TAVERN = "Taverne";
        const String FORTIFICATIONS = "Wehranlagen";
        const String ARSENAL = "Zeughaus";

        List<Castle> castles = new List<Castle>();
        public List<Action> actions;
        List<MessageAtTime> timedMessages = new List<MessageAtTime>();
        #endregion

        public Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                MessageBox.Show("Another instance of the L&K Bot is already running.");
                Environment.Exit(0);
            }

            InitializeComponent();

            Rectangle rect = new Rectangle(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height);
            tabControl1.Region = new Region(rect);

            this.Width = 485;
            this.Height = 260;

            txtBoxEmail.Text = Properties.Settings.Default.email;
            txtBoxPassword.Text = Properties.Settings.Default.password;
            checkBoxAutoLogin.Checked = Properties.Settings.Default.autoLogin;

            actions = loadActions();
            extraWindow = new Messages(this);
            dropDownAction.SelectedIndex = 0;
            txtBoxEmail.Focus();

            if (checkBoxAutoLogin.Checked) login(txtBoxEmail.Text, txtBoxPassword.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveActions();
            Properties.Settings.Default.Save();

            e.Cancel = true;
            this.Hide();
            extraWindow.Hide();
            formVisible = false;
        }

        private void updateCastleInfo(Castle c)
        {
            if (c != null)
            {
                lblCopper.Text = c.copper;
                lblSilver.Text = c.silver;
                lblPeople.Text = (Convert.ToInt32(c.peopleStoreAmount) - Convert.ToInt32(c.people)).ToString();
                lblWood.Text = c.wood;
                lblStone.Text = c.stone;
                lblIron.Text = c.ore;

                if (Convert.ToInt32(c.copper) == Convert.ToInt32(c.copperStoreAmount)) lblCopper.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.copper) > Convert.ToInt32(c.copperStoreAmount) * 0.8) lblCopper.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblCopper.ForeColor = Color.Sienna;

                if (Convert.ToInt32(c.silver) == Convert.ToInt32(c.silverStoreAmount)) lblSilver.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.silver) > Convert.ToInt32(c.silverStoreAmount) * 0.8) lblSilver.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblSilver.ForeColor = Color.Silver;

                if (Convert.ToInt32(c.wood) == Convert.ToInt32(c.woodStoreAmount)) lblWood.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.wood) > Convert.ToInt32(c.woodStoreAmount) * 0.8) lblWood.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblWood.ForeColor = Color.Black;

                if (Convert.ToInt32(c.stone) == Convert.ToInt32(c.stoneStoreAmount)) lblStone.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.stone) > Convert.ToInt32(c.stoneStoreAmount) * 0.8) lblStone.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblStone.ForeColor = Color.Black;

                if (Convert.ToInt32(c.ore) == Convert.ToInt32(c.oreStoreAmount)) lblIron.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.ore) > Convert.ToInt32(c.oreStoreAmount) * 0.8) lblIron.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblIron.ForeColor = Color.Black;

                if (Convert.ToInt32(c.people) == Convert.ToInt32(c.peopleStoreAmount)) lblPeople.ForeColor = Properties.Settings.Default.ResourceFull;
                else if (Convert.ToInt32(c.people) > Convert.ToInt32(c.peopleStoreAmount) * 0.8) lblPeople.ForeColor = Properties.Settings.Default.ResourceAlmostFull;
                else lblPeople.ForeColor = Color.Black;


                label17.Text = c.KeepLevel.ToString();
                label18.Text = c.ArsenalLevel.ToString();
                label19.Text = c.TavernLevel.ToString();
                label20.Text = c.LibraryLevel.ToString();
                label21.Text = c.FortificationsLevel.ToString();
                label22.Text = c.MarketLevel.ToString();
                label23.Text = c.FarmLevel.ToString();
                label24.Text = c.LumberjackLevel.ToString();
                label25.Text = c.WoodStoreLevel.ToString();
                label26.Text = c.QuarryLevel.ToString();
                label27.Text = c.StoneStoreLevel.ToString();
                label28.Text = c.OreMineLevel.ToString();
                label29.Text = c.OreStoreLevel.ToString();

                labelSpearmen.Text = c.spearmen;
                labelSwordsmen.Text = c.swordsmen;
                labelArchers.Text = c.archers;
                labelCrossbowmen.Text = c.crossbowmen;
                labelScorpion.Text = c.scorpions;
                labelLancer.Text = c.lancers;
                labelCart.Text = c.pushCarts;
                labelOxCart.Text = c.oxCarts;

                if (c.KeepLevel < buildings.Find(delegate(Building b) { return b.name == KEEP; }).maxLevel)
                {
                    button2.Enabled = true;
                }
                else button2.Enabled = false;
                if (c.ArsenalLevel < buildings.Find(delegate(Building b) { return b.name == ARSENAL; }).maxLevel)
                {
                    button3.Enabled = true;
                }
                else button3.Enabled = false;
                if (c.TavernLevel < buildings.Find(delegate(Building b) { return b.name == TAVERN; }).maxLevel)
                {
                    button4.Enabled = true;
                }
                else button4.Enabled = false;
                if (c.LibraryLevel < buildings.Find(delegate(Building b) { return b.name == LIBRARY; }).maxLevel)
                {
                    button5.Enabled = true;
                }
                else button5.Enabled = false;
                if (c.FortificationsLevel < buildings.Find(delegate(Building b) { return b.name == FORTIFICATIONS; }).maxLevel)
                {
                    button6.Enabled = true;
                }
                else button6.Enabled = false;
                if (c.MarketLevel < buildings.Find(delegate(Building b) { return b.name == MARKET; }).maxLevel)
                {
                    button7.Enabled = true;
                }
                else button7.Enabled = false;
                if (c.FarmLevel < buildings.Find(delegate(Building b) { return b.name == FARM; }).maxLevel)
                {
                    button13.Enabled = true;
                }
                else button13.Enabled = false;
                if (c.LumberjackLevel < buildings.Find(delegate(Building b) { return b.name == LUMBERJACK; }).maxLevel)
                {
                    button12.Enabled = true;
                }
                else button12.Enabled = false;
                if (c.WoodStoreLevel < buildings.Find(delegate(Building b) { return b.name == WOODSTORE; }).maxLevel)
                {
                    button11.Enabled = true;
                }
                else button11.Enabled = false;
                if (c.QuarryLevel < buildings.Find(delegate(Building b) { return b.name == QUARRY; }).maxLevel)
                {
                    button10.Enabled = true;
                }
                else button10.Enabled = false;
                if (c.StoneStoreLevel < buildings.Find(delegate(Building b) { return b.name == STONESTORE; }).maxLevel)
                {
                    button9.Enabled = true;
                }
                else button9.Enabled = false;
                if (c.OreMineLevel < buildings.Find(delegate(Building b) { return b.name == OREMINE; }).maxLevel)
                {
                    button8.Enabled = true;
                }
                else button8.Enabled = false;
                if (c.OreStoreLevel < buildings.Find(delegate(Building b) { return b.name == ORESTORE; }).maxLevel)
                {
                    button19.Enabled = true;
                }
                else button19.Enabled = false;

                lblLocation.Text = "Map Location: [" + c.x + ", " + c.y + "]";

                listBoxUpgrades.Items.Clear();
                foreach (Upgrade u in c.upgrades)
                {
                    listBoxUpgrades.Items.Add(u.Building.engName + "(" + u.Level + ") - " + (DateTime.Now + u.Time).ToString());
                }
            }
        }

        private void Log(String Message)
        {
            log.Add(DateTime.Now + " - " + Message);
        }

        private void MessageOrPopUp(String Message, MessageBoxIcon Icon=MessageBoxIcon.Information)
        {
            System.Media.SystemSounds.Beep.Play();
            if (formVisible && !messageshowing)
            {
                messageshowing = true;
                MessageBox.Show(Message, "L&K Bot", MessageBoxButtons.OK, Icon, MessageBoxDefaultButton.Button1);
                messageshowing = false;
            }
            else
            {
                ToolTipIcon icon;
                switch(Icon) {
                    case MessageBoxIcon.Error:
                        icon = ToolTipIcon.Error;
                        break;
                    case MessageBoxIcon.Warning:
                        icon = ToolTipIcon.Warning;
                        break;
                    case MessageBoxIcon.Information:
                        icon = ToolTipIcon.Info;
                        break;
                    default:
                        icon = ToolTipIcon.None;
                        break;
                }
                notifyIcon.ShowBalloonTip(2500, "L&K Bot", Message, icon);
            }
            Log(Message);
        }

        public void recruitUnits(Castle castle, Unit unit, int amount)
        {
            if (amount > 0)
            {
                String response = GetHtml(Properties.Settings.Default.world + "/wa/HabitatAction/buildUnit?callback=&primaryKey=" + unit.PrimaryKey.ToString() + "&orderAmount=" + amount.ToString() + "&paymentGranted=false&habitatID=" + castle.id + "&PropertyListVersion=3");
                if (!response.Contains("\"error\""))
                {
                    String unitName = unit.Name;
                    if (unitName.EndsWith("man"))
                    {
                        unitName.Replace("man", "men");
                    }
                    else unitName += "s";
                    MessageOrPopUp("Recruiting " + amount.ToString() + " " + unitName + " in " + castle.name + ".");
                    castle.wood = (Convert.ToInt32(castle.wood) - (amount * unit.WoodCost)).ToString();
                    castle.stone = (Convert.ToInt32(castle.stone) - (amount * unit.StoneCost)).ToString();
                    castle.ore = (Convert.ToInt32(castle.ore) - (amount * unit.OreCost)).ToString();
                }
                else Log("Error while trying to recruit " + amount.ToString() + " " + unit.Name + " in " + castle.name + ".");
            }
        }

        private Double distance(double x1, double y1, double x2, double y2)
        {
            Double ax = x1 - Math.Floor(y1 / 2);
            Double ay = x1 + Math.Ceiling(y1 / 2);
            Double bx = x2 - Math.Floor(y2 / 2);
            Double by = x2 + Math.Ceiling(y2 / 2);
            Double dx = bx - ax;
            Double dy = by - ay;
            Double distance = 0;
            if (Math.Sign(dx) == Math.Sign(dy))
            {
                distance = Math.Max(Math.Abs(dx), Math.Abs(dy));
            }
            else
            {
                distance = Math.Abs(dx) + Math.Abs(dy);
            }
            return distance;
        }

        private String computeHash(String input)
        {
            String ret = BitConverter.ToString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "");
            return ret.ToLower();
        }

        public string GetHtml(string Url)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            StreamReader sr;

            request = (HttpWebRequest)HttpWebRequest.Create(Url);
            request.Referer = "";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 10000;
            request.CookieContainer = cookieContainer;
            if (Properties.Settings.Default.proxyEnabled)
            {
                WebProxy proxy = new WebProxy(Properties.Settings.Default.proxyIp, Properties.Settings.Default.proxyPort);
                request.Proxy = proxy;
            }

            request.Method = "GET";
            request.UserAgent = Properties.Settings.Default.useragent;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());
                string html = sr.ReadToEnd();
                sr.Close();
                response.Close();
                getHtmlTries = 1;

                return html;
            }
            catch (Exception ex)
            {
                if (getHtmlTries <= 3)
                {
                    System.Threading.Thread.Sleep(200);
                    return GetHtml(Url);
                }
                else
                {
                    MessageOrPopUp("Error while connecting." + Environment.NewLine + ex.Message);
                    getHtmlTries = 1;
                    return "\"Error\"";
                }
            }
        }

        private void login(String username, String password)
        {
            Properties.Settings.Default.email = username;
            Properties.Settings.Default.password = password;
            Properties.Settings.Default.autoLogin = checkBoxAutoLogin.Checked;

            password = computeHash(password);

            GetHtml("http://ios-hh.lordsandknights.com/XYRALITY/WebObjects/BKLoginServer.woa/wa/LoginAction/checkValidLogin?callback=&login=" + username + "&password=" + password + "&isDoubleOptIn=true");

            String response = GetHtml("http://ios-hh.lordsandknights.com/XYRALITY/WebObjects/BKLoginServer.woa/wa/worlds?callback=&login=" + username + "&password=" + password + "&deviceId=" + username + "&deviceType=Email");
            response = response.TrimStart('(').TrimEnd(')').Replace("=", ":");
            dynamic data = JsonConvert.DeserializeObject(response);

            if (data["loginConnectedWorlds"] != null)
            {
                listBoxWorlds.Items.Clear();
                foreach (var v in data["loginConnectedWorlds"])
                {
                    String name = String.Empty;
                    String url = String.Empty;
                    for (int i = 0; i < data["allAvailableWorlds"].Count; i++)
                    {
                        if (data["allAvailableWorlds"][i]["id"] == v["id"])
                        {
                            name = data["allAvailableWorlds"][i]["name"];
                            url = data["allAvailableWorlds"][i]["url"];
                        }
                    }
                    listBoxWorlds.Items.Add(name + " (" + v["worldStatus"]["description"] + ") [" + url + "] <" + v["id"] + ">");
                }
            }

            if (response.Contains("error") || !response.Contains("loginConnectedWorlds"))
            {
                label3.Text = "An error occured. Please check your inputs.";
            }
            else
            {
                tabControl1.SelectedIndex++;
            }
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login(txtBoxEmail.Text, txtBoxPassword.Text);
        }

        private void sessionExpired()
        {
            tabControl1.SelectedIndex = 0;
            label3.Text = "Your session expired. Please login again.";
            this.Width = 485;
            this.Height = 260;
            secondsTimer.Enabled = false;
            randomTimer.Enabled = false;
            this.Text = "L&K Bot - Not logged in.";
            extraWindow.Hide();
            MessageOrPopUp("Your session expired. Please login again.");
        }

        private void listBoxWorlds_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxWorlds.SelectedItem != null)
            {
                Properties.Settings.Default.world = Regex.Match(listBoxWorlds.SelectedItem.ToString(), @"\[([^)]*)\]").Groups[1].Value;
                Properties.Settings.Default.world_id = Regex.Match(listBoxWorlds.SelectedItem.ToString(), @"\<([^)]*)\>").Groups[1].Value;
                Properties.Settings.Default.world_name = listBoxWorlds.SelectedItem.ToString().Remove(listBoxWorlds.SelectedItem.ToString().Length - (Properties.Settings.Default.world.Length + Properties.Settings.Default.world_id.Length + 6), Properties.Settings.Default.world.Length + Properties.Settings.Default.world_id.Length + 6);
                String response = GetHtml(Properties.Settings.Default.world + "/wa/LoginAction/connectBrowser?callback=&login=" + Properties.Settings.Default.email + "&password=" + computeHash(Properties.Settings.Default.password) + "&worldId=" + Properties.Settings.Default.world_id + "&logoutUrl=http%3A%2F%2Flordsandknights.com&PropertyListVersion=3");
                response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
                if (response.Contains("error") && !response.Contains("player"))
                {
                    sessionExpired();
                }
                else
                {
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(response);
                    Properties.Settings.Default.user_id = data["player"]["id"];
                    this.Text = "L&K Bot - Connected to " + Properties.Settings.Default.world_name;
                    labelName.Text = data["player"]["nick"];
                    linkLabel4.Text = "Messages";

                    dropDownCastles.Items.Clear();
                    castles.Clear();
                    totalSilver = 0;

                    playerAllianceId = data["player"]["alliance"]["id"];
                    foreach (var v in data["player"]["alliance"]["diplomacyToArray"])
                    {
                        diplomacies.Add(new Diplomacy() { Name = v["name"], Id = v["id"], Relation = v["relationship"] });
                    }

                    foreach (var v in data["player"]["habitatDictionary"])
                    {
                        String spearmen, swordsmen, archers, crossbows, scorpions, lancers, pushcarts, oxcarts, name;
                        List<Upgrade> ugrades = new List<Upgrade>();
                        bool isRecruiting = false;
                        int spearmen_i = 0;
                        int swordsmen_i = 0;
                        int archers_i = 0;
                        int crossbows_i = 0;
                        int scorpions_i = 0;
                        int lancers_i = 0;
                        int pushcarts_i = 0;
                        int oxcarts_i = 0;

                        if (v.First["habitatUnitOrderArray"].Count > 0) isRecruiting = true;

                        // spear: 1, sword: 2, archer: 101, crossbow: 102, scropion: 201, lancer: 202, push: 10001, ox: 10002
                        if(v.First["habitatUnitArray"] != null)
                            foreach (var v2 in v.First["habitatUnitArray"])
                            {
                                if (v2["habitatUnitDictionary"] != null)
                                {
                                    if (v2["battleType"] == "0")
                                    {
                                        if (v2["habitatUnitDictionary"]["1"] != null)
                                            spearmen_i += (int)v2["habitatUnitDictionary"]["1"];
                                        if (v2["habitatUnitDictionary"]["2"] != null)
                                            swordsmen_i += (int)v2["habitatUnitDictionary"]["2"];
                                        if (v2["habitatUnitDictionary"]["101"] != null)
                                            archers_i += (int)v2["habitatUnitDictionary"]["101"];
                                        if (v2["habitatUnitDictionary"]["102"] != null)
                                            crossbows_i += (int)v2["habitatUnitDictionary"]["102"];
                                        if (v2["habitatUnitDictionary"]["201"] != null)
                                            scorpions_i += (int)v2["habitatUnitDictionary"]["201"];
                                        if (v2["habitatUnitDictionary"]["202"] != null)
                                            lancers_i += (int)v2["habitatUnitDictionary"]["202"];
                                        if (v2["habitatUnitDictionary"]["10001"] != null)
                                            pushcarts_i += (int)v2["habitatUnitDictionary"]["10001"];
                                        if (v2["habitatUnitDictionary"]["10002"] != null)
                                            oxcarts_i += (int)v2["habitatUnitDictionary"]["10002"];
                                    }
                                }
                            }
                        spearmen = spearmen_i.ToString();
                        swordsmen = swordsmen_i.ToString();
                        archers = archers_i.ToString();
                        crossbows = crossbows_i.ToString();
                        scorpions = scorpions_i.ToString();
                        lancers = lancers_i.ToString();
                        pushcarts = pushcarts_i.ToString();
                        oxcarts = oxcarts_i.ToString();
                        if (v.First["name"] != null && v.First["name"] != String.Empty) name = v.First["name"];
                        else name = "Free Castle "+v.First["id"];

                        foreach (var v2 in v.First["habitatBuildingUpgradeArray"])
                        {
                            Building bldgUpgraded = new Building();
                            int id = Convert.ToInt32(v2["buildingTargetID"].ToString());
                            int level = 0;
                            foreach (Building bldg in buildings)
                            {
                                if (id > bldg.primaryKey && id <= bldg.primaryKey + bldg.maxLevel)
                                {
                                    bldgUpgraded = bldg;
                                    level = id - bldg.primaryKey;
                                }
                            }/*
                            if (id > 2 && id < 12) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == KEEP; });
                            else if (id > 100 && id < 130) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == LUMBERJACK; });
                            else if (id > 200 && id < 220) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == WOODSTORE; });
                            else if (id > 300 && id < 330) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == QUARRY; });
                            else if (id > 400 && id < 420) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == STONESTORE; });
                            else if (id > 500 && id < 530) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == OREMINE; });
                            else if (id > 600 && id < 620) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == ORESTORE; });
                            else if (id > 800 && id < 830) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == FARM; });
                            else if (id > 900 && id < 908) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == MARKET; });
                            else if (id > 1000 && id < 1010) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == LIBRARY; });
                            else if (id > 1100 && id < 1110) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == TAVERN; });
                            else if (id > 1200 && id < 1220) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == FORTIFICATIONS; });
                            else if (id > 1300 && id < 1330) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == ARSENAL; });*/
                            ugrades.Add(new Upgrade() { Time = TimeSpan.FromSeconds(Convert.ToInt32(v2["durationInSeconds"].ToString())), Building = bldgUpgraded, Level = level });
                        }
                        foreach(Upgrade u in ugrades) {
                            timedMessages.Add(new MessageAtTime() { Message = "An upgrade was completed in " + name + ".", Time = DateTime.Now + u.Time });
                        }

                        totalSilver += Convert.ToInt32(v.First["habitatResourceDictionary"]["6"]["amount"].ToString());

                        castles.Add(new Castle()
                        {
                            name = name,
                            id = v.First["id"],
                            points = v.First["points"],
                            wood = v.First["habitatResourceDictionary"]["1"]["amount"],
                            stone = v.First["habitatResourceDictionary"]["2"]["amount"],
                            ore = v.First["habitatResourceDictionary"]["3"]["amount"],
                            people = v.First["habitatResourceDictionary"]["4"]["amount"],
                            copper = v.First["habitatResourceDictionary"]["5"]["amount"],
                            silver = v.First["habitatResourceDictionary"]["6"]["amount"],
                            woodStoreAmount = v.First["habitatResourceDictionary"]["1"]["storeAmount"],
                            stoneStoreAmount = v.First["habitatResourceDictionary"]["2"]["storeAmount"],
                            oreStoreAmount = v.First["habitatResourceDictionary"]["3"]["storeAmount"],
                            peopleStoreAmount = v.First["habitatResourceDictionary"]["4"]["storeAmount"],
                            copperStoreAmount = v.First["habitatResourceDictionary"]["5"]["storeAmount"],
                            silverStoreAmount = v.First["habitatResourceDictionary"]["6"]["storeAmount"],
                            x = v.First["mapX"],
                            y = v.First["mapY"],
                            isRecruiting = isRecruiting,
                            spearmen = spearmen,
                            swordsmen = swordsmen,
                            archers = archers,
                            crossbowmen = crossbows,
                            scorpions = scorpions,
                            lancers = lancers,
                            pushCarts = pushcarts,
                            oxCarts = oxcarts,
                            KeepLevel = Convert.ToInt32(v.First["b1Level"].ToString()),
                            ArsenalLevel = Convert.ToInt32(v.First["b2Level"].ToString()),
                            TavernLevel = Convert.ToInt32(v.First["b3Level"].ToString()),
                            LibraryLevel = Convert.ToInt32(v.First["b4Level"].ToString()),
                            FortificationsLevel = Convert.ToInt32(v.First["b5Level"].ToString()),
                            MarketLevel = Convert.ToInt32(v.First["b6Level"].ToString()),
                            FarmLevel = Convert.ToInt32(v.First["b7Level"].ToString()),
                            LumberjackLevel = Convert.ToInt32(v.First["b8Level"].ToString()),
                            WoodStoreLevel = Convert.ToInt32(v.First["b9Level"].ToString()),
                            QuarryLevel = Convert.ToInt32(v.First["b10Level"].ToString()),
                            StoneStoreLevel = Convert.ToInt32(v.First["b11Level"].ToString()),
                            OreMineLevel = Convert.ToInt32(v.First["b12Level"].ToString()),
                            OreStoreLevel = Convert.ToInt32(v.First["b13Level"].ToString()),
                            upgrades = ugrades
                        });
                        dropDownCastles.Items.Add(v.First["name"] + " (" + v.First["points"] + ")");

                        timedMessages.Clear();
                        foreach (var v3 in v.First["habitatTransitArray"])
                        {
                            if (v3["destinationETA"] != null && v3["destinationETA"].ToString() != String.Empty && 1==2)
                            {
                                DateTimeOffset time = DateTimeOffset.ParseExact(v3["destinationETA"].ToString(), "MM/dd/yyyy HH:mm:ss", null);
                                DateTime local = time.ToLocalTime().DateTime;
                                MessageAtTime m = new MessageAtTime();
                                m.Message = "Units from " + v3["sourceHabitat"]["name"].ToString();
                                m.Message+=" arrived at " + v3.First["destinationHabitat"].First["name"] + ".";
                                m.Time = local;
                                timedMessages.Add(m);
                            }
                        }
                        foreach (var v3 in v.First["habitatBuildingUpgradeArray"])
                        {
                            if (v3["destinationETA"] != null && v3["destinationETA"].ToString() != String.Empty)
                            {
                                DateTimeOffset time = DateTimeOffset.ParseExact(v3["destinationETA"].ToString(), "MM/dd/yyyy HH:mm:ss", null);
                                DateTime local = time.DateTime + time.Offset;
                                timedMessages.Add(new MessageAtTime() { Message = "A building was upgraded in " + name + ".", Time = local });
                            }
                        }
                    }
                    dropDownCastles.SelectedIndex = 0;
                    tabControl1.SelectedIndex++;
                    this.Height = 650;
                    this.Width = 850;
                    labelTotalSilver.Text = totalSilver.ToString();
                    labelSilverForCastle.Text = (castles.Count * 1000).ToString();
                    randomTimer.Enabled = true;
                    secondsTimer.Enabled = true;

                    runAutoUpgrade();
                }
            }
        }

        public List<message> loadMessages()
        {
            List<message> msgs = new List<message>();
            String response = GetHtml(Properties.Settings.Default.world + "/wa/DiscussionAction/discussionTitleArray?callback=&PropertyListVersion=3");
            response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
            try
            {
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response);

                if (data["discussionTitleArray"] != null)
                    foreach (var v in data["discussionTitleArray"])
                    {
                        msgs.Add(new message() { Title = v["title"], Id = v["id"] });
                    }
            }
            catch (Exception e) { msgs.Add(new message() { Title = "Unable to load messages." + Environment.NewLine + e.Message, Id = "" }); }
            return msgs;
        }

        private void dropDownCastles_SelectedIndexChanged(object sender, EventArgs e)
        {
            String castlename = dropDownCastles.Text;
            String lvl = Regex.Match(castlename, @"\(([^)]*)\)").Groups[1].Value;
            castlename = castlename.Remove(castlename.Length - (lvl.Length + 3), lvl.Length + 3);

            Castle c = castles.Find(delegate(Castle castle) { return castle.name == castlename; });
            if (c != null) updateCastleInfo(c);
        }

        private void updateData(dynamic data)
        {
            Properties.Settings.Default.user_id = data["player"]["id"];
            this.Text = "L&K Bot - Connected to " + Properties.Settings.Default.world_name;

            labelName.Text = data["player"]["nick"];
            linkLabel4.Text = "Messages";
            totalSilver = 0;

            diplomacies.Clear();
            foreach (var v in data["player"]["alliance"]["diplomacyToArray"])
            {
                diplomacies.Add(new Diplomacy() { Name = v["name"], Id = v["id"], Relation = v["relationship"] });
            }

            foreach (var v in data["player"]["habitatDictionary"])
            {

                String spearmen, swordsmen, archers, crossbows, scorpions, lancers, pushcarts, oxcarts, name;
                bool isRecruiting = false;
                List<Upgrade> ugrades = new List<Upgrade>();

                int spearmen_i = 0;
                int swordsmen_i = 0;
                int archers_i = 0;
                int crossbows_i = 0;
                int scorpions_i = 0;
                int lancers_i = 0;
                int pushcarts_i = 0;
                int oxcarts_i = 0;

                if (v.First["habitatUnitOrderArray"].Count > 0) isRecruiting = true;

                // spear: 1, sword: 2, archer: 101, crossbow: 102, scropion: 201, lancer: 202, push: 10001, ox: 10002
                if (v.First["habitatUnitArray"] != null)
                    foreach (var v2 in v.First["habitatUnitArray"])
                    {
                        if (v2["habitatUnitDictionary"] != null)
                        {
                            if (v2["battleType"] == "0")
                            {
                                if (v2["habitatUnitDictionary"]["1"] != null)
                                    spearmen_i += (int)v2["habitatUnitDictionary"]["1"];
                                if (v2["habitatUnitDictionary"]["2"] != null)
                                    swordsmen_i += (int)v2["habitatUnitDictionary"]["2"];
                                if (v2["habitatUnitDictionary"]["101"] != null)
                                    archers_i += (int)v2["habitatUnitDictionary"]["101"];
                                if (v2["habitatUnitDictionary"]["102"] != null)
                                    crossbows_i += (int)v2["habitatUnitDictionary"]["102"];
                                if (v2["habitatUnitDictionary"]["201"] != null)
                                    scorpions_i += (int)v2["habitatUnitDictionary"]["201"];
                                if (v2["habitatUnitDictionary"]["202"] != null)
                                    lancers_i += (int)v2["habitatUnitDictionary"]["202"];
                                if (v2["habitatUnitDictionary"]["10001"] != null)
                                    pushcarts_i += (int)v2["habitatUnitDictionary"]["10001"];
                                if (v2["habitatUnitDictionary"]["10002"] != null)
                                    oxcarts_i += (int)v2["habitatUnitDictionary"]["10002"];
                            }
                        }
                    }
                spearmen = spearmen_i.ToString();
                swordsmen = swordsmen_i.ToString();
                archers = archers_i.ToString();
                crossbows = crossbows_i.ToString();
                scorpions = scorpions_i.ToString();
                lancers = lancers_i.ToString();
                pushcarts = pushcarts_i.ToString();
                oxcarts = oxcarts_i.ToString();
                if (v.First["name"] == null || v.First["name"] == string.Empty) name = "Free Castle " + v.First["id"];
                else name = v.First["name"];

                TimeSpan totalUpgradeTime = new TimeSpan(0);
                foreach (var v2 in v.First["habitatBuildingUpgradeArray"])
                {
                    Building bldgUpgraded = new Building();
                    int id = Convert.ToInt32(v2["buildingTargetID"].ToString());
                    int level = 0;
                    foreach (Building bldg in buildings)
                    {
                        if (id > bldg.primaryKey && id <= bldg.primaryKey + bldg.maxLevel)
                        {
                            bldgUpgraded = bldg;
                            level = id - bldg.primaryKey;
                        }
                    }/*
                    if (id > 2 && id < 12) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == KEEP; });
                    else if (id > 100 && id <= 130) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == LUMBERJACK; });
                    else if (id > 200 && id <= 220) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == WOODSTORE; });
                    else if (id > 300 && id <= 330) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == QUARRY; });
                    else if (id > 400 && id <= 420) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == STONESTORE; });
                    else if (id > 500 && id <= 530) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == OREMINE; });
                    else if (id > 600 && id <= 620) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == ORESTORE; });
                    else if (id > 800 && id <= 830) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == FARM; });
                    else if (id > 900 && id <= 908) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == MARKET; });
                    else if (id > 1000 && id <= 1010) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == LIBRARY; });
                    else if (id > 1100 && id <= 1110) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == TAVERN; });
                    else if (id > 1200 && id <= 1220) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == FORTIFICATIONS; });
                    else if (id > 1300 && id <= 1330) bldgUpgraded = buildings.Find(delegate(Building b) { return b.name == ARSENAL; });*/
                    ugrades.Add(new Upgrade() { Time = TimeSpan.FromSeconds(Convert.ToInt32(v2["durationInSeconds"].ToString()) + totalUpgradeTime.TotalSeconds), Building = bldgUpgraded, Level = level });
                    totalUpgradeTime += TimeSpan.FromSeconds(Convert.ToInt32(v2["durationInSeconds"].ToString()));
                }

                totalSilver += Convert.ToInt32(v.First["habitatResourceDictionary"]["6"]["amount"].ToString());

                Castle edit = castles.Find(delegate(Castle c) { return c.id == v.First["id"].ToString(); });
                edit.name = name;
                edit.points = v.First["points"];
                edit.wood = v.First["habitatResourceDictionary"]["1"]["amount"];
                edit.stone = v.First["habitatResourceDictionary"]["2"]["amount"];
                edit.ore = v.First["habitatResourceDictionary"]["3"]["amount"];
                edit.people = v.First["habitatResourceDictionary"]["4"]["amount"];
                edit.copper = v.First["habitatResourceDictionary"]["5"]["amount"];
                edit.silver = v.First["habitatResourceDictionary"]["6"]["amount"];
                edit.woodStoreAmount = v.First["habitatResourceDictionary"]["1"]["storeAmount"];
                edit.stoneStoreAmount = v.First["habitatResourceDictionary"]["2"]["storeAmount"];
                edit.oreStoreAmount = v.First["habitatResourceDictionary"]["3"]["storeAmount"];
                edit.peopleStoreAmount = v.First["habitatResourceDictionary"]["4"]["storeAmount"];
                edit.copperStoreAmount = v.First["habitatResourceDictionary"]["5"]["storeAmount"];
                edit.silverStoreAmount = v.First["habitatResourceDictionary"]["6"]["storeAmount"];
                edit.spearmen = spearmen;
                edit.swordsmen = swordsmen;
                edit.archers = archers;
                edit.crossbowmen = crossbows;
                edit.scorpions = scorpions;
                edit.lancers = lancers;
                edit.pushCarts = pushcarts;
                edit.oxCarts = oxcarts;
                edit.KeepLevel = Convert.ToInt32(v.First["b1Level"].ToString());
                edit.ArsenalLevel = Convert.ToInt32(v.First["b2Level"].ToString());
                edit.TavernLevel = Convert.ToInt32(v.First["b3Level"].ToString());
                edit.LibraryLevel = Convert.ToInt32(v.First["b4Level"].ToString());
                edit.FortificationsLevel = Convert.ToInt32(v.First["b5Level"].ToString());
                edit.MarketLevel = Convert.ToInt32(v.First["b6Level"].ToString());
                edit.FarmLevel = Convert.ToInt32(v.First["b7Level"].ToString());
                edit.LumberjackLevel = Convert.ToInt32(v.First["b8Level"].ToString());
                edit.WoodStoreLevel = Convert.ToInt32(v.First["b9Level"].ToString());
                edit.QuarryLevel = Convert.ToInt32(v.First["b10Level"].ToString());
                edit.StoneStoreLevel = Convert.ToInt32(v.First["b11Level"].ToString());
                edit.OreMineLevel = Convert.ToInt32(v.First["b12Level"].ToString());
                edit.OreStoreLevel = Convert.ToInt32(v.First["b13Level"].ToString());
                edit.upgrades = ugrades;
                edit.isRecruiting = isRecruiting;
            }
            labelTotalSilver.Text = totalSilver.ToString();
            labelSilverForCastle.Text = (castles.Count * 1000).ToString();
        }

        private void runAutoUpgrade()
        {
            Log("Running automatic upgrade..");
            foreach (Castle c in castles)
            {
                Boolean notEnoughPopulation = false;
                if (c.upgrades.Count < 2 && Convert.ToInt32(c.points) < 288)
                {
                    Building lowestBldg = new Building();
                    TimeSpan lowestTime = new TimeSpan(4, 0, 0, 0); // No upgrade takes 4 days....
                    foreach (Building b in buildings)
                    {
                        int add = 0;
                        if (c.upgrades.Count > 0) if (c.upgrades[0].Building == b) add = 1; 
                        if (c.getLevel(b.engName) + add < b.maxLevel)
                        {
                            if (b.timeForLevel[c.getLevel(b.engName) + 1 + add] < lowestTime)
                            {
                                lowestTime = b.timeForLevel[c.getLevel(b.engName) + 1];
                                lowestBldg = b;
                            }
                        }
                    }
                    if (notEnoughPopulation) upgradeBuilding(FARM);
                    else if(lowestBldg.name != null) upgradeBuilding(lowestBldg.name, c);
                }
                else if (Convert.ToInt32(c.points) == 288 && Convert.ToInt32(c.wood) > 5000 && Convert.ToInt32(c.stone) > 5000 && Convert.ToInt32(c.ore) > 5000 && c.people == c.peopleStoreAmount)
                {
                    if (Convert.ToInt32(c.copper) < Convert.ToInt32(c.copperStoreAmount))
                    {
                        // Change resources to copper.
                        exchangeCopper(c);
                    }
                    else if (Convert.ToInt32(c.silver) < Convert.ToInt32(c.silverStoreAmount))
                    {
                        // Change resources to silver.
                        exchangeSilver(c);
                    }
                }
                else if (Convert.ToInt32(c.points) > 200 && Convert.ToInt32(c.people) < Convert.ToInt32(c.peopleStoreAmount) && !c.isRecruiting)
                {
                    Unit unit = null;
                    int amount = 0;
                    //   1000 1200 1200 200 100 100 0 22
                    if (Convert.ToInt32(c.spearmen) < 1000)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Spearman"; });
                        amount = 1000 - Convert.ToInt32(c.spearmen);
                    }
                    else if (Convert.ToInt32(c.swordsmen) < 1200)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Swordsman"; });
                        amount = 1200 - Convert.ToInt32(c.swordsmen);
                    }
                    else if (Convert.ToInt32(c.archers) < 1200)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Archer"; });
                        amount = 1200 - Convert.ToInt32(c.archers);
                    }
                    else if (Convert.ToInt32(c.crossbowmen) < 200)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Crossbowman"; });
                        amount = 200 - Convert.ToInt32(c.crossbowmen);
                    }
                    else if (Convert.ToInt32(c.scorpions) < 100)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Armoured Horseman"; });
                        amount = 100 - Convert.ToInt32(c.scorpions);
                    }
                    else if (Convert.ToInt32(c.lancers) < 100)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Lancer Horseman"; });
                        amount = 100 - Convert.ToInt32(c.lancers);
                    }
                    else if (Convert.ToInt32(c.oxCarts) < 22)
                    {
                        unit = units.Find(delegate(Unit u) { return u.Name == "Ox Cart"; });
                        amount = 22 - Convert.ToInt32(c.oxCarts);
                    }
                    if (unit != null)
                    {
                        if (Convert.ToInt32(c.wood) > amount * unit.WoodCost && Convert.ToInt32(c.stone) > amount * unit.StoneCost && Convert.ToInt32(c.ore) > amount * unit.OreCost && (Convert.ToInt32(c.peopleStoreAmount) - Convert.ToInt32(c.people)) > amount * unit.PopulationCost)
                            recruitUnits(c, unit, amount);
                        else
                        {
                            if (Convert.ToInt32(Math.Floor(Convert.ToDouble(c.wood) / unit.WoodCost)) < amount) amount = Convert.ToInt32(Math.Floor(Convert.ToDouble(c.wood) / unit.WoodCost));
                            if (Convert.ToInt32(Math.Floor(Convert.ToDouble(c.stone) / unit.StoneCost)) < amount) amount = Convert.ToInt32(Math.Floor(Convert.ToDouble(c.stone) / unit.StoneCost));
                            if (Convert.ToInt32(Math.Floor(Convert.ToDouble(c.ore) / unit.OreCost)) < amount) amount = Convert.ToInt32(Math.Floor(Convert.ToDouble(c.ore) / unit.OreCost));
                            if (Convert.ToInt32(Math.Floor((Convert.ToDouble(c.peopleStoreAmount) - Convert.ToDouble(c.people)) / unit.PopulationCost)) < amount) amount = Convert.ToInt32(Math.Floor((Convert.ToDouble(c.peopleStoreAmount) - Convert.ToDouble(c.people)) / unit.PopulationCost));

                            recruitUnits(c, unit, amount);
                        }
                    }
                }
            }
            Log("Finished automatic upgrading.");
        }

        private void exchangeSilver(Castle castle)
        {
            exchangeResource(castle, "Silver");
        }
        private void exchangeCopper(Castle castle)
        {
            exchangeResource(castle, "Copper");
        }
        public void exchangeResource(Castle castle, String resource)
        {
            String wantedResource = String.Empty;
            switch (resource)
            {
                case "Silver":
                    wantedResource = "6";
                    break;
                case "Copper":
                    wantedResource = "5";
                    break;
            }
            if (wantedResource != String.Empty)
            {
                Double Wood = Convert.ToInt32(castle.wood);
                Double Stone = Convert.ToInt32(castle.stone);
                Double Ore = Convert.ToInt32(castle.ore);
                Double OxCarts = Convert.ToInt32(castle.oxCarts);
                if (OxCarts != 0)
                {
                    if (OxCarts * 2500 < Wood + Stone + Ore)
                    {
                        Wood = Stone = Ore = OxCarts * 2500 / 3;
                    }
                    else
                    {
                        OxCarts = Math.Ceiling((Wood + Stone + Ore) / 2500);
                    }
                    String response = GetHtml(Properties.Settings.Default.world + "/wa/MarketAction/tradeResources?callback=&habitatID=" + castle.id + "&wantedResourceID=" + wantedResource + "&resourceDictionary={3=" + Ore.ToString() + ";+2=" + Stone.ToString() + ";+1=" + Wood.ToString() + ";}&unitDictionary={10002=" + OxCarts.ToString() + ";}&PropertyListVersion=3");
                    if (!response.Contains("\"error\""))
                    {
                        castle.wood = (Convert.ToInt32(castle.wood) - Wood).ToString();
                        castle.stone = (Convert.ToInt32(castle.stone) - Stone).ToString();
                        castle.ore = (Convert.ToInt32(castle.ore) - Ore).ToString();
                        MessageOrPopUp("Exchanged resources for " + resource + " in " + castle.name + ".");
                    }
                }
            }
        }

        private void randomTimer_Tick(object sender, EventArgs e)
        {
            randomTimer.Interval = new Random().Next(10000, 50000);
            String response = GetHtml(Properties.Settings.Default.world + "/wa/SessionAction/update?callback=&PropertyListVersion=3");
            response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
            if (response.Contains("error") && response.Contains("session"))
            {
                sessionExpired();
            }
            else
            {
                updateData(JsonConvert.DeserializeObject<dynamic>(response));
            }
            if (Properties.Settings.Default.autoUpgrade) runAutoUpgrade();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(tabControl1.SelectedTab.Left, tabControl1.SelectedTab.Top, tabControl1.SelectedTab.Width, tabControl1.SelectedTab.Height);
            tabControl1.Region = new Region(rect);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dropDownPlayerCastle.Items.Clear();
            foreach (Castle cast in castles)
            {
                if (cast.name != null) dropDownPlayerCastle.Items.Add(cast.name);
                else dropDownPlayerCastle.Items.Add("Free Castle " + cast.id);
            }
            dropDownPlayerCastle.SelectedIndex = 0;

            listBoxActions.Items.Clear();
            foreach (Action act in actions)
            {
                String args = String.Empty;
                foreach (String s in act.arguments) args += s+", ";
                listBoxActions.Items.Add("[" + act.Time.ToShortDateString() + "-" + act.Time.ToShortTimeString() + "] " + act.action + "(" + args + ")");
            }
            tabControl1.SelectedIndex = 3;
        }

        private void exec_action(Action action)
        {
            switch (action.action)
            {
                case "attack":
                    bool addPlus = false;
                    String player_castle, enemy_castle, spearmen, swordsmen, archers, crossbows, armoured, lancers, pushCarts, oxCarts, silver, unitDictionary;
                    player_castle = action.arguments[0];
                    enemy_castle = action.arguments[1];
                    spearmen = action.arguments[2];
                    swordsmen = action.arguments[3];
                    archers = action.arguments[4];
                    crossbows = action.arguments[5];
                    armoured = action.arguments[6];
                    lancers = action.arguments[7];
                    pushCarts = action.arguments[8];
                    oxCarts = action.arguments[9];
                    silver = action.arguments[10];

                    unitDictionary = "{";
                    // Only add these values if they are unequal to 0.
                    if (spearmen != "0")
                    {
                        unitDictionary += "1=" + spearmen + ";";
                        addPlus = true;
                    }
                    if (swordsmen != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "2=" + swordsmen + ";";
                        addPlus = true;
                    }
                    if (archers != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "101=" + archers + ";";
                        addPlus = true;
                    }
                    if (crossbows != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "102=" + crossbows + ";";
                        addPlus = true;
                    }
                    if (armoured != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "201=" + armoured + ";";
                        addPlus = true;
                    }
                    if (lancers != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "202=" + lancers + ";";
                        addPlus = true;
                    }
                    if (pushCarts != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "10001=" + pushCarts + ";";
                        addPlus = true;
                    }
                    if (oxCarts != "0")
                    {
                        if (addPlus) unitDictionary += "+";
                        unitDictionary += "10002=" + oxCarts + ";";
                        addPlus = true;
                    }
                    unitDictionary += "}";

                    //units: %7B1%3D1%3B+2%3D2%3B+101%3D3%3B+102%3D4%3B+202%3D6%3B+10002%3D7%3B%7D
                    //{ 1=1;+2=2;+101=3;+102=4;+202=6;+10002=7; }
                    String response = GetHtml(Properties.Settings.Default.world + "/wa/TransitAction/startTransit?callback=&sourceHabitatID=" + player_castle + "&destinationHabitatID=" + enemy_castle + "&transitType=2&unitDictionary=" + unitDictionary + "&resourceDictionary={6=" + silver + ";}&PropertyListVersion=3");
                    if (response.Contains("\"error\"")) MessageOrPopUp("There was an error with the current attack", MessageBoxIcon.Error);
                    else MessageOrPopUp(DateTime.Now.ToShortTimeString() + "\r\nAttacking " + enemy_castle + " from " + castles.Find(delegate(Castle castle) { return castle.id == player_castle; }).name + ":\r\n Spearmen: " + spearmen + "\r\n Swordsmen: " + swordsmen + "\r\n Archers: " + archers + "\r\n Crossbowmen: " + crossbows + "\r\n Armoured horemen: " + armoured + "\r\n Lancer horsemen: " + lancers + "\r\n Push Carts: " + pushCarts + "\r\n Oxcarts: " + oxCarts + "\r\n Silver: " + silver, MessageBoxIcon.Information);
                    break;
            }
        }

        private void secondsTimer_Tick(object sender, EventArgs e)
        {
            List<Action> toDelete = new List<Action>();
            List<Action> localActions = actions; // Create a copy so there is no error when an child gets deleted.
            foreach (Action act in localActions)
            {
                if (act.Time.Date == DateTime.Now.Date && act.Time.Hour == DateTime.Now.Hour && act.Time.Minute == DateTime.Now.Minute && act.Time.Second == DateTime.Now.Second)
                {
                    exec_action(act);
                }
                else if (act.Time < DateTime.Now) toDelete.Add(act);
            }
            foreach (Action a in toDelete)
            {
                actions.Remove(a);
            }
            foreach (MessageAtTime msg in timedMessages)
            {
                if (msg.Time.Date == DateTime.Now.Date && msg.Time.Hour == DateTime.Now.Hour && msg.Time.Minute == DateTime.Now.Minute && msg.Time.Second == DateTime.Now.Second)
                {
                    MessageOrPopUp(msg.Message);
                }
            }
            foreach (Action a in toDelete)
            {
                actions.Remove(a);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void upgradeBuilding(String german_name, Castle castle = null)
        {
            String primaryKey;

            if (castle == null)
            {
                String castlename = dropDownCastles.Text;
                String lvl = Regex.Match(castlename, @"\(([^)]*)\)").Groups[1].Value;
                castlename = castlename.Remove(castlename.Length - (lvl.Length + 3), lvl.Length + 3);
                castle = castles.Find(delegate(Castle c) { return c.name == castlename; });
            }
            Building buildg = buildings.Find(delegate(Building b) { return b.name == german_name; });
            primaryKey = (buildg.primaryKey + castle.getLevel(buildg.engName) + 1).ToString();
            String res = GetHtml(Properties.Settings.Default.world + "/wa/HabitatAction/upgradeBuilding?habitatID=" + castle.id + "&primaryKey=" + primaryKey + "&PropertyListVersion=3");
            if (!res.Contains("error"))
            {
                MessageOrPopUp("Starting upgrade of " + buildg.engName + " in " + castle.name + ".");
                castle.upgrades.Add(new Upgrade() { Building = buildg, Level = castle.getLevel(buildg.engName) + 1, Time = buildg.timeForLevel[castle.getLevel(buildg.engName) + 1] });
            }
            else Log("Error while trying to upgrade " + buildg.engName + " in " + castle.name + ".");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            upgradeBuilding(KEEP);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            upgradeBuilding(ARSENAL);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            upgradeBuilding(TAVERN);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            upgradeBuilding(LIBRARY);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            upgradeBuilding(FORTIFICATIONS);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            upgradeBuilding(MARKET);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            upgradeBuilding(FARM);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            upgradeBuilding(LUMBERJACK);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            upgradeBuilding(WOODSTORE);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            upgradeBuilding(QUARRY);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            upgradeBuilding(STONESTORE);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            upgradeBuilding(OREMINE);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            upgradeBuilding(ORESTORE);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetHtml(Properties.Settings.Default.world + "/wa/SessionAction/disconnect?callback=&PropertyListVersion=3");
            MessageOrPopUp("You have been successfully logged out.");
            label3.Text = "You have been logged out.";
            tabControl1.SelectedIndex = 0;
            this.Width = 485;
            this.Height = 260;
            this.Text = "L&K Bot - Not logged in.";
            randomTimer.Enabled = false;
            secondsTimer.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            actions.Add(new Action() { Time = dateTimePickerAction.Value, action = dropDownAction.Text, arguments = new List<String>() { castles.Find(delegate(Castle c) { return c.name == dropDownPlayerCastle.Text; }).id, textBoxTargetCastle.Text, numericSpearmen.Text, numericSwordsmen.Text, numericArchers.Text, numericCrossbowmen.Text, numericScorpionRiders.Text, numericLancers.Text, numericPushCarts.Text, numericOxCarts.Text, numericSilver.Text } });

            listBoxActions.Items.Clear();
            foreach (Action act in actions)
            {
                String args = String.Empty;
                foreach (String s in act.arguments) args += s + ", ";
                args.TrimEnd(',');
                listBoxActions.Items.Add("[" + act.Time.ToShortDateString() + "-" + act.Time.ToShortTimeString() + "] " + act.action + "(" + args + ")");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            try
            {
                String link = Prompt.ShowDialog("Enter or paste the link to the castle: ", "Get castle ID from l&k link");
                String pos_x = link.Substring(18, 5);
                String pos_y = link.Substring(24, 5);
                String response = GetHtml(Properties.Settings.Default.world + "/wa/MapAction/map?callback=&mapX=" + pos_x + "&mapY=" + pos_y + "&mapWidth=1&mapHeight=1&PropertyListVersion=3");
                response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
                dynamic json = JsonConvert.DeserializeObject<dynamic>(response);
                String id = "";
                foreach (var v in json["map"]["tileArray"].First["habitatDictionary"])
                {
                    if (v.First["mapX"] == pos_x && v.First["mapY"] == pos_y)
                    {
                        id = v.First["id"];
                    }
                }
                if (id != String.Empty) textBoxTargetCastle.Text = id;
                else
                {
                    textBoxTargetCastle.Text = "00000";
                    MessageOrPopUp("Could not get the ID from the link you provided.", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                textBoxTargetCastle.Text = "00000";
                MessageOrPopUp("Could not get the ID from the link you provided. \r\n\r\n" + ex.Message, MessageBoxIcon.Error);
            }
        }

        void saveActions()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, actions);
                    ms.Position = 0;
                    byte[] buffer = new byte[(int)ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    Properties.Settings.Default.actions = Convert.ToBase64String(buffer);
                }
            }
        }

        List<Action> loadActions()
        {
            if (Properties.Settings.Default.actions == String.Empty) { return new List<Action>(); }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.actions)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Action>)bf.Deserialize(ms);
            }
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            GetHtml(Properties.Settings.Default.world + "/wa/SessionAction/disconnect?callback=&PropertyListVersion=3");
            notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void toolStripMenuItemHideForm_Click(object sender, EventArgs e)
        {
            this.Show();
            formVisible = true;
        }

        private void listBoxActions_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBoxActions.SelectedIndex != -1 && e.KeyCode == Keys.Delete)
            {
                actions.RemoveAt(listBoxActions.SelectedIndex);
                listBoxActions.Items.RemoveAt(listBoxActions.SelectedIndex);
                listBoxActions.Refresh();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            extraWindow = new Messages(this);
            extraWindow.Show();
        }

        private void txtBoxEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Profile(Properties.Settings.Default.user_id, this).Show();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Send Copper
            String castlename = dropDownCastles.Text;
            String lvl = Regex.Match(castlename, @"\(([^)]*)\)").Groups[1].Value;
            castlename = castlename.Remove(castlename.Length - (lvl.Length + 3), lvl.Length + 3);

            Castle c = castles.Find(delegate(Castle castle) { return castle.name == castlename; });
            if (c != null)
            {
                new changeResources(this, c, "Copper").ShowDialog();
            }
        }

        private void lblSilver_MouseClick(object sender, MouseEventArgs e)
        {
            // Send Silver
            String castlename = dropDownCastles.Text;
            String lvl = Regex.Match(castlename, @"\(([^)]*)\)").Groups[1].Value;
            castlename = castlename.Remove(castlename.Length - (lvl.Length + 3), lvl.Length + 3);

            Castle c = castles.Find(delegate(Castle castle) { return castle.name == castlename; });
            if (c != null)
            {
                new changeResources(this, c, "Silver").ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm(this).ShowDialog();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            formVisible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            formVisible = true;
        }

        private void doResearch(Castle castle, Knowledge knowledge)
        {
            String response = GetHtml(Properties.Settings.Default.world + "/wa/HabitatAction/researchKnowledge?callback=&primaryKey=" + knowledge.PrimaryKey + "&habitatID=" + castle.id + "&PropertyListVersion=3");
            if (!response.Contains("\"error\"")) MessageOrPopUp("Started research of " + knowledge.Name + " in " + castle.name + ".");
            else Log("Error while trying to start research of " + knowledge.Name + " in " + castle.name + ".");
        }

        private void showRecruitUnits(String unitName)
        {
            String castlename = dropDownCastles.Text;
            String lvl = Regex.Match(castlename, @"\(([^)]*)\)").Groups[1].Value;
            castlename = castlename.Remove(castlename.Length - (lvl.Length + 3), lvl.Length + 3);

            Castle c = castles.Find(delegate(Castle castle) { return castle.name == castlename; });
            Unit u = units.Find(delegate(Unit unit) { return unit.Name == unitName; });
            
            if (c != null && u != null)
            {
                new RecruitUnitForm(c, u, this).ShowDialog();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Spearman");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Swordsman");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Archer");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Crossbowman");
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Armoured Horseman");
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Lancer Horseman");
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Handcart");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            showRecruitUnits("Ox Cart");
        }

        private void runAutoupgradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runAutoUpgrade();
        }

    }

    public class Diplomacy
    {
        public String Name
        {
            get;
            set;
        }
        public String Id
        {
            get;
            set;
        }
        public String Relation
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class Action
    {
        public DateTime Time
        {
            get;
            set;
        }
        public String action
        {
            get;
            set;
        }
        public List<String> arguments
        {
            get;
            set;
        }
    }
    public class Building
    {
        public String name
        {
            get;
            set;
        }
        public String engName
        {
            get;
            set;
        }
        public int primaryKey
        {
            get;
            set;
        }
        public int maxLevel
        {
            get;
            set;
        }
        public Dictionary<int, TimeSpan> timeForLevel
        {
            get;
            set;
        }
        public Dictionary<int, int> peopleForLevel
        {
            get;
            set;
        }
        public Dictionary<int, int> woodForLevel
        {
            get;
            set;
        }
        public Dictionary<int, int> stoneForLevel
        {
            get;
            set;
        }
        public Dictionary<int, int> oreForLevel
        {
            get;
            set;
        }
    }
    public class Castle
    {
        public String name
        {
            get;
            set;
        }
        public String points
        {
            get;
            set;
        }
        public String id
        {
            get;
            set;
        }

        public int getLevel(String buildingName)
        {
            switch (buildingName.ToLower())
            {
                case "keep":
                    return KeepLevel;
                case "arsenal":
                    return ArsenalLevel;
                case "tavern":
                    return TavernLevel;
                case "library":
                    return LibraryLevel;
                case "fortifications":
                    return FortificationsLevel;
                case "market":
                    return MarketLevel;
                case "farm":
                    return FarmLevel;
                case "lumberjack":
                    return LumberjackLevel;
                case "woodstore":
                    return WoodStoreLevel;
                case "quarry":
                    return QuarryLevel;
                case "stonestore":
                    return StoneStoreLevel;
                case "oremine":
                    return OreMineLevel;
                case "orestore":
                    return OreStoreLevel;
                default:
                    return 0;
            }
        }

        public int KeepLevel
        {
            get;
            set;
        }
        public int ArsenalLevel
        {
            get;
            set;
        }
        public int TavernLevel
        {
            get;
            set;
        }
        public int LibraryLevel
        {
            get;
            set;
        }
        public int FortificationsLevel
        {
            get;
            set;
        }
        public int MarketLevel
        {
            get;
            set;
        }
        public int FarmLevel
        {
            get;
            set;
        }
        public int LumberjackLevel
        {
            get;
            set;
        }
        public int WoodStoreLevel
        {
            get;
            set;
        }
        public int QuarryLevel
        {
            get;
            set;
        }
        public int StoneStoreLevel
        {
            get;
            set;
        }
        public int OreMineLevel
        {
            get;
            set;
        }
        public int OreStoreLevel
        {
            get;
            set;
        }

        public String people
        {
            get;
            set;
        }
        public String wood
        {
            get;
            set;
        }
        public String stone
        {
            get;
            set;
        }
        public String ore
        {
            get;
            set;
        }
        public String copper
        {
            get;
            set;
        }
        public String silver
        {
            get;
            set;
        }
        public String peopleStoreAmount
        {
            get;
            set;
        }
        public String woodStoreAmount
        {
            get;
            set;
        }
        public String stoneStoreAmount
        {
            get;
            set;
        }
        public String oreStoreAmount
        {
            get;
            set;
        }
        public String copperStoreAmount
        {
            get;
            set;
        }
        public String silverStoreAmount
        {
            get;
            set;
        }
        public String x
        {
            get;
            set;
        }
        public String y
        {
            get;
            set;
        }

        public bool isRecruiting
        {
            get;
            set;
        }
        public String spearmen
        {
            get;
            set;
        }
        public String swordsmen
        {
            get;
            set;
        }
        public String archers
        {
            get;
            set;
        }
        public String crossbowmen
        {
            get;
            set;
        }
        public String scorpions
        {
            get;
            set;
        }
        public String lancers
        {
            get;
            set;
        }
        public String pushCarts
        {
            get;
            set;
        }
        public String oxCarts
        {
            get;
            set;
        }

        public List<Upgrade> upgrades
        {
            get;
            set;
        }
    }
    public class Unit
    {
        public String Name
        {
            get;
            set;
        }
        public int PrimaryKey
        {
            get;
            set;
        }
        public int WoodCost
        {
            get;
            set;
        }
        public int StoneCost
        {
            get;
            set;
        }
        public int OreCost
        {
            get;
            set;
        }
        public int PopulationCost
        {
            get;
            set;
        }
        public TimeSpan TimeForField
        {
            get;
            set;
        }
        public int TransportCapacity
        {
            get;
            set;
        }
    }
    public class Upgrade
    {
        public TimeSpan Time
        {
            get;
            set;
        }
        public Building Building
        {
            get;
            set;
        }
        public int Level
        {
            get;
            set;
        }
    }
    public class MessageAtTime
    {
        public DateTime Time
        {
            get;
            set;
        }
        public String Message
        {
            get;
            set;
        }
    }
    public class Knowledge
    {
        public String Name
        {
            get;
            set;
        }
        public int RequiredLibraryLevel
        {
            get;
            set;
        }
        public int PrimaryKey
        {
            get;
            set;
        }
        public TimeSpan Time
        {
            get;
            set;
        }
        public int WoodCost
        {
            get;
            set;
        }
        public int StoneCost
        {
            get;
            set;
        }
        public int OreCost
        {
            get;
            set;
        }
        public int PopulationCost
        {
            get;
            set;
        }
    }
    public static class Prompt
    {
        public static String ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Icon = Properties.Resources.favicon;
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 400 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            Button cancel = new Button() { Text = "Cancel", Left = 230, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { textBox.Text = ""; prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }
    }

}
