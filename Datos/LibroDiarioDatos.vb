Imports MySql.Data.MySqlClient
Imports System.Data

Public Class LibroDiarioDatos
    Public Property LibroDiarioEntidad As Object

    Public Function ObtenerLibroDiario(ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdDiario, Ejercicio, Fecha, EntradaSalida, Importe, CajaBanco, " &
                               "Concepto, Debe, Haber, Saldo FROM librodiario WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar el libro diario desde Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarApunte(fila As DataRow)
        Dim query As String = "INSERT INTO librodiario (Ejercicio, Fecha, EntradaSalida, Importe, CajaBanco, Concepto, Debe, Haber, Saldo) " &
                               "VALUES (@Ejercicio, @Fecha, @EntradaSalida, @Importe, @CajaBanco, @Concepto, @Debe, @Haber, @Saldo)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@Fecha", fila("Fecha"))
                cmd.Parameters.AddWithValue("@EntradaSalida", fila("EntradaSalida"))
                cmd.Parameters.AddWithValue("@Importe", fila("Importe"))
                cmd.Parameters.AddWithValue("@CajaBanco", fila("CajaBanco"))
                cmd.Parameters.AddWithValue("@Concepto", fila("Concepto"))
                cmd.Parameters.AddWithValue("@Debe", fila("Debe"))
                cmd.Parameters.AddWithValue("@Haber", fila("Haber"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar el apunte contable en el libro diario: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Function GetDebeHaberLibroDiario(ejercicio As Integer) As Double
        Dim sumadebehaber As Double = 0
        Dim query As String = "SELECT SUM(Haber)-SUM(Debe) FROM librodiario WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)

                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()

                    ' 🔥 CONTROL DE CERO: Si el año está vacío, evitamos caídas devolviendo un saldo de 0.00€
                    If resultado IsNot Nothing AndAlso Not Convert.IsDBNull(resultado) Then
                        sumadebehaber = Convert.ToDouble(resultado)
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al calcular el balance del libro diario en Aiven: " & ex.Message)
                End Try
            End Using
        End Using

        Return sumadebehaber
    End Function

    Public Function GetDebeHaberLibroDiarioTransaccional(ejercicio As Integer, conn As MySqlConnection, transaccion As MySqlTransaction) As Double
        Dim saldoCalculado As Double = 0
        Dim sql As String = "SELECT COALESCE(SUM(Haber) - SUM(Debe), 0) FROM librodiario WHERE Ejercicio = @ejercicio"

        Using cmd As New MySqlCommand(sql, conn, transaccion)
            cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
            Dim resultado = cmd.ExecuteScalar()
            If resultado IsNot Nothing AndAlso Not Convert.IsDBNull(resultado) Then
                saldoCalculado = Convert.ToDouble(resultado)
            End If
        End Using
        Return saldoCalculado
    End Function


    ' Sobrecarga transaccional para el asiento de caja
    Public Sub InsertarApunteTransaccional(fila As DataRow, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "INSERT INTO librodiario (Ejercicio, Fecha, EntradaSalida, Importe, CajaBanco, Concepto, Debe, Haber, Saldo) " &
                               "VALUES (@Ejercicio, @Fecha, @EntradaSalida, @Importe, @CajaBanco, @Concepto, @Debe, @Haber, @Saldo)"

        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
            cmd.Parameters.AddWithValue("@Fecha", fila("Fecha"))
            cmd.Parameters.AddWithValue("@EntradaSalida", fila("EntradaSalida"))
            cmd.Parameters.AddWithValue("@Importe", fila("Importe"))
            cmd.Parameters.AddWithValue("@CajaBanco", fila("CajaBanco"))
            cmd.Parameters.AddWithValue("@Concepto", fila("Concepto"))
            cmd.Parameters.AddWithValue("@Debe", fila("Debe"))
            cmd.Parameters.AddWithValue("@Haber", fila("Haber"))
            cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))
            cmd.ExecuteNonQuery()
        End Using
    End Sub



End Class
