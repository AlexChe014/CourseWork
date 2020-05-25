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
        public StudentForm()
        { }
        public void AddStudent(DateTimePicker dateTimePicker)
        {
            var logs = (from l in db.users
                        select l.login).ToList();

            foreach (string log in logs)
            {
                if (log == login) throw new Exception("Данный логин уже занят");
            }
            int new_id = db.objects.Max(n => n.id_o) + 1;
            DateTime date = dateTimePicker.Value;
            if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
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
        public void EditStudent(users item, users result, string name, string surname, string patron, DateTime date, string old_pass, string new_pass, bool check)
        {
            if (date > DateTime.Now) throw new Exception("Эта дата еще не наступила");
            if (check)
            {
                if (old_pass != item.pass) throw new Exception("Неверный пароль");
                else if (old_pass == item.pass && new_pass == "") throw new Exception("Некорректный пароль");
                else result.pass = new_pass;
            }
            patron = (!String.IsNullOrEmpty(patron) ? Program.NameString(patron) : "");
            result.surname = Program.NameString(surname);
            result.name = Program.NameString(name);
            result.patron = patron;
            result.birthday = date;
            db.SaveChanges();
        }
    }
}
