using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class AutoForm
    {
        string num { get; set; }
        string model { get; set; }
        int year { get; set; }
        public driveCourseEntities1 db = new driveCourseEntities1();

        public AutoForm()
        { }
        public AutoForm(string num, string model, int year)
        {
            this.num = num;
            this.model = model;
            this.year = year;
        }
        public void AddAuto()
        {
            var nums = (from a in db.auto
                        select a.num).ToList();
            foreach (string num in nums)
            {
                if (num == this.num.ToUpper()) throw new Exception("Данный автомобиль уже зарегестрирован в базе");
            }
            int new_id = db.objects.Max(n => n.id_o) + 1;
            objects obj = new objects { id_o = new_id, type_o = "a" };
            auto auto = new auto
            {
                id_a = new_id,
                model = model,
                num = num.ToUpper(),
                year_a = year
            };
            db.objects.Add(obj);
            db.auto.Add(auto);
            db.SaveChanges();


        }
        public void EditAuto(auto item,string mod, string numb, int y, auto result)
        {
            if (item.num != numb)
            {
                var nums = (from a in db.auto
                            select a.num).ToList();
                foreach (string num in nums)
                {
                    if (num == numb.ToUpper()) throw new Exception("Данный автомобиль уже зарегестрирован в базе");
                }
            }
            result = db.auto.SingleOrDefault(n => n.id_a == item.id_a);
            result.model = mod;
            result.num = numb;
            result.year_a = y;
            db.SaveChanges();
        }
    }
}
