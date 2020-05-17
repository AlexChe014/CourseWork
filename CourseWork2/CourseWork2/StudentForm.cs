using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class StudentForm
    {
        string surname { get; set; }
        string name { get; set; }
        string patron { get; set; }
        string login { get; set; }
        string password { get; set; }
        DateTime birthday { get; set; }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public StudentForm(string surname, string name, string patron, string login, string password)
        {
            this.surname = surname;
            this.name = name;
            this.patron = patron;
            this.login = login;
            this.password = password;
        }
        public void AddStudent(int year, int month, int day)
        {          
                var logs = (from l in db.users
                            select l.login).ToList();

                foreach (string log in logs)
                { if (log == login) throw new Exception("Данный логин уже занят"); }
                int new_id = db.objects.Max(n => n.id_o) + 1;
                DateTime date = new DateTime(year, month, day);
                try { date = new DateTime(year, month, day); }
                catch { throw new Exception("Некорректная дата!"); }
                objects obj = new objects { id_o = new_id, type_o = "u" };
                users new_user = new users
                {
                    id_u = new_id,
                    surname = this.surname,
                    name = this.name,
                    patron = this.patron,
                    login = this.login,
                    pass = password,
                    birthday = date,
                    role = "s"
                    
                };
                db.objects.Add(obj);
                db.users.Add(new_user);
                db.SaveChanges();
            
        }
    }
}
