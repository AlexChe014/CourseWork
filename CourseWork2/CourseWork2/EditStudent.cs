using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CourseWork2
{
    public partial class EditStudent : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        users item;
        public EditStudent(users user)
        {
            item = user;
            InitializeComponent();
            textBox1.Text = item.surname;
            textBox2.Text = item.name;
            textBox3.Text = item.patron;
            dateTimePicker1.Value = item.birthday.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.CheckString(new string[] { textBox1.Text, textBox2.Text}))
            {
                try
                {
                    bool check = (groupBox1.Enabled) ? true : false;
                    var result = ((Main)Owner).db.users.SingleOrDefault(n => n.id_u == item.id_u);
                    StudentForm stud = new StudentForm();
                    stud.EditStudent(item, result, textBox2.Text, textBox1.Text, textBox3.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text, check);
                    ((Main)Owner).usersSheet = ((Main)Owner).db.users.OrderBy(n => n.id_u).ToList();
                    ((Main)Owner).db.SaveChanges();
                    ((Main)Owner).ученикиToolStripMenuItem_Click(sender, e);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Не все поля заполнены!");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = (!groupBox1.Enabled) ? true : false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }
    }
}
