using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    class DeleteData
    {
        public DeleteData()
        { }
        public driveCourseEntities1 db = new driveCourseEntities1();
        public void Delete(DataGridView dataGridView, int activeTable)
        {
            try
            {
                if (dataGridView.SelectedCells.Count == 1)
                {
                    switch (activeTable)
                    {
                        case (1):
                        case (2):
                            {
                                List<users> query = (from us in db.users
                                                     select us).ToList();
                                users item = query.First(n => n.id_u.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                                objects obj = (from o in db.objects
                                               where o.id_o == item.id_u
                                               select o).First();
                                if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    db.users.Remove(item);
                                    db.objects.Remove(obj);
                                    db.SaveChanges();
                                }
                                break;
                            }
                        case (3):
                            {
                                List<auto> query = (from au in db.auto
                                                    select au).ToList();
                                auto item = query.First(n => n.id_a.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                                objects obj = (from o in db.objects
                                               where o.id_o == item.id_a
                                               select o).First();
                                if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    db.auto.Remove(item);
                                    db.objects.Remove(obj);
                                    db.SaveChanges();
                                }
                                break;
                            }
                        case (4):
                            {
                                List<payment> query = (from p in db.payment
                                                       select p).ToList();
                                payment item = query.First(n => n.id_p.ToString() == dataGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                                if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    db.payment.Remove(item);
                                    db.SaveChanges();
                                }
                                break;
                            }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления данных");
            }
        }
    }
}
