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

namespace QuanLySach
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "" || txtHo.Text == "")
            {
                MessageBox.Show("Không được để trống trường Họ và Tên", "Thông báo");
                return;
            }
            
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
                string query = string.Format("insert into ThongTinKhachHang (Ho, Ten, NgaySinh, NamSinh, DienThoai, Email, DiaChi, QuocGia, TinhThanh, QuanHuyen, PhuongXa, GhiChu) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}',N'{7}', '{8}', '{9}', '{10}', '{11}')", txtHo.Text, txtTen.Text, txtNgaySinh.Text, txtNamSinh.Text, txtDienThoai.Text, txtEmail.Text, txtDiaChi.Text, txtQuocGia.Text, txtTinhThanh.Text, txtQuanHuyen.Text, txtPhuongXa.Text, txtGhiChu.Text);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tạo mới khách hàng thành công", "Thông báo");
                    }
                    catch
                    {
                        MessageBox.Show("Thông tin nhập không hợp lệ. Vui lòng nhập lại", "Lỗi");
                        return;
                    }
                }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn đăng xuất khỏi tài khoản này?",
     "Thông báo", MessageBoxButtons.YesNo);
            if (dlr == DialogResult.Yes) this.Close();
        }
        public string TaiKhoan { set; get; }
    }
}
