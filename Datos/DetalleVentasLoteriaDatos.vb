Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DetalleVentasLoteriaDatos
    Public Property DetalleVentasLoteriaEntidad As Object

    Public Function ObtenerDetalleVentasLoteria() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Id, Ejercicio, CodigoVendedor, FechaApunte, IdSorteo, TipoSorteo, " &
                               "ImportePapeleta, PapeletasEntregadas, PapeletasDevueltas, Liquidado, " &
                               "PapeletasPremiadas, ImportePremiadas FROM detallesventaloteria"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al obtener el detalle global de ventas: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Function ObtenerDetalleVentasLoteriaVendedor(ejercicio As Integer, codigo As Integer, idsorteo As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Id, Ejercicio, CodigoVendedor, FechaApunte, IdSorteo, TipoSorteo, " &
                               "ImportePapeleta, PapeletasEntregadas, PapeletasDevueltas, Liquidado, " &
                               "PapeletasPremiadas, ImportePremiadas FROM detallesventaloteria " &
                               "WHERE Ejercicio = @ejercicio AND CodigoVendedor = @codigo AND IdSorteo = @idsorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@codigo", codigo)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al obtener las ventas del vendedor en el sorteo: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Function GetResumenSorteoVendor(IdSorteo As Integer, CodigoVendedor As Integer, Ejercicio As Integer) As DataRow
        Dim stabla As New DataTable()
        Dim query As String = "SELECT SUM(PapeletasEntregadas) AS PapeletasEntregadas, " &
                              "SUM(PapeletasDevueltas) As PapeletasDevueltas, " &
                              "SUM(Liquidado) AS Liquidado, SUM(PapeletasPremiadas) AS PapeletasPremiadas " &
                              "FROM detallesventaloteria " &
                               "WHERE IdSorteo = @IdSorteo AND CodigoVendedor = @CodigoVendedor " &
                               "AND Ejercicio = @Ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdSorteo", IdSorteo)
                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor)
                cmd.Parameters.AddWithValue("@Ejercicio", Ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)

                        If stabla.Rows.Count > 0 AndAlso Not Convert.IsDBNull(stabla.Rows(0)(0)) Then
                            Return stabla.Rows(0)
                        End If
                    Catch ex As Exception
                        Throw New Exception("Error al calcular el resumen del sorteo para el vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return Nothing
    End Function

    Public Function GetResumenTotalVendor(CodigoVendedor As Integer, Ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdSorteo AS Sorteo, SUM(PapeletasEntregadas) AS Entregadas, " &
                                "SUM(PapeletasDevueltas) AS Devueltas, SUM(Liquidado) AS Liquidadas, SUM(PapeletasPremiadas) As Premiadas, " &
                                "SUM(ImportePremiadas) AS Premios, " &
                                "SUM(Liquidado) - (MAX(ImportePapeleta) * (SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas))) AS Saldo, " &
                                "SUM(PapeletasEntregadas - PapeletasDevueltas) * " &
                                "IF(SUM(PapeletasPremiadas) = 0, 0, SUM(ImportePremiadas) / SUM(PapeletasPremiadas)) " &
                                "- SUM(ImportePremiadas) AS Beneficio " &
                                "FROM detallesventaloteria WHERE Ejercicio = @Ejercicio AND CodigoVendedor= @CodigoVendedor GROUP BY IdSorteo"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor)
                cmd.Parameters.AddWithValue("@Ejercicio", Ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)

                        Return stabla
                    Catch ex As Exception
                        Throw New Exception("Error al calcular el resumen del sorteo para el vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return Nothing
    End Function

    Public Function GetPagosVendedor(CodigoVendedor As Integer, Ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdSorteo AS Sorteo, 
		SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas) As Entregadas,
		(SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas)) * MAX(ImportePapeleta) AS Adeudado,
        SUM(Liquidado) AS Liquidado,
        ((SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas)) * MAX(ImportePapeleta)) -SUM(Liquidado) AS Deuda
        FROM detallesventaloteria
        WHERE Ejercicio=@Ejercicio AND CodigoVendedor=@CodigoVendedor 
		GROUP BY IdSorteo
        HAVING Deuda > 0
        ORDER BY IdSorteo ASC"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor)
                cmd.Parameters.AddWithValue("@Ejercicio", Ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)

                        Return stabla
                    Catch ex As Exception
                        Throw New Exception("Error al calcular los pagos del vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return Nothing
    End Function

    Public Function GetPagosVendedorTransaccional(CodigoVendedor As Integer, Ejercicio As Integer, conn As MySqlConnection, transaccion As MySqlTransaction) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT IdSorteo AS Sorteo, " &
        "SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas) As Entregadas, " &
        "(SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas)) * MAX(ImportePapeleta) AS Adeudado, " &
        "SUM(Liquidado) AS Liquidado, " &
        "((SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas)) * MAX(ImportePapeleta)) - SUM(Liquidado) AS Deuda " &
        "FROM detallesventaloteria " &
        "WHERE Ejercicio=@Ejercicio AND CodigoVendedor=@CodigoVendedor " &
        "GROUP BY IdSorteo " &
        "HAVING Deuda > 0 " &
        "ORDER BY IdSorteo ASC"

        Using cmd As New MySqlCommand(query, conn, transaccion)
            cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor)
            cmd.Parameters.AddWithValue("@Ejercicio", Ejercicio)

            Using adapter As New MySqlDataAdapter(cmd)
                Try
                    adapter.Fill(stabla)
                    Return stabla
                Catch ex As Exception
                    Throw New Exception("Error transaccional al calcular los pagos del vendedor: " & ex.Message)
                End Try
            End Using
        End Using
    End Function



    Public Function GetResumenSorteo(IdSorteo As Integer, Ejercicio As Integer) As DataRow
        Dim stabla As New DataTable()
        Dim query As String = "SELECT SUM(PapeletasEntregadas) AS PapeletasEntregadas, " &
                              "SUM(PapeletasDevueltas) AS PapeletasDevueltas, SUM(Liquidado) AS Liquidado, " &
                               "MAX(ImportePapeleta) AS ImportePapeleta FROM detallesventaloteria " &
                               "WHERE IdSorteo = @IdSorteo AND Ejercicio = @Ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@IdSorteo", IdSorteo)
                cmd.Parameters.AddWithValue("@Ejercicio", Ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)

                        If stabla.Rows.Count > 0 AndAlso Not Convert.IsDBNull(stabla.Rows(0)(0)) Then
                            Return stabla.Rows(0)
                        End If
                    Catch ex As Exception
                        Throw New Exception("Error al obtener el resumen global del sorteo: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return Nothing
    End Function

    Public Sub InsertarDetalleVentasLoteria(fila As DataRow)
        Dim query As String = "INSERT INTO detallesventaloteria (Ejercicio, CodigoVendedor, FechaApunte, IdSorteo, " &
                               "TipoSorteo, ImportePapeleta, PapeletasEntregadas, PapeletasDevueltas, Liquidado, " &
                               "PapeletasPremiadas, ImportePremiadas) " &
                               "VALUES (@Ejercicio, @CodigoVendedor, @FechaApunte, @IdSorteo, @TipoSorteo, " &
                               "@ImportePapeleta, @PapeletasEntregadas, @PapeletasDevueltas, @Liquidado, " &
                               "@PapeletasPremiadas, @ImportePremiadas)"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@CodigoVendedor", fila("CodigoVendedor"))
                cmd.Parameters.AddWithValue("@FechaApunte", fila("FechaApunte"))
                cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
                cmd.Parameters.AddWithValue("@TipoSorteo", fila("TipoSorteo"))
                cmd.Parameters.AddWithValue("@ImportePapeleta", fila("ImportePapeleta"))
                cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
                cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
                cmd.Parameters.AddWithValue("@Liquidado", fila("Liquidado"))
                cmd.Parameters.AddWithValue("@PapeletasPremiadas", fila("PapeletasPremiadas"))
                cmd.Parameters.AddWithValue("@ImportePremiadas", fila("ImportePremiadas"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar el detalle de venta de lotería: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EliminarDetalleVentasLoteria(fila As DataRow)
        Dim query As String = "DELETE FROM detallesventaloteria WHERE Id = @Id"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", fila("Id"))
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al eliminar el detalle de venta de lotería: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


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
                    Throw New Exception("Error escalar en detalles de venta de lotería: " & ex.Message)
                End Try
            End Using
        End Using
    End Function
    Public Function PremiosAbonadosEjercicio(ejercicio)
        Return ExecEscalar("SELECT SUM(ImportePremiadas) FROM detallesventaloteria WHERE Ejercicio=@ejercicio", ejercicio, 0)
    End Function
    Public Function PremiosAbonados(idsorteo, ejercicio)
        Return ExecEscalar("SELECT SUM(ImportePremiadas) FROM detallesventaloteria WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function
    Public Function LiquidadoEjercicio(ejercicio, idsorteo)
        Return ExecEscalar("SELECT SUM(Liquidado) FROM detallesventaloteria WHERE Ejercicio=@ejercicio AND IdSorteo=@idsorteo", ejercicio, idsorteo)
    End Function

    Public Function ObtenerTotalesVentasLoteriaSorteo(ejercicio As Integer, idsorteo As Integer) As DataTable
        Dim stabla As New DataTable()

        ' 🔥 REPARACIÓN CRÍTICA: Parametrizamos el WHERE y aplicamos MAX() a los campos 
        ' informativos para garantizar compatibilidad total con el modo estricto de MySQL 8 en Aiven
        Dim query As String = "SELECT MAX(Id) As Id, Ejercicio, CodigoVendedor, MAX(FechaApunte) As FechaApunte, " &
                               "IdSorteo, MAX(TipoSorteo) As TipoSorteo, MAX(ImportePapeleta) As ImportePapeleta, " &
                               "SUM(PapeletasEntregadas) As PapeletasEntregadas, SUM(PapeletasDevueltas) As PapeletasDevueltas, " &
                               "SUM(Liquidado) As Liquidado, SUM(PapeletasPremiadas) As PapeletasPremiadas, " &
                               "SUM(ImportePremiadas) As ImportePremiadas " &
                               "FROM detallesventaloteria " &
                               "WHERE Ejercicio = @ejercicio AND IdSorteo = @idsorteo " &
                               "GROUP BY CodigoVendedor, Ejercicio, IdSorteo"

        ' La conexión nace y muere dentro del bloque Using, protegiendo el plan de Aiven
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                cmd.Parameters.AddWithValue("@idsorteo", idsorteo)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        ' El adapter mapea las columnas automáticas del SUM() directo al DataTable
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al calcular los totales de venta por vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Sub InsertarDetalleVentasTransaccional(fila As DataRow, conn As MySqlConnection, trans As MySqlTransaction)
        Dim query As String = "INSERT INTO detallesventaloteria (Ejercicio, CodigoVendedor, FechaApunte, IdSorteo, TipoSorteo, " &
                               "ImportePapeleta, PapeletasEntregadas, PapeletasDevueltas, Liquidado, PapeletasPremiadas, ImportePremiadas) " &
                               "VALUES (@Ejercicio, @CodigoVendedor, @FechaApunte, @IdSorteo, @TipoSorteo, " &
                               "@ImportePapeleta, @PapeletasEntregadas, @PapeletasDevueltas, @Liquidado, @PapeletasPremiadas, @ImportePremiadas)"

        Using cmd As New MySqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
            cmd.Parameters.AddWithValue("@CodigoVendedor", fila("CodigoVendedor"))
            cmd.Parameters.AddWithValue("@FechaApunte", fila("FechaApunte"))
            cmd.Parameters.AddWithValue("@IdSorteo", fila("IdSorteo"))
            cmd.Parameters.AddWithValue("@TipoSorteo", fila("TipoSorteo"))
            cmd.Parameters.AddWithValue("@ImportePapeleta", fila("ImportePapeleta"))
            cmd.Parameters.AddWithValue("@PapeletasEntregadas", fila("PapeletasEntregadas"))
            cmd.Parameters.AddWithValue("@PapeletasDevueltas", fila("PapeletasDevueltas"))
            cmd.Parameters.AddWithValue("@Liquidado", fila("Liquidado"))
            cmd.Parameters.AddWithValue("@PapeletasPremiadas", fila("PapeletasPremiadas"))
            cmd.Parameters.AddWithValue("@ImportePremiadas", fila("ImportePremiadas"))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Function ObtenerLiquidacionFlexible(ejercicio As Integer, idVendedor As Integer) As DataTable
        Dim dt As New DataTable()
        Dim query As String = ""

        ' 🔥 EL MOTOR DINÁMICO: Cambiamos las agrupaciones según la directiva del formulario
        If idVendedor = 0 Then
            ' CASO A: Liquidación GLOBAL por Sorteo a lo largo del año activo
            query = "SELECT IdSorteo AS sorteo, " &
                    "SUM(PapeletasEntregadas) AS entregadas, " &
                    "SUM(PapeletasDevueltas) AS devueltas, " &
                    "SUM(Liquidado) AS Liquidadas, " &
                    "SUM(PapeletasPremiadas) AS premiadas, " &
                    "SUM(ImportePremiadas) AS premios, " &
                    "SUM(Liquidado) - (MAX(ImportePapeleta) * (SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas))) AS Saldo, " &
                    "SUM(PapeletasEntregadas - PapeletasDevueltas) * IF(SUM(PapeletasPremiadas) = 0, 0, SUM(ImportePremiadas) / SUM(PapeletasPremiadas)) - SUM(ImportePremiadas) AS beneficio " &
                    "FROM detallesventaloteria " &
                    "WHERE Ejercicio = @ejercicio " &
                    "GROUP BY IdSorteo"
        Else
            ' CASO B: Resumen de UN VENDEDOR específico a lo largo de todos sus sorteos
            query = "SELECT IdSorteo AS sorteo, " &
                    "SUM(PapeletasEntregadas) AS entregadas, " &
                    "SUM(PapeletasDevueltas) AS devueltas, " &
                    "SUM(Liquidado) AS Liquidadas, " &
                    "SUM(PapeletasPremiadas) AS premiadas, " &
                    "SUM(ImportePremiadas) AS premios, " &
                    "SUM(Liquidado) - (MAX(ImportePapeleta) * (SUM(PapeletasEntregadas) - SUM(PapeletasDevueltas))) AS Saldo, " &
                    "SUM(PapeletasEntregadas - PapeletasDevueltas) * IF(SUM(PapeletasPremiadas) = 0, 0, SUM(ImportePremiadas) / SUM(PapeletasPremiadas)) - SUM(ImportePremiadas) AS beneficio " &
                    "FROM detallesventaloteria " &
                    "WHERE Ejercicio = @ejercicio AND CodigoVendedor = @vendedor " &
                    "GROUP BY IdSorteo"
        End If

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                If idVendedor > 0 Then cmd.Parameters.AddWithValue("@vendedor", idVendedor)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(dt)
                    Catch ex As Exception
                        Throw New Exception("Error al extraer Resumen total ejercicio/vendedor: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
        Return dt
    End Function

End Class


