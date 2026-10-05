Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DetallesAdmonDatos
    Public Property DetallesAdmonEntidad As Object

    Public Function ObtenerDetallesAdmon() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Id, IdAdmon, Ejercicio, IdSorteo, NumJugado, NumJugado2, Fecha, PagoAbono, Modo, ImportePagado, PagoPremios, ImporteDecimos FROM detallesadmon"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al cargar los detalles de administración desde Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarDetallesAdmon(fila As DataRow)
        Dim query As String = "INSERT INTO detallesadmon (IdAdmon, Ejercicio, IdSorteo, NumJugado, NumJugado2, Fecha, PagoAbono, Modo, ImportePagado, PagoPremios, ImporteDecimos) " &
                               "VALUES (@IdAdmon, @Ejercicio, @IdSorteo, @NumJugado, @NumJugado2, @Fecha, @PagoAbono, @Modo, @ImportePagado, @PagoPremios, @ImporteDecimos)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdAdmon", fila("IdAdmon"))
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
                cmd.Parameters.AddWithValue("@NumJugado", fila("NumJugado"))
                cmd.Parameters.AddWithValue("@NumJugado2", fila("NumJugado2"))
                cmd.Parameters.AddWithValue("@Fecha", fila("Fecha"))
                cmd.Parameters.AddWithValue("@PagoAbono", fila("PagoAbono"))
                cmd.Parameters.AddWithValue("@Modo", fila("Modo"))
                cmd.Parameters.AddWithValue("@ImportePagado", fila("ImportePagado"))
                cmd.Parameters.AddWithValue("@PagoPremios", fila("PagoPremios"))
                cmd.Parameters.AddWithValue("@ImporteDecimos", fila("ImporteDecimos"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al registrar el movimiento contable en la nube: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Function TotalPagadoSorteo(idsorteo As Integer) As Double
        Dim query As String = "SELECT SUM(ImportePagado) FROM detallesadmon WHERE idsorteo = @idsorteo AND PagoAbono = 'P'"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                Try
                    conn.Open()
                    ' ExecuteScalar es la vía directa: toma la primera celda del resultado y la devuelve
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al calcular el total pagado del sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function TotalPagadoAdmon(ejercicio As Integer) As Double
        Dim query As String = "SELECT SUM(ImportePagado) FROM detallesadmon WHERE Ejercicio = @ejercicio AND PagoAbono = 'P'"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al calcular el total pagado de la administración: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ImporteDecimosSorteo(ejercicio As Integer, idsorteo As Integer) As Double
        Dim query As String = "SELECT SUM(ImporteDecimos) FROM detallesadmon WHERE Ejercicio = @ejercicio AND IdSorteo = @idsorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al consultar el importe de décimos del sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ImporteDecimosEjercicio(ejercicio As Integer) As Double
        Dim query As String = "SELECT SUM(ImporteDecimos) FROM detallesadmon WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al consultar el importe de décimos del ejercicio: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ABonadoPremiosSorteo(ejercicio As Integer, idsorteo As Integer) As Double
        Dim query As String = "SELECT SUM(ImportePremiadas) FROM detallesventaloteria WHERE Ejercicio = @ejercicio AND IdSorteo = @idsorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al calcular los premios abonados del sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Function
    Public Function CobradoPremiosSorteo(idsorteo As Integer, ejercicio As Integer) As Double
        Dim query As String = "SELECT SUM(PagoPremios) FROM detallesadmon WHERE Ejercicio = @ejercicio AND IdSorteo = @idsorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al calcular los premios abonados del sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Function
    Public Function AbonadoPremiosEjercicio(ejercicio As Integer) As Double
        Dim query As String = "SELECT SUM(PagoPremios) FROM detallesadmon WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If Convert.IsDBNull(resultado) OrElse resultado Is Nothing Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al calcular los premios abonados del ejercicio: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    ' Sobrecarga transaccional para guardar el detalle
    Public Sub InsertarDetallesAdmonTransaccional(fila As DataRow, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "INSERT INTO detallesadmon (IdAdmon, Ejercicio, IdSorteo, NumJugado, NumJugado2, Fecha, PagoAbono, Modo, ImportePagado, PagoPremios, ImporteDecimos) " &
                               "VALUES (@IdAdmon, @Ejercicio, @IdSorteo, @NumJugado, @NumJugado2, @Fecha, @PagoAbono, @Modo, @ImportePagado, @PagoPremios, @ImporteDecimos)"

        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@IdAdmon", fila("IdAdmon"))
            cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
            cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
            cmd.Parameters.AddWithValue("@NumJugado", fila("NumJugado"))
            cmd.Parameters.AddWithValue("@NumJugado2", fila("NumJugado2"))
            cmd.Parameters.AddWithValue("@Fecha", fila("Fecha"))
            cmd.Parameters.AddWithValue("@PagoAbono", fila("PagoAbono"))
            cmd.Parameters.AddWithValue("@Modo", fila("Modo"))
            cmd.Parameters.AddWithValue("@ImportePagado", fila("ImportePagado"))
            cmd.Parameters.AddWithValue("@PagoPremios", fila("PagoPremios"))
            cmd.Parameters.AddWithValue("@ImporteDecimos", fila("ImporteDecimos"))
            cmd.ExecuteNonQuery()
        End Using
    End Sub


End Class
