Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DetallesSorteoVendedorDatos
    Public Property DetallesSorteoVendedorEntidad As Object

    Public Function GetDetallesSorteoVendorEjercicio(ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdSorteo, IdVendedor, Ejercicio, PapeletasEntregadas, PapeletasDevueltas, " &
                               "TotalAdeudado, EntregadoParcial, Saldo FROM detallesorteovendedor WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar los detalles del ejercicio: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Function GetDetallesSorteoVendor(ejercicio As Integer, idsorteo As Integer, idvendor As Integer) As DataTable
        Dim stabla As New DataTable()
        ' 🔥 REPARACIÓN: Añadimos de forma limpia los espacios y los operadores AND parametrizados
        Dim query As String = "SELECT IdSorteo, IdVendedor, Ejercicio, PapeletasEntregadas, PapeletasDevueltas, " &
                               "TotalAdeudado, EntregadoParcial, Saldo FROM detallesorteovendedor " &
                               "WHERE Ejercicio = @ejercicio AND IdSorteo = @idsorteo AND IdVendedor = @idvendor"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                cmd.Parameters.AddWithValue("@idvendor", idvendor)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al consultar el detalle específico del vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarApunte(fila As DataRow)
        Dim query As String = "INSERT INTO detallesorteovendedor (IdSorteo, IdVendedor, Ejercicio, PapeletasEntregadas, " &
                               "PapeletasDevueltas, TotalAdeudado, EntregadoParcial, Saldo) " &
                               "VALUES (@IdSorteo, @IdVendedor, @Ejercicio, @PapeletasEntregadas, " &
                               "@PapeletasDevueltas, @TotalAdeudado, @EntregadoParcial, @Saldo)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
                cmd.Parameters.AddWithValue("@IdVendedor", fila("IdVendedor"))
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
                cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
                cmd.Parameters.AddWithValue("@TotalAdeudado", fila("TotalAdeudado"))
                cmd.Parameters.AddWithValue("@EntregadoParcial", fila("EntregadoParcial"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar el apunte de lotería del vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub ActualizarDetalleSorteoVendor(fila As DataRow)
        Dim query As String = "UPDATE detallesorteovendedor SET PapeletasEntregadas = @PapeletasEntregadas, " &
                               "PapeletasDevueltas = @PapeletasDevueltas, TotalAdeudado = @TotalAdeudado, " &
                               "EntregadoParcial = @EntregadoParcial, Saldo = @Saldo " &
                               "WHERE Ejercicio = @Ejercicio AND IdVendedor = @IdVendedor"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                ' 🔥 REPARACIÓN CLAVE: Inyectamos los parámetros obligatorios del WHERE que faltaban
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@IdVendedor", fila("IdVendedor"))

                ' Parámetros de actualización contable
                cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
                cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
                cmd.Parameters.AddWithValue("@TotalAdeudado", fila("TotalAdeudado"))
                cmd.Parameters.AddWithValue("@EntregadoParcial", fila("EntregadoParcial"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al actualizar las cuentas de lotería del vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class
