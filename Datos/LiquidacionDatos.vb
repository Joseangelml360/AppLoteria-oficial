Imports MySql.Data.MySqlClient
Imports System.Data

Public Class LiquidacionDatos
    Public Property LiquidacionEntidad As Object

    Public Function ObtenerLiquidacion() As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Comprado, Vendido, Premios, AbonoPremios, ACuotas, ABeneficiosSocio, JugadoEmpresa, Saldo, BeneficioEmpresa FROM liquidacion"

        ' La conexión nace y muere aquí, liberando al instante los recursos en la nube
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al obtener el histórico de liquidaciones desde Aiven: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function

    Public Function GetLiquidacionFilter(ejercicio As Integer) As DataTable
        Dim stabla As New DataTable()
        Dim query As String = "SELECT Ejercicio, Comprado, Vendido, Premios, AbonoPremios, ACuotas, ABeneficiosSocio, JugadoEmpresa, Saldo, BeneficioEmpresa FROM liquidacion WHERE Ejercicio = @ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)

                Using adapter As New MySqlDataAdapter(cmd)
                    Try
                        conn.Open()
                        adapter.Fill(stabla)
                    Catch ex As Exception
                        Throw New Exception("Error al filtrar la liquidación del ejercicio activo: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using

        Return stabla
    End Function


    ' =========================================================================
    ' 1. CONSULTA DE SORTEOS (Trae Comprado, Vendido y JugadoEmpresa de un golpe)
    ' =========================================================================
    Public Function ObtenerTotalesSorteos(ejercicio As Integer) As DataRow
            Dim stabla As New DataTable()
            Dim query As String = "SELECT SUM(ImporteTotal) AS Comprado, " &
                                   "SUM(LoteriaJugada) AS Vendido, " &
                                   "SUM(JuegaEmpresa) AS JugadoEmpresa " &
                                   "FROM sorteos WHERE Ejercicio = @ejercicio"

            Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Using adapter As New MySqlDataAdapter(cmd)
                        Try
                            conn.Open()
                            adapter.Fill(stabla)
                            If stabla.Rows.Count > 0 Then Return stabla.Rows(0)
                        Catch ex As Exception
                            Throw New Exception("Error en totales de sorteos: " & ex.Message)
                        End Try
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' =========================================================================
        ' 2. CONSULTA DE ADMINISTRACIÓN (Trae Pagado y Premios de un golpe)
        ' =========================================================================
        Public Function ObtenerTotalesAdmon(ejercicio As Integer) As DataRow
            Dim stabla As New DataTable()
            Dim query As String = "SELECT SUM(ImportePagado) AS Pagado, " &
                                   "SUM(PagoPremios) AS Premios " &
                                   "FROM detallesadmon WHERE Ejercicio = @ejercicio"

            Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Using adapter As New MySqlDataAdapter(cmd)
                        Try
                            conn.Open()
                            adapter.Fill(stabla)
                            If stabla.Rows.Count > 0 Then Return stabla.Rows(0)
                        Catch ex As Exception
                            Throw New Exception("Error en totales de administración: " & ex.Message)
                        End Try
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' =========================================================================
        ' 3. CONSULTA DE DETALLES VENTAS (Trae VentasLoteria y AbonoPremios de un golpe)
        ' =========================================================================
        Public Function ObtenerTotalesVentasVendedores(ejercicio As Integer) As DataRow
            Dim stabla As New DataTable()
            Dim query As String = "SELECT SUM(Liquidado) AS VentasLoteria, " &
                                   "SUM(ImportePremiadas) AS AbonoPremios " &
                                   "FROM detallesventaloteria WHERE Ejercicio = @ejercicio"

            Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Using adapter As New MySqlDataAdapter(cmd)
                        Try
                            conn.Open()
                            adapter.Fill(stabla)
                            If stabla.Rows.Count > 0 Then Return stabla.Rows(0)
                        Catch ex As Exception
                            Throw New Exception("Error en totales de ventas vendedores: " & ex.Message)
                        End Try
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' =========================================================================
        ' 4. CONSULTA DE CUOTAS AMORTIZADAS (Retorno único rápido con ExecuteScalar)
        ' =========================================================================
        Public Function ObtenerTotalACuotas(ejercicio As Integer) As Double
            Dim query As String = "SELECT SUM(Acuota) FROM vendedores WHERE Ejercicio = @ejercicio"
            Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Try
                        conn.Open()
                        Dim res = cmd.ExecuteScalar()
                        If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then Return Convert.ToDouble(res)
                    Catch ex As Exception
                        Throw New Exception("Error en total a cuotas: " & ex.Message)
                    End Try
                End Using
            End Using
            Return 0
        End Function

    ' =========================================================================
    ' 5. CONSULTA DE BENEFICIOS ACUMULADOS SOCIOS (Retorno único rápido con ExecuteScalar)
    ' =========================================================================
    Public Function ObtenerTotalBeneficiosSocios(ejercicio As Integer) As Double
        Dim query As String = "SELECT SUM(Saldo) FROM vendedores WHERE Saldo > 0 AND Ejercicio = @ejercicio"
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ejercicio", ejercicio)
                Try
                    conn.Open()
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then Return Convert.ToDouble(res)
                Catch ex As Exception
                    Throw New Exception("Error en total beneficios socios: " & ex.Message)
                End Try
            End Using
        End Using
        Return 0
    End Function

    Public Sub InsertarLiquidacion(fila As DataRow)
        Dim query As String = "INSERT INTO liquidacion (Ejercicio, Comprado, Vendido, Premios, " &
                               "AbonoPremios, ACuotas, ABeneficiosSocio, JugadoEmpresa, Saldo, BeneficioEmpresa) " &
                               "VALUES (@Ejercicio, @Comprado, @Vendido, @Premios, " &
                               "@AbonoPremios, @ACuotas, @ABeneficiosSocio, @JugadoEmpresa, @Saldo, @BeneficioEmpresa)"

        ' La conexión nace y muere aquí de forma segura liberando recursos de red
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@Comprado", fila("Comprado"))
                cmd.Parameters.AddWithValue("@Vendido", fila("Vendido"))
                cmd.Parameters.AddWithValue("@Premios", fila("Premios"))
                cmd.Parameters.AddWithValue("@AbonoPremios", fila("AbonoPremios"))
                cmd.Parameters.AddWithValue("@ACuotas", fila("ACuotas"))
                cmd.Parameters.AddWithValue("@ABeneficiosSocio", fila("ABeneficiosSocio"))
                cmd.Parameters.AddWithValue("@JugadoEmpresa", fila("JugadoEmpresa"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))
                cmd.Parameters.AddWithValue("@BeneficioEmpresa", fila("BeneficioEmpresa"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al insertar la instantánea de liquidación: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub EditarLiquidacion(fila As DataRow)
        ' 🔥 FILTRO DE BÚSQUEDA: Buscamos por el año del Ejercicio (Llave Primaria)
        Dim query As String = "UPDATE liquidacion SET Comprado = @Comprado, Vendido = @Vendido, " &
                               "Premios = @Premios, AbonoPremios = @AbonoPremios, ACuotas = @ACuotas, " &
                               "ABeneficiosSocio = @ABeneficiosSocio, JugadoEmpresa = @JugadoEmpresa, " &
                               "Saldo = @Saldo, BeneficioEmpresa = @BeneficioEmpresa WHERE Ejercicio = @Ejercicio"

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Using cmd As New MySqlCommand(query, conn)
                ' Pasamos los parámetros contables para actualizar el renglón existente
                cmd.Parameters.AddWithValue("@Ejercicio", fila("Ejercicio"))
                cmd.Parameters.AddWithValue("@Comprado", fila("Comprado"))
                cmd.Parameters.AddWithValue("@Vendido", fila("Vendido"))
                cmd.Parameters.AddWithValue("@Premios", fila("Premios"))
                cmd.Parameters.AddWithValue("@AbonoPremios", fila("AbonoPremios"))
                cmd.Parameters.AddWithValue("@ACuotas", fila("ACuotas"))
                cmd.Parameters.AddWithValue("@ABeneficiosSocio", fila("ABeneficiosSocio"))
                cmd.Parameters.AddWithValue("@JugadoEmpresa", fila("JugadoEmpresa"))
                cmd.Parameters.AddWithValue("@Saldo", fila("Saldo"))
                cmd.Parameters.AddWithValue("@BeneficioEmpresa", fila("BeneficioEmpresa"))

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al actualizar la instantánea de liquidación: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

End Class



