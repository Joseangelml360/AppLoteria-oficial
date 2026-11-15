Imports MySql.Data.MySqlClient
Imports System.Data

Public Class UsuarioDatos
    Public Function BuscarUsuario(cNombre As String, cContraseña As String) As DataTable
        Dim sTabla As New DataTable()

        ' 🔥 REPARACIÓN INTEGRADA: Llamamos directamente a tu función estática de conexión
        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Dim query As String = "SELECT idusuarios, nombre, idrol, activo FROM usuarios WHERE nombre = @nombre AND password = @password"

            Using cmd As New MySqlCommand(query, conn)
                ' Parámetros blindados contra Inyección SQL y errores de comillas
                cmd.Parameters.AddWithValue("@nombre", cNombre)
                cmd.Parameters.AddWithValue("@password", cContraseña)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        ' El adapter lee el flujo del servidor y rellena la tabla automáticamente
                        adapter.Fill(sTabla)
                    Catch ex As Exception
                        ' Lanza la excepción hacia la capa de negocio
                        Throw New Exception("Error en la base de datos al buscar usuario: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return sTabla
    End Function
End Class
