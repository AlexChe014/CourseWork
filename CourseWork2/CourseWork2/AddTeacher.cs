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
    public partial class AddTeacher : Form
    {
        public driveCourseEntities1 db = new driveCourseEntities1();
        public AddTeacher()
        {
            InitializeComponent();
        }

        private void button2_Click(object senderAT, EventArgs eAT)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.CheckString(new string[] { textBox1.Text, textBox2.Text, textBox5.Text, textBox6.Text }))
            {
                try
                {
                    string patron = (!String.IsNullOrEmpty(textBox3.Text) ? Program.NameString(textBox3.Text) : "");
                    TeacherForm teach = new TeacherForm(Program.NameString(textBox1.Text), Program.NameString(textBox2.Text),
                    patron, Program.NameString(textBox5.Text), Program.NameString(textBox6.Text));
                    teach.AddTeacher((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value, checkBox1);
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else MessageBox.Show("Не все поля заполнены!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 8)
                e.Handled = true;
        }
    }
}