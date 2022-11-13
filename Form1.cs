using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowfall
{
    public partial class Form1 : Form
    {
        private readonly IList<Sn> Snowfalls;
        private readonly Timer t;
        Bitmap background, bg, sn, snow, SnowYellow, SnowBlack, SnowWhite;
        private Graphics draw;
        public Form1()
        {
            InitializeComponent();
            Snowfalls = new List<Sn>();
            background = (Bitmap)Properties.Resources.Winter;
            SnowWhite = (Bitmap)Properties.Resources.Snow;
            SnowYellow = (Bitmap)Properties.Resources.YellowSnow;
            SnowBlack = (Bitmap)Properties.Resources.BlackSnow;
            bg = new Bitmap(background,
                                  Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height);

            sn = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height);
            Addsnow();
            t = new Timer();
            t.Interval = 50;
            t.Tick += Timer_Tick;
            draw = Graphics.FromImage(sn);
            Addsnow();

        } 
        private void Timer_Tick(object sender, EventArgs e)
        {
            t.Stop();
            foreach (var snowflake in Snowfalls)
            {
                snowflake.Y += snowflake.Size;
                if (snowflake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflake.Y = -snowflake.Size;
                }
            }
            Draw();
            t.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }       
        private void Addsnow()
        {
            var r = new Random();
            for (int i = 0; i < 50; i++)
            {
                Snowfalls.Add(new Sn
                {
                    X = r.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -r.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Color = r.Next(0, 3),
                    Size = r.Next(10, 30)
                });
            }
        }
        private void Draw()
        {
            draw.DrawImage(background, 0, 0);
            var rnd = new Random();
            foreach (var sn in Snowfalls)
            {
                if (sn.Y > 0)
                {   
                    if (sn.Color == 0)
                    {
                        snow = SnowWhite;
                    }
                    else if (sn.Color == 1)
                    {
                        snow = SnowYellow;
                    }
                    else if (sn.Color == 2) 
                    {
                        snow = SnowBlack;                      
                    }
                    draw.DrawImage(snow,
                        new Rectangle(sn.X,
                                      sn.Y,
                                      sn.Size + 10,
                                      sn.Size + 10));
                }
            }

            var gr = CreateGraphics();
            gr.DrawImage(sn, 0, 0);
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (t.Enabled)
            {
                t.Stop();
            }
            else
            {
                t.Start();
            }
        }
        }
    }

