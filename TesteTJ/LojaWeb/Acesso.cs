using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaWeb
{
    public partial class Frm_Acesso : Form
    {
        public Frm_Acesso()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Vendas;Data Source=DESKTOP-5QQDCV3\\MSSQLSERVEREXAME");
            SqlCommand comand = new SqlCommand("select * from usuario where usuario=@usuario and senha=@senha", cnx);
            comand.Parameters.Add("@usuario", SqlDbType.VarChar).Value = textBox1.Text;
            comand.Parameters.Add("@senha", SqlDbType.VarChar).Value = textBox2.Text;

            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                   throw new Exception("Usuario ou senha na cadastrado!");

                }
                rdms.Read();
                Frm_Menu frm = new Frm_Menu();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnx.Close();
            }
                    
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
