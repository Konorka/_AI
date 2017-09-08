using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SpaceBattle.Common;
using System.IO;
using System.Reflection;

namespace SpaceBattle_Server
{
    public partial class ServerForm : Form
    {
        /*
        colors.Add("_UNKNOWN_", Brushes.Pink);
        colors.Add("_AI_", Brushes.Blue);
        colors.Add("BELA", Brushes.Magenta);
        colors.Add("csaki", Brushes.CornflowerBlue);
        colors.Add("doubleInf", Brushes.White);
        colors.Add("LAPH", Brushes.Pink);
        colors.Add("nema", Brushes.Green);
        colors.Add("Fatrat", Brushes.DarkOrange);
        colors.Add("iLoop", Brushes.Yellow);
        colors.Add("sudowin", Brushes.Red);
        colors.Add("HardCores", Brushes.DarkMagenta);
        colors.Add("BITsPlease", Brushes.Chartreuse);
        colors.Add("Deadline", Brushes.Cyan);
        */

        Font Font_Small = new Font("Calibri", 7);
        Font Font_Big = new Font("Calibri", 22);
        Dictionary<string, Brush> colors = new Dictionary<string, Brush>();
        GameLevel level;
        Dictionary<string, IBattleClient> clients = new Dictionary<string, IBattleClient>();
        Timer moveTimer;
        public ServerForm()
        {
            InitializeComponent();

            moveTimer = new Timer();
            moveTimer.Interval = 30;
            moveTimer.Tick += moveTimer_Tick;

            level = new GameLevel("SpaceBattle_level1.ini", moveTimer.Interval);
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);

            level.SystemMessage += level_SystemMessage;
            chkMessages.Checked = false;
            chkToolbox.Checked = false;
            chkMapMask.Checked = false;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            LoadDLL();
            moveTimer.Start();
        }

        void LoadDLL()
        {
            clients.Clear();
            string[] plugins = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
            foreach (string akt in plugins)
            {
                Assembly assembly = Assembly.LoadFile(akt);
                foreach (Type aktType in assembly.GetTypes())
                {
                    if (aktType.GetInterface("IBattleClient") != null)
                    {
                        IBattleClient instance = Activator.CreateInstance(aktType) as IBattleClient;
                        clients.Add(instance.ClientName, instance);
                        //aktType.InvokeMember("InitPlugin", BindingFlags.Default | BindingFlags.InvokeMethod, null, instance, new object[] { core });
                    }
                }
            }
            lstClients.DataSource = null;
            lstClients.DataSource = clients.Keys.ToList();
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogExc(e.Exception.ToString());
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogExc(e.ExceptionObject.ToString());
        }
        object excLogObj = new object();
        void LogExc(string s)
        {
            lock (excLogObj)
            {
                File.AppendAllText("exception.log", s + "\n >>> " +
                    DateTime.Now.ToLongTimeString() + " <<< \n\n\n\n");
            }
        }

        const int changeTicks = 40;
        int currentPlayer=0;
        int currentTicks = changeTicks;

