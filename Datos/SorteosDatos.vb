Imports MySql.Data.MySqlClient
Imports System.Data

Public Class SorteosDatos
    Public Property SorteosEntidad As Object

    Public Function ObtenerSorteos() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdSorteo, Ejercicio, TipoSorteo, Fecha, IdAdmon, NumeroJugado, NumeroJugado2,ImporteDecimo, " &
                               "ImportePapeleta, PapeletasImpresas, PapeletasEntregadas, PapeletasDevueltas, LoteriaJugada, " &
                               "DecimosJugados, ImporteTotal, JuegaEmpresa, ImportePremioPapeleta, ImportePagado, " &
                               "ImportePremioNum1, ImportePremioNum2 FROM sorteos"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al descargar sorteos: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return stabla
    End Function

    Public Function GetSorteoEjercicio(ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT * FROM sorteos WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al filtrar sorteos: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return stabla
    End Function

    Public Sub InsertarSorteo(fila As DataRow)
        Dim query As String = "INSERT INTO sorteos (IdSorteo, Ejercicio, TipoSorteo, Fecha, IdAdmon, NumeroJugado, NumeroJugado2," &
                               "ImporteDecimo, ImportePapeleta, PapeletasImpresas, PapeletasEntregadas, PapeletasDevueltas, " &
                               "LoteriaJugada, DecimosJugados, ImporteTotal, JuegaEmpresa, ImportePremioPapeleta, ImportePagado, " &
                               "ImportePremioNum1, ImportePremioNum2) " &
                               "VALUES (@IdSorteo, @Ejercicio, @TipoSorteo, @Fecha, @IdAdmon, @NumeroJugado, @NumeroJugado2, " &
                               "@ImporteDecimo, @ImportePapeleta, @PapeletasImpresas, @PapeletasEntregadas, @PapeletasDevueltas, " &
                               "@LoteriaJugada, @DecimosJugados, @ImporteTotal, @JuegaEmpresa, @ImportePremioPapeleta, @ImportePagado, " &
                               "@ImportePremioNum1, @ImportePremioNum2)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@TipoSorteo", fila("TipoSorteo"))
                cmd.Parameters.AddWithValue("@Fecha", fila("Fecha"))
                cmd.Parameters.AddWithValue("@IdAdmon", fila("IdAdmon"))
                cmd.Parameters.AddWithValue("@NumeroJugado", fila("NumeroJugado"))
                cmd.Parameters.AddWithValue("@NumeroJugado2", fila("NumeroJugado2"))
                cmd.Parameters.AddWithValue("@ImporteDecimo", fila("ImporteDecimo"))
                cmd.Parameters.AddWithValue("@ImportePapeleta", fila("ImportePapeleta"))
                cmd.Parameters.AddWithValue("@PapeletasImpresas", fila("PapeletasImpresas"))
                cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
                cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
                cmd.Parameters.AddWithValue("@LoteriaJugada", fila("LoteriaJugada"))
                cmd.Parameters.AddWithValue("@DecimosJugados", fila("DecimosJugados"))
                cmd.Parameters.AddWithValue("@ImporteTotal", fila("ImporteTotal"))
                cmd.Parameters.AddWithValue("@JuegaEmpresa", fila("JuegaEmpresa"))
                cmd.Parameters.AddWithValue("@ImportePremioPapeleta", fila("ImportePremioPapeleta"))
                cmd.Parameters.AddWithValue("@ImportePagado", fila("ImportePagado"))
                cmd.Parameters.AddWithValue("@ImportePremioNum1", fila("ImportePremioNum1"))
                cmd.Parameters.AddWithValue("@ImportePremioNum2", fila("ImportePremioNum2"))
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarSorteo(fila As DataRow)
        Dim query As String = "UPDATE sorteos SET PapeletasEntregadas = @PapeletasEntregadas, " &
                               "PapeletasDevueltas = @PapeletasDevueltas, LoteriaJugada = @LoteriaJugada, " &
                               "DecimosJugados = @DecimosJugados, ImporteTotal = @ImporteTotal, " &
                               "JuegaEmpresa = @JuegaEmpresa, ImportePremioPapeleta = @ImportePremioPapeleta, " &
                               "ImportePagado=@ImportePagado, " &
                               "NumeroJugado=@NumeroJugado, NumeroJugado2 = @NumeroJugado2, ImportePremioNum1 = @ImportePremioNum1, " &
                               "ImportePremioNum2 = @ImportePremioNum2 WHERE IdSorteo = @IdSorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
                cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
                cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
                cmd.Parameters.AddWithValue("@LoteriaJugada", fila("LoteriaJugada"))
                cmd.Parameters.AddWithValue("@DecimosJugados", fila("DecimosJugados"))
                cmd.Parameters.AddWithValue("@ImporteTotal", fila("ImporteTotal"))
                cmd.Parameters.AddWithValue("@JuegaEmpresa", fila("JuegaEmpresa"))
                cmd.Parameters.AddWithValue("@ImportePremioPapeleta", fila("ImportePremioPapeleta"))
                cmd.Parameters.AddWithValue("@ImportePagado", fila("ImportePagado"))
                cmd.Parameters.AddWithValue("@NumeroJugado", fila("NumeroJugado"))
                cmd.Parameters.AddWithValue("@NumeroJugado2", fila("NumeroJugado2"))
                cmd.Parameters.AddWithValue("@ImportePremioNum1", fila("ImportePremioNum1"))
                cmd.Parameters.AddWithValue("@ImportePremioNum2", fila("ImportePremioNum2"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al editar sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub ActualizarPagado(idsorteo As Integer, pagado As Double)
        Dim query As String = "UPDATE sorteos SET ImportePagado = @pagado WHERE IdSorteo = @idsorteo"
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
                cmd.Parameters.AddWithValue("@pagado", pagado)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al actualizar pago del sorteo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' 📊 OPTIMIZACIÓN ESCALAR: Funciones de lectura rápida sin DataReader
    Private Function ExecEscalar(query As String, ej As Integer, id As Integer) As Double
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ej)
                cmd.Parameters.AddWithValue("@idsorteo", id)
                Try
                    conn.Open()
                    Dim res As Object = cmd.ExecuteScalar()
                    If res Is Nothing OrElse Convert.IsDBNull(res) Then Return 0
                    Return Convert.ToDouble(res)
                Catch ex As Exception
                    Throw New Exception("Error escalar en sorteos: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ImporteTotal(ejercicio As Integer, idsorteo As Integer) As Double
        Return ExecEscalar("SELECT ImporteTotal FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function ImportePagadoSorteo(ejercicio As Integer, idsorteo As Integer) As Double
        Return ExecEscalar("SELECT ImportePagado FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function ImportePagadoTotal(ejercicio As Integer) As Double
        Return ExecEscalar("SELECT SUM(ImportePagado) FROM sorteos WHERE Ejercicio=@ejercicio", ejercicio, 0)
    End Function

    Public Function ImportePptaPremio(idsorteo As Integer, ejercicio As Integer) As Double
        Return ExecEscalar("SELECT ImportePremioPapeleta FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function ImporteDecimo(idsorteo As Integer, ejercicio As Integer) As Double
        Return ExecEscalar("SELECT ImporteDecimo, ImportePapeleta, ImportePremioPapeleta, NumeroJugado, 
                               NumeroJugado2, ImportePremioNum1, ImportePremioNum2 FROM sorteos 
                            WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function DecimosComprados(idsorteo As Integer, ejercicio As Integer) As Double
        Return ExecEscalar("SELECT DecimosJugados FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function ImportePpta(idsorteo As Integer, ejercicio As Integer) As Double
        Return ExecEscalar("SELECT ImportePapeleta FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function
    Public Function ImportePapeleta(idsorteo As Integer, ejercicio As Integer) As Double
        Return ExecEscalar("SELECT ImportePapeleta FROM sorteos WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function
    Public Function JugadoComisionEjercicio(ejercicio As Integer) As Double
        Return ExecEscalar("SELECT SUM(JuegaEmpresa) FROM sorteos WHERE Ejercicio=@ejercicio", ejercicio, 0)
    End Function

    ' Sobrecarga transaccional para actualizar el acumulado del sorteo
    Public Sub ActualizarPagadoTransaccional(idsorteo As Integer, pagado As Double, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "UPDATE sorteos SET ImportePagado = @pagado WHERE IdSorteo = @idsorteo"
        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@idsorteo", idsorteo)
            cmd.Parameters.AddWithValue("@pagado", pagado)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub EditarSorteoTransaccional(fila As DataRow, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "UPDATE sorteos SET PapeletasEntregadas = @PapeletasEntregadas, " &
                               "PapeletasDevueltas = @PapeletasDevueltas, LoteriaJugada = @LoteriaJugada, " &
                               "DecimosJugados = @DecimosJugados, ImporteTotal = @ImporteTotal, " &
                               "JuegaEmpresa = @JuegaEmpresa, ImportePremioPapeleta = @ImportePremioPapeleta " &
                               "WHERE IdSorteo = @IdSorteo"

        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
            cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
            cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
            cmd.Parameters.AddWithValue("@LoteriaJugada", fila("LoteriaJugada"))
            cmd.Parameters.AddWithValue("@DecimosJugados", fila("DecimosJugados"))
            cmd.Parameters.AddWithValue("@ImporteTotal", fila("ImporteTotal"))
            cmd.Parameters.AddWithValue("@JuegaEmpresa", fila("JuegaEmpresa"))
            cmd.Parameters.AddWithValue("@ImportePremioPapeleta", fila("ImportePremioPapeleta"))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class
