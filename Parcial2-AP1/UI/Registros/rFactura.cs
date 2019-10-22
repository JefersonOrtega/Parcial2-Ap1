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

namespace Parcial2_AP1.UI.Registros
{
    public partial class rFactura : Form
    {
        public List<ServicioDetalle> Detalle { get; set; }
        public rFactura()
        {
            InitializeComponent();
            this.Detalle = new List<ServicioDetalle>();
        }

        private void CargarGrid()
        {
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = this.Detalle;
        }

        private void Limpiar()
        {
            MyErrorProvider.Clear();

            IdNumericUpDown.Value = 0;
            FechaDateTimePicker.Value = DateTime.Now;
            EstudianteTextBox.Text = string.Empty;
            CategoriaComboBox.Text = "";
            CantidadTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            ImporteTextBox.Text = string.Empty;
            TotalTextBox.Text = string.Empty;
            this.Detalle = new List<ServicioDetalle>();
            CargarGrid();
        }

        private Facturas LlenarClase()
        {
            Facturas factura = new Facturas();
            factura.FacturaId = Convert.ToInt32(IdNumericUpDown.Value);
            factura.Fecha = FechaDateTimePicker.Value;
            factura.Estudiante = EstudianteTextBox.Text;
            factura.Total = Convert.ToSingle(TotalTextBox.Text);

            factura.Servicios = this.Detalle;
            return factura;
        }

        private void LlenarCampos(Facturas factura)
        {
            IdNumericUpDown.Value = factura.FacturaId;
            FechaDateTimePicker.Value = factura.Fecha;
            EstudianteTextBox.Text = factura.Estudiante;
            TotalTextBox.Text = Convert.ToString(factura.Total);
            this.Detalle = factura.Servicios;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(EstudianteTextBox.Text))
            {
                MyErrorProvider.SetError(EstudianteTextBox, "No se puede dejar este campo vacío");
                EstudianteTextBox.Focus();
                paso = false;
            }
            if (this.Detalle.Count == 0)
            {
                MyErrorProvider.SetError(DetalleDataGridView, "Debe agregar al menos un servicio");
                CategoriaComboBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {
      
            if (CategoriaComboBox.Text.Trim().Length > 0) 
            {
                if (DetalleDataGridView.DataSource != null)
                    this.Detalle = (List<ServicioDetalle>)DetalleDataGridView.DataSource;

                this.Detalle.Add(
                    new ServicioDetalle(
                        ServicioId: 0,
                        FacturaId : (int)IdNumericUpDown.Value,
                        Categoria : CategoriaComboBox.Text,
                        Cantidad : Convert.ToInt32(CantidadTextBox.Text),
                        Precio :  Convert.ToSingle(PrecioTextBox.Text),
                        Importe : Convert.ToSingle(ImporteTextBox.Text)
                        )
                    );       
                CargarGrid();
                
                CategoriaComboBox.Text = "";
                float total = 0;
                foreach (var item in this.Detalle)
                {

                    total+= item.Importe;
                }
                TotalTextBox.Text = Convert.ToString(total);

            }
            else
            {
                MyErrorProvider.Clear();
                MyErrorProvider.SetError(CategoriaComboBox, "No hay ninguna Categoria seleccionada");
                CategoriaComboBox.Focus();
            }
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Facturas> repositorio = new RepositorioBase<Facturas>();
            Facturas facturas = repositorio.Buscar((int)IdNumericUpDown.Value);
            return (facturas != null);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Facturas factura;
            bool paso = false;

            RepositorioBase<Facturas> repositorio = new RepositorioBase<Facturas>();
            FacturasBLL facturasBLL = new FacturasBLL();

            if (!Validar())
                return;

            factura = LlenarClase();

            //Determinar si es guargar o modificar
            if (IdNumericUpDown.Value == 0)
                paso = repositorio.Guardar(factura);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un registro que no existe");
                    return;
                }
                paso = facturasBLL.Modificar(factura);
            }

            //Informar el resultado
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if (DetalleDataGridView.Rows.Count > 0 && DetalleDataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(DetalleDataGridView.CurrentRow.Index);
                CargarGrid();
                float total = 0;
                foreach (var item in this.Detalle)
                {

                    total += item.Importe;
                }
                TotalTextBox.Text = Convert.ToString(total);
            }
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {//
            int id;
            int.TryParse(IdNumericUpDown.Text, out id);

            Facturas factura = new Facturas();

            RepositorioBase<Facturas> repositorio = new RepositorioBase<Facturas>();
            Limpiar();

            factura = repositorio.Buscar(id);

            if (factura != null)
            {
                 LlenarCampos(factura);
            }
            else
            {
                MessageBox.Show("Registro No encontrado");
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(IdNumericUpDown.Text, out id);

            RepositorioBase<Facturas> repositorio = new RepositorioBase<Facturas>();
            Limpiar();

            if (repositorio.Buscar(id) != null)
            {
                if (repositorio.Eliminar(id))
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MyErrorProvider.SetError(IdNumericUpDown, "No se puede eliminar un registro que no existe");
                IdNumericUpDown.Focus();
            }
        }

        private void PrecioTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                CantidadTextBox.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                PrecioTextBox.Text = "0";
            }
            ImporteTextBox.Text = Convert.ToString(Convert.ToSingle(CantidadTextBox.Text) * Convert.ToSingle(PrecioTextBox.Text));
        }

        private void rFactura_Load(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> Categorias = new RepositorioBase<Categorias>();
            foreach (var auxiliar in Categorias.GetList(p => true))
            {
                CategoriaComboBox.Items.Add(auxiliar.Descripcion);
            }
        }

        private void CantidadTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                PrecioTextBox.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                CantidadTextBox.Text = "0";
            }
            ImporteTextBox.Text = Convert.ToString(Convert.ToSingle(CantidadTextBox.Text) * Convert.ToSingle(PrecioTextBox.Text));

        }
    }
}
