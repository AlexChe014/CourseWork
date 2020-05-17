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
    public partial class AddAuto : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        public AddAuto()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.CheckString(new string[] { maskedTextBox1.Text, textBox1.Text }))
            {
                try
                {
                    AutoForm auto = new AutoForm(maskedTextBox1.Text, textBox1.Text, (int)numericUpDown1.Value);
                    auto.AddAuto();
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else MessageBox.Show("Не все поля заполнены!");
        }
    }
}
