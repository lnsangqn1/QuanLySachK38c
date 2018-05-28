using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void CheckLogin()
        {
            string connectionString = connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
            string query = "select * from Login";
            SqlConnection con = new SqlConnection(connectionString);
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    int dem = 0;
                    while(dr.Read())
                    {
                        
                        if (txtbUserName.Text == (string)dr["TaiKhoan"] && txtbPassWord.Text == (string)dr["MatKhau"])
                        {
                            if ((int)dr["NhanVien"] == 0)
                            {
                                fBookStore m = new fBookStore();
                                m.TaiKhoan = (string)dr["TaiKhoan"];
                                m.ShowDialog();
                            }
                            else
                            {
                                fAdmin adm = new fAdmin();
                                adm.TaiKhoan = (string)dr["TaiKhoan"];
                                this.Hide();
                                adm.ShowDialog();
                            }
                            dem = 1;
                        }
                    }
                    if (dem == 0)
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.\n Vui lòng nhập lại", "Thông báo");
                        return;
                    }
                }
                con.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckLogin();
            this.Show();
        }



    }
}
