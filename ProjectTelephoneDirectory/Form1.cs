using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectTelephoneDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-BOV3B8B\SQLEXPRESS;Initial Catalog=DbRehber;Integrated Security=True");

        private void listDirectory()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from TblRehber", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listDirectory();
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                txtPhoto.Text = openFileDialog1.FileName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into TblRehber (ad,soyad,telefon,mail,resim) values(@p1,@p2,@p3,@p4,@p5)", conn);
                cmd.Parameters.AddWithValue("@p1", txtName.Text);
                cmd.Parameters.AddWithValue("@p2", txtSurname.Text);
                cmd.Parameters.AddWithValue("@p3", mskPhone.Text);
                cmd.Parameters.AddWithValue("@p4", txtMail.Text);
                cmd.Parameters.AddWithValue("@p5", txtPhoto.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The person saved to the directory book.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listDirectory();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please type valid values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult warning;
                warning = MessageBox.Show("You are about to delete this person from directory book!! Are you sure to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (warning == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from TblRehber where id=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", txtId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The person deleted from the directory book.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listDirectory();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("The deletion process has been canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selected = dataGridView1.SelectedCells[0].RowIndex;
                txtId.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
                txtSurname.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
                mskPhone.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
                txtMail.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
                txtPhoto.Text = dataGridView1.Rows[selected].Cells[5].Value.ToString();
                pictureBox1.ImageLocation = dataGridView1.Rows[selected].Cells[5].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update TblRehber set ad=@p1,soyad=@p2,telefon=@p3,mail=@p4,resim=@p5 where id=@p6", conn);
                cmd.Parameters.AddWithValue("@p1", txtName.Text);
                cmd.Parameters.AddWithValue("@p2", txtSurname.Text);
                cmd.Parameters.AddWithValue("@p3", mskPhone.Text);
                cmd.Parameters.AddWithValue("@p4", txtMail.Text);
                cmd.Parameters.AddWithValue("@p5", txtPhoto.Text);
                cmd.Parameters.AddWithValue("@p6", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("The person updated to the directory book.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listDirectory();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please type valid values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtMail.Text = "";
            txtName.Text = "";
            txtPhoto.Text = "";
            txtSurname.Text = "";
            mskPhone.Text = "";
            pictureBox1.ImageLocation = null;
            txtName.Focus();
        }
    }
}
