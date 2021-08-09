using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV.Model;

namespace QLSV.Cotroller
{
    public class QLSVCotronller
    {
        private static QLSVCotronller _ins;

        public static QLSVCotronller Ins
        {
            get
            {
                if (_ins == null) _ins = new QLSVCotronller();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        private QLSVCotronller() { }

        public void ShowData(DataGridView data) {
            data.DataSource = DataProvider.Ins.DB.Sinhviens.ToList();
        }

        public void CellClick(TextBox Hoten, TextBox Khoa, TextBox Lop, TextBox Gioitinh, DateTimePicker Ngaysinh, TextBox Sodienthoai, RichTextBox Thongtinkhac, int id, Button btDelete, Button btEdit){
            var sinhvien = DataProvider.Ins.DB.Sinhviens.Where(p => p.Id == id).FirstOrDefault();
            Hoten.Text = sinhvien.Hoten;
            Khoa.Text = sinhvien.Khoa;
            Lop.Text = sinhvien.Lop;
            Gioitinh.Text = sinhvien.Giotinh;
            Ngaysinh.Value = sinhvien.Ngaysinh.Value;
            Sodienthoai.Text = sinhvien.Sodienthoai;
            Thongtinkhac.Text = sinhvien.Thongtinkhac;
            btDelete.Enabled = true;
            btEdit.Enabled = true;
        }

        public void Add(TextBox Hoten, TextBox Khoa, TextBox Lop, TextBox Gioitinh, DateTimePicker Ngaysinh, TextBox Sodienthoai, RichTextBox Thongtinkhac, DataGridView data) {
            DataProvider.Ins.DB.Sinhviens.Add(new Sinhvien()
            {
                Hoten = Hoten.Text,
                Khoa = Khoa.Text,
                Lop = Lop.Text,
                Giotinh = Gioitinh.Text,
                Ngaysinh = Ngaysinh.Value.Date,
                Sodienthoai = Sodienthoai.Text,
                Thongtinkhac = Thongtinkhac.Text
            }) ;
            DataProvider.Ins.DB.SaveChanges();
            data.DataSource = DataProvider.Ins.DB.Sinhviens.ToList();

        }

        public void Delete(TextBox Hoten, TextBox Khoa, TextBox Lop, TextBox Gioitinh, DateTimePicker Ngaysinh, TextBox Sodienthoai, RichTextBox Thongtinkhac, int id, DataGridView data) {
            var sinhvien = DataProvider.Ins.DB.Sinhviens.Where(p=>p.Id == id).FirstOrDefault();
            DataProvider.Ins.DB.Sinhviens.Remove(sinhvien);
            Hoten.Text = "";
            Khoa.Text = "";
            Lop.Text = "";
            Gioitinh.Text = "";
            Ngaysinh.Value = DateTime.Now.Date;
            Sodienthoai.Text = "";
            Thongtinkhac.Text = "";
            DataProvider.Ins.DB.SaveChanges();
            data.DataSource = DataProvider.Ins.DB.Sinhviens.ToList();
        }

        public void Edit(TextBox Hoten, TextBox Khoa, TextBox Lop, TextBox Gioitinh, DateTimePicker Ngaysinh, TextBox Sodienthoai, RichTextBox Thongtinkhac, int id, DataGridView data)
        {
            var sinhvien = DataProvider.Ins.DB.Sinhviens.Where(p => p.Id == id).FirstOrDefault();
            sinhvien.Hoten = Hoten.Text;
            sinhvien.Khoa = Khoa.Text;
            sinhvien.Lop = Lop.Text;
            sinhvien.Giotinh = Gioitinh.Text;
            sinhvien.Ngaysinh = Ngaysinh.Value.Date;
            sinhvien.Sodienthoai = Sodienthoai.Text;
            sinhvien.Thongtinkhac = Thongtinkhac.Text;
            DataProvider.Ins.DB.SaveChanges();
            data.DataSource = DataProvider.Ins.DB.Sinhviens.ToList();
        }
    }

}
