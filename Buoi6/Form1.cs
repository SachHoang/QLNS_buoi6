using Buoi6.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Buoi6.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Entity;

namespace Buoi6
{
    public partial class Form1 : Form
    {
        /*public static void SaveToDatabase(string maNV, string hoTen, DateTime ngaySinh, string maPhong)
        {
            // Code INSERT dữ liệu vào bảng NhanVien
            string sql = "INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, MaPB) VALUES (@MaNV, @TenNV, @NgaySinh, @MaPB)";
        }*/
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNV.SelectedItems.Count > 0)
            {
                ListViewItem lv = lvNV.SelectedItems[0];
                string maNV = lv.SubItems[0].Text;
                txtId.Text = maNV;
                string tenNV = lv.SubItems[1].Text;
                txtName.Text = tenNV;
                DateTime ngaySinh = DateTime.Parse( lv.SubItems[2].Text);
                dtNgaySinh.Text = ngaySinh.ToString();
                string tenPB = lv.SubItems[3].Text;
                cboPhongBan.Text = tenPB;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            QLNS qLNS = new QLNS();
            List<NhanVien> dsnv = qLNS.NhanViens.ToList();
            List<PhongBan> pb = qLNS.PhongBans.ToList();
            foreach (NhanVien nv in dsnv )
            {
                // code thêm mỗi nhân viên vào ListView
                ListViewItem item = new ListViewItem(nv.MaNV);
                item.SubItems.Add(nv.TenNV);
                item.SubItems.Add(nv.NgaySinh.ToString());
                item.SubItems.Add(nv.PhongBan.TenPB);
                // ... thêm các thông tin khác
                lvNV.Items.Add(item);
                

                //...
            }

            //...Thêm Header Collums
            lvNV.Columns.Add("Mã NV", 100);
            lvNV.Columns.Add("Họ Tên", 200);
            lvNV.Columns.Add("Ngày Sinh", 100);
            lvNV.Columns.Add("Phòng Ban", 100);

            //Load data vào combobox
            using (var db = new QLNS())
            {
                List<PhongBan> list = db.PhongBans.ToList();
                cboPhongBan.DataSource = list;
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";
            }               
                      

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maNV = txtId.Text;
            string hoTen = txtName.Text;
            DateTime ngaySinh = DateTime.Parse(dtNgaySinh.Text);
            string tenPB = cboPhongBan.SelectedValue.ToString();
                // code thêm mỗi nhân viên vào ListView
                ListViewItem item = new ListViewItem(maNV);
                item.SubItems.Add(hoTen);
                item.SubItems.Add(ngaySinh.ToString());
                item.SubItems.Add(tenPB);
                // ... thêm các thông tin khác
                lvNV.Items.Add(item);


                //...thêm vào database
                using(var db = new QLNS())
                {
                    NhanVien nv = new NhanVien();
                    nv.MaNV = maNV;
                    nv.TenNV = hoTen;
                    nv.NgaySinh = ngaySinh;
                    nv.MaPB = tenPB;
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                }
           

        }

        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (lvNV.SelectedItems.Count > 0)
            {
                // Code cập nhật nhân viên
                string maNV = txtId.Text;
                string hoTen = txtName.Text;
                string tenPB = cboPhongBan.SelectedValue.ToString();
                DateTime ngaySinh = DateTime.Parse(dtNgaySinh.Text);

                using (var db = new QLNS())
                {
                    PhongBan pb = db.PhongBans.FirstOrDefault();
                    NhanVien nv = db.NhanViens.Find(maNV);
                    if (nv != null)
                    {
                        // Có tìm thấy nhân viên, tiến hành cập nhật
                        nv.TenNV = hoTen;
                        nv.NgaySinh = ngaySinh;
                        nv.MaPB = tenPB;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Không tìm thấy nhân viên, có thể thông báo cho người dùng
                        
                    }
                    
                }

                // Cap nhat ListView
                lvNV.SelectedItems[0].SubItems[1].Text = hoTen;
                lvNV.SelectedItems[0].SubItems[2].Text = ngaySinh.ToString();
                lvNV.SelectedItems[0].SubItems[3].Text = tenPB.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn chắc chắn muốn xóa thông tin sinh viên này!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                string maNV = lvNV.SelectedItems[0].Text;

                using (var db = new QLNS())
                {
                    NhanVien nv = db.NhanViens.Find(maNV);
                    db.NhanViens.Remove(nv);
                    db.SaveChanges();

                }

                //...Xoa tren database
                lvNV.Items.Remove(lvNV.SelectedItems[0]);
            }
            
        }
    }
}
