using Parcial2_AP1.BLL;
using Parcial2_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2_AP1.UI.Consultas
{
    public partial class cFactura : Form
    {
        public cFactura()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Facturas> repositorio = new RepositorioBase<Facturas>();

            var listado = new List<Facturas>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarComboBox.SelectedIndex)
                {
                    case 0: //Todo
                        {
                            listado = repositorio.GetList(p => true);
                            break;
                        }

                    case 1: //Id
                        {
                            int id = Convert.ToInt32(CriterioTextBox.Text);
                            listado = repositorio.GetList(p => p.FacturaId == id);
                            break;
                        }

                    case 3: //Nombre
                        {
                            listado = repositorio.GetList(p => p.Estudiante == CriterioTextBox.Text);
                            break;
                        }

                    case 4: //Monto
                        {
                            float monto = Convert.ToSingle(CriterioTextBox.Text);
                            listado = repositorio.GetList(p => p.Total == monto);
                            break;
                        }


                }
                listado = listado.Where(c => c.Fecha.Date >= DesdeDateTimePicker.Value.Date && c.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = repositorio.GetList(p => true);
            }
            ConsultaDataGridView.DataSource = null;
            ConsultaDataGridView.DataSource = listado;
        }
    }
    
}
