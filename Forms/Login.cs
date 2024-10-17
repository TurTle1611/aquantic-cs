using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyAuth;
using Loader;

namespace ZBase.Forms
{
    public partial class Login : Form
    {
        Timer t1 = new Timer();
        /*
* 
* WATCH THIS VIDEO TO SETUP APPLICATION: https://youtube.com/watch?v=RfDTdiBq4_o
* 
 * READ HERE TO LEARN ABOUT KEYAUTH FUNCTIONS https://github.com/KeyAuth/KeyAuth-CSHARP-Example#keyauthapp-instance-definition
 *
*/
        public void Show()
        {
            Visible = true;
        }

        public static api KeyAuthApp = new api(
            name: "jm", // Application Name
            ownerid: "bBPort1iBz", // Owner ID
            version: "1.0" // Application Version /*
                           //path: @"Your_Path_Here" // (OPTIONAL) see tutorial here https://www.youtube.com/watch?v=I9rxt821gMk&t=1s
        );

        public Login()
        {
            InitializeComponent();
            Drag.MakeDraggable(this);
        }

        #region Misc References
        public static bool SubExist(string name)
        {
            if (KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;

        }
        #endregion

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

        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
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

        Color konec = Color.FromArgb(10, 10, 10);
        Color seda = Color.FromArgb(128, 128, 128);
        Color bila = Color.FromArgb(255, 255, 255);
        Color konecna = Color.FromArgb(21, 19, 21);
        Color cernoch = Color.FromArgb(17, 16, 17);
        Color modra = Color.FromArgb(48, 104, 194);

        private void Login_Load(object sender, EventArgs e)
        {
            guna2ProgressBar1.Hide();
            KeyAuthApp.init();
            Opacity = 0;

            t1.Interval = 10;
            t1.Tick += new EventHandler(fadeIn);
            t1.Start();

            if (!KeyAuthApp.response.success)
            {
                MessageBox.Show(KeyAuthApp.response.message);
                Environment.Exit(0);
            }
        }

        private async void elawinAnimation_Tick(object sender, EventArgs e)
        {
            var positions = new Point[] {
                new Point(87, 52),
                new Point(97, 52),
                new Point(107, 52),
                new Point(117, 52),
                new Point(127, 52),
                new Point(137, 52),
                new Point(147, 52),
                new Point(157, 52),
                new Point(167, 52),

            };

            foreach (var position in positions)
            {
                logotext.Location = position;
                await Task.Delay(3);
            }
        }

        private async void LOGOVIDET_Tick(object sender, EventArgs e)
        {
            LOGOVIDET.Stop();
        }

        private async void PROGRESBARANIMACE_Tick(object sender, EventArgs e)
        {
            guna2ProgressBar1.Value += 1;

            if (guna2ProgressBar1.Value >= 100)
            {
                PROGRESBARANIMACE.Stop();
                await Task.Delay(400);
                PROGBARZMIZENI2.Start();
                PROGBARZMIZENI.Start();
                LOGOBAR.Start();
                await Task.Delay(10);
                EXITCOLORZPATKY.Start();
                LogoTextBackAnimation.Start();

                LOGOVIDET.Start();
            }
        }

        private async void PROGBARZMIZENI2_Tick(object sender, EventArgs e)
        {
            PROGBARZMIZENI2.Stop();
            if (guna2ProgressBar1.ProgressColor != konec)
            {
                var colorSteps = new Color[] {
                    Color.FromArgb(48, 104, 194),
                    Color.FromArgb(41, 89, 165),
                    Color.FromArgb(39, 84, 157),
                    Color.FromArgb(36, 77, 145),
                    Color.FromArgb(29, 65, 124),
                    Color.FromArgb(23, 52, 99),
                    Color.FromArgb(17, 39, 76),
                    Color.FromArgb(10, 10, 10)
                };

                foreach (var color in colorSteps)
                {
                    guna2ProgressBar1.ProgressColor = color;
                    await Task.Delay(3);
                }
            }
        }

        private async void PROGBARZMIZENI_Tick(object sender, EventArgs e)
        {
            PROGBARZMIZENI.Stop();
            if (guna2ProgressBar1.ProgressColor2 != konec)
            {
                var colorSteps = new Color[] {
                    Color.FromArgb(48, 104, 194),
                    Color.FromArgb(41, 89, 165),
                    Color.FromArgb(39, 84, 157),
                    Color.FromArgb(36, 77, 145),
                    Color.FromArgb(29, 65, 124),
                    Color.FromArgb(23, 52, 99),
                    Color.FromArgb(17, 39, 76),
                    Color.FromArgb(10, 10, 10)
                };

                foreach (var color in colorSteps)
                {
                    guna2ProgressBar1.ProgressColor2 = color;
                    await Task.Delay(3);
                }
            }
        }

        private void LOGOBAR_Tick(object sender, EventArgs e)
        {
            LOGOBAR.Stop();
        }

        private async void EXITCOLORZPATKY_Tick(object sender, EventArgs e)
        {
            EXITCOLORZPATKY.Stop();

            {
                int[][] colors = {
                new int[] {10, 10, 10},
                new int[] {28, 28, 28},
                new int[] {67, 67, 67},
                new int[] {111, 111, 111},
                new int[] {144, 144, 144},
                new int[] {185, 185, 185},
                new int[] {222, 222, 222},
                new int[] {255, 255, 255}
                };

                foreach (var color in colors)
                {
                    await Task.Delay(10);
                }
            }
        }

        private async void ErrorMessageBackAnimation_Back(object sender, EventArgs e)
        {
            ErrorMessageBackAnimation.Stop();

            var positions = new Point[] {
                new Point(222, 245),
                new Point(232, 245),
                new Point(242, 245),
                new Point(252, 245),
                new Point(262, 245),
                new Point(272, 245),
                new Point(282, 245),
                new Point(292, 245),
                new Point(302, 245),
                new Point(312, 245),
                new Point(322, 245),
                new Point(332, 245),
                new Point(342, 245),
                new Point(352, 245),
                new Point(363, 245),
                new Point(373, 245),
                new Point(422, 245)
            };

            foreach (var position in positions)
            {
                errornotification.Location = position;
                await Task.Delay(3);
            }
        }

        private async void WrongXAnimation_Tick(object sender, EventArgs e)
        {
            WrongXAnimation.Stop();

            wrongX.Location = new Point(375, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(365, 228);
            wrongX.Location = new Point(355, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(345, 228);
            wrongX.Location = new Point(335, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(325, 228);
            wrongX.Location = new Point(315, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(305, 228);
            wrongX.Location = new Point(295, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(285, 228);
            wrongX.Location = new Point(275, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(265, 228);
            wrongX.Location = new Point(255, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(245, 228);
            wrongX.Location = new Point(235, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(225, 228);
            wrongX.Location = new Point(215, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(205, 228);
            wrongX.Location = new Point(195, 228);
            await Task.Delay(3);
            wrongX.Location = new Point(185, 228);
        }

        private async void ErrorMessageAnimation_Tick(object sender, EventArgs e)
        {
            ErrorMessageAnimation.Stop();
            errornotification.Location = new Point(422, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(373, 245);
            errornotification.Location = new Point(363, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(352, 245);
            errornotification.Location = new Point(342, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(332, 245);
            errornotification.Location = new Point(322, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(312, 245);
            errornotification.Location = new Point(302, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(292, 245);
            errornotification.Location = new Point(282, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(272, 245);
            errornotification.Location = new Point(262, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(252, 245);
            errornotification.Location = new Point(242, 245);
            await Task.Delay(3);
            errornotification.Location = new Point(232, 245);
            errornotification.Location = new Point(222, 245);

        }

        private async void WrongXBackAnimation_Back(object sender, EventArgs e)
        {
            WrongXBackAnimation.Stop();

            int startX = 185;
            int step = 10;

            for (int i = 0; i < 22; i++)
            {
                wrongX.Location = new Point(startX + (i * step), 228);
                await Task.Delay(3);
            }
        }

        private async void Label2TextColorChangeAnimation_Tick(object sender, EventArgs e)
        {
            Label2TextColorChangeAnimation.Stop();

            if (label2.ForeColor != konec)
            {
                int[][] colors = {
                new int[] {128, 128, 128},
                new int[] {100, 100, 100},
                new int[] {80, 80, 80},
                new int[] {60, 60, 60},
                new int[] {50, 50, 50},
                new int[] {30, 30, 30},
                new int[] {20, 20, 20},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    label2.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
                    await Task.Delay(10);
                }
            }
        }

        private async void label2HorizontalPositionAnimation_Tick(object sender, EventArgs e)
        {
            label2HorizontalPositionAnimation.Stop();

            for (int i = -103; i <= 65; i += 8)
            {
                label2.Location = new Point(i, 73);
                await Task.Delay(10);
            }

            label2TextColorAnimation.Start();
        }

        private async void AnimateLabel2Left_Tick(object sender, EventArgs e)
        {
            AnimateLabel2Left.Stop();

            //89; 62
            for (int x = 28; x >= -103; x -= 8)
            {
                label2.Location = new Point(x, 62);
                await Task.Delay(3);
            }
        }

        private async void ZBARVENITEXTUANIMACE_Tick(object sender, EventArgs e)
        {
            label2TextColorAnimation.Stop();

            if (label2.ForeColor == Color.White)
            {
                return;
            }

            for (int i = 128; i <= 255; i += 20)
            {
                label2.ForeColor = Color.FromArgb(i, i, i);
                await Task.Delay(10);
            }

            label2.ForeColor = Color.White;
        }

        private async void LogoTextBackAnimation_Back(object sender, EventArgs e)
        {
            LogoTextBackAnimation.Stop();

            {
                int[][] colors = {
                new int[] {10, 10, 10},
                new int[] {28, 28, 28},
                new int[] {67, 67, 67},
                new int[] {111, 111, 111},
                new int[] {144, 144, 144},
                new int[] {185, 185, 185},
                new int[] {222, 222, 222},
                new int[] {255, 255, 255}
                };

                foreach (var color in colors)
                {
                    await Task.Delay(10);
                }
            }
        }

        private async void SignInAnimation2_Tick(object sender, EventArgs e)
        {
            SignInAnimation2.Stop();

            if (signin.ForeColor != konec)
            {
                int[][] colors = {
                new int[] {255, 255, 255},
                new int[] {222, 222, 222},
                new int[] {185, 186, 186},
                new int[] {144, 145, 146},
                new int[] {111, 111, 111},
                new int[] {67, 67, 67},
                new int[] {28, 28, 28},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    signin.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
                    await Task.Delay(10);
                }

                guna2ProgressBar1.Location = new Point(72, 206);
                signin.Hide();
            }
        }

        private async void SignInAnimation_Tick(object sender, EventArgs e)
        {
            SignInAnimation.Stop();

            if (signin.FillColor != konec)
            {
                int[][] colors = {
                new int[] {48, 104, 194},
                new int[] {41, 89, 165},
                new int[] {39, 84, 157},
                new int[] {36, 77, 145},
                new int[] {29, 65, 124},
                new int[] {23, 52, 99},
                new int[] {17, 39, 76},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    signin.FillColor = Color.FromArgb(color[0], color[1], color[2]);
                    await Task.Delay(10);
                }
            }

            username.Visible = false;
            password.Visible = false;
            label3.Visible = false;
            label2.Visible = false;
            logotext.Visible = false;
        }

        private async void Label3TextColorAnimation_Tick(object sender, EventArgs e)
        {
            Label3TextColorAnimation.Stop();

            if (label3.ForeColor != konec)
            {
                int[][] colors = {
                new int[] {128, 128, 128},
                new int[] {100, 100, 100},
                new int[] {80, 80, 80},
                new int[] {60, 60, 60},
                new int[] {50, 50, 50},
                new int[] {30, 30, 30},
                new int[] {20, 20, 20},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    label3.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
                    await Task.Delay(10);
                }
            }
        }

        private async void logotextColorChangeAnimation_Tick(object sender, EventArgs e)
        {
            logotextColorChangeAnimation.Stop();


            {
                int[][] colors = {
                new int[] {255, 255, 255},
                new int[] {222, 222, 222},
                new int[] {185, 186, 186},
                new int[] {144, 145, 146},
                new int[] {111, 111, 111},
                new int[] {67, 67, 67},
                new int[] {28, 28, 28},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    await Task.Delay(10);
                }
            }
        }

        private async void ExitAnimation_Tick(object sender, EventArgs e)
        {
            ExitAnimation.Stop();
            await Task.Delay(350);
            Application.Exit();
        }

        private async void OpacityAnimation_Tick(object sender, EventArgs e)
        {
            OpacityAnimation.Stop();

            for (int opacity = 99; opacity >= 1; opacity--)
            {
                this.Opacity = opacity / 100.0;
                await Task.Delay(10);
            }
        }

        private void LOGOTIMER_Tick(object sender, EventArgs e)
        {
            LOGOTIMER.Stop();
        }

        private async void HELLOANIMACEBARVA_Tick(object sender, EventArgs e)
        {
            HELLOANIMACEBARVA.Stop();
            if (label2.ForeColor != konec)
            {
                var colorSteps = new Color[] {
                    Color.FromArgb(255, 255, 255),
                    Color.FromArgb(222, 222, 222),
                    Color.FromArgb(185, 186, 186),
                    Color.FromArgb(144, 145, 146),
                    Color.FromArgb(111, 111, 111),
                    Color.FromArgb(67, 67, 67),
                    Color.FromArgb(28, 28, 28),
                    Color.FromArgb(10, 10, 10)
                };

                foreach (var color in colorSteps)
                {
                    label2.ForeColor = color;
                    await Task.Delay(10);
                }
            }
        }

        private void LOGOPRYC_Tick(object sender, EventArgs e)
        {
            LOGOPRYC.Stop();
        }

        private async void EXITCOLOR_Tick(object sender, EventArgs e)
        {
            EXITCOLOR.Stop();

            if (guna2ControlBox1.IconColor != konec)
            {
                int[][] colors = {
                new int[] {255, 255, 255},
                new int[] {222, 222, 222},
                new int[] {185, 186, 186},
                new int[] {144, 145, 146},
                new int[] {111, 111, 111},
                new int[] {67, 67, 67},
                new int[] {28, 28, 28},
                new int[] {10, 10, 10}
                };

                foreach (var color in colors)
                {
                    guna2ControlBox1.IconColor = Color.FromArgb(color[0], color[1], color[2]);
                    await Task.Delay(10);
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            TerminateProcess(GetCurrentProcess(), 1);
            SignInAnimation.Start();
            Label3TextColorAnimation.Start();
            Label2TextColorChangeAnimation.Start();
            logotextColorChangeAnimation.Start();
            SignInAnimation2.Start();
            OpacityAnimation.Start();
            AnimateLabel2Left.Start();
            ExitAnimation.Start();
        }



        private async void signin_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(username.Text, password.Text);
            if (KeyAuthApp.response.success)
            {
                label1.Hide();
                guna2ProgressBar1.Show();
                HELLOANIMACEBARVA.Start();
                SignInAnimation.Start();
                logotextColorChangeAnimation.Start();
                LOGOPRYC.Start();
                Label3TextColorAnimation.Start();
                EXITCOLOR.Start();
                SignInAnimation2.Start();
                await Task.Delay(100);
                LOGOTIMER.Start();
                PROGRESBARANIMACE.Start();

                await Task.Delay(2000);
                Loader loader = new Loader();
                loader.Show();
                this.Hide();

            }
            else


           ErrorMessageAnimation.Start();
            WrongXAnimation.Start();

            await Task.Delay(1000);
            WrongXBackAnimation.Start();
            ErrorMessageBackAnimation.Start();
        }

        private void logotext_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            OpacityAnimation.Start();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }
    }
}
