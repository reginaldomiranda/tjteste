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
    public partial class Frm_Carrinho : Form
    {
        public Frm_Carrinho()
        {
            InitializeComponent();
        }

        SqlConnection cnx = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Vendas;Data Source=DESKTOP-5QQDCV3\\MSSQLSERVEREXAME");

        private void limparcampos()
        {
            txtprd.Clear();
            txtDesc.Clear();
            txtVlr.Clear();
            txtqtde.Clear();
            txtTotal.Clear();
            txtprd.Focus();
        }
     
        private void carregarDatagrid()
        {
           SqlCommand comand = new SqlCommand("select venda.*, cli.nome, prd.descricao from cliente where vendas.idcliente=cli.idcliente and prd.idprd=venda.idprd and idvenda=@idvenda", cnx);
           comand.Parameters.Add("@idvenda", SqlDbType.Int).Value = txtid.Text;

            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                    txtprd.Focus();
                    throw new Exception("Cliente não Cadastrado!");
                }

                dataGridView1.DataSource = rdms;
                rdms.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int multiplicar(int numero1, int numero2)
        {
            return numero1 * numero2;      
        }

        private void btnPrd_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("select descricao from produto where idprd=@idprd", cnx);
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtprd.Text;

            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                    txtprd.Focus();
                    throw new Exception("produto não encontrado");
                }
                rdms.Read();               
                txtDesc.Text = Convert.ToString(rdms["descricao"]);
              //  txtVlr.Text = Convert.ToString(rdms["valor"]);
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            txtTotal.Text = multiplicar(Convert.ToInt32(txtVlr.Text), Convert.ToInt32(txtqtde.Text)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("insert into vendas (idvenda,idprd,idcliente,vlrunit,qtde,vlrTotal,flag) values (@idvenda,@idprd,idcliente,@vlrunit,@qtde,@vlrTotal,@flag)", cnx);
            comand.Parameters.Add("@idvenda", SqlDbType.Int).Value = txtid.Text;
            comand.Parameters.Add("@idprd", SqlDbType.Int).Value = txtprd.Text;
            comand.Parameters.Add("@idcliente", SqlDbType.Int).Value =txtCliente.Text;
            comand.Parameters.Add("@vlrunit", SqlDbType.Int).Value = txtVlr.Text;
            comand.Parameters.Add("@qtde", SqlDbType.Int).Value = txtqtde.Text;
            comand.Parameters.Add("@vlrTotal", SqlDbType.Int).Value = txtTotal.Text;
            comand.Parameters.Add("@flag", SqlDbType.Char).Value = "N";

            if (txtprd.Text != string.Empty) // && (txtCpf.Text != string.Empty)
            {
                try
                {
                    cnx.Open();
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com Sucesso", "Vendas Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    //limpar dados
                    limparcampos();
                    txtprd.Focus();
                    
                    //Carregar dados lançado
                    carregarDatagrid();

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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand comand = new SqlCommand("select idcliente,nome from cliente where cpf=@cpf", cnx);
            comand.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtcpf.Text;

            try
            {
                cnx.Open();
                SqlDataReader rdms = comand.ExecuteReader();
                if (rdms.HasRows == false)
                {
                    txtprd.Focus();
                    throw new Exception("Cliente não Cadastrado!");
                }
                rdms.Read();
                txtCliente.Text = Convert.ToString(rdms["idcliente"]);
                txtNomeCli.Text = Convert.ToString(rdms["nome"]);              
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
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
