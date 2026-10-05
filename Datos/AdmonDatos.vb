Imports MySql.Data.MySqlClient
Imports System.Data

Public Class AdmonDatos
    Public Property AdministracionesEntidad As Object

    Public Function GetNombre(idadmon As Integer) As String
        Dim nombre As String = ""
        Dim query As String = "SELECT Nombre FROM administraciones WHERE NumAdmon = @idadmon"

        ' 🔥 CLAVE: La conexión nace y muere dentro del Using, garantizando que el puerto SSL se libere siempre
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idadmon", idadmon)

                Try
                    conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            nombre = reader.GetString(0)
                        End If
                    End Using
                Catch ex As Exception
                    Throw New Exception("Error al obtener el nombre de la administración: " & ex.Message)
                End Try
            End Using
        End Using

        Return nombre
    End Function

    Public Function ObtenerAdministraciones() As DataTable
        ' Instanciamos una tabla limpia en memoria
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Id, NumAdmon, Nombre, Direccion, CodPost, Poblacion, Telefono, Mail, Responsable FROM administraciones"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                ' Usamos un DataAdapter para rellenar la tabla de forma instantánea sin bucles manuales 'While'
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar el listado de administraciones: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarAdmon(fila As DataRow)
        Dim query As String = "INSERT INTO administraciones (NumAdmon, Nombre, Direccion, CodPost, Poblacion, Telefono, Mail, Responsable) " &
                               "VALUES (@NumAdmon, @Nombre, @Direccion, @CodPost, @Poblacion, @Telefono, @Mail, @Responsable)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@NumAdmon", fila("NumAdmon"))
                cmd.Parameters.AddWithValue("@Nombre", fila("Nombre"))
                cmd.Parameters.AddWithValue("@Direccion", fila("Direccion"))
                cmd.Parameters.AddWithValue("@CodPost", fila("CodPost"))
                cmd.Parameters.AddWithValue("@Poblacion", fila("Poblacion"))
                cmd.Parameters.AddWithValue("@Telefono", fila("Telefono"))
                cmd.Parameters.AddWithValue("@Mail", fila("Mail"))
                cmd.Parameters.AddWithValue("@Responsable", fila("Responsable"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al registrar la administración en la nube: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarAdmon(fila As DataRow)
        Dim query As String = "UPDATE administraciones SET NumAdmon=@NumAdmon, Nombre=@Nombre, Direccion=@Direccion, " &
                               "CodPost=@CodPost, Poblacion=@Poblacion, Telefono=@Telefono, Mail=@Mail, Responsable=@Responsable " &
                               "WHERE Id=@Id"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", fila("Id"))
                cmd.Parameters.AddWithValue("@NumAdmon", fila("NumAdmon"))
                cmd.Parameters.AddWithValue("@Nombre", fila("Nombre"))
                cmd.Parameters.AddWithValue("@Direccion", fila("Direccion"))
                cmd.Parameters.AddWithValue("@CodPost", fila("CodPost"))
                cmd.Parameters.AddWithValue("@Poblacion", fila("Poblacion"))
                cmd.Parameters.AddWithValue("@Telefono", fila("Telefono"))
                cmd.Parameters.AddWithValue("@Mail", fila("Mail"))
                cmd.Parameters.AddWithValue("@Responsable", fila("Responsable"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al actualizar los datos de la administración: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EliminarAdmon(fila As DataRow)
        Dim query As String = "DELETE FROM administraciones WHERE Id = @Id"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", fila("Id"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al eliminar la administración de la base de datos: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class

