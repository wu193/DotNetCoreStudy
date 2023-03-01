using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
     
            //File.WriteAllText(@"C:\Users\z1494\Desktop\Vue学习\a.txt", textBox1.Text);
            // 如果文件不存在，创建文件； 如果存在，覆盖文件 
            StreamWriter sW1 = File.AppendText(@"C:\Users\z1494\Desktop\Vue学习\a.txt");
            sW1.Write(textBox1.Text);
            sW1.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
