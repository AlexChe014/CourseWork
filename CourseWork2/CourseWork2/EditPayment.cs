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
    public partial class EditPayment : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        payment item;
        public EditPayment(payment pays)
        {
            item = pays;
            InitializeComponent();
            var query = (from us in db.users
                         where us.role.ToString() == "s"
                         orderby us.id_u
                         select new { us.id_u, us.surname, us.name, us.patron }).Distinct();
            foreach (var str in query)
            {
                comboBox1.Items.Add($"{str.id_u}. {str.surname} {str.name} {str.patron}");
            }
            var query2 = (from us in db.users
                          where us.id_u == item.id_s
                          select us).ToList();
            comboBox1.SelectedItem = $"{query2[0].id_u}. {query2[0].surname} {query2[0].name} {query2[0].patron}";
            textBox1.Text = item.summa.ToString();
            dateTimePicker1.Value = item.pay_day;
            
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
                    var result = ((Main)Owner).db.payment.SingleOrDefault(n => n.id_p == item.id_p);
                    PayForm pay = new PayForm();
                    pay.EditPayment(item, result, dateTimePicker1.Value
                        , Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf('.'))),
                        (float)Convert.ToDouble(textBox1.Text));
                    ((Main)Owner).paySheet = ((Main)Owner).db.payment.OrderBy(n => n.id_p).ToList();
                    ((Main)Owner).db.SaveChanges();
                    ((Main)Owner).платежиStripMenuItem_Click(sender, e);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Не все поля заполнены!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