        void moveTimer_Tick(object sender, EventArgs e)
        {
            level.OneTick();
            int x, y;

            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
            foreach (GameItem akt in level.GameItemsCopy)
            {
                //if (akt.PosX != 24) continue;
                x = (int)(bmp.Width * akt.PosX / level.SizeX);
                y = (int)(bmp.Height * akt.PosY / level.SizeX);
                int tilew = bmp.Width / level.SizeX;
                int tileh = bmp.Height / level.SizeY;
                Brush aktBrush = Brushes.Gray;
                if (akt.ItemPlayer != null)
                {
                    aktBrush = akt.ItemPlayer.PlayerClient.ClientBrush;
                }
                if (akt is Ship)
                {
                    g.FillRectangle(aktBrush, x, y, tilew, tileh);
                }
                else
                {
                    g.FillEllipse(aktBrush, x, y, tilew, tileh);
                }
                g.DrawString(akt.NumberOfUnits.ToString(), Font_Small, Brushes.Black, x + 3, y + 3);
                //g.DrawString(akt.ItemId.ToString(), Font_Small, Brushes.Black, x + 2, y + 2);
            }
            //g.DrawString(level.CalcEndOfGame(false), MYFONT, Brushes.White, 10, 10);
            level.CalcEndOfGame(false);
            bmp = (Bitmap)pictureBox3.Image;
            g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
            y = 5;
            x = 5;
            foreach (Player akt in level.GamePlayers.Values)
            {
                g.DrawString(akt.PlayerClient.ClientName+": "+akt.FinalPoints, Font_Big, akt.PlayerClient.ClientBrush, x, y);
                y += Font_Big.Height;
            }
            y += Font_Big.Height;
            g.DrawString("Remaining:" + level.currentTime, Font_Big, Brushes.White, x, y);
            y += Font_Big.Height;

            int idx=-1;
            foreach (Player akt in level.GamePlayers.Values)
            {
                idx++;
                if (idx == currentPlayer)
                {
                    string planets = string.Format("{0}/+{1}/-{2}", akt.FinalEndPlanets, akt.FinalPlanetsCaptured, akt.FinalPlanetsLost);
                    g.DrawString(planets, Font_Big, akt.PlayerClient.ClientBrush, x, y);
                    y += Font_Big.Height;
                    g.DrawString("  Shoots: " + akt.FinalShoots, Font_Big, akt.PlayerClient.ClientBrush, x, y);
                    y += Font_Big.Height;
                    g.DrawString("  Units: " + akt.FinalEndUnits, Font_Big, akt.PlayerClient.ClientBrush, x, y);
                    y += Font_Big.Height;
                    g.DrawString("  Points: " + akt.FinalPoints, Font_Big, akt.PlayerClient.ClientBrush, x, y);
                    y += Font_Big.Height;
                }
            }
            currentTicks--;
            if (currentTicks <= 0)
            {
                currentTicks = changeTicks;
                currentPlayer++;
                if (currentPlayer >= level.GamePlayers.Count) currentPlayer = 0;
            }

            pictureBox1.Invalidate();
            pictureBox3.Invalidate();
            if (chkMapMask.Visible && level.GamePlayers.ContainsKey(txtMaskPlayer.Text))
            {
                Player akt = level.GamePlayers[txtMaskPlayer.Text];
                Bitmap uj = new Bitmap(300, 300);
                g = Graphics.FromImage(uj);
                g.FillRectangle(Brushes.Black, 0, 0, uj.Width, uj.Height);
                int width = akt.MapMask.GetLength(0);
                int height = akt.MapMask.GetLength(1);
                int tilew = uj.Width / width;
                int tileh = uj.Height / height;
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        if (akt.MapMask[x, y] == 1)
                        {
                            g.FillEllipse(Brushes.White, x * tilew, y * tileh, tilew, tileh);
                        }
                    }
                }
                pictureBox2.Image = uj;
            }
        }

        void level_SystemMessage(object sender, string e)
        {
            if (chkTimer.Checked) txtMessages.BeginInvoke(new MethodInvoker(() =>
            {
                txtMessages.Text += string.Format("[{0}] {1}{2}", DateTime.Now.ToLongTimeString(), e, Environment.NewLine);
                txtMessages.SelectionStart = txtMessages.Text.Length - 1;
                txtMessages.SelectionLength = 0;
                txtMessages.ScrollToCaret();
            }));
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void chkToolbox_CheckedChanged(object sender, EventArgs e)
        {
            grpToolbox.Visible = chkToolbox.Checked;
        }

        private void chkMapMask_CheckedChanged(object sender, EventArgs e)
        {
            grpMapMask.Visible = chkMapMask.Checked;
        }

        private void ServerForm_DoubleClick(object sender, EventArgs e)
        {
            if (FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }
            else
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            level.Reset();
            foreach (string akt in lstClients.SelectedItems)
            {
                level.AddPlayer(new Player(clients[akt]));
            }
            level.Start();
            chkToolbox.Checked = false;
        }

        private void chkMessages_CheckedChanged(object sender, EventArgs e)
        {
            grpMessages.Visible = chkMessages.Checked;
        }

        private void chkTimer_CheckedChanged(object sender, EventArgs e)
        {
            moveTimer.Enabled = chkTimer.Checked;
        }
    }
}
