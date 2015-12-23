using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace coins_hokke
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //[STAThread]
        public const int tipmon = 32;
        public static List<int[]> x, y, n;
        public static long yk = 0, time = 0;
        public static double ysk = 1.0;
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var MainForm = new Form1();
            MainForm.Show();
            var oup = System.IO.File.OpenText("replay.chrpl");
            x = new List<int[]>();
            y = new List<int[]>();
            n = new List<int[]>();
            int ih = 0;
            while (!oup.EndOfStream)
            {
                x.Add(new int[11]);
                y.Add(new int[11]);
                n.Add(new int[11]);
                for (int i = 0; i < 11; i++)
                {
                    var sr = oup.ReadLine().Split().Select(x1 => int.Parse(x1)).ToArray();
                    x[ih][i] = sr[1];
                    y[ih][i] = sr[2];
                    n[ih][i] = sr[0];
                }
                ih++;
            }
            var tim = new System.Diagnostics.Stopwatch();
            tim.Start();
            while (MainForm.Created)
            {
                time += (long)(tim.ElapsedMilliseconds * ysk);
                tim.Restart();
                MainForm.drawoll();
                Application.DoEvents();
                prd(time);
            }
        }
        public static void prd(long tick)
        {
            while (yk * 21 < time) yk++;
            while (yk * 21 - 21 > time) yk--;
            if (yk >= x.Count || yk < 0)
            {
                yk = 0;
                time = 0;
            }
        }
        public static void getgrp1(System.Drawing.Graphics g)
        {
            g.Clear(System.Drawing.Color.White);
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
            var f = new System.Drawing.Font("Arial", 30);
            g.DrawString(ysk.ToString(), f, System.Drawing.Brushes.Black, 0, 0);
            //
            //g.DrawString(Z.ch1.ToString() + " : " + Z.ch2.ToString(), f, System.Drawing.Brushes.Black, 793 / 2 - 40, 5);
        }
        public static void klik(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F) ysk = Math.Min(ysk * 2, 64);
            if (e.KeyCode == System.Windows.Forms.Keys.S) ysk = Math.Max(0.25, ysk / 2);
            if (e.KeyCode == System.Windows.Forms.Keys.P && ysk != 0) ysk = 0;
            else
                if (e.KeyCode == System.Windows.Forms.Keys.P) ysk = 1;
            if (e.KeyCode == System.Windows.Forms.Keys.N) time += 1500;
            if (e.KeyCode == System.Windows.Forms.Keys.A) time -= 1500;
        }
        public static void mdklik(object sender, MouseEventArgs e)
        {

        }
        public static void mmklik(object sender, MouseEventArgs e)
        {

        }
        public static void muklik(object sender, MouseEventArgs e)
        {

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
            picob = Image.FromFile(fileor + ras);
            picre = Image.FromFile(filere + ras);
        }
    }
}

//(x1m1+x2m2)k3-(y1m1+y2m2)
//m2k4-m2k3
//https://query.yahooapis.com/v1/public/yql?q=select+*+from+yahoo.finance.xchange+where+pair+=+%22USDRUB,EURRUB%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=
//{"query":{"count":2,"created":"2015-04-17T16:26:06Z","lang":"ru-RU","results":{"rate":[{"id":"USDRUB","Name":"USD/RUB","Rate":"52.3635","Date":"4/17/2015","Time":"5:26pm","Ask":"52.2100","Bid":"52.3635"},{"id":"EURRUB","Name":"EUR/RUB","Rate":"56.4181","Date":"4/17/2015","Time":"5:26pm","Ask":"56.4364","Bid":"56.3998"}]}}}