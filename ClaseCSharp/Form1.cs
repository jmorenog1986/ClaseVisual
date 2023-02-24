using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClaseCSharp
{
  
    public partial class Form1 : Form
       
    {
        int documento;
        String nombre;
        String apellido;
        String email;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            llenar();
            String query = "INSERT INTO Personas VALUES('"+documento+"','"+nombre+"','"+apellido+"','"+email+"')";
            ejecutar(query,1);
           

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            llenar();
            String query = "UPDATE Personas SET nombre='" + nombre + "', apellido='" + apellido + "', email='" + email + "' WHERE documento='" + documento + "'";
            ejecutar(query,1);
        }

        public void llenar()
        {
             documento = Int16.Parse(txtDocumento.Text);
             nombre = txtNombre.Text;
             apellido = txtApellido.Text;
             email = txtEmail.Text;
        }

        private void ejecutar(String query, int op)
        {
            SqlConnection conexion = new SqlConnection("Data Source=192.168.1.24;Initial Catalog=db01;Persist Security Info=True;User ID=SQLServer01;Password=123456");
            conexion.Open();
           

            SqlCommand comando = new SqlCommand(query, conexion);
            if (op==1)
            {
                comando.ExecuteNonQuery();
            }else if (op==2)
            {
                SqlDataReader r=comando.ExecuteReader();
                while (r.Read())
                {
                    txtDocumento.Text = r["documento"].ToString();
                    txtNombre.Text= r["nombre"].ToString();
                    txtApellido.Text= r["apellido"].ToString();
                    txtEmail.Text=r["email"].ToString();
                }
            }
           

            conexion.Close();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            llenar();
            String query = "DELETE FROM personas WHERE documento='"+documento+"'";
            ejecutar(query,1);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            llenar();

            String query = "SELECT * FROM personas WHERE documento='"+documento+"'";
            ejecutar(query, 2);
            
        }

        private void btnConsultarTodos_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Personas");
            dt.Columns.Add("Documento", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            // Agregar algunos datos


            SqlConnection conexion = new SqlConnection("Data Source=192.168.1.24;Initial Catalog=db01;Persist Security Info=True;User ID=SQLServer01;Password=123456");
            conexion.Open();
            String query = "SELECT * FROM personas";

            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader r = comando.ExecuteReader();
            while (r.Read())
            {

                dt.Rows.Add(
                        r["documento"].ToString(),
                        r["nombre"].ToString(),
                        r["apellido"].ToString(),
                        r["email"].ToString()
                    );

            }

            
            // Asignar el DataTable como origen de datos del DataGridView
            tablaPersonas.DataSource = dt;


            conexion.Close();
        }
    }
}
