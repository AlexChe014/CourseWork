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
    public partial class EditTeacher : Form
        
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        users item;
        public EditTeacher(users user)
        {
            item = user;
            InitializeComponent();
            textBox1.Text = item.surname;
            textBox2.Text = item.name;
            textBox3.Text = item.patron;
            dateTimePicker1.Value = item.birthday.Value;
            checkBox1.Checked = (item.role == "a") ? true : false;
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
                    var result = ((Main)Owner).db.users.SingleOrDefault(n => n.id_u == item.id_u);
                    bool check = checkBox2.Checked ? true : false, check2 = checkBox1.Checked ? true : false;
                    TeacherForm teach = new TeacherForm();
                    teach.EditTeacher(item, result, textBox2.Text, textBox1.Text, textBox3.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text, check, check2);
                    ((Main)Owner).usersSheet = ((Main)Owner).db.users.OrderBy(n => n.id_u).ToList();
                    ((Main)Owner).db.SaveChanges();
                    ((Main)Owner).преподавателиToolStripMenuItem_Click(sender, e);
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
