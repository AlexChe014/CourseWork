using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class PayForm
    {
        string id_s { get; set; }
        float summa { get; set; }
        DateTime pay_day { get; set; }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public PayForm(float summa)
        {
            this.summa = summa;
        }
        public void AddPayment(int year, int month, int day, ComboBox comboBox1)
        {
            try
            {
                int new_id = db.payment.Max(n => n.id_p) + 1;
                DateTime date;
                try { date = new DateTime(year, month, day); }
                catch { throw new Exception("Некорректная дата"); }
                if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
                string id_st = comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf('.'));
                var query = (from u in db.users
                             where u.id_u.ToString() == id_st
                             select u.id_u).ToList();
                payment new_pay = new payment
                {
                    id_p = new_id,
                    id_s = query[0],
                    summa = this.summa,
                    pay_day = date
                };
                db.payment.Add(new_pay);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
