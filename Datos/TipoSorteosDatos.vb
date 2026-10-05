Imports MySql.Data.MySqlClient
Imports System.Data

Public Class TipoSorteosDatos
    Public Property TipoSorteo As Object

    Public Function ObtenerTipoSorteo() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Codigo, Tipo FROM tiposorteos"

        ' La conexión nace y muere aquí, liberando recursos en Aiven al instante
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar tipos de sorteo: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return stabla
    End Function

    Public Sub InsertarTipoSorteo(fila As DataRow)
        Dim query As String = "INSERT INTO tiposorteos (Codigo, Tipo) VALUES (@codigo, @tipo)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@codigo", fila("Codigo"))
                cmd.Parameters.AddWithValue("@tipo", fila("Tipo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar tipo de sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarTipoSorteo(fila As DataRow)
        Dim query As String = "UPDATE tiposorteos SET tipo = @tipo WHERE Codigo = @codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@codigo", fila("Codigo"))
                cmd.Parameters.AddWithValue("@tipo", fila("Tipo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al editar tipo de sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EliminarTipoSorteo(fila As DataRow)
        Dim query As String = "DELETE FROM tiposorteos WHERE Codigo = @codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@codigo", fila("Codigo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al eliminar tipo de sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class
