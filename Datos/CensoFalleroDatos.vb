Imports MySql.Data.MySqlClient
Imports System.Data

Public Class CensoFalleroDatos
    Public Property FalleroEntidad As Object

    Public Function ObtenerCensoFalleros() As DataTable
        ' Instanciamos una tabla limpia en memoria
        Dim stabla As New DataTable()
        Dim query As String = "SELECT ANO_CEN, FALLERO_CODIGO, NOMBRE, APELLIDOS, LOTERIA, TIPFAL FROM fallero WHERE BAJA = 0 AND LOTERIA = 0"

        ' 🔥 LA CONEXIÓN NACE Y MUERE AQUÍ: Garantiza la liberación del puerto SSL en Aiven
        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                ' El DataAdapter realiza la ráfaga de descarga masiva sin bucles manuales
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al consultar el censo de socios (No vendedores) en Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Function ImportarCensoFalleros() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT ANO_CEN, FALLERO_CODIGO, NOMBRE, APELLIDOS, LOTERIA, TIPFAL FROM fallero WHERE BAJA = 0 AND LOTERIA = 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al importar el censo de vendedores autorizados desde Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function
End Class
