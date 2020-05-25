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
        ScheduleTable schedule;
        public Schedule(string role)
        {
            InitializeComponent();
            if (role == "d" || role == "a")
                button2.Visible = button3.Visible = true;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            schedule  = new ScheduleTable();
            schedule.ShowSchedule(dataGridView1, dateTimePicker1.Value, dateTimePicker2.Value);
        }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public Excel.Worksheet ObjWorkSheet;
        /*string GetConnectionString(FileInfo _file)
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
        */
        private void button4_Click(object sender, EventArgs e)
        {
            schedule.ShowSchedule(dataGridView1, dateTimePicker1.Value, dateTimePicker2.Value);           
        }
        public string[] arr;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                schedule.AddScheduleString(dataGridView1, dateTimePicker1.Value, dateTimePicker2.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 2)
            {
                if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        schedule.DeleteScheduleRow(dataGridView1);
                        button4_Click(sender, e);
                    }
                    catch { MessageBox.Show("Непредвиденная ошибка", "Ошибка"); }
                }
            }

        }
    }
}
