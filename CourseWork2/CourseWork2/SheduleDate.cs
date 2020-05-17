using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    public partial class SheduleDate : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        public SheduleDate()
        {
            InitializeComponent();
            
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();

            foreach (var sur in group)
                comboBox1.Items.Add(sur.surname);
            RadioButton[] radio = new RadioButton[] { radioButton1, radioButton2, radioButton3, radioButton4, radioButton5, radioButton6 };
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            var group = (from g in db.s_group
                         join u in db.users on g.teach_id equals u.id_u
                         select new { g.id_g, g.teach_id, u.surname }).Distinct();
            foreach (var gr in group.Where(n => n.surname.ToString() == comboBox1.SelectedItem.ToString()))
                comboBox2.Items.Add(gr.id_g);
            comboBox2.SelectedIndex = 0;
        }
    }
}
