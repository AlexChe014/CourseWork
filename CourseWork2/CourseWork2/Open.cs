using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class Open
    {
        string login { get; set; }
        string password { get; set; }
        public Open(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public string[] Check()
        {
            try
            {
                string[] arr = new string[2];
                var query = (from us in db.users
                             where us.login == this.login
                             select us).Distinct().First();
                if (this.password == query.pass)
                {
                    arr[0] = query.role;
                    arr[1] = query.name;
                }
                return arr;
            }
            catch { throw new Exception("Неверный логин/пароль"); }           
        }
    }
}
