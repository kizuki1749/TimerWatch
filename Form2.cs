using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerWatch
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int GetMenuItemCount(IntPtr hMenu);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            const Int32 MF_BYPOSITION = 0x400;
            const Int32 MF_REMOVE = 0x1000;

            IntPtr menu = GetSystemMenu(this.Handle, false);
            int menuCount = GetMenuItemCount(menu);
            if (menuCount > 1)
            {
                //メニューの「閉じる」とセパレータを削除
                RemoveMenu(menu, (uint)(menuCount - 1), MF_BYPOSITION | MF_REMOVE);
                RemoveMenu(menu, (uint)(menuCount - 2), MF_BYPOSITION | MF_REMOVE);
                DrawMenuBar(this.Handle);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Form1.Form1Istance.form1labeltime.Text;
            label2.Text = Form1.Form1Istance.form1labelp1.Text;
            label3.Text = Form1.Form1Istance.form1labelp2.Text;
            label4.Text = Form1.Form1Istance.form1labelt1.Text;
            label5.Text = Form1.Form1Istance.form1labelt2.Text;
            label8.Text = Form1.Form1Istance.form1labelsiai.Text;
            label8.Visible = Form1.Form1Istance.form1labelsiai.Visible;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}