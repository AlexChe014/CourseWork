using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class Show
    {
        public Show()
        { }
        driveCourseEntities1 db = new driveCourseEntities1();
        public void ShowUser(DataGridView dataGridView, string role1, string role2)
        {
            driveCourseEntities1 db = new driveCourseEntities1();
            dataGridView.Visible = true;
            dataGridView.ReadOnly = true;
            List<users> usersSheet = (from us in db.users
                                      select us).ToList();
            var query = (from t in usersSheet
                         where t.role.ToString() == role1 || t.role.ToString() == role2
                         orderby t.id_u
                         select new { t.id_u, t.surname, t.name, t.patron, t.birthday }).Distinct().ToList();
            dataGridView.DataSource = query;
            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[1].HeaderText = "Фамилия";
            dataGridView.Columns[2].HeaderText = "Имя";
            dataGridView.Columns[3].HeaderText = "Отчество";
            dataGridView.Columns[4].HeaderText = "Дата рождения";
        }
        public void ShowAuto(DataGridView dataGridView)
        {
            driveCourseEntities1 db = new driveCourseEntities1();
            dataGridView.Visible = true;
            dataGridView.ReadOnly = true;
            List<auto> autoSheet = (from au in db.auto
                         select au).ToList();
            var autos = (from au in autoSheet
                         orderby au.id_a
                         select new { au.id_a, au.model, au.num, au.year_a }).ToList();
            dataGridView.DataSource = autos;
            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[1].HeaderText = "Модель";
            dataGridView.Columns[2].HeaderText = "Гос. номер";
            dataGridView.Columns[3].HeaderText = "Год";
        }
        public void ShowPay(DataGridView dataGridView)
        {
            driveCourseEntities1 db = new driveCourseEntities1();
            dataGridView.Visible = true;
            dataGridView.ReadOnly = true;
            List<payment> paySheet = (from p in db.payment
                        select p).ToList();
            var pays = (from p in paySheet
                        join u in db.users on p.id_s equals u.id_u
                        orderby p.id_p
                        select new { p.id_p, u.surname, u.name, u.patron, p.summa, p.pay_day }).ToList();
            dataGridView.DataSource = pays;
            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[1].HeaderText = "Фамилия";
            dataGridView.Columns[2].HeaderText = "Имя";
            dataGridView.Columns[3].HeaderText = "Отчество";
            dataGridView.Columns[4].HeaderText = "Сумма";
            dataGridView.Columns[5].HeaderText = "Дата платежа";
        }
        public Form ShowAddForm(int activeTable)
        {
            switch(activeTable)
            {
                case (1): { AddTeacher form = new AddTeacher(); return form; }
                case (2): { AddStudent form = new AddStudent(); return form; }
                case (3): { AddAuto form2 = new AddAuto(); return form2; }
                case (4): { AddPayment form3 = new AddPayment(); return form3; }
                default: Form f = new Form(); return f;

            }
        }
        public Form ShowEditForm(DataGridView dataGridView, int activeTable)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
                switch (activeTable)
                {
                    case (1):
                        {
                            List<users> query = (from us in db.users
                                                 select us).ToList();
                            users item = query.First(n => n.id_u.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                            EditTeacher form = new EditTeacher(item);
                            return form;
                        }
                    case (2):
                        {
                            List<users> query = (from us in db.users
                                                 select us).ToList();
                            users item = query.First(n => n.id_u.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                            EditStudent form = new EditStudent(item);
                            return form;
                        }
                    case (3):
                        {
                            List<auto> query = (from au in db.auto
                                                 select au).ToList();
                            auto item = query.First(n => n.id_a.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                            EditAuto form = new EditAuto(item);
                            return form;
                        }
                    case (4):
                        {
                            List<payment> query = (from p in db.payment
                                                    select p).ToList();
                            payment item = query.First(n => n.id_p.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                            EditPayment form = new EditPayment(item);
                            return form;
                        }
                    default: Form f = new Form(); return f;
                }
            }
            else { Form f = new Form(); return f; }
        }
        
    }
}
