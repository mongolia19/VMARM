using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMARM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //movMR 0 13
            //movMr 1 14
            //Or    0 1
            //12=1
            //13=2
            CPU mMachine = new CPU();
            mMachine.RAM = new UInt32[]{1,0,13,9,1,1,12,9,7,0,1,9,1,2,0,0};
            for (int i = 0; i < 18; i++)
            {
                CPU.OneTick(mMachine);
                
            }

        }
    }
}
