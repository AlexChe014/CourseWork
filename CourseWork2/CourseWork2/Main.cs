using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.HSSF.UserModel;

namespace CourseWork2
{
    public partial class Main : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        public List<users> usersSheet;
        public List<auto> autoSheet;
        public List<payment> paySheet;
        //функция включения отображения скрытых элементов
        public bool labelsOn(string[] arr)
        {
            label2.Visible = true;
            label3.Text = arr[1];
            linkLabel1.Text = "Выйти";
            bool check = false;
            switch (arr[0])
            {
                case ("d"):
                case ("a"):
                    {
                        преподавателиToolStripMenuItem.Visible =
                        ученикиToolStripMenuItem.Visible = 
                        автоToolStripMenuItem.Visible = 
                        платежиStripMenuItem.Visible = 
                        расписаниеStripMenuItem.Visible = 
                        отчетToolStripMenuItem.Visible = 
                        check = true;
                        break;
                    }
                case ("t"):
                    {
                        преподавателиToolStripMenuItem.Visible = 
                        ученикиToolStripMenuItem.Visible =                        
                        автоToolStripMenuItem.Visible = 
                        расписаниеStripMenuItem.Visible = 
                        check = true;
                        break;
                    }
                case ("s"):
                    {
                        расписаниеStripMenuItem.Visible = 
                        check = true;
                        break;
                    }
            }
            return check;
        }
        int activeTable;
        string activeRole;
        public Main()
        {
            InitializeComponent();
        }

        //ссылка Войти/Выйти
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Войти")
            {
                EnterForm enter = new EnterForm();
                enter.Owner = this;
                enter.Show();
                enter.button1.Click += (senderEnt, eEnt) =>
                {
                    try
                    {
                        Open user = new Open(enter.login, enter.password);
                        string[] check = user.Check();
                        activeRole = check[0];
                        if (labelsOn(check))
                            enter.Close();
                        else
                            throw new Exception("Неверный логин и/или пароль");
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };

            }
            else
            {
                Application.Restart();
            }
        }
        //кнопка Назад для браузера
        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        //отображение рельной даты-времени
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        #region StripMenu items
        public void преподавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeTable = 1;
            label4.Text = "Преподаватели";
            if (activeRole == "d" || activeRole == "a")
                button1.Visible = button2.Visible = button3.Visible = true;
            else
                button1.Visible = button2.Visible = button3.Visible = false;
            Show show = new Show();
            show.ShowUser(dataGridView1, "t", "a");
        }
        public void ученикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeTable = 2;
            label4.Text = "Ученики";
            if (activeRole == "d" || activeRole == "a")
                button1.Visible = button2.Visible = button3.Visible = true;
            else
                button1.Visible = button2.Visible = button3.Visible = false;
            Show show = new Show();
            show.ShowUser(dataGridView1, "s", "s");
        }
        public void автоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeTable = 3;
            label4.Text = "Авто";
            if (activeRole == "d" || activeRole == "a")
                button1.Visible = button2.Visible = button3.Visible = true;
            else
                button1.Visible = button2.Visible = button3.Visible = false;
            Show show = new Show();
            show.ShowAuto(dataGridView1);
        }
        public void платежиStripMenuItem_Click(object sender, EventArgs e)
        {
            activeTable = 4;
            dataGridView1.Visible = true;
            label4.Text = "Платежи";
            if (activeRole == "d")
                button1.Visible = button2.Visible = button3.Visible = true;
            else
                button1.Visible = button2.Visible = button3.Visible = false;
            Show show = new Show();
            show.ShowPay(dataGridView1);
        }
        public void расписаниеStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Schedule form = new Schedule(activeRole);
                form.Owner = this;
                form.Show();
            }
            else Application.OpenForms[0].Focus();
        }
        #endregion
        //кнопка Добавить
        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Show show = new Show();
                Form form = show.ShowAddForm(activeTable);
                form.Owner = this;
                form.Show();
            }
            else Application.OpenForms[0].Focus();
        }

        //кнопка Изменить
        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Show show = new Show();
                Form form = show.ShowEditForm(dataGridView1, activeTable);
                form.Owner = this;
                form.Show();
            }
            else Application.OpenForms[0].Focus();
        }

        //кнопка Удалить
        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                DeleteData del = new DeleteData();
                del.Delete(dataGridView1, activeTable);
                switch (activeTable)
                {
                    case (1): преподавателиToolStripMenuItem_Click(sender, e); break;
                    case (2): ученикиToolStripMenuItem_Click(sender, e); break;
                    case (3): автоToolStripMenuItem_Click(sender, e); break;
                    case (4): платежиStripMenuItem_Click(sender, e); break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.pdd24.com/");
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void инфоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"readme.txt");
        }        
        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeTable == 4)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dialog.DefaultExt = ".xls";
                dialog.Filter = "Таблицы Excel (*.xls)|*.xls|Все файлы (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.FileName = "Отчет";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                    var query = (from p in db.payment
                                join u in db.users on p.id_s equals u.id_u
                                orderby p.id_p
                                select new { p.id_p, u.surname, u.name, u.patron, p.summa, p.pay_day }).ToList();
                    var template = new MemoryStream(Properties.Resources.template, true);
                    HSSFWorkbook workbook = new HSSFWorkbook(template);
                    var sheet1 = workbook.GetSheet("Лист1");
                    sheet1.CreateRow(3).CreateCell(0).SetCellValue(DateTime.Now.ToShortDateString());
                    int row = 6;
                    foreach (var item in query.OrderBy(o => o.id_p))
                    {
                        var rowInsert = sheet1.CreateRow(row);
                        rowInsert.CreateCell(0).SetCellValue(item.id_p);
                        rowInsert.CreateCell(1).SetCellValue(item.surname);
                        rowInsert.CreateCell(2).SetCellValue(item.name);
                        rowInsert.CreateCell(3).SetCellValue(item.patron);
                        rowInsert.CreateCell(4).SetCellValue(item.summa);
                        rowInsert.CreateCell(5).SetCellValue(item.pay_day);
                        row++;

                    }
                    workbook.Write(file);
                }
            }
        }


    }
}
