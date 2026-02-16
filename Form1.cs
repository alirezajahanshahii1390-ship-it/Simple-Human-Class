using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace main_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int id;
        bool flag = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (flag) 
            {
                human human = new human();
                if (human.creat(textBox1.Text, textBox2.Text, Convert.ToByte(textBox3.Text)))
                {
                    MessageBox.Show("ثبت نام با موفقیت انجام شد");
                }
                else
                {
                    MessageBox.Show("مشکلی پیش اومده");
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = new db().humans.ToList();
            }
            else
            {
                human human = new human();
                human.update(textBox1.Text, textBox2.Text, Convert.ToByte(textBox3.Text), id);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = new db().humans.ToList();
                button1.Text = "sign in";
                flag = true;
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            id = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new db().humans.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new human().search(textBox4.Text).ToList();
            textBox4.Clear();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db db1 = new db();
            var q = from i in db1.humans.ToList() where i.id == id select i;
            db1.humans.Remove(q.Single());
            db1.SaveChanges();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new db().humans.ToList();
            id = 0;
        }
        private void updateToolStripMenuItem_Click_2(object sender, EventArgs e )
        {
            flag = false;
            button1.Text = "update";
            human human = new human();
            human.name = human.search(id).name.ToString();
            human.family = human.search(id).family.ToString();
            human.age = human.search(id).age;
            textBox1 .Text = human.name.ToString();
            textBox2.Text = human.family.ToString();
            textBox3.Text = human.age.ToString();
            id = 0;
        }
        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = (int)(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            if (e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }
    }

}
