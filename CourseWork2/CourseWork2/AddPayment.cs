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
    public partial class AddPayment : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        public AddPayment()
        {
            InitializeComponent();
            var query = (from us in db.users
                         where us.role.ToString() == "s"
                         orderby us.id_u
                         select new { us.id_u, us.surname, us.name, us.patron }).Distinct();
            foreach (var str in query)
            {
                comboBox1.Items.Add($"{str.id_u}. {str.surname} {str.name} {str.patron}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.CheckString(new string[] { textBox1.Text }) && comboBox1.SelectedIndex != 0)
            {
                try
                {
                    PayForm pay = new PayForm((float)Convert.ToDouble(textBox1.Text));
                    pay.AddPayment(dateTimePicker1, comboBox1);
                    ((Main)Owner).платежиStripMenuItem_Click(sender, e);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
            else MessageBox.Show("Не все поля заполнены!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }
    }
}
