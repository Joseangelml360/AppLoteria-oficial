Imports MySql.Data.MySqlClient
Imports System.Data

Public Class VendedoresDatos
    Public Property VendedorEntidad As Object

    Public Function GetVendedoresMayor() As Integer
        Dim numMayores As Integer = 0
        Dim query As String = "SELECT COUNT(*) FROM vendedores WHERE Comision = 1"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim res As Object = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then
                        numMayores = Convert.ToInt32(res)
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al contar vendedores mayores: " & ex.Message)
                End Try
            End Using
        End Using
        Return numMayores
    End Function

    Public Function GetVendedoresJuvenil() As Integer
        Dim numJuveniles As Integer = 0
        Dim query As String = "SELECT COUNT(*) FROM vendedores WHERE Comision = 3"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim res As Object = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then
                        numJuveniles = Convert.ToInt32(res)
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al contar vendedores juveniles: " & ex.Message)
                End Try
            End Using
        End Using
        Return numJuveniles
    End Function

    Public Function GetVendedoresExtra(ejercicio As Integer) As Integer
        Dim numExtra As Integer = 0
        Dim query As String = "SELECT COUNT(*) FROM vendedores WHERE CuotaLoteria = 0 AND Ejercicio = @ejercicio"

        ' 🔥 REPARACIÓN: Parametrizamos de forma limpia el año del ejercicio contable
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Try
                    conn.Open()
                    Dim res As Object = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then
                        numExtra = Convert.ToInt32(res)
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al contar vendedores extraordinarios: " & ex.Message)
                End Try
            End Using
        End Using
        Return numExtra
    End Function

    Public Function ListaVendedores(ejercicio As Integer) As DataTable
        Dim tabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria, " &
                               "LoteriaAsignada, Liquidado, ACuota, Saldo FROM vendedores WHERE Ejercicio = @ejercicio " &
                               "ORDER BY Codigo ASC"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(tabla)
                    Catch ex As Exception
                        Throw New Exception("Error al listar vendedores del ejercicio: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Function GetVendorEjercicioTransaccional(codigoVendedor As Integer, ejercicio As Integer, conn As MySqlConnection, transaccion As MySqlTransaction) As DataRow
        Dim dt As New DataTable()
        Dim sql As String = "SELECT Liquidado, Saldo, ACuota, LoteriaAsignada FROM vendedores WHERE Codigo = @vendedor AND Ejercicio = @ejercicio"

        Using cmd As New MySqlCommand(sql, conn, transaccion)
            cmd.Parameters.AddWithValue("@vendedor", codigoVendedor)
            cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
            Using da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Throw New Exception($"No se encontró la ficha del vendedor {codigoVendedor} para el ejercicio {ejercicio}.")
        End If
    End Function
    Public Function GetVendorEjercicio(ejercicio As Integer, codigo As Integer) As DataTable
        Dim tabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria, " &
                               "LoteriaAsignada, Liquidado, ACuota, Saldo FROM vendedores WHERE Ejercicio = @ejercicio AND Codigo = @codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@codigo", codigo)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(tabla)
                    Catch ex As Exception
                        Throw New Exception("Error al buscar la ficha del vendedor en Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Function GetListaBeneficios(ejercicio As Integer) As DataTable
        Dim tabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria, Saldo" &
                               " FROM vendedores WHERE Ejercicio = @ejercicio AND Saldo >0"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(tabla)
                    Catch ex As Exception
                        Throw New Exception("Error al buscar la ficha del vendedor en Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Sub ActualizarMovimientoVendor(fila As DataRow)
        Dim query As String = "UPDATE vendedores SET Liquidado = @liquidado, ACuota = @acuota, Saldo = @saldo " &
                               "WHERE Ejercicio = @ejercicio AND Codigo = @codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@codigo", fila("Codigo"))
                cmd.Parameters.AddWithValue("@liquidado", fila("Liquidado"))
                cmd.Parameters.AddWithValue("@acuota", fila("ACuota"))
                cmd.Parameters.AddWithValue("@saldo", fila("Saldo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error contable al actualizar el balance del vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarVendedorTransaccional(liquidado As Double, acuota As Double, saldo As Double, codigo As Integer, ejercicio As Integer, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "UPDATE vendedores SET Liquidado = @Liquidado, ACuota = @ACuota, Saldo = @Saldo " &
                               "WHERE Codigo = @Codigo AND Ejercicio = @Ejercicio"

        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@Liquidado", liquidado)
            cmd.Parameters.AddWithValue("@ACuota", acuota)
            cmd.Parameters.AddWithValue("@Saldo", saldo)
            cmd.Parameters.AddWithValue("@Codigo", codigo)
            cmd.Parameters.AddWithValue("@Ejercicio", ejercicio)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class
