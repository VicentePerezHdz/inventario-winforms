using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario
{
    public partial class Compras : Form
    {
        Conexion con = new Conexion();
        public Compras()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GuardarCompra();

        }

        private void GuardarCompra()
        {
            
            string mensaje = con.ActualizarEnAlmacen(txtnombre.Text, txtcantidad.Text);
            MessageBox.Show(mensaje, "Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Panel panel = new Panel();
            panel.Show();
        }
    }
}
