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
            numericUpDown1.Value = (int)item.birthday.Value.Day;
            numericUpDown2.Value = (int)item.birthday.Value.Month;
            numericUpDown3.Value = (int)item.birthday.Value.Year;
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
                    DateTime date = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value);
                    try { date = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value); }
                    catch { throw new Exception("Некорректная дата!"); }
                    if (groupBox1.Enabled)
                    {
                        if (textBox5.Text != item.pass) throw new Exception("Неверный пароль");
                        else if (textBox5.Text == item.pass && textBox6.Text == "") throw new Exception("Некорректный пароль");
                        else result.pass = textBox6.Text;
                    }
                    string patron = (!String.IsNullOrEmpty(textBox3.Text) ? Program.NameString(textBox3.Text) : "");
                    result.surname = Program.NameString(textBox1.Text);
                    result.name = Program.NameString(textBox2.Text);
                    result.patron = patron;
                    result.birthday = date;
                    ((Main)Owner).usersSheet = ((Main)Owner).db.users.OrderBy(n => n.id_u).ToList();
                    ((Main)Owner).db.SaveChanges();
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
