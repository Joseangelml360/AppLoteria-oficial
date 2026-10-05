Imports MySql.Data.MySqlClient
Imports System.Data

Public Class VersionLoteriaDatos
    Public Function ObtenerUltimaVersion() As DataRow
        Dim stabla As New DataTable()
        Dim query As String = "SELECT id, version_loteria, url_zip, obligatoria " &
                               "FROM version_loteria ORDER BY id DESC LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                        If stabla.Rows.Count > 0 Then Return stabla.Rows(0)
                    Catch ex As Exception
                        Throw New Exception("Error al consultar la última versión: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function ObtenerHistorialVersiones() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT id, version_loteria, url_zip, obligatoria FROM version_loteria ORDER BY id DESC"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar el historial de versiones: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return stabla
    End Function

    Public Sub PublicarVersion(fila As DataRow)
        Dim query As String = "INSERT INTO version_loteria (version_loteria, url_zip, obligatoria) " &
                               "VALUES (@version, @url, @obligatoria)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@version", fila("version_loteria"))
                cmd.Parameters.AddWithValue("@url", fila("url_zip"))
                cmd.Parameters.AddWithValue("@obligatoria", fila("obligatoria"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar la nueva versión en Aiven: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class
