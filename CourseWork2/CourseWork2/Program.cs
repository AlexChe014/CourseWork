using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
        public static bool CheckString(string[] array)
        {
            bool check = true;
            foreach (string str in array)
            {
                if (String.IsNullOrEmpty(str))
                { check = false; break; }
            }
            return check;
        }
        public static string NameString(string name)
        {
            string new_name = name[0].ToString().ToUpper() + name.Remove(0, 1).ToLower();
            return new_name;
        }
    }
}
