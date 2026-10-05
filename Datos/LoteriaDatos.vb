Imports MySql.Data.MySqlClient
Imports System.Data

Public Class LoteriaDatos
    Public Property VendedorEntidad As Object

    Public Function ObtenerVendedoresLoteria() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria, LoteriaAsignada, Liquidado, ACuota, Saldo FROM vendedores ORDER BY Codigo ASC"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al descargar el fichero de vendedores desde Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarVendedor(fila As DataRow)
        Dim query As String = "INSERT INTO vendedores (Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria, LoteriaAsignada, Liquidado, ACuota, Saldo) " &
                               "VALUES (@Ejercicio, @Codigo, @Nombre, @Apellidos, @Comision, @CuotaLoteria, @LoteriaAsignada, @Liquidado, @ACuota, @Saldo)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@Codigo", fila("Codigo"))
                cmd.Parameters.AddWithValue("@Nombre", fila("Nombre"))
                cmd.Parameters.AddWithValue("@Apellidos", fila("Apellidos"))
                cmd.Parameters.AddWithValue("@Comision", fila("Comision"))
                cmd.Parameters.AddWithValue("@CuotaLoteria", fila("CuotaLoteria"))
                cmd.Parameters.AddWithValue("@LoteriaAsignada", fila("LoteriaAsignada"))
                cmd.Parameters.AddWithValue("@ACuota", fila("ACuota"))
                cmd.Parameters.AddWithValue("@Liquidado", fila("Liquidado"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar el nuevo vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarVendedor(fila As DataRow)
        Dim query As String = "UPDATE vendedores SET CuotaLoteria = @CuotaLoteria, LoteriaAsignada = @LoteriaAsignada, " &
                               "Liquidado = @Liquidado, ACuota = @ACuota, Saldo = @Saldo WHERE Codigo = @Codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@CuotaLoteria", fila("CuotaLoteria"))
                cmd.Parameters.AddWithValue("@LoteriaAsignada", fila("LoteriaAsignada"))
                cmd.Parameters.AddWithValue("@Liquidado", fila("Liquidado"))
                cmd.Parameters.AddWithValue("@ACuota", fila("ACuota"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))
                cmd.Parameters.AddWithValue("@Codigo", fila("Codigo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al actualizar los datos del vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarVendedorDetallesVenta(fila As DataRow)
        Dim query As String = "UPDATE vendedores SET Liquidado = @Liquidado, ACuota = @ACuota, Saldo = @Saldo " &
                               "WHERE Codigo = @Codigo AND Ejercicio = @Ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Liquidado", fila("Liquidado"))
                cmd.Parameters.AddWithValue("@ACuota", fila("ACuota"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))
                cmd.Parameters.AddWithValue("@Codigo", fila("Codigo"))
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error contable al actualizar balances desde detalles de venta: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EliminarVendedor(fila As DataRow)
        Dim query As String = "DELETE FROM vendedores WHERE Codigo = @Codigo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Codigo", fila("Codigo"))
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al eliminar al vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' 🔥 BLINDAJE TRANSACCIONAL COMPUESTO (Unificamos Importar y Rellenar en un solo bloque seguro)
    Public Sub ImportarNuevosVendedores()
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Dim transaccion As MySqlTransaction = Nothing

            Try
                conn.Open()
                transaccion = conn.BeginTransaction()

                ' 1. Pasamos el censo activo de falleros a la tabla de vendedores
                Dim q1 As String = "INSERT INTO vendedores (Ejercicio, Codigo, Nombre, Apellidos, Comision, CuotaLoteria) " &
                                   "SELECT ANO_CEN, FALLERO_CODIGO, NOMBRE, APELLIDOS, TIPFAL, LOTERIA FROM fallero " &
                                   "WHERE LOTERIA = 1 AND BAJA = 0"

                Using cmd1 As New MySqlCommand(q1, conn, transaccion)
                    cmd1.ExecuteNonQuery()
                End Using

                ' 2. Inicializamos todos los saldos a cero de forma preventiva
                Dim q2 As String = "UPDATE vendedores SET LoteriaAsignada = 0, Liquidado = 0, Acuota = 0, Saldo = 0"
                Using cmd2 As New MySqlCommand(q2, conn, transaccion)
                    cmd2.ExecuteNonQuery()
                End Using

                ' 3. Asignamos la lotería base según el tipo de comisión (Infantiles/Mayores)
                Dim q3 As String = "UPDATE vendedores SET LoteriaAsignada = CASE " &
                                   "WHEN Comision = 1 THEN 750 " &
                                   "WHEN Comision = 3 THEN 510 " &
                                   "ELSE 0 END"
                Using cmd3 As New MySqlCommand(q3, conn, transaccion)
                    cmd3.ExecuteNonQuery()
                End Using

                ' 4. Calculamos el saldo contable inicial aplicando el 20% de beneficio
                Dim q4 As String = "UPDATE vendedores SET Saldo = -(LoteriaAsignada) * 0.2"
                Using cmd4 As New MySqlCommand(q4, conn, transaccion)
                    cmd4.ExecuteNonQuery()
                End Using

                ' 🔥 ¡COMMIT!: Si los 4 pasos masivos se procesan con éxito, guardamos de golpe en Aiven
                transaccion.Commit()

            Catch ex As Exception
                ' ❌ ABORTO: Si el internet parpadea, deshacemos toda la importación para no dejar datos corruptos
                If transaccion IsNot Nothing Then transaccion.Rollback()
                Throw New Exception("Error crítico en la ráfaga de importación. Base de datos revertida: " & ex.Message)
            End Try
        End Using
    End Sub
End Class
