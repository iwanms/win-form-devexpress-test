using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace TestWinForm
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        Koneksi DB = new Koneksi();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private bool fnQRY_P_TEST_Q(string workType)
        {
            try
            {
                DB.con.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM TBL_TEST", DB.con);
                OracleDataAdapter dr = new OracleDataAdapter(cmd);

                DataTable dt = new DataTable();
                if (workType == "Q")
                {
                    dr.Fill(dt);
                    gridControl1.DataSource = dt;
                }


                DB.con.Close();
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Message", MessageBoxButtons.OK);
                DB.con.Close();
                return false;
            }
        }

        private void load_data()
        {
            fnQRY_P_TEST_Q("Q");
        }

        private void jumlah()
        {
            decimal nilai = Convert.ToDecimal(gridView1.GetRowCellValue(0, gridView1.Columns["NILAI"]));
            decimal nilai2 = 2;
            decimal total = nilai * nilai2;
            label1.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal nilai = Convert.ToDecimal(gridView1.GetRowCellValue(0, gridView1.Columns["NILAI"]));
            try
            {
                DB.con.Open();
                OracleCommand cmd = new OracleCommand("INSERT INTO TBL_TEST (NILAI) VALUES('"+ nilai.ToString() + "')", DB.con);
                cmd.ExecuteNonQuery();
                DB.con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Message", MessageBoxButtons.OK);
                DB.con.Close();
            }

            jumlah();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string val = textBox1.Text;
            decimal angka1;

            try
            {
                angka1 = Convert.ToDecimal(val);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                label2.Text = "Angka tidak valid";
                return;
            }

            decimal jumlah = angka1 * 2;
            label2.Text = jumlah.ToString();
        }
    }
}
