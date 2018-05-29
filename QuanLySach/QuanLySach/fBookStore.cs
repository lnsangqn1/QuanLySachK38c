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
    public partial class fBookStore : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["quanlysach"].ConnectionString;
        int dem = 1;
        byte dem1 = 0;
        public fBookStore()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void btn_Click(object sender, EventArgs e)
        {
            
        }

        private void fBookStore_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Tác giả";
            LoadSach();
        }
        
        private void LoadSach()
        {
            string query = "select TenSach, GiaSach, TacGia from ThongTinSach order by Id DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int i = 0;
                        int temp = 55;
                        while(dr.Read())
                        {
                            Label lb = new Label();
                            lb.Text = Convert.ToString(dr["GiaSach"]) + " đ";
                            ListViewItem item = new ListViewItem();
                            listView1.Items.Add(item).Text = (string)dr["TenSach"] + "\n\n" + lb.Text;
                            label1.Text = TaiKhoan;

                            Button btn = new Button();
                            btn.Text = "Chi tiết";
                            btn.Click += new EventHandler(button1_Click);
                            btn.Location = new Point(temp,290);
                            listView1.Controls.Add(btn);
                            temp+=195;
                            i++;
                        }
                        con.Close();
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
            if (dem1 == 0)
            {
                dem++;
                TabPage tabnew = new TabPage();
                tabnew.Text = "Chi tiết";
                
                tabControl1.TabPages.Add(tabnew);
                tabControl1.SelectedIndex = dem;
                dem1 = 1;
            }
            else
                tabControl1.SelectedIndex = dem;
        }
        public string TaiKhoan { set; get; }
    }
}
