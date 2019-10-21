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
    public partial class rCategorias : Form
    {
        public rCategorias()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            CategoriaIDNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            MyErrorProvider.Clear();
        }
        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private Categorias LlenarClase() //del form a la base de datos
        {
            Categorias categoria = new Categorias();
            categoria.CategoriaID = Convert.ToInt32(CategoriaIDNumericUpDown.Value);
            categoria.Descripcion = DescripcionTextBox.Text;
            return categoria;
        }

        private void LLenarCampo(Categorias categroria)
        {
            CategoriaIDNumericUpDown.Value = categroria.CategoriaID;
            DescripcionTextBox.Text = categroria.Descripcion;
            
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
            Categorias categoria = repositorio.Buscar((int)CategoriaIDNumericUpDown.Value);
            return (categoria != null);
        }

        private bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                MyErrorProvider.SetError(DescripcionTextBox, "Debe agregar una descripción");
                DescripcionTextBox.Focus();
                paso = false;
            }

            return paso;
        }


        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Categorias categoria;
            bool paso = false;

            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();


            if (!Validar())
                return;

            categoria = LlenarClase();

            //Determinar si es guargar o modificar
            if (CategoriaIDNumericUpDown.Value == 0)
                paso = repositorio.Guardar(categoria);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un registro que no existe");
                    return;
                }
                paso = repositorio.Modificar(categoria);
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

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(CategoriaIDNumericUpDown.Text, out id);

            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
            Limpiar();

            if (repositorio.Buscar(id) != null)
            {
                if (repositorio.Eliminar(id))
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MyErrorProvider.SetError(CategoriaIDNumericUpDown, "No se puede eliminar un registro que no existe");
                CategoriaIDNumericUpDown.Focus();
            }
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(CategoriaIDNumericUpDown.Text, out id);

            Categorias categoria = new Categorias();

            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            Limpiar();

            categoria = repositorio.Buscar(id);

            if (categoria != null)
            {
                LLenarCampo(categoria);
            }
            else
            {
                MessageBox.Show("Registro No encontrado");
            }
        }
    }
}
