using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerWatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
            public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
            public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
            public UInt32 uCount;    // 点滅する回数
            public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
        }

        // 点滅を止める
        public const UInt32 FLASHW_STOP = 0;
        // タイトルバーを点滅させる
        public const UInt32 FLASHW_CAPTION = 1;
        // タスクバー・ボタンを点滅させる
        public const UInt32 FLASHW_TRAY = 2;
        // タスクバー・ボタンとタイトルバーを点滅させる
        public const UInt32 FLASHW_ALL = 3;
        // FLASHW_STOPが指定されるまでずっと点滅させる
        public const UInt32 FLASHW_TIMER = 4;
        // ウィンドウが最前面に来るまでずっと点滅させる
        public const UInt32 FLASHW_TIMERNOFG = 12;

        private static Form1 _form1Instance;

        public static Form1 Form1Istance
        {
            get
            {
                return _form1Instance;
            }
            set
            {
                _form1Instance = value;
            }
        }

        public bool timer1e
        {
            set
            {
                timer1.Enabled = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1Istance = this;
            Form2 f = new Form2();
            f.Show();
        }
        TimeSpan time = new TimeSpan(00,00,00);
        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(01,00,00);
            time = time.Add(add);
            label1.Text = time.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time < new TimeSpan(00,00,00))
            {
                time = new TimeSpan(00, 00, 00);
                label1.Text = time.ToString("c");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 10, 00);
            time = time = time.Add(add);
            label1.Text = time.ToString("c");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 01, 00);
            time = time.Add(add);
            label1.Text = time.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 00, 10);
            time = time.Add(add);
            label1.Text = time.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 00, 01);
            time = time.Add(add);
            label1.Text = time.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 00, 01);
            time = time.Subtract(add);
            label1.Text = time.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 10, 00);
            time = time.Subtract(add);
            label1.Text = time.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 01, 00);
            time = time.Subtract(add);
            label1.Text = time.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(00, 00, 10);
            time = time.Subtract(add);
            label1.Text = time.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TimeSpan add = new TimeSpan(01, 00, 00);
            time = time.Subtract(add);
            label1.Text = time.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            time = new TimeSpan(00, 00, 00);
            label1.Text = time.ToString();
        }

        enum TimerMode
        {
            Timer,
            StopWatch
        }

        TimerMode mode = TimerMode.Timer;

        private void button12_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (mode == TimerMode.Timer)
            {
                if (time == new TimeSpan(00,00,00))
                {
                    timer2.Enabled = false;
                    FLASHWINFO fInfo = new FLASHWINFO();
                    fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                    fInfo.hwnd = GetMainWindowHandle("devenv");
                    fInfo.dwFlags = FLASHW_TIMERNOFG;
                    fInfo.dwTimeout = 10;
                    FlashWindowEx(ref fInfo);
                    MessageBox.Show("時間です。","通知",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                time = time.Subtract(new TimeSpan(00, 00, 01));
                label1.Text = time.ToString();
            }
            if (mode == TimerMode.StopWatch)
            {
                time = time.Add(new TimeSpan(00, 00, 01));
                label1.Text = time.ToString();
            }
        }

        public IntPtr GetMainWindowHandle(string processName)
        {
            Process curProcess = Process.GetCurrentProcess();
            Process[] allProcesses = Process.GetProcessesByName(processName);

            foreach (Process oneProcess in allProcesses)
            {
                if (oneProcess.MainWindowHandle != IntPtr.Zero)
                {
                    return oneProcess.MainWindowHandle;
                }
            }

            // 指定したプロセス名のアプリケーションが見つからない！
            return IntPtr.Zero;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = numericUpDown1.Value.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                label3.Text = "R"+numericUpDown2.Value.ToString();
            }
            else
            {
                label3.Text = numericUpDown2.Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label5.Text = textBox2.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                mode = TimerMode.StopWatch;
            }
            else
            {
                mode = TimerMode.Timer;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                label6.Text = "ラウンド数";
                textBox2.Enabled = false;
                label5.Text = "ラウンド";
                label3.Text = "R" + numericUpDown2.Value.ToString();
            }
            else
            {
                label5.Text = textBox2.Text;
                label6.Text = "チーム2(右)";
                textBox2.Enabled = true;
                label3.Text = numericUpDown2.Value.ToString(); 
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Visible = true;
                label8.Text = "前半";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Visible = true;
                label8.Text = "休憩";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                label8.Visible = true;
                label8.Text = "後半";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                label8.Visible = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            if (checkBox2.Checked == false)
            {
                numericUpDown2.Value = 0;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = "チーム1";
            textBox2.Text = "チーム2";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            numericUpDown1.Value = 0;
            if (checkBox2.Checked == false)
            {
                numericUpDown2.Value = 0;
            }
            time = new TimeSpan(00, 00, 00);
            label1.Text = time.ToString();
            radioButton4.Checked = true;
        }

        /*
        Form2 用の設定たち
        */

        public Label form1labeltime
        {
            get { return label1; }
        }

        public Label form1labelp1
        {
            get { return label2; }
        }

        public Label form1labelp2
        {
            get { return label3; }
        }

        public Label form1labelt1
        {
            get { return label4; }
        }

        public Label form1labelt2
        {
            get { return label5; }
        }

        public Label form1labelsiai
        {
            get { return label8; }
        }
        string[] stArrayData;

        private void button17_Click(object sender, EventArgs e)
        {
            time = new TimeSpan(int.Parse(stArrayData[0]), int.Parse(stArrayData[1]), int.Parse(stArrayData[2]));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            label3.Text = numericUpDown2.Value.ToString();
            label5.Text = textBox2.Text;
        }
        TimeSpan preset = new TimeSpan(00,00,00);
        private void button16_Click_1(object sender, EventArgs e)
        {
            time = new TimeSpan(int.Parse(numericUpDown3.Value.ToString()), int.Parse(numericUpDown4.Value.ToString()),int.Parse(numericUpDown5.Value.ToString()));
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label1.Text = time.ToString();
        }

        /*
        終わり
        */
    }
}
