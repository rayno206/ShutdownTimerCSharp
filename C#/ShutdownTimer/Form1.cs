
/*
    Author: Patrick Thanh Phuong Mai
    Email: rayno206@gmail.com
    GitHub: https://github.com/rayno206
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ShutdownTimer
{
    public partial class Form1 : Form
    {
        public static int timeLeft;
        public static int hour;
        public static int min;
        public static int sec;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000; // Time Interval
            timer.Tick += new EventHandler(timer_Tick); //Raising Event on each tick
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Timing Shutdown");
            comboBox1.Items.Add("Timing Restart");
            comboBox1.Items.Add("Timing Sleep");
            comboBox1.SelectedItem = "Timing Shutdown";
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
            txtHour.Text = "00";
            txtMinute.Text = "60";
            txtSecond.Text = "00";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("Timing Shutdown"))
            {
                if (timeLeft > 0)
                {
                    timeLeft = timeLeft - 1; // decrement of timeleft on each tick
                    hour = timeLeft / 3600; // Left hours
                    min = (timeLeft - (hour * 3600)) / 60; //Left Minutes
                    sec = timeLeft - (hour * 3600) - (min * 60); //Left Seconds
                    txtHour.Text = hour.ToString(); // Setting hour Text on each timer tick
                    txtMinute.Text = min.ToString(); // Setting minutes Text on each timer tick
                    txtSecond.Text = sec.ToString(); // Setting sec Text on each timer tick
                }
                else
                {
                    timer.Stop(); // Stop Timer
                    Process.Start("shutdown", "/s /t 0"); // Shutdown PC when Time is over
                }
            }
            else if (comboBox1.SelectedItem.Equals("Timing Restart"))
            {
                if (timeLeft > 0)
                {
                    timeLeft = timeLeft - 1; // decrement of timeleft on each tick
                    hour = timeLeft / 3600; // Left hours
                    min = (timeLeft - (hour * 3600)) / 60; //Left Minutes
                    sec = timeLeft - (hour * 3600) - (min * 60); //Left Seconds
                    txtHour.Text = hour.ToString(); // Setting hour Text on each timer tick
                    txtMinute.Text = min.ToString(); // Setting minutes Text on each timer tick
                    txtSecond.Text = sec.ToString(); // Setting sec Text on each timer tick
                }
                else
                {
                    timer.Stop(); // Stop Timer
                    Process.Start("shutdown", "/r /t 0"); // Restart PC when Time is over
                }
            }
            else if (comboBox1.SelectedItem.Equals("Timing Sleep"))
            {
                if (timeLeft > 0)
                {
                    timeLeft = timeLeft - 1; // decrement of timeleft on each tick
                    hour = timeLeft / 3600; // Left hours
                    min = (timeLeft - (hour * 3600)) / 60; //Left Minutes
                    sec = timeLeft - (hour * 3600) - (min * 60); //Left Seconds
                    txtHour.Text = hour.ToString(); // Setting hour Text on each timer tick
                    txtMinute.Text = min.ToString(); // Setting minutes Text on each timer tick
                    txtSecond.Text = sec.ToString(); // Setting sec Text on each timer tick
                }
                else
                {
                    timer.Stop(); // Stop Timer
                    SetSuspendState(false, true, true); // Shutdown PC when Time is over
                }
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            hour = Int16.Parse(txtHour.Text);
            min = Int16.Parse(txtMinute.Text);
            sec = Int16.Parse(txtSecond.Text);
            timeLeft = hour * 3600 + min * 60 + sec;
            timer.Start();
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            txtHour.Enabled = false;
            txtMinute.Enabled = false;
            txtSecond.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            hour = 0;
            min = 0;
            sec = 0;
            timeLeft = 0;
            txtHour.Text = "00";
            txtMinute.Text = "60";
            txtSecond.Text = "00";
            timer.Stop();
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
            txtHour.Enabled = true;
            txtMinute.Enabled = true;
            txtSecond.Enabled = true;
            comboBox1.Enabled = true;
        }
    }
}
