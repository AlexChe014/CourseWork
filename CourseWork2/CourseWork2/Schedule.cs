using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork2
{
    public partial class Schedule : Form
    {
        public Schedule(string role)
        {
            InitializeComponent();
            if (role == "d" || role == "a")
                button2.Visible = button3.Visible = true;
        }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public Excel.Worksheet ObjWorkSheet;
        string GetConnectionString(FileInfo _file)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();


            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = _file.FullName;


            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            while (dataGridView1.Rows.Count > 1)
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();
            FileInfo _file = new FileInfo(@"schedule/schedule2.xlsx");
            Excel.Application ObjExcel = new Excel.Application();
            //Открываем книгу.                                                                                                                                                        
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_file.FullName);
            //Выбираем таблицу(лист).
            Excel.Worksheet ObjWorkSheet;
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
                DateTime date, leftdate, rightdate;
                date = DateTime.ParseExact(min_arr[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                leftdate = DateTime.ParseExact(maskedTextBox1.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                rightdate = DateTime.ParseExact(maskedTextBox2.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                if (date >= leftdate && date <= rightdate)
                    dataGridView1.Rows.Add(min_arr);
                Application.DoEvents();
            }
            dataGridView1.Rows.Add();
            //Удаляем приложение (выходим из экселя)
            ObjExcel.Quit();
        }
        public string[] arr;
        private void button2_Click(object sender, EventArgs e)
        {
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();
            SheduleDate form = new SheduleDate();
            form.Owner = this;
            form.Show();
            arr = new string[7];
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
                            else if (form.radioButton3.Checked) { arr[3] = form.comboBox2.SelectedItem.ToString(); it = arr[1]; }
                            else if (form.radioButton4.Checked) { arr[4] = form.comboBox2.SelectedItem.ToString(); it = arr[1]; }
                            else if (form.radioButton5.Checked) { arr[5] = form.comboBox2.SelectedItem.ToString(); it = arr[1]; }
                            else { arr[6] = form.comboBox2.SelectedItem.ToString(); it = arr[1]; }
                            
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
                    DateTime.ParseExact(form.maskedTextBox1.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    arr[0] = form.maskedTextBox1.Text;
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
                }
                catch
                {
                    throw new Exception("Неправильная дата");
                }
            };

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 2)
            {
                if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileInfo _file = new FileInfo(@"schedule/schedule2.xlsx");
                    Excel.Application ObjExcel = new Excel.Application();
                    //Открываем книгу                                                                                                                                                        
                    Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_file.FullName);
                    //Выбираем таблицу(лист)
                    Excel.Worksheet ObjWorkSheet;
                    ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
                    int ind = dataGridView1.SelectedCells[0].OwningRow.Index;
                    Excel.Range rg = (Excel.Range)ObjWorkSheet.Rows[ind + 1];
                    rg.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    button4_Click(sender, e);
                    ObjWorkBook.Save();
                    ObjExcel.Quit();
                }
            }

        }
    }
}
