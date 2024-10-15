using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZBase.Forms
{
    public partial class Loader : Form
    {

        Timer t1 = new Timer();

        public Loader()
        {
            InitializeComponent();
        }


        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }


        public static bool SubExist(string name, int len)
        {
            for (var i = 0; i < len; i++)
            {
                if (Login.KeyAuthApp.user_data.subscriptions[i].subscription == name)
                {
                    return true;
                }
            }
            return false;
        }
        public string expirydaysleft()
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime();
            TimeSpan difference = dtDateTime - DateTime.Now;
            return Convert.ToString(difference.Days + " Days " + difference.Hours + " Hours Left");
        }

        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }

        string chatchannel = "test"; // chat channel name, must be set in order to send/retrieve messages

        public static class Util
        {
            public enum Effect { Roll, Slide, Center, Blend }

            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];
                if (ctl.Visible) { flags |= 0x10000; angle += 180; }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }
                flags |= dirmap[(angle % 360) / 45];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
            }

            private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
        }

        private void Loader_Load(object sender, EventArgs e)
        {
            gunapanel1.HorizontalScroll.Visible = false; // horizontalen Scrollbalken unsichtbar machen
            gunapanel1.VerticalScroll.Visible = false; //
            injectcsgo.Hide();
            injectcr.Hide();
            injectmc.Hide();
            Csgopanel.Hide();
            Minecraftpanel.Hide();
            CrabGamepanel.Hide();
            guna2PictureBox2.Hide();
            guna2PictureBox3.Hide();
            guna2PictureBox4.Hide();

            Opacity = 0;

            t1.Interval = 10;
            t1.Tick += new EventHandler(fadeIn);
            t1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 15000; // get chat messages every 15 seconds
            if (!String.IsNullOrEmpty(chatchannel))
            {
                var messages = Login.KeyAuthApp.chatget(chatchannel);
                if (messages == null)
                {
                }
                else
                {
                    foreach (var message in messages)
                    {
                    }
                }
            }
            else
            {
                timer1.Stop();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            injectcsgo.Show();
            injectmc.Hide();
            injectcr.Hide();
            Csgopanel.Visible = true;
            Minecraftpanel.Visible = false;
            CrabGamepanel.Visible = false;
            guna2PictureBox3.Visible = false;
            guna2PictureBox4.Visible = false;

            Util.Animate(guna2PictureBox2, Util.Effect.Slide, 0, 0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            injectcr.Visible = true;
            injectmc.Hide();
            injectcsgo.Hide();
            CrabGamepanel.Visible = true;
            Minecraftpanel.Visible= false;
            Csgopanel.Visible = false;
            guna2PictureBox2.Visible = false;
            guna2PictureBox4.Visible = false;
            Util.Animate(guna2PictureBox3, Util.Effect.Slide, 0, 0);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            injectmc.Show();
            injectcr.Hide();
            injectcsgo.Hide();
            Minecraftpanel.Visible = true;
            Csgopanel.Visible = false;
            CrabGamepanel.Visible = false;
            guna2PictureBox2.Visible = false;
            guna2PictureBox3.Visible = false;
            Util.Animate(guna2PictureBox4, Util.Effect.Slide, 0, 0);
        }

        private void CrabGamepanel_Paint(object sender, PaintEventArgs e)
        {
            if (Login.KeyAuthApp.user_data.subscriptions[0].subscription == "crab game")
            {
                expirylabelcrabgame.Text = $"{UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry))}";
            }
        }


        private void expirylabelcrabgame_Click(object sender, EventArgs e)
        {

        }

        private void CrabGamepanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void gunapanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Minecraftpanel_Paint(object sender, PaintEventArgs e)
        {
            if (Login.KeyAuthApp.user_data.subscriptions[0].subscription == "minecraft")
            {
                expirylabelcrabgame.Text = $"{UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry))}";
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void expirylabelcsgo_Click(object sender, EventArgs e)
        {

        }

        private void Csgopanel_Paint(object sender, PaintEventArgs e)
        {
            if (Login.KeyAuthApp.user_data.subscriptions[0].subscription == "csgo")
            {
                expirylabelcsgo.Text = $"{UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry))}";
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
