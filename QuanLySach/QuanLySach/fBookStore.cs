using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Data.SqlClient;
using System.IO;

namespace QuanLySach
{
    public partial class fBookStore : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
        int dem = 1;
        int[] idSach = new int[3];
        byte dem1 = 0;
        public fBookStore()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void fBookStore_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Tác giả";
            LoadHangMoi();
            
        }
        // Hàm chuyển đổi Byte sang Hình ảnh
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void LoadHangMoi()
        {
            string query = "select TOP 3 Id, TenSach, GiaSach, Image from ThongTinSach order by Id DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int imageIndex = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                            // Thêm ảnh vào ImageList
                            imageList1.Images.Add(ByteArrayToImage((byte[])dt.Rows[i][3]));

                        // Tạo biến viewItem để thêm dữ liệu vào
                        var viewItem = new ListViewItem();
                        { };
                        // Gán ảnh có chỉ số imageIndex vào viewItem
                        viewItem.ImageIndex = imageIndex;

                        // Thêm viewItem vào listView1
                        listView1.Items.Add(viewItem).Text = dt.Rows[i][1].ToString();
                        lbl1.ForeColor = System.Drawing.Color.Red;
                        lbl2.ForeColor = System.Drawing.Color.Red;
                        lbl3.ForeColor = System.Drawing.Color.Red;

                        if(i == 0)
                            lbl1.Text = dt.Rows[i][2].ToString() + " đ";
                        else if(i==1)
                            lbl2.Text = dt.Rows[i][2].ToString() + " đ";
                        else
                            lbl3.Text = dt.Rows[i][2].ToString() + " đ";

                        idSach[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
                        imageIndex++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void chiTiet(int idsach, int id)
        {
            //if (dem1 == 0)
            //{
            //    dem++;
            //    TabPage tabnew = new TabPage();
            //    tabnew.Text = "Chi tiết";

            //    tabControl1.TabPages.Add(tabnew);
            //    tabControl1.SelectedIndex = dem;
            //    dem1 = 1;
            //}
            //else
            //    tabControl1.SelectedIndex = dem;
            string query = "select TenSach, GiaSach, TacGia, LoaiSach from ThongTinSach where Id = @ids";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ids", SqlDbType.Int).Value = idsach;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Tạo pictureBox
                            PictureBox picb = new PictureBox();
                            picb.Size = new Size(166, 295);
                            picb.Location = new Point(20, 20);
                            picb.SizeMode = PictureBoxSizeMode.StretchImage;
                            picb.Image = imageList1.Images[id];
                            // Tạo label
                            Label lblTS = new Label();
                            lblTS.Text = "Tên sách :  " + (string)dr["TenSach"];
                            lblTS.AutoSize = true;
                            lblTS.Font = new Font(lblTS.Font, FontStyle.Bold);
                            lblTS.Font = new Font(lblTS.Font.FontFamily, 10);
                            lblTS.Location = new Point(215, 45);
                            Label lblGS = new Label();
                            lblGS.Text = "Giá sách :  " + Convert.ToString(dr["GiaSach"]) + " đ";
                            lblGS.AutoSize = true;
                            lblGS.Font = new Font(lblTS.Font, FontStyle.Bold);
                            lblGS.Font = new Font(lblTS.Font.FontFamily, 10);
                            lblGS.Location = new Point(215, 75);
                            Label lblTG = new Label();
                            lblTG.Text = "Tác giả :  " + (string)dr["TacGia"];
                            lblTG.AutoSize = true;
                            lblTG.Font = new Font(lblTS.Font, FontStyle.Bold);
                            lblTG.Font = new Font(lblTS.Font.FontFamily, 10);
                            lblTG.Location = new Point(215, 105);
                            Label lblLS = new Label();
                            lblLS.Text = "Loại sách :  " + (string)dr["LoaiSach"];
                            lblLS.AutoSize = true;
                            lblLS.Font = new Font(lblTS.Font, FontStyle.Bold);
                            lblLS.Font = new Font(lblTS.Font.FontFamily, 10);
                            lblLS.Location = new Point(215, 135);
                            // Tạo button
                            Button btnDangKySach = new Button();
                            btnDangKySach.Text = "Đăng ký lấy sách";
                            btnDangKySach.Font = new Font(btnDangKySach.Font, FontStyle.Bold);
                            btnDangKySach.Font = new Font(btnDangKySach.Font.FontFamily, 13);
                            btnDangKySach.Size = new Size(180,60);
                            btnDangKySach.Location = new Point(360, 260);

                            TabPage tabnew = new TabPage();
                            tabnew.Text = "Chi tiết";
                            tabnew.Controls.Add(picb);
                            tabnew.Controls.Add(lblTS);
                            tabnew.Controls.Add(lblGS);
                            tabnew.Controls.Add(lblTG);
                            tabnew.Controls.Add(lblLS);
                            tabnew.Controls.Add(btnDangKySach);

                            tabControl1.TabPages.Add(tabnew);
                            //tabControl1.SelectedIndex = dem;
                        }
                    }
                }
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn đăng xuất khỏi tài khoản này?",
     "Thông báo", MessageBoxButtons.YesNo);
            if (dlr == DialogResult.Yes) this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public string TaiKhoan { set; get; }

        private void btn1_Click(object sender, EventArgs e)
        {
            chiTiet(idSach[0], 0);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            chiTiet(idSach[1], 1);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            chiTiet(idSach[2], 2);
        }
    }
}
