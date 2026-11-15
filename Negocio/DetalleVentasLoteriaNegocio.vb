Imports Datos
Imports Entidades
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class DetalleVentasLoteriaNegocio
    Private ReadOnly datosAdmon As New DetallesAdmonDatos
    Private ReadOnly datosVentas As New DetalleVentasLoteriaDatos()
    Private ReadOnly datosVendor As New VendedoresDatos()
    Private ReadOnly datosLibro As New LibroDiarioDatos()
    Private ReadOnly datosSorteo As New SorteosDatos()

    Public Function GetDetallesVentaLoteria()
        Return datosVentas.ObtenerDetalleVentasLoteria()
    End Function

    Public Function GetDetallesVentaLoteriaSorteo(ejercicio, idsorteo)
        Return datosVentas.ObtenerTotalesVentasLoteriaSorteo(ejercicio, idsorteo)
    End Function

    Public Function GetDetallesVentaLoteriaVendor(ejercicio As Integer, codigo As Integer, idsorteo As Integer)
        Return datosVentas.ObtenerDetalleVentasLoteriaVendedor(ejercicio, codigo, idsorteo)
    End Function

    Public Function ObtenerResumenSorteoVendor(idsorteo As Integer, codigovendor As Integer, ejercicio As Integer)
        Return datosVentas.GetResumenSorteoVendor(idsorteo, codigovendor, ejercicio)
    End Function

    Public Function ObtenerResumenTotalVendor(codigo As Integer, ejercicio As Integer)
        Return datosVentas.ObtenerLiquidacionFlexible(ejercicio, codigo)
    End Function

    Public Function ObtenerPagosVendedor(codigo As Integer, ejercicio As Integer)
        Return datosVentas.GetPagosVendedor(codigo, ejercicio)
    End Function

    Public Function GetLiquidacionReporte(ejercicio As Integer, idVendedor As Integer) As DataTable
        ' Lógica limpia: delegamos directamente en la capa de datos parametrizada
        Return datosVentas.ObtenerLiquidacionFlexible(ejercicio, idVendedor)

    End Function


    Public Function ObtenerResumenSorteo(idsorteo As Integer, ejercicio As Integer)
        Return datosVentas.GetResumenSorteo(idsorteo, ejercicio)
    End Function

    Public Function ObtenerImportePremios(idsorteo, ejercicio)
        'Return datosVentas.PremiosAbonados(idsorteo, ejercicio)
        Return datosAdmon.CobradoPremiosSorteo(idsorteo, ejercicio)
    End Function

    Public Sub GuardarDetalleVentasLoteria(Id, Ejercicio, CodigoVendedor, FechaApunte, IdSorteo, TipoSorteo, ImportePpta,
                                                 PptasEntregadas, PptasDevueltas, liquidado, PapeletasPremiadas, ImportePremiadas)

        Dim stabla As DataTable = DetalleVentasLoteriaEntidad.TablaDetalleVentasLoteria
        Dim fila As DataRow = stabla.NewRow

        fila("Id") = Id
        fila("Ejercicio") = Ejercicio
        fila("CodigoVendedor") = CodigoVendedor
        fila("FechaApunte") = FechaApunte
        fila("IdSorteo") = IdSorteo
        fila("TipoSorteo") = TipoSorteo
        fila("ImportePapeleta") = ImportePpta
        fila("PapeletasEntregadas") = PptasEntregadas
        fila("PapeletasDevueltas") = PptasDevueltas
        fila("Liquidado") = liquidado
        fila("PapeletasPremiadas") = PapeletasPremiadas
        fila("ImportePremiadas") = ImportePremiadas

        stabla.Rows.Add(fila)
        datosVentas.InsertarDetalleVentasLoteria(fila)

    End Sub

    Public Sub ProcesarVentaYRecalculoAtomico(
    ejercicio As Integer,
    codigoVendedor As Integer,
    idSorteo As Integer,
    pptasEntregadas As Integer,
    pptasDevueltas As Integer,
    numPptasPremiadas As Integer,
    importePpta As Double,
    importeEntregado As Double,
    importePremiadas As Double,
    tipoSorteo As String,
    liquidado As Double,
    acuota As Double,
    saldo As Double,
    entradaSalida As String,
    concepto As String,
    cajaBanco As String,
    importeLibro As Double,
    debe As Double,
    haber As Double)

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Dim transaccion As MySqlTransaction = Nothing

            Try
                conn.Open()
                transaccion = conn.BeginTransaction()

                ' =============================================================
                ' PASO A: REGISTRAR EL DETALLE DE LA VENTA EN HISTÓRICO
                ' =============================================================
                Dim tablaVentas As DataTable =
                    Entidades.DetalleVentasLoteriaEntidad.TablaDetalleVentasLoteria()
                Dim filaVentas As DataRow = tablaVentas.NewRow()

                filaVentas("Ejercicio") = ejercicio
                filaVentas("CodigoVendedor") = codigoVendedor
                filaVentas("FechaApunte") = Today()
                filaVentas("IdSorteo") = idSorteo
                filaVentas("TipoSorteo") = tipoSorteo
                filaVentas("ImportePapeleta") = Math.Round(importePpta, 2)
                filaVentas("PapeletasEntregadas") = pptasEntregadas
                filaVentas("PapeletasDevueltas") = pptasDevueltas
                filaVentas("Liquidado") = Math.Round(importeEntregado, 2)
                filaVentas("PapeletasPremiadas") = numPptasPremiadas
                filaVentas("ImportePremiadas") = Math.Round(importePremiadas, 2)

                datosVentas.InsertarDetalleVentasTransaccional(
                    filaVentas, conn, transaccion)

                ' =============================================================
                ' PASO B: ACTUALIZAR LA FICHA DEL VENDEDOR EN VENDEDORES
                ' =============================================================
                datosVendor.EditarVendedorTransaccional(
                    Math.Round(liquidado, 2),
                    Math.Round(acuota, 2),
                    Math.Round(saldo, 2),
                    codigoVendedor,
                    ejercicio, conn, transaccion)

                ' =============================================================
                ' PASO C: REGISTRAR ASIENTO EN EL LIBRO DIARIO
                ' =============================================================
                If importeLibro > 0 Then
                    Dim saldoLibroActual As Double = 0
                    Dim querySaldo As String =
                        "SELECT SUM(Haber)-SUM(Debe) FROM librodiario WHERE Ejercicio = @ejercicio"

                    Using cmdSaldo As New MySqlCommand(querySaldo, conn, transaccion)
                        cmdSaldo.Parameters.AddWithValue("@ejercicio", ejercicio)
                        Dim resSaldo = cmdSaldo.ExecuteScalar()
                        If resSaldo IsNot Nothing AndAlso Not Convert.IsDBNull(resSaldo) Then
                            saldoLibroActual = Convert.ToDouble(resSaldo)
                        End If
                    End Using

                    If entradaSalida = "S" Then
                        saldoLibroActual -= importeLibro
                    ElseIf entradaSalida = "E" Then
                        saldoLibroActual += importeLibro
                    End If

                    Dim tablaLibro As DataTable =
                        Entidades.LibroDiarioEntidad.TablaLibroDiario()
                    Dim filaLibro As DataRow = tablaLibro.NewRow()
                    filaLibro("Ejercicio") = ejercicio
                    filaLibro("Fecha") = Today()
                    filaLibro("EntradaSalida") = entradaSalida
                    filaLibro("Importe") = Math.Round(importeLibro, 2)
                    filaLibro("CajaBanco") = cajaBanco
                    filaLibro("Concepto") = concepto
                    filaLibro("Debe") = Math.Round(debe, 2)
                    filaLibro("Haber") = Math.Round(haber, 2)
                    filaLibro("Saldo") = Math.Round(saldoLibroActual, 2)

                    datosLibro.InsertarApunteTransaccional(
                        filaLibro, conn, transaccion)
                End If

                ' =============================================================
                ' PASO D: ALGORITMO MATEMÁTICO DE RECALCULO GENERAL DEL SORTEO
                ' =============================================================
                Dim entregadasTotales, devueltasTotales As Integer
                Dim liquidadoTotal, importepapeletaBase As Double

                Dim queryResumen As String =
                    "SELECT SUM(PapeletasEntregadas), SUM(PapeletasDevueltas), " &
                    "SUM(Liquidado), MAX(ImportePapeleta) FROM detallesventaloteria " &
                    "WHERE IdSorteo = @idSorteo AND Ejercicio = @ejercicio"

                Using cmdRes As New MySqlCommand(queryResumen, conn, transaccion)
                    cmdRes.Parameters.AddWithValue("@idSorteo", idSorteo)
                    cmdRes.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Using dr = cmdRes.ExecuteReader()
                        If dr.Read() AndAlso Not Convert.IsDBNull(dr(0)) Then
                            entregadasTotales = Convert.ToInt32(dr(0))
                            devueltasTotales = Convert.ToInt32(dr(1))
                            liquidadoTotal = Convert.ToDouble(dr(2)) * 0.8
                            importepapeletaBase = Convert.ToDouble(dr(3))
                        End If
                    End Using
                End Using

                Dim importedecimo, importepptapremio As Double
                Dim num1, num2 As Integer
                Dim Premio1, Premio2 As Double

                Dim querySorteo As String =
                    "SELECT ImporteDecimo, NumeroJugado, NumeroJugado2, " &
                    "ImportePremioNum1, ImportePremioNum2 FROM sorteos " &
                    "WHERE IdSorteo = @idSorteo AND Ejercicio = @ejercicio"

                Using cmdSort As New MySqlCommand(querySorteo, conn, transaccion)
                    cmdSort.Parameters.AddWithValue("@idSorteo", idSorteo)
                    cmdSort.Parameters.AddWithValue("@ejercicio", ejercicio)
                    Using dr = cmdSort.ExecuteReader()
                        If dr.Read() Then
                            importedecimo = Val(dr("ImporteDecimo").ToString())
                            num1 = CInt(Val(dr("NumeroJugado").ToString()))
                            num2 = CInt(Val(dr("NumeroJugado2").ToString()))
                            Premio1 = Val(dr("ImportePremioNum1").ToString())
                            Premio2 = Val(dr("ImportePremioNum2").ToString())
                            importepptapremio = Premio1 + Premio2
                        End If
                    End Using
                End Using

                Dim pptasaventa As Integer = entregadasTotales - devueltasTotales
                Dim numdecimos As Integer = 0

                If importedecimo > 0 Then
                    numdecimos = CInt(Math.Ceiling(
                        pptasaventa * importepapeletaBase * 0.8 / importedecimo))
                End If

                Dim importetotalCalculado As Double = numdecimos * importedecimo
                Dim pptastotales As Double = 0

                If importepapeletaBase > 0 Then
                    pptastotales = importetotalCalculado / (importepapeletaBase * 0.8)
                End If

                Dim pptasempresa As Integer = CInt(Math.Floor(pptastotales - pptasaventa))
                Dim juegaempresa As Double = Math.Round(pptasempresa * importepapeletaBase * 0.8, 2)
                Dim loteriajugada As Double = pptastotales * importepapeletaBase

                Dim tablaSorteo As DataTable = Entidades.SorteosEntidad.TablaSorteos()
                Dim filaSorteo As DataRow = tablaSorteo.NewRow()

                filaSorteo("IdSorteo") = idSorteo
                filaSorteo("PapeletasEntregadas") = entregadasTotales
                filaSorteo("PapeletasDevueltas") = devueltasTotales
                filaSorteo("LoteriaJugada") = loteriajugada
                filaSorteo("DecimosJugados") = numdecimos
                filaSorteo("ImporteTotal") = importetotalCalculado
                filaSorteo("JuegaEmpresa") = juegaempresa
                filaSorteo("ImportePremioPapeleta") = importepptapremio

                datosSorteo.EditarSorteoTransaccional(filaSorteo, conn, transaccion)

                ' Consolidamos definitivamente en la nube si todo ha ido bien
                transaccion.Commit()

            Catch ex As Exception
                If transaccion IsNot Nothing Then transaccion.Rollback()
                Throw New Exception(
                    "Ráfaga contable rechazada. Sistema protegido: " & ex.Message)
            End Try
        End Using
    End Sub

    Public Sub ProcesarLiquidacionMasivaFIFO(
    ejercicio As Integer,
    codigoVendedor As Integer,
    importeTotalEntregado As Double,
    cajaBanco As String,
    conceptoPersonalizado As String)

        If importeTotalEntregado <= 0 Then Throw New ArgumentException("El importe entregado debe ser mayor que cero.")

        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Dim transaccion As MySqlTransaction = Nothing
            Try
                conn.Open()
                transaccion = conn.BeginTransaction()

                ' 1. Obtener deudas activas desde tu consulta SQL filtrada
                Dim dtDeudas As DataTable = datosVentas.GetPagosVendedorTransaccional(codigoVendedor, ejercicio, conn, transaccion)
                Dim bolsaEfectivo As Double = importeTotalEntregado
                Dim descripcionesSorteosAfectados As New List(Of String)()

                For Each filaDeuda As DataRow In dtDeudas.Rows
                    If bolsaEfectivo <= 0 Then Exit For

                    Dim idSorteo As Integer = Convert.ToInt32(filaDeuda("Sorteo"))
                    Dim tipoSorteo As String = "Sorteo Reg. " & idSorteo.ToString()
                    Dim adeudadoSorteo As Double = Convert.ToDouble(filaDeuda("Adeudado"))
                    Dim entregadasSorteo As Integer = Convert.ToInt32(filaDeuda("Entregadas"))
                    Dim importePpta As Double = If(entregadasSorteo > 0, adeudadoSorteo / entregadasSorteo, 0)
                    Dim saldoSorteo As Double = adeudadoSorteo - Convert.ToDouble(filaDeuda("Liquidado"))

                    Dim pagoAsignado As Double = Math.Min(saldoSorteo, bolsaEfectivo)
                    bolsaEfectivo -= pagoAsignado
                    descripcionesSorteosAfectados.Add($"{tipoSorteo} ({Math.Round(pagoAsignado, 2)}€)")

                    ' PASO A: Insertar en histórico con tu objeto datosVentas
                    Dim tablaVentas As DataTable = DetalleVentasLoteriaEntidad.TablaDetalleVentasLoteria()
                    Dim filaVentas As DataRow = tablaVentas.NewRow()
                    filaVentas("Ejercicio") = ejercicio
                    filaVentas("CodigoVendedor") = codigoVendedor
                    filaVentas("FechaApunte") = Today()
                    filaVentas("IdSorteo") = idSorteo
                    filaVentas("TipoSorteo") = tipoSorteo
                    filaVentas("ImportePapeleta") = Math.Round(importePpta, 2)
                    filaVentas("PapeletasEntregadas") = 0
                    filaVentas("PapeletasDevueltas") = 0
                    filaVentas("Liquidado") = Math.Round(pagoAsignado, 2)
                    filaVentas("PapeletasPremiadas") = 0
                    filaVentas("ImportePremiadas") = 0
                    datosVentas.InsertarDetalleVentasTransaccional(filaVentas, conn, transaccion)

                    ' PASO D: Invocar al sub de recálculo (Bloque 2)
                    RecalcularMetricasSorteoInterno(idSorteo, ejercicio, conn, transaccion)
                Next

                '' 2. Gestión segura de excedentes / anticipos si sobra dinero
                'If bolsaEfectivo > 0 Then
                '    Dim idSorteoActivo As Integer = datosSorteo.ObtenerIdSorteoActivo(ejercicio, conn, transaccion)
                '    Dim tipoSorteoActivo As String = datosSorteo.ObtenerTipoSorteo(idSorteoActivo, ejercicio, conn, transaccion)
                '    Dim importePptaActivo As Double = datosSorteo.ObtenerImportePapeleta(idSorteoActivo, ejercicio, conn, transaccion)
                '    descripcionesSorteosAfectados.Add($"{tipoSorteoActivo} [Anticipo: {Math.Round(bolsaEfectivo, 2)}€]")

                '    Dim tablaVentas As DataTable = datosVentas.ObtenerEstructuraDetalleVentas()
                '    Dim filaVentas As DataRow = tablaVentas.NewRow()
                '    filaVentas("Ejercicio") = ejercicio
                '    filaVentas("CodigoVendedor") = codigoVendedor
                '    filaVentas("FechaApunte") = Today()
                '    filaVentas("IdSorteo") = idSorteoActivo
                '    filaVentas("ImportePapeleta") = Math.Round(importePptaActivo, 2)
                '    filaVentas("PapeletasEntregadas") = 0
                '    filaVentas("PapeletasDevueltas") = 0
                '    filaVentas("Liquidado") = Math.Round(bolsaEfectivo, 2)
                '    filaVentas("PapeletasPremiadas") = 0
                '    filaVentas("ImportePremiadas") = 0
                '    datosVentas.InsertarDetalleVentasTransaccional(filaVentas, conn, transaccion)
                '    RecalcularMetricasSorteoInterno(idSorteoActivo, ejercicio, conn, transaccion)
                'End If

                ' =========================================================================
                ' 3. PASO B: ACTUALIZAR LA FICHA GENERAL CON ALGORITMO DE CUOTAS DE LA FALLA
                ' =========================================================================
                ' 1. Recuperamos los valores iniciales reales y fuertemente tipados desde la base de datos cloud
                Dim drVendor As DataRow = datosVendor.GetVendorEjercicioTransaccional(codigoVendedor, ejercicio, conn, transaccion)

                Dim liquidadoAcumulado As Double = Convert.ToDouble(drVendor("Liquidado"))
                Dim acuotaAcumulada As Double = Convert.ToDouble(drVendor("ACuota"))
                Dim saldoActual As Double = Convert.ToDouble(drVendor("Saldo"))
                Dim loteriaAsignada As Double = Convert.ToDouble(drVendor("LoteriaAsignada"))
                Dim beneficioAcumulado As Double = 0 ' Se asume que mapea con la columna correspondiente de beneficio si existiera por registro

                ' 2. Aplicamos la lógica exacta de umbral, pero usando el importe total de la entrega masiva
                liquidadoAcumulado += importeTotalEntregado

                If liquidadoAcumulado >= loteriaAsignada Then
                    ' Ha cubierto la lotería obligatoria: la cuota se congela en el tope del 20%
                    acuotaAcumulada = loteriaAsignada * 0.2

                    ' El excedente sobre la asignación genera un 10% de beneficio directo para el fallero
                    beneficioAcumulado = (liquidadoAcumulado - loteriaAsignada) * 0.1
                    saldoActual = beneficioAcumulado
                Else
                    ' No ha llegado al mínimo: sumamos el 20% del pago a su cuota y reducimos saldo deudor
                    acuotaAcumulada += (importeTotalEntregado * 0.2)
                    saldoActual += (importeTotalEntregado * 0.2)
                End If

                ' 3. Consolidamos los datos calculados de forma atómica en la tabla 'vendedores'
                datosVendor.EditarVendedorTransaccional(
                Math.Round(liquidadoAcumulado, 2),
                Math.Round(acuotaAcumulada, 2),
                Math.Round(saldoActual, 2),
                codigoVendedor,
                ejercicio, conn, transaccion)


                ' 4. PASO C: Un solo apunte agrupado en el libro diario con datosLibro
                Dim saldoLibroActual As Double = datosLibro.GetDebeHaberLibroDiarioTransaccional(ejercicio, conn, transaccion)
                saldoLibroActual += importeTotalEntregado
                Dim conceptoFinal As String = conceptoPersonalizado & " -> Vendedor: " & codigoVendedor
                If conceptoFinal.Length > 255 Then conceptoFinal = conceptoFinal.Substring(0, 252) & "..."

                Dim tablaLibro As DataTable = LibroDiarioEntidad.TablaLibroDiario()
                Dim filaLibro As DataRow = tablaLibro.NewRow()
                filaLibro("Ejercicio") = ejercicio
                filaLibro("Fecha") = Today()
                filaLibro("EntradaSalida") = "E"
                filaLibro("Importe") = Math.Round(importeTotalEntregado, 2)
                filaLibro("CajaBanco") = cajaBanco
                filaLibro("Concepto") = conceptoFinal
                filaLibro("Debe") = 0
                filaLibro("Haber") = Math.Round(importeTotalEntregado, 2)
                filaLibro("Saldo") = Math.Round(saldoLibroActual, 2)
                datosLibro.InsertarApunteTransaccional(filaLibro, conn, transaccion)

                transaccion.Commit()
            Catch ex As Exception
                If transaccion IsNot Nothing Then transaccion.Rollback()
                Throw New Exception("La liquidación masiva FIFO ha fallado de forma segura. Motivo: " & ex.Message, ex)
            End Try
        End Using
    End Sub

    Private Sub RecalcularMetricasSorteoInterno(idSorteo As Integer, ejercicio As Integer, conn As MySqlConnection, trans As MySqlTransaction)
        Dim entregadasTotales, devueltasTotales As Integer
        Dim liquidadoTotal, importepapeletaBase As Double

        Dim queryResumen As String =
        "SELECT SUM(PapeletasEntregadas), SUM(PapeletasDevueltas), " &
        "SUM(Liquidado), MAX(ImportePapeleta) FROM detallesventaloteria " &
        "WHERE IdSorteo = @idSorteo AND Ejercicio = @ejercicio"

        Using cmdRes As New MySqlCommand(queryResumen, conn, trans)
            cmdRes.Parameters.AddWithValue("@idSorteo", idSorteo)
            cmdRes.Parameters.AddWithValue("@ejercicio", ejercicio)
            Using dr = cmdRes.ExecuteReader()
                If dr.Read() AndAlso Not Convert.IsDBNull(dr(0)) Then
                    entregadasTotales = Convert.ToInt32(dr(0))
                    devueltasTotales = Convert.ToInt32(dr(1))
                    liquidadoTotal = Convert.ToDouble(dr(2)) * 0.8
                    importepapeletaBase = Convert.ToDouble(dr(3))
                End If
            End Using
        End Using

        Dim importedecimo, importepptapremio As Double
        Dim num1, num2 As Integer
        Dim Premio1, Premio2 As Double

        Dim querySorteo As String =
        "SELECT ImporteDecimo, NumeroJugado, NumeroJugado2, " &
        "ImportePremioNum1, ImportePremioNum2 FROM sorteos " &
        "WHERE IdSorteo = @idSorteo AND Ejercicio = @ejercicio"

        Using cmdSort As New MySqlCommand(querySorteo, conn, trans)
            cmdSort.Parameters.AddWithValue("@idSorteo", idSorteo)
            cmdSort.Parameters.AddWithValue("@ejercicio", ejercicio)
            Using dr = cmdSort.ExecuteReader()
                If dr.Read() Then
                    importedecimo = Val(dr("ImporteDecimo").ToString())
                    num1 = CInt(Val(dr("NumeroJugado").ToString()))
                    num2 = CInt(Val(dr("NumeroJugado2").ToString()))
                    Premio1 = Val(dr("ImportePremioNum1").ToString())
                    Premio2 = Val(dr("ImportePremioNum2").ToString())
                    importepptapremio = Premio1 + Premio2
                End If
            End Using
        End Using

        Dim pptasaventa As Integer = entregadasTotales - devueltasTotales
        Dim numdecimos As Integer = 0

        If importedecimo > 0 Then
            numdecimos = CInt(Math.Ceiling(pptasaventa * importepapeletaBase * 0.8 / importedecimo))
        End If

        Dim importetotalCalculado As Double = numdecimos * importedecimo
        Dim pptastotales As Double = 0

        If importepapeletaBase > 0 Then
            pptastotales = importetotalCalculado / (importepapeletaBase * 0.8)
        End If

        Dim pptasempresa As Integer = CInt(Math.Floor(pptastotales - pptasaventa))
        Dim juegaempresa As Double = Math.Round(pptasempresa * importepapeletaBase * 0.8, 2)

        ' Aquí puedes añadir las líneas finales con tu consulta de UPDATE sobre la tabla 'sorteos'
    End Sub



End Class
