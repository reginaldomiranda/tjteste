using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaWeb
{
    public partial class Frm_Cliente : Form
    {
        public Frm_Cliente()
        {
            InitializeComponent();
        }


        SqlConnection cnx = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Vendas;Data Source=DESKTOP-5QQDCV3\\MSSQLSERVEREXAME");

        private void limparcampos()
        {
            txtIdcliente.Clear();
            txtCpf.Clear();
            txtnome.Clear();
            txtEndereco.Clear();
            txtbairro.Clear();        
        }


    private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("insert into cliente (idcliente,cpf,nome,endereco,bairro) values( @idcliente,@cpf,@nome,@endereco,@bairro)",cnx);
            comand.Parameters.Add("@idcliente", SqlDbType.Int).Value = txtIdcliente.Text;
            comand.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtCpf.Text;
            comand.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtnome.Text;
            comand.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comand.Parameters.Add("@bairro", SqlDbType.VarChar).Value = txtbairro.Text;

            if (txtIdcliente.Text != string.Empty) // && (txtCpf.Text != string.Empty)
            {
                try
                {
                    cnx.Open();
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Cadastro efetuado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparcampos();
                    txtIdcliente.Focus();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("update cliente set cpf=@cpf, nome=@nome, endereco=@endereco, bairro=@bairro where idcliente=@idcliente", cnx);
            comand.Parameters.Add("@idcliente", SqlDbType.Int).Value = txtIdcliente.Text;
            comand.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtCpf.Text;
            comand.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtnome.Text;
            comand.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comand.Parameters.Add("@bairro", SqlDbType.VarChar).Value = txtbairro.Text;

            if (txtIdcliente.Text != string.Empty) // && (txtCpf.Text != string.Empty)
            {
                try
                {
                    cnx.Open();
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Cadastro alterado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparcampos();
                    txtIdcliente.Focus();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("delete fromcliente where idcliente=@idcliente", cnx);
            comand.Parameters.Add("@idcliente", SqlDbType.Int).Value = txtIdcliente.Text;
            try
            {
                cnx.Open();
                comand.ExecuteNonQuery();
                MessageBox.Show("Cadastro delete com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limparcampos();
                txtIdcliente.Focus();
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

        private void button5_Click(object sender, EventArgs e)
        {

            var service = new SoapPessoa.SOAPServiceClient();
            var result = service.ConsultarCPFP3(txtCpf.Text.Trim(), "43280382149");
            var pessoa = result.FirstOrDefault();

            if (pessoa != null)
            {
                txtnome.Text = pessoa.Nome;
                txtEndereco.Text = pessoa.Logradouro;
                txtbairro.Text = pessoa.Bairro;
                txtCpf.Focus();
            }
            else
            {
                MessageBox.Show("Pessoa não encontrada");
            }


            return;

            SqlCommand comand = new SqlCommand("select * from cliente where cpf=@cpf", cnx);
            comand.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtCpf.Text;
            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                    limparcampos();
                    txtCpf.Focus();

                    throw new Exception("Dados não encontrado");                  

                }
                rdms.Read();
                txtIdcliente.Text = Convert.ToString(rdms["idcliente"]);
                txtnome.Text = Convert.ToString(rdms["nome"]);
                txtEndereco.Text = Convert.ToString(rdms["endereco"]);
                txtbairro.Text = Convert.ToString(rdms["bairro"]);
                txtCpf.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            limparcampos();
            txtIdcliente.Focus();
        }
    }
}
