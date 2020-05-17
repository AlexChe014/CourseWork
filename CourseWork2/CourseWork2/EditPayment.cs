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
        payment item3;
        public EditPayment(payment pays)
        {
            item3 = pays;
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
                          where us.id_u == item3.id_s
                          select us).ToList();
            comboBox1.SelectedItem = $"{query2[0].id_u}. {query2[0].surname} {query2[0].name} {query2[0].patron}";
            textBox1.Text = item3.summa.ToString();
            numericUpDown1.Value = item3.pay_day.Date.Day;
            numericUpDown2.Value = item3.pay_day.Date.Month;
            numericUpDown3.Value = item3.pay_day.Date.Year;
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
                    var result = ((Main)Owner).db.payment.SingleOrDefault(n => n.id_s == item3.id_s);
                    DateTime date = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value);
                    try { date = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value); }
                    catch { throw new Exception("Некорректная дата"); }
                    if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
                    result.id_s = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf('.')));
                    result.summa = (float)Convert.ToDouble(textBox1.Text);
                    result.pay_day = date;
                    ((Main)Owner).paySheet = ((Main)Owner).db.payment.OrderBy(n => n.id_p).ToList();
                    ((Main)Owner).db.SaveChanges();
                    this.Close();
                }
                catch
                {

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
