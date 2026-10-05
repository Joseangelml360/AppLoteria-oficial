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

    Public Function ObtenerResumenSorteoVendor(idsorteo, codigovendor, ejercicio)
        Return datosVentas.GetResumenSorteoVendor(idsorteo, codigovendor, ejercicio)
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

End Class
