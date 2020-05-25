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
        public PayForm()
        { }
        public PayForm(float summa)
        {
            this.summa = summa;
        }
        public void AddPayment(DateTimePicker dateTimePicker, ComboBox comboBox1)
        {
            int new_id = db.payment.Max(n => n.id_p) + 1;
            DateTime date = dateTimePicker.Value;
            if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
            if (this.summa <= 0) throw new Exception("Неверное значение суммы");
            string id_st = comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf('.'));
            var query = (from u in db.users
                         where u.id_u.ToString() == id_st
                         select u.id_u).ToList();
            payment new_pay = new payment
            {
                id_p = new_id,
                id_s = query[0],
                summa = (float)Math.Round(this.summa, 2),
                pay_day = date
            };
            db.payment.Add(new_pay);
            db.SaveChanges();

        }
        public void EditPayment(payment item, payment result, DateTime date, int id, float sum)
        {
            if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
            if (sum <= 0) throw new Exception("Неверное значение суммы");
            result.id_s = id;
            result.summa = (float)Math.Round(sum, 2);
            result.pay_day = date;
            db.SaveChanges();
        }
    }
}
