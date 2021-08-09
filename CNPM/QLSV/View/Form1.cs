using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV.Cotroller;

namespace QLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            QLSVCotronller.Ins.ShowData(dgSinhvien);
            
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            QLSVCotronller.Ins.CellClick(tbHoten, tbKhoa, tbLop, tbGioitinh, dtNgaysinh, tbSodienthoai, tbThongtinkhac, (int)dgSinhvien.CurrentRow.Cells[0].Value, btDelete, btEdit);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            QLSVCotronller.Ins.Add(tbHoten, tbKhoa, tbLop, tbGioitinh, dtNgaysinh, tbSodienthoai, tbThongtinkhac, dgSinhvien);

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            QLSVCotronller.Ins.Delete(tbHoten, tbKhoa, tbLop, tbGioitinh, dtNgaysinh, tbSodienthoai, tbThongtinkhac, (int)dgSinhvien.CurrentRow.Cells[0].Value, dgSinhvien);
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            QLSVCotronller.Ins.Edit(tbHoten, tbKhoa, tbLop, tbGioitinh, dtNgaysinh, tbSodienthoai, tbThongtinkhac, (int)dgSinhvien.CurrentRow.Cells[0].Value, dgSinhvien);
        }
    }
}
