using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace Inventario
{
    class Conexion
    {
        public SqlConnection con;
        public DataView vista;

        private static string astrCadenaConexion = ConfigurationSettings.AppSettings["SistemaConnectionString"].ToString();
        public Conexion()
        {
            Iniciar();

            //vista = lista().DefaultView;
        }

        public void Iniciar()
        {
            con = new SqlConnection();
            con = new SqlConnection(astrCadenaConexion);


        }
        public SqlConnection getcon
        {
            get
            {
                return con;
            }
        }

        public DataTable ObtenerTodoSEmpleado()
        {
            SqlDataAdapter ds = new SqlDataAdapter("select Nombre,Apellidos,Salario,IdAlmacen from Empleado", getcon);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            return dt;
        }


        public DataTable ObtenerTodoSAlmacen()
        {
            SqlDataAdapter ds = new SqlDataAdapter("SELECT  IdAlmacen FROM  Almacen where activo=1", getcon);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            return dt;
        }

        public DataTable ObtenerTodoAlmacen()
        {
            SqlDataAdapter ds = new SqlDataAdapter("SELECT   *FROM  Almacen ", getcon);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            return dt;
        }
        public DataTable ObtenerTodoStblusuario(string txtusuario, string txtpassword)
        {
            DataTable dt = new DataTable();
            try
            {
                // Consulta parametrizada: el usuario y la contraseña viajan como datos, nunca como parte del texto SQL,
                // así que un valor como ' OR '1'='1 no puede cambiar la consulta (evita la inyección SQL).
                const string sql = "SELECT * FROM tblusuario WHERE Usuario = @Usuario AND Contrasenia = @Contrasenia AND ACTIVO = 1";
                using (SqlCommand cmd = new SqlCommand(sql, getcon))
                {
                    cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = txtusuario;
                    cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 50).Value = txtpassword;
                    using (SqlDataAdapter ds = new SqlDataAdapter(cmd))
                    {
                        ds.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Si la base de datos no responde se regresa la tabla vacía y el login rechaza el acceso.
            }
            return dt;
        }

        public DataTable ObtenerTodoStblusuario()
        {
            SqlDataAdapter ds = new SqlDataAdapter("SELECT Usu.Usuario,Usu.Correo,Usu.Activo FROM  tblusuario  Usu  INNER JOIN tblusuariorol  ROL ON Usu.UsuarioRolID=ROL.UsuarioRolID", getcon);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            return dt;
        }




        public DataView filtrar(string RFC)
        {
            // Se duplican las comillas simples para que el texto no rompa la expresión del filtro.
            vista.RowFilter = "RFC LIKE '" + RFC.Replace("'", "''") + "%'";
            return vista;
        }


        public void guardarEmpleado(string  Nombre,string Apellidos,string Salario,string IdAlmacen,ref int Resultado)
        {
            int IntIdAlmacen = 0;
            //operador Ternario parecido if, else el cual valida si  IdAlmacen viene null o vacio la 
            //variable IntIdAlmacen es igual a 0 IntIdAlmacen=0
            IntIdAlmacen = string.IsNullOrEmpty(IdAlmacen) ? Convert.ToInt32(IdAlmacen) : 0;
            if (con.State != ConnectionState.Open)
            {

       
                con.Open();
                SqlCommand comando = new SqlCommand("insert into  Empleado (Nombre,Apellidos,Edad,Domicilio,Colonia,Sexo,Telefono,Email,Salario,IdAlmacen)  VALUES (@Nombre,@Apellidos,@Edad,@Domicilio,@Colonia,@Sexo,@Telefono,@Email,@Salario,@IdAlmacen)", con);
                //SqlCommand comando1 = new SqlCommand("insert into  (UsuarioRolID,Usuario,Contrasenia ,Correo,Activo) tblusuarioVALUES(@UsuarioRolID,@Usuario,@Contrasenia ,@Correo,@Activo)", con);
                //SqlCommand comando2 = new SqlCommand("insert into Datos2_Empleado (Nombre, Apellidos, Fecha_Ingreso, Fecha_Vacaciones, Hora_Entrada, Hora_Salida, Ocupacion, Departamento) values(@nombre, @apellidos, @fecha_ingreso, @fecha_vacaciones, @hora_entrada, @hora_salida, @ocupacion, @departamento)", con);

                //string fecha = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();

                comando.Parameters.AddWithValue("nombre", Nombre);
                comando.Parameters.AddWithValue("Apellidos", Apellidos);
                comando.Parameters.AddWithValue("Edad", "");
                comando.Parameters.AddWithValue("Domicilio", "");
                comando.Parameters.AddWithValue("colonia", "");
                comando.Parameters.AddWithValue("Sexo", "");
                comando.Parameters.AddWithValue("Telefono", "");
                comando.Parameters.AddWithValue("Email", "");
                comando.Parameters.AddWithValue("Salario",float.Parse(Salario));
                comando.Parameters.AddWithValue("IdAlmacen", Convert.ToInt32(IntIdAlmacen) );






                Resultado= comando.ExecuteNonQuery();
            }
            else
            {
                SqlCommand comando = new SqlCommand("insert into  Empleado (Nombre,Apellidos,Edad,Domicilio,Colonia,Sexo,Telefono,Email,Salario,IdAlmacen)  VALUES (@Nombre,@Apellidos,@Edad,@Domicilio,@Colonia,@Sexo,@Telefono,@Email,@Salario,@IdAlmacen)", con);

                comando.Parameters.AddWithValue("nombre", Nombre);
                comando.Parameters.AddWithValue("Apellidos", Apellidos);
                comando.Parameters.AddWithValue("Edad", "");
                comando.Parameters.AddWithValue("Domicilio", "");
                comando.Parameters.AddWithValue("colonia", "");
                comando.Parameters.AddWithValue("Sexo", "");
                comando.Parameters.AddWithValue("Telefono", "");
                comando.Parameters.AddWithValue("Email", "");
                comando.Parameters.AddWithValue("Salario", float.Parse(Salario));
                comando.Parameters.AddWithValue("IdAlmacen", Convert.ToInt32(IntIdAlmacen));



                Resultado = comando.ExecuteNonQuery();
            }
            
        }


        public void GuardarUsuario(string Usuario, string Contrasenia, string Correo, string IdAlmacen, ref int Resultado)
        {

            int resultado = 0;
            if (con.State != ConnectionState.Open)
            {
                this.con = new SqlConnection(astrCadenaConexion);
                con.Open();
                SqlCommand comando = new SqlCommand("insert into tblusuario (UsuarioRolID,Usuario,Contrasenia ,Correo,Activo) VALUES(@UsuarioRolID,@Usuario,@Contrasenia ,@Correo,@Activo)", con);

                //string fecha = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();
                comando.Parameters.AddWithValue("UsuarioRolID", 1);

                comando.Parameters.AddWithValue("Usuario", Usuario);
                comando.Parameters.AddWithValue("Contrasenia", Contrasenia);

                comando.Parameters.AddWithValue("Correo", Correo);
                comando.Parameters.AddWithValue("Activo", 1);


                Resultado = comando.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand comando = new SqlCommand("insert into tblusuario  (UsuarioRolID,Usuario,Contrasenia ,Correo,Activo) VALUES(@UsuarioRolID,@Usuario,@Contrasenia ,@Correo,@Activo)", con);

                //string fecha = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();
                comando.Parameters.AddWithValue("UsuarioRolID", 1);

                comando.Parameters.AddWithValue("nombre", Usuario);
                comando.Parameters.AddWithValue("Contrasenia", Contrasenia);

                comando.Parameters.AddWithValue("Correo", Correo);
                comando.Parameters.AddWithValue("Activo", 1);



                Resultado  = comando.ExecuteNonQuery();
                con.Close();

            }
        }

        public void guardarEnAlmacen(string Codigo, string Nombre, string Descripcion, string Cantidad, string PrecioUnidad, string Activo)
        {
            if (con.State != ConnectionState.Open)
            {
                this.con = new SqlConnection(astrCadenaConexion);
                con.Open();
                SqlCommand comando = new SqlCommand("insert into  Almacen  (Codigo ,Fecha_Ingreso ,Nombre ,Descripcion,Cantidad,PrecioUnidad,Activo) Values(@Codigo, @Fecha_Ingreso, @Nombre, @Descripcion, @Cantidad, @PrecioUnidad,@Activo)", con);

                //string fecha = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();

                comando.Parameters.AddWithValue("Codigo", Codigo);
                comando.Parameters.AddWithValue("Fecha_Ingreso", DateTime.Now.ToString("MM-dd-yyyy"));
                comando.Parameters.AddWithValue("Nombre", Nombre);
                comando.Parameters.AddWithValue("Cantidad", Convert.ToInt32(Cantidad));
                comando.Parameters.AddWithValue("PrecioUnidad", float.Parse(PrecioUnidad));
                comando.Parameters.AddWithValue("Activo", 1);


                comando.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand comando = new SqlCommand("insert into  Almacen (Codigo ,Fecha_Ingreso ,Nombre ,Descripcion,Cantidad,PrecioUnidad,Activo) Values(@Codigo, @Fecha_Ingreso, @Nombre, @Descripcion, @Cantidad, @PrecioUnidad,@Activo)", con);

                //string fecha = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();

                comando.Parameters.AddWithValue("Codigo", Codigo);
                comando.Parameters.AddWithValue("Fecha_Ingreso", DateTime.Now.ToString("MM-dd-yyyy"));
                comando.Parameters.AddWithValue("Nombre", Nombre);
                comando.Parameters.AddWithValue("Cantidad", Convert.ToInt32(Cantidad));
                comando.Parameters.AddWithValue("PrecioUnidad", float.Parse(PrecioUnidad));
                comando.Parameters.AddWithValue("Activo", 1);


                comando.ExecuteNonQuery();
                con.Close();
            }
        }


        /// <summary>
        /// Registra una compra: descuenta la cantidad del producto en el almacén si hay existencia suficiente.
        /// Regresa el mensaje que se muestra al usuario.
        /// </summary>
        public string ActualizarEnAlmacen(string TextBoxProducto, string astrCantidad)
        {
            int cantidad;
            if (!int.TryParse(astrCantidad, out cantidad) || cantidad <= 0)
            {
                return "Escribe una cantidad válida mayor a cero.";
            }

            using (SqlConnection cn = new SqlConnection(astrCadenaConexion))
            {
                cn.Open();

                int existencia;
                using (SqlCommand consulta = new SqlCommand("SELECT Cantidad FROM Almacen WHERE Nombre = @Nombre", cn))
                {
                    consulta.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = TextBoxProducto;
                    object valor = consulta.ExecuteScalar();
                    if (valor == null)
                    {
                        return "No existe el Producto.";
                    }
                    existencia = valor == DBNull.Value ? 0 : Convert.ToInt32(valor);
                }

                if (existencia < cantidad)
                {
                    return "No hay producto suficiente, solo hay en existencia: " + existencia;
                }

                // La condición "Cantidad >= @Cantidad" evita dejar la existencia en negativo
                // si otra compra se registró entre la consulta y la actualización.
                using (SqlCommand actualiza = new SqlCommand(
                    "UPDATE Almacen SET Cantidad = Cantidad - @Cantidad WHERE Nombre = @Nombre AND Cantidad >= @Cantidad", cn))
                {
                    actualiza.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                    actualiza.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = TextBoxProducto;
                    if (actualiza.ExecuteNonQuery() == 0)
                    {
                        return "No hay producto suficiente para completar la compra.";
                    }
                }
            }

            return "Se ha realizado el proceso de compra.";
        }




        public DataTable llenardatos()
        {
            DataTable tb = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Almacen", con);
            da.Fill(tb);
            con.Close();

            return tb;
        }


    }
}
