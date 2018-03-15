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
    public partial class Frm_Produto : Form
    {
        public Frm_Produto()
        {
            InitializeComponent();
        }

        SqlConnection cnx = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Vendas;Data Source=DESKTOP-5QQDCV3\\MSSQLSERVEREXAME");

        private void limparcampos()
        {
            txtPrd.Clear();
            txtDescricao.Clear();
            txtmarca.Clear();
            txtvalor.Clear();           
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("insert into produto (idprd,descricao,marca,vlrunit) values( @idprd,@descricao,@marca,@vlrunit)", cnx);
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtPrd.Text;
            comand.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
            comand.Parameters.Add("@marca", SqlDbType.VarChar).Value = txtmarca.Text;
            comand.Parameters.Add("@vlrunit", SqlDbType.Float).Value = txtvalor.Text;

            if (txtPrd.Text != string.Empty) // && (txtCpf.Text != string.Empty)
            {
                try
                {
                    cnx.Open();
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparcampos();
                    txtPrd.Focus();
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

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("update produto set idprd=@idprd, descricao=@descricao, marca=@marca, vlrunit=@vlrunit where idcliente=@idcliente", cnx);
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtPrd.Text;
            comand.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
            comand.Parameters.Add("@marca", SqlDbType.VarChar).Value = txtmarca.Text;
            comand.Parameters.Add("@vlrunit", SqlDbType.Float).Value = txtvalor.Text;

            if (txtPrd.Text != string.Empty)
            {
                try
                {
                    cnx.Open();
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Cadastro alterado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparcampos();
                    txtPrd.Focus();
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
            else
            {
              MessageBox.Show("campos com preenchimento obrigatório", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Question);
            } 
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("delete from produto where idprd=@idprd", cnx);
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtPrd.Text;
            try
            {
                cnx.Open();
                comand.ExecuteNonQuery();
                MessageBox.Show("Cadastro deletado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limparcampos();
                txtPrd.Focus();
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

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparcampos();
            txtPrd.Focus();
        }

        private void btnpesq_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("select * from produto where idprd=@idprd", cnx);
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtPrd.Text;
           
            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                    limparcampos();
                    txtPrd.Focus();

                    throw new Exception("Dados não encontrado");

                }
                rdms.Read();
                txtPrd.Text = Convert.ToString(rdms["idprd"]);
                txtDescricao.Text = Convert.ToString(rdms["descricao"]);
                txtmarca.Text = Convert.ToString(rdms["marca"]);
                txtvalor.Text = Convert.ToString(rdms["valor"]);
                txtPrd.Focus();
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
    }
}
