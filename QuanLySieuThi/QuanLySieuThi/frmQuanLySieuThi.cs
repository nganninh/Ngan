﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{

    public partial class frmQuanLySieuThi : Form

    {
        public frmQuanLySieuThi()
        {
            InitializeComponent();
        }
        
        List<string> ID_DELETE;
        DATA.CHITIETHOADONDATA CTHD;
        DATA.HANGHOADATA HH;
        DATA.HOADONDATA HD;
        DATA.KHACHHANGDATA KH;
        DATA.NHANVIENDATA NV;
        DataTable db_HH;
        DataTable db_HD;
        DataTable db_KH;
        DataTable db_NV;
        DataTable db_CTHD;
        DataTable db_NS;
        List<string> list_HH;
            List<string> list_KH;
            List<string> list_HD;
            List<string> list_NV;
            List<string> list_CTHD;
       

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void ADD_LIST_KH()
        {
            list_KH.Add(tbx_ID_KH.Text.ToString());
            list_KH.Add(tbx_NAME_KH.Text.ToString());
            list_KH.Add(tbx_CMT_KH.Text.ToString());
            list_KH.Add(tbx_PHONE_KH.Text.ToString());
        }
        private void ADD_LIST_NV()
        {
            list_NV.Add(tbx_ID_NV.Text.ToString());
            list_NV.Add(tbx_NAME_NV.Text.ToString());
            list_NV.Add(tbx_CMT_NV.Text.ToString());
            list_NV.Add(tbx_PHONE_NV.Text.ToString());
        }
        private void ADD_LIST_HD()
        {
            list_HD.Add(tbx_ID_HD.Text.ToString());
            list_HD.Add(tbx_ID_NVHD.Text.ToString());
            list_HD.Add(tbx_ID_KHHD.Text.ToString());
            list_HD.Add(tbx_DATE_HD.Text.ToString());
        }
        private void ADD_LIST_HH()
        {
            list_HH.Add(tbx_ID_HH.Text.ToString());
            list_HH.Add(tbx_NAME_HH.Text.ToString());
            list_HH.Add(tbx_COUT_HH.Text.ToString());
            list_HH.Add(tbx_COST_HH.Text.ToString());
        }
        private bool CHECK_NUMBER(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) == false)
                    return false;
            }
            return true;
        }
        private void ADD_LIST_CTHD()
        {
            list_CTHD.Add(tbx_ID_HD.Text.ToString());
            list_CTHD.Add(tbx_ID_HHHD.Text.ToString());
            list_CTHD.Add(tbx_COUTHD.Text.ToString());
        }
        #region THAOTAC
        private void QUAYLAI()
        {
            this.Hide();
            frm_Start frmmain = new frm_Start();
            frmmain.ShowDialog();
            this.Close();
        }
        private void THEM(DataTable db,List<string> list,int CHECK_ERROR)
        {
            DataRow row = db.NewRow();
            for (int i = 0; i < list.Count; i++)
                row[i] = list[i];
            if(CHECK_ERROR==0)
            db.Rows.Add(row);
            list.Clear();
           
        }
        private void SUA(DataGridView DGV,List<string> list,int CHECK_ERROR)
        {
            if (CHECK_ERROR == 0)
            {
                for (int i = 1; i < DGV.ColumnCount; i++)
                {
                    DGV.SelectedRows[0].Cells[i].Value = list[i - 1];
                }
            }
            list.Clear();
        }
        private void XOA(DataGridView DGV,List<string> ID)
        {
            int dem = 0;
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DGV.Rows[i].Cells[0].Value) == true)
                {
                    ID.Add(DGV.Rows[i].Cells[1].Value.ToString());
                    DGV.Rows.RemoveAt(i);



                    //HH.DeleteHH(ID);
                   // list_HH.Clear();
                    dem++;
                    i--;
                }
            }
            if (dem == 0)
            {
                MessageBox.Show("CHỌN DÒNG CẦN XÓA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void VALUE_CHANGE()
        {
            if (sb_HD.Value == true)
            {
                panel_CTHD.Enabled = true;
                panel_HD.Enabled = false;
                DGV_CTHD.Enabled = true;
            }
            if (sb_HD.Value == false)
            {
                panel_HD.Enabled = true;
                panel_CTHD.Enabled = false;
                DGV_HD.Enabled = true;
            }
        }
        #endregion
        private void QUANLYSIEUTHI_Load(object sender, EventArgs e)
        {
            VALUE_CHANGE();
            CTHD = new DATA.CHITIETHOADONDATA();
            KH = new DATA.KHACHHANGDATA();
            NV = new DATA.NHANVIENDATA();
            HD = new DATA.HOADONDATA();
            HH = new DATA.HANGHOADATA();
            db_NS = new DataTable();
            db_CTHD = new DataTable();
            db_HD = new DataTable();
            db_HH = new DataTable();
            db_KH = new DataTable();
            db_NV = new DataTable();
            list_CTHD = new List<string>();
            list_HD = new List<string>();
            list_HH = new List<string>();
            list_KH = new List<string>();
            list_NV = new List<string>();
            ID_DELETE = new List<string>();
            db_HD = HD.SHOWDATA();
            db_HH = HH.SHOWDATA();
            db_KH = KH.SHOWDATA();
            db_NV = NV.SHOWDATA();
            DGV_HD.DataSource = db_HD;
            DGV_HH.DataSource = db_HH;
            DGV_KH.DataSource = db_KH;
            DGV_NV.DataSource = db_NV;
        }

        private void btn_DELETE_KH_Click(object sender, EventArgs e)
        {
            XOA(DGV_KH, ID_DELETE);
            for (int i = 0; i < ID_DELETE.Count;i++)
                KH.DeleteKH(ID_DELETE[i].ToString());
            ID_DELETE.Clear();
        }

       
        private void btn_ADD_HH_Click(object sender, EventArgs e)
        {
            ADD_LIST_HH();
            HH.InsertHH(list_HH);
            THEM(db_HH,list_HH,HH.CHECK);
        }

        private void btn_ADD_NV_Click(object sender, EventArgs e)
        {
            ADD_LIST_NV();
            NV.InsertNV(list_NV);
            THEM(db_NV, list_NV, NV.CHECK);
        }

        private void btn_ADD_KH_Click(object sender, EventArgs e)
        {
            ADD_LIST_KH();
            KH.InsertKH(list_KH);
            THEM(db_KH, list_KH, KH.CHECK);
        }

        private void btn_DELETE_HH_Click(object sender, EventArgs e)
        {
            XOA(DGV_HH, ID_DELETE);
            for (int i = 0; i < ID_DELETE.Count; i++)
                HH.DeleteHH(ID_DELETE[i].ToString());
            ID_DELETE.Clear();
        }

        private void DELETE_NV_Click(object sender, EventArgs e)
        {
            XOA(DGV_NV, ID_DELETE);
            for (int i = 0; i < ID_DELETE.Count; i++)
                NV.DeleteNV(ID_DELETE[i].ToString());
            ID_DELETE.Clear();
        }

        private void btn_FIX_NV_Click(object sender, EventArgs e)
        {
            ADD_LIST_NV();
            NV.UpdateNV(list_NV);
            SUA(DGV_NV, list_NV, HH.CHECK);
        }

        private void btn_FIX_HH_Click(object sender, EventArgs e)
        {
            ADD_LIST_HH();
            HH.UpdateHH(list_HH);
            SUA(DGV_HH, list_HH, HH.CHECK);
        }

        private void btn_FIX_KH_Click(object sender, EventArgs e)
        {
            ADD_LIST_KH();
            KH.UpdateKH(list_KH);
            SUA(DGV_KH, list_KH, HH.CHECK);
        }

        private void btn_BACK_KH_Click(object sender, EventArgs e)
        {
            QUAYLAI();
        }

        private void btn_BACK_HH_Click(object sender, EventArgs e)
        {
            QUAYLAI();
        }

        private void btn_BACK_NV_Click(object sender, EventArgs e)
        {
            QUAYLAI();
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            QUAYLAI();
        }



        private void DGV_HD_DoubleClick(object sender, EventArgs e)
        {
            tbx_ID_HD.Text = DGV_HD.SelectedRows[0].Cells[1].Value.ToString();
            tbx_ID_NVHD.Text = DGV_HD.SelectedRows[0].Cells[2].Value.ToString();
            tbx_ID_KHHD.Text = DGV_HD.SelectedRows[0].Cells[3].Value.ToString();
            tbx_DATE_HD.Text = DGV_HD.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void sb_HD_ValueChanged(object sender, EventArgs e)
        {
            VALUE_CHANGE();
        }

        private void DGV_HH_DoubleClick(object sender, EventArgs e)
        {
            tbx_ID_HH.Text = DGV_HH.SelectedRows[0].Cells[1].Value.ToString();
            tbx_NAME_HH.Text = DGV_HH.SelectedRows[0].Cells[2].Value.ToString();
            tbx_COUT_HH.Text = DGV_HH.SelectedRows[0].Cells[3].Value.ToString();
            tbx_COST_HH.Text = DGV_HH.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void DGV_KH_DoubleClick(object sender, EventArgs e)
        {
            tbx_ID_KH.Text = DGV_KH.SelectedRows[0].Cells[1].Value.ToString();
            tbx_NAME_KH.Text = DGV_KH.SelectedRows[0].Cells[2].Value.ToString();
            tbx_CMT_KH.Text = DGV_KH.SelectedRows[0].Cells[3].Value.ToString();
            tbx_PHONE_KH.Text = DGV_KH.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void DGV_NV_DoubleClick(object sender, EventArgs e)
        {
            tbx_ID_NV.Text = DGV_NV.SelectedRows[0].Cells[1].Value.ToString();
            tbx_NAME_NV.Text = DGV_NV.SelectedRows[0].Cells[2].Value.ToString();
            tbx_CMT_NV.Text = DGV_NV.SelectedRows[0].Cells[3].Value.ToString();
            tbx_PHONE_NV.Text = DGV_NV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void DGV_HD_Click(object sender, EventArgs e)
        {
            CTHD.SHOW_CTHD(DGV_HD.SelectedRows[0].Cells[1].Value.ToString());
            db_CTHD = CTHD.SHOWDATA();
            DGV_CTHD.DataSource = db_CTHD;
        }

        private void DGV_CTHD_DoubleClick(object sender, EventArgs e)
        {
            tbx_ID_HD.Text = DGV_CTHD.SelectedRows[0].Cells[1].Value.ToString();
            tbx_ID_HHHD.Text = DGV_CTHD.SelectedRows[0].Cells[2].Value.ToString();
            tbx_COUTHD.Text = DGV_CTHD.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            if (sb_HD.Value == true)
            {
                ADD_LIST_CTHD();
                CTHD.InsertCTHD(list_CTHD);
                THEM(db_CTHD, list_CTHD, CTHD.CHECK);
            }
            else
            {
                ADD_LIST_HD();
                HD.InsertHD(list_HD);
                THEM(db_HD, list_HD, HD.CHECK);
            }
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            if (sb_HD.Value == false)
            {
                XOA(DGV_HD, ID_DELETE);
                for (int i = 0; i < ID_DELETE.Count; i++)
                    HD.DeleteHD(ID_DELETE[i].ToString());
                ID_DELETE.Clear();
            }
            else
            {
                XOA(DGV_CTHD, ID_DELETE);
                for (int i = 0; i < ID_DELETE.Count; i++)
                    CTHD.DeleteCTHD(ID_DELETE[i].ToString());
                ID_DELETE.Clear();
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            if (sb_HD.Value == false)
            {
                ADD_LIST_HD();
                HD.UpdateHD(list_HD);
                SUA(DGV_HD, list_HD, HH.CHECK);
            }
            else
            {
                ADD_LIST_CTHD();
                CTHD.UpdateCTHD(list_CTHD);
                SUA(DGV_CTHD, list_CTHD, HH.CHECK);
            }
        }

        

        private void btn_SEARCH_HH_Click(object sender, EventArgs e)
        {
            if (btn_SEARCH_HH.Text == "NANG SUAT")
            {
                db_NS = HH.NangSuatHH();
                DGV_HH.DataSource = db_NS;
                DGV_HH.Enabled = false;
                btn_FIX_HH.Enabled = false;
                btn_DELETE_HH.Enabled = false;
                btn_ADD_HH.Enabled = false;
                groupPanel3.Enabled = false;
                btn_SEARCH_HH.Text = "QUAY LAI";
            }
            else
            {
                DGV_HH.DataSource = db_HH;
                DGV_HH.Enabled = true;
                btn_FIX_HH.Enabled = true;
                btn_DELETE_HH.Enabled = true;
                btn_ADD_HH.Enabled = true;
                groupPanel3.Enabled = true;
                DGV_HH.Enabled = true;
                btn_SEARCH_HH.Text = "NANG SUAT";
            }
           
        }
       

       
       
    }
}
