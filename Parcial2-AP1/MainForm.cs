using Parcial2_AP1.UI.Consultas;
using Parcial2_AP1.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2_AP1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rFactura rFactura = new rFactura();
            rFactura.MdiParent = this;
            rFactura.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCategorias rCategorias = new rCategorias();
            rCategorias.MdiParent = this;
            rCategorias.Show();
        }

        private void facturasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cFactura cFactura = new cFactura();
            cFactura.MdiParent = this;
            cFactura.Show();
        }
    }
}
