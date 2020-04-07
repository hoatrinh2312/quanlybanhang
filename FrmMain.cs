using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyKhachSan
{

    public partial class FrmMain : Form
    {
        
        SqlConnection con = new SqlConnection();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-N5IMV44\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = ConnectionString;
            con.Open();
            loadDatatoGridView();
            dataGridView_Phong.AllowUserToAddRows = false; //Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_Phong.EditMode = DataGridViewEditMode.EditProgrammatically;//Không cho phép sửa dữ liệu trực tiếp trên lưới
        }

        private void loadDatatoGridView()
        {
            string sql = "Select* From Phong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tablePhong = new DataTable();
            adp.Fill(tablePhong);
            dataGridView_Phong.DataSource = tablePhong;
        }

        private void dataGribView_Phong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonGia.Text = dataGridView_Phong.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Text = dataGridView_Phong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView_Phong.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtMaPhong.Enabled = false;
        }
        //private bool CheckKey(string sql)
        //{
        //    SqlDataAdapter adp = new SqlDataAdapter(sql,con);
        //    DataTable tablePhong = new DataTable();
        //    adp.Fill(tablePhong);
        //    if (tablePhong.Rows.Count > 0)
        //        return true;
        //    else
        //        return false;
        //}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete from Phong where MaPhong = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDatatoGridView();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaPhong.Enabled = true;
            txtMaPhong.Focus();
        }
        private bool CheckKey(string sql)
        {

            SqlDataAdapter dap = new SqlDataAdapter(sql, con);
            DataTable tablePhong = new DataTable();
            dap.Fill(tablePhong);
            if (tablePhong.Rows.Count > 0)
                return true;
            else return false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("ban chua nhap ma phong");
                txtMaPhong.Focus();
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("ban chua nhap ten phong");
                txtTenPhong.Focus();
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("bạn chưa nhập đơn giá.");
                txtDonGia.Focus();
            }

            string sql = "Select MaPhong from Phong where MaPhong='" + txtMaPhong.Text + "'";
            if (CheckKey(sql))
            {
                MessageBox.Show("Mã phòng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtMaPhong.Focus();
                txtMaPhong.Text = "";
                return;
            }
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDatatoGridView();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 13) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = true;
            txtMaPhong.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaPhong.ReadOnly = true;
            string sql = "Update Phong Set TenPhong= '" + txtTenPhong.Text.ToString() + "' , DonGia=' " + txtDonGia.Text.ToString() + "' where MaPhong='" + txtMaPhong.Text + "'";
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("ban chua chon ban ghi nao", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("ban phai nhap ten phong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("ban phai nhap don gia", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDatatoGridView();
            //ResetValues();
            //btnHuy.Enabled = false;
        }
        private void ResetValues()
        {
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Text = "";
        }

    }
}


