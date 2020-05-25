using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace CourseWork2
{
    class ScheduleTable
    {
        public ScheduleTable()
        { }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public Excel.Worksheet ObjWorkSheet;
        public void ShowSchedule(DataGridView dataGridView, DateTime left, DateTime right)
        {
            while (dataGridView.Rows.Count > 1)
                dataGridView.Rows.Remove(dataGridView.Rows[0]);
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();
            FileInfo _file = new FileInfo(@"schedule/schedule2.xlsx");
            Excel.Application ObjExcel = new Excel.Application();
            //Открываем книгу.                                                                                                                                                        
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_file.FullName);
            //Выбираем таблицу(лист).
            ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            int end = ObjWorkSheet.UsedRange.Rows.Count;
            for (int i = 1; i <= end; i++)
            {
                //Выбираем область таблицы
                string[] arr = new string[] { "A", "B", "C", "D", "E", "F", "G" };
                string[] min_arr = new string[7];
                for (int j = 0; j < min_arr.Length; j++)
                {
                    Excel.Range range = ObjWorkSheet.get_Range(arr[j] + i.ToString());
                    min_arr[j] = range.Text;
                    foreach (var it in group)
                    {
                        if (range.Text == it.id_g.ToString())
                            min_arr[j] = $"Преподаватель {it.surname}, {Environment.NewLine}№ группы: {it.id_g}";
                    }
                }
                DateTime date;
                date = DateTime.ParseExact(min_arr[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);


                if (date >= left && date <= right)
                    dataGridView.Rows.Add(min_arr);
                Application.DoEvents();
            }
            dataGridView.Rows.Add();
            //Удаляем приложение (выходим из экселя)
            ObjExcel.Quit();
        }
        public void AddScheduleString(DataGridView dataGridView, DateTime left, DateTime right)
        {
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();
            SheduleDate form = new SheduleDate();
            //form.Owner = this;
            form.Show();
            string[] arr = new string[7];
            RadioButton[] radio = new RadioButton[] { form.radioButton1, form.radioButton2, form.radioButton3, form.radioButton4, form.radioButton5, form.radioButton6 };
            foreach (RadioButton rb in radio)
            {
                rb.CheckedChanged += (sender1, e1) =>
                {
                    string it = arr[rb.TabIndex];
                    if (!String.IsNullOrEmpty(it))
                        form.label5.Text = $"Преподаватель: {group.First(n => n.id_g.ToString() == it).surname},\n№ гр. - {it}";
                    else form.label5.Text = "";
                };
            }

            form.button3.Click += (senderE, eE) =>
            {
                string it;
                try
                {
                    if (form.radioButton1.Checked) { arr[1] = form.comboBox2.SelectedItem.ToString(); it = arr[1]; }
                    else if (form.radioButton2.Checked) { arr[2] = form.comboBox2.SelectedItem.ToString(); it = arr[2]; }
                    else if (form.radioButton3.Checked) { arr[3] = form.comboBox2.SelectedItem.ToString(); it = arr[3]; }
                    else if (form.radioButton4.Checked) { arr[4] = form.comboBox2.SelectedItem.ToString(); it = arr[4]; }
                    else if (form.radioButton5.Checked) { arr[5] = form.comboBox2.SelectedItem.ToString(); it = arr[5]; }
                    else { arr[6] = form.comboBox2.SelectedItem.ToString(); it = arr[6]; }

                }
                catch
                {
                    MessageBox.Show("Некорректный выбор");
                }
            };
            form.button1.Click += (senderE, eE) =>
            {
                try
                {
                    DateTime date = form.dateTimePicker1.Value;
                    if (date <= DateTime.Now) throw new Exception("Нельзя ввести прошедшую дату!");
                    arr[0] = date.ToShortDateString();
                    FileInfo _file = new FileInfo(@"schedule/schedule2.xlsx");
                    Excel.Application ObjExcel = new Excel.Application();
                    //Открываем книгу.                                                                                                                                                        
                    Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_file.FullName);
                    //Выбираем таблицу(лист).
                    Excel.Worksheet ObjWorkSheet;
                    ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
                    int cl = ObjWorkSheet.UsedRange.Rows.Count + 1;
                    for (int i = 1; i <= arr.Length; i++)
                        ObjWorkSheet.Cells[cl, i].Value = arr[i - 1];
                    ObjWorkBook.Save();
                    ObjExcel.Quit();
                    this.ShowSchedule(dataGridView, left, right);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            };
        }
        public void DeleteScheduleRow(DataGridView dataGridView)
        {
            FileInfo _file = new FileInfo(@"schedule/schedule2.xlsx");
            Excel.Application ObjExcel = new Excel.Application();
            //Открываем книгу                                                                                                                                                        
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_file.FullName);
            //Выбираем таблицу(лист)
            Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            int ind = dataGridView.SelectedCells[0].OwningRow.Index;
            Excel.Range rg = (Excel.Range)ObjWorkSheet.Rows[ind + 1];
            rg.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
            ObjWorkBook.Save();
            ObjExcel.Quit();
        }
    }
}
