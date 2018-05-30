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
using System.IO;


namespace QuanLySach
{
    public partial class fAdmin : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
        public fAdmin()
        {
            InitializeComponent();
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            LayLoaiSach();
        }
        private void LayLoaiSach()
        {
            string query = "select LoaiSach from ThongTinSach";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LoaiSach");
                    cmbLoaiSach.DisplayMember = "LoaiSach";
                    cmbLoaiSach.DataSource = ds.Tables["LoaiSach"];

                }
            }
        }
        private bool CheckHopLe()
        {
            if (!string.IsNullOrEmpty(cmbLoaiSach.Text) && !string.IsNullOrEmpty(txtTenSach.Text) && !string.IsNullOrEmpty(txtTacGia.Text) && !string.IsNullOrEmpty(txtPathImage.Text))
            {
                return true;
            }
            return false;
        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "" || txtHo.Text == "")
            {
                MessageBox.Show("Không được để trống trường Họ và Tên", "Thông báo");
                return;
            }
            
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
                string query = string.Format("insert into ThongTinKhachHang (Ho, Ten, NgaySinh, NamSinh, GioiTinh, DienThoai, Email, DiaChi, QuocGia, TinhThanh, QuanHuyen, PhuongXa, GhiChu) values (N'{0}', N'{1}', '{2}', '{3}', N'{4}', '{5}', '{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}')", "abc", "bcb", txtNgaySinh.Text, txtNamSinh.Text, txtGioiTinh.Text, txtDienThoai.Text, txtEmail.Text, txtDiaChi.Text, txtQuocGia.Text, txtTinhThanh.Text, txtQuanHuyen.Text, txtPhuongXa.Text, txtGhiChu.Text);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tạo mới khách hàng thành công", "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi đã xảy ra:\n" + ex.ToString(), "Lỗi");
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
        
        // Hàm chuyển hình ảnh sang binary
        private byte[] convertImagetoBinary()
        {
            FileStream fs;
            fs = new FileStream(txtPathImage.Text, FileMode.Open, FileAccess.Read);

            //Tạo 1 biến có kiểu byte để đọc image
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckHopLe())
            {
                string query = "insert into ThongTinSach (TenSach, GiaSach, TacGia, LoaiSach, Image) values (@TS, @GS, @TG, @LS, @HA)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TS", txtTenSach.Text);
                        cmd.Parameters.AddWithValue("@GS", nudGiaSach.Value);
                        cmd.Parameters.AddWithValue("@TG", txtTacGia.Text);
                        cmd.Parameters.AddWithValue("@LS", cmbLoaiSach.Text);
                        cmd.Parameters.AddWithValue("@HA", convertImagetoBinary());

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            
                            LayLoaiSach();
                            MessageBox.Show("Thêm sách thành công !", "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi đã xảy ra\n" + ex.ToString());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thêm được thêm. Vui lòng kiểm tra lại", "Thông báo");
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdl = new OpenFileDialog();
            ofdl.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            ofdl.FilterIndex = 1;
            ofdl.RestoreDirectory = true;
            if (ofdl.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox2.ImageLocation = ofdl.FileName;
                    txtPathImage.Text = ofdl.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi đã xảy ra:\n" + ex.ToString());
                }
            }
        }
    }
}
