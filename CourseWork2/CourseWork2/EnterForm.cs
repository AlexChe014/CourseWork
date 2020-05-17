using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    public partial class EnterForm : Form
    {
        public string role, login, password;
        public EnterForm()
        {
            InitializeComponent();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void button1_Click(object senderEnt, EventArgs eEnt)
        {
            login = textBox1.Text;
            password = textBox2.Text;
            
        }
    }
}
