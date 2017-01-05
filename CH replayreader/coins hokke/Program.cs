using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace coins_hockey
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //[STAThread]
        public const int type_mon = 26;
        public static List<int[]> x, y, n;
        public static long yk = 0, time = 0;
        public static double ysk = 1.0;
        private static string file_read = "";
        private static bool file_exist = false;
        public static int sit = 0;
        private static int menu_pick = -1, button_pick = -1;
        public static bool is_moving = false;
        private static Image ago_im, next_im, fast_im, slow_im, play_im, pause_im;
        private static Image ago_dark, next_dark, fast_dark, slow_dark, play_dark, pause_dark;
        private static int replay_time, cnt_tick;

        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ago_im = Image.FromFile("./textures/ago.png");
            next_im = Image.FromFile("./textures/next.png");
            slow_im = Image.FromFile("./textures/slower.png");
            fast_im = Image.FromFile("./textures/faster.png");
            play_im = Image.FromFile("./textures/play.png");
            pause_im = Image.FromFile("./textures/pause.png");
            ago_dark = Image.FromFile("./textures/ago_dark.png");
            next_dark = Image.FromFile("./textures/next_dark.png");
            slow_dark = Image.FromFile("./textures/slower_dark.png");
            fast_dark = Image.FromFile("./textures/faster_dark.png");
            play_dark = Image.FromFile("./textures/play_dark.png");
            pause_dark = Image.FromFile("./textures/pause_dark.png");
            var MainForm = new Form1();
            MainForm.Show();
            Z.clwidth = MainForm.ClientSize.Width;
            Z.clheight = MainForm.ClientSize.Height - 50;
            var tim = new System.Diagnostics.Stopwatch();
            tim.Start();
            while (MainForm.Created)
            {
                if (sit == 1)
                {
                    if (!is_moving)
                        time += (long)(tim.ElapsedMilliseconds * ysk);
                    update(time);
                }
                tim.Restart();
                MainForm.drawoll();
                Application.DoEvents();
            }
        }
        public static void update(long tick)
        {
            while (yk * 21 < time)
                yk++;
            while (yk * 21 - 21 > time)
                yk--;
            if (yk >= x.Count || yk < 0)
            {
                yk = 0;
                time = 0;
            }
        }
        public static void graphic_play(Graphics g)
        {
            g.Clear(System.Drawing.Color.White);
            g.DrawLine(new Pen(Color.Maroon), 0, 0, Z.clwidth, 0);
            g.DrawLine(new Pen(Color.Maroon), 0, 0, 0, Z.clheight);
            g.DrawLine(new Pen(Color.Maroon), Z.clwidth - 1, 0, Z.clwidth - 1, Z.clheight - 1);
            g.DrawLine(new Pen(Color.Maroon), 0, Z.clheight - 1, Z.clwidth - 1, Z.clheight - 1);
            
            g.FillRectangle(System.Drawing.Brushes.Maroon, 0, 0, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, 1, 0, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, Z.clwidth - Z.radangl, 0, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, Z.clwidth - 2 * Z.radangl - 1, 0, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, 0, Z.clheight - Z.radangl, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, 1, Z.clheight - 2 * Z.radangl - 1, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, Z.clwidth - Z.radangl, Z.clheight - Z.radangl, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, Z.clwidth - 2 * Z.radangl - 1, Z.clheight - 2 * Z.radangl - 1, Z.radangl * 2, Z.radangl * 2);

            g.FillRectangle(System.Drawing.Brushes.Red, 0, 532 / 2 - 50, 3, 100);
            g.FillRectangle(System.Drawing.Brushes.Red, 790, 532 / 2 - 50, 3, 100);
            for (int i = 0; i < 4; i++)
            {
                int xh = x[(int)yk][i], yh = y[(int)yk][i], nh = n[(int)yk][i];
                g.DrawImage(Z.tcoin[nh].picre, xh - Z.tcoin[nh].r, yh - Z.tcoin[nh].r, 2 * Z.tcoin[nh].r, 2 * Z.tcoin[nh].r);
            }
            for (int i = 4; i < 11; i++)
            {
                int xh = x[(int)yk][i], yh = y[(int)yk][i], nh = n[(int)yk][i];
                g.DrawImage(Z.tcoin[nh].picob, xh - Z.tcoin[nh].r, yh - Z.tcoin[nh].r, 2 * Z.tcoin[nh].r, 2 * Z.tcoin[nh].r);
            }
            g.FillRectangle(Brushes.Maroon, 0, Z.clheight, Z.clwidth + 5, 50);
            int mid = Z.clwidth / 2;
            if (button_pick == 0)
                g.DrawImage(ago_im, mid - 160, Z.clheight + 5, 40, 40);
            else
                g.DrawImage(ago_dark, mid - 160, Z.clheight + 5, 40, 40);
            if (button_pick == 1)
                g.DrawImage(slow_im, mid - 90, Z.clheight + 5, 40, 40);
            else
                g.DrawImage(slow_dark, mid - 90, Z.clheight + 5, 40, 40);

            if (button_pick == 2 && ysk == 0)
                g.DrawImage(play_im, mid - 20, Z.clheight + 5, 40, 40);
            else if (ysk == 0)
                g.DrawImage(play_dark, mid - 20, Z.clheight + 5, 40, 40);
            else if (button_pick == 2)
                g.DrawImage(pause_im, mid - 20, Z.clheight + 5, 40, 40);
            else
                g.DrawImage(pause_dark, mid - 20, Z.clheight + 5, 40, 40);

            if (button_pick == 3)
                g.DrawImage(fast_im, mid + 50, Z.clheight + 5, 40, 40);
            else
                g.DrawImage(fast_dark, mid + 50, Z.clheight + 5, 40, 40);
            if (button_pick == 4)
                g.DrawImage(next_im, mid + 120, Z.clheight + 5, 40, 40);
            else
                g.DrawImage(next_dark, mid + 120, Z.clheight + 5, 40, 40);

            var f = new System.Drawing.Font("Arial", 30);
            g.DrawString(ysk.ToString(), f, System.Drawing.Brushes.Black, 60, 0);
            f = new System.Drawing.Font("Arial", 20);
            g.DrawString(file_read, f, Brushes.White, 5, Z.clheight + 5);
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Gray)), 0, Z.clheight - 10, Z.clwidth, 10);
            g.FillEllipse(Brushes.Black, time * Z.clwidth / replay_time - 5, Z.clheight - 10, 10, 10);
            //
            //g.DrawString(Z.ch1.ToString() + " : " + Z.ch2.ToString(), f, System.Drawing.Brushes.Black, 793 / 2 - 40, 5);
        }
        public static void graphic_pick(Graphics g)
        {
            g.Clear(Color.Maroon);
            var fn = new System.Drawing.Font("Courier", 20);
            g.DrawString("Введите имя файла, в котором записан ваш replay", fn, Brushes.White, 75, 200);
            g.FillRectangle(Brushes.White, 190, 260, 420, 50);
            g.DrawString(file_read, fn, Brushes.Black, 210, 265);
            fn = new System.Drawing.Font("Courier", 30);
            if (!file_exist)
                g.DrawString("Далее", fn, Brushes.Gray, 325, 350);
            else if (menu_pick == 1)
                g.DrawString("Далее", fn, Brushes.White, 325, 350);
            else
                g.DrawString("Далее", fn, Brushes.Bisque, 325, 350);
        }
        public static void read_data()
        {
            if (file_read == "")
                return;
            try
            {
                var oup = System.IO.File.OpenText(file_read + ".chrpl");
                x = new List<int[]>();
                y = new List<int[]>();
                n = new List<int[]>();
                cnt_tick = 0;
                while (!oup.EndOfStream)
                {
                    x.Add(new int[11]);
                    y.Add(new int[11]);
                    n.Add(new int[11]);
                    for (int i = 0; i < 11; i++)
                    {
                        var sr = oup.ReadLine().Split().Select(x1 => int.Parse(x1)).ToArray();
                        x[cnt_tick][i] = sr[1];
                        y[cnt_tick][i] = sr[2];
                        n[cnt_tick][i] = sr[0];
                    }
                    cnt_tick++;
                }
                oup.Close();
                sit = 1;
                replay_time = cnt_tick * 21;
            }
            catch { }
        }
        public static void klik(object sender, KeyEventArgs e)
        {
            if (sit == 0)
            {
                int code = (int)e.KeyCode;
                if (code >= (int)Keys.A && code <= (int)Keys.Z && file_read.Length <= 12)
                {
                    char c = (char)(code - Keys.A + 'a');
                    file_read += c;
                }
                if (code >= (int)Keys.D0 && code <= (int)Keys.D9 && file_read.Length <= 12)
                {
                    char c = (char)(code - Keys.D0 + '0');
                    file_read += c;
                }
                if (code == (int)Keys.Back && file_read.Length > 0)
                    file_read = file_read.Substring(0, file_read.Length - 1);
                var dich = Directory.GetFiles("./").Select(s => s.ToLower());
                file_exist = Directory.GetFiles("./").Select(s => s.ToLower()).Contains("./" + file_read + ".chrpl");
                if (code == (int)Keys.Enter)
                    read_data();
                return;
            }
            if (sit == 1)
            {
                if (e.KeyCode == Keys.F)
                    fast();
                if (e.KeyCode == Keys.S)
                    slow();
                if (e.KeyCode == Keys.P || e.KeyCode == Keys.Space)
                    pause();
                if (e.KeyCode == Keys.N)
                    next();
                if (e.KeyCode == Keys.A)
                    ago();
            }
        }
        public static void ago()
        {
            time -= 1500;
        }
        public static void next()
        {
            time += 1500;
        }
        public static void fast()
        {
            ysk = Math.Min(ysk * 2, 64);
        }
        public static void pause()
        {
            if (ysk != 0)
                ysk = 0;
            else
                ysk = 1;
        }
        public static void slow()
        {
            ysk = Math.Max(0.25, ysk / 2);
        }
        public static void mdklik(object sender, MouseEventArgs e)
        {
            if (menu_pick == 1 && sit == 0)
                read_data();
            if (sit == 1)
            {
                if (e.Y >= Z.clheight - 10 && e.Y < Z.clheight && Math.Abs(e.X - Z.clwidth * time / replay_time) <= 5)
                {
                    is_moving = true;
                }                
                if (button_pick == 0)
                    ago();
                if (button_pick == 1)
                    slow();
                if (button_pick == 2)
                    pause();
                if (button_pick == 3)
                    fast();
                if (button_pick == 4)
                    next();
            }
        }
        public static void mmklik(object sender, MouseEventArgs e)
        {
            if (sit == 0 && e.X >= 320 && e.X <= Z.clwidth - 320 && e.Y >= 345 && e.Y <= 395)
                menu_pick = 1;
            else
                menu_pick = -1;
            if (sit == 1)
            {
                int mid = Z.clwidth / 2;
                if (e.Y >= Z.clheight && e.X >= mid - 175 && e.X <= mid + 175)
                    button_pick = (e.X - mid + 175) / 70;
                else
                    button_pick = -1;
                if (is_moving)
                {
                    int x = Math.Min(Math.Max(e.X, 0), Z.clwidth);
                    time = replay_time * x / Z.clwidth;
                    yk = cnt_tick * x / Z.clwidth;
                    update(0);
                }
            }
        }
        public static void muklik(object sender, MouseEventArgs e)
        {
            is_moving = false;
        }
    }

    class cointype
    {
        public int r { get; set; }
        public int m { get; set; }
        public int stoim { get; set; }
        public string name { get; set; }
        public System.Drawing.Image picob { get; set; }
        public Image picre { get; set; }
        public cointype(int r1, int m1, int s1, string n, string fileor, string filere, string ras = ".png")
        {
            m = m1;
            r = r1;
            stoim = s1;
            name = n;
            picob = Image.FromFile("textures\\" + fileor + ras);
            picre = Image.FromFile("textures\\" + filere + ras);
        }
    }
}

//(x1m1+x2m2)k3-(y1m1+y2m2)
//m2k4-m2k3
//https://query.yahooapis.com/v1/public/yql?q=select+*+from+yahoo.finance.xchange+where+pair+=+%22USDRUB,EURRUB%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=
//{"query":{"count":2,"created":"2015-04-17T16:26:06Z","lang":"ru-RU","results":{"rate":[{"id":"USDRUB","Name":"USD/RUB","Rate":"52.3635","Date":"4/17/2015","Time":"5:26pm","Ask":"52.2100","Bid":"52.3635"},{"id":"EURRUB","Name":"EUR/RUB","Rate":"56.4181","Date":"4/17/2015","Time":"5:26pm","Ask":"56.4364","Bid":"56.3998"}]}}}