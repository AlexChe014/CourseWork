﻿using System;
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
    public partial class EditAuto : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        auto item2;
        public EditAuto(auto auto)
        {
            item2 = auto;
            InitializeComponent();
            textBox1.Text = item2.model;
            maskedTextBox1.Text = item2.num;
            numericUpDown1.Value = (int)item2.year_a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.CheckString(new string[] { textBox1.Text, maskedTextBox1.Text }))
            {
                try
                {
                    var result = ((Main)Owner).db.auto.SingleOrDefault(n => n.id_a == item2.id_a);
                    AutoForm auto = new AutoForm();
                    auto.EditAuto(item2, textBox1.Text, maskedTextBox1.Text, (int)numericUpDown1.Value, result);
                    ((Main)Owner).autoSheet = ((Main)Owner).db.auto.OrderBy(n => n.id_a).ToList();
                    ((Main)Owner).db.SaveChanges();
                    ((Main)Owner).автоToolStripMenuItem_Click(sender, e);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
