using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaWeb
{
    public partial class Frm_Menu : Form
    {
        public Frm_Menu()
        {
            InitializeComponent();
        }


        private void Frm_Menu_Load(object sender, EventArgs e)
        {

        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Cliente frm = new Frm_Cliente();
            frm.Show();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Produto frm = new Frm_Produto();
            frm.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void carrinhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Carrinho frm = new Frm_Carrinho();
            frm.Show();
        }

  

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void calculadoraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //  Process.Start("calc.exe");
        }
    }
}
