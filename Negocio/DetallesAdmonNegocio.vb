Imports Datos
Imports Entidades
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class DetallesAdmonNegocio

    Private ReadOnly datosAdmon As New DetallesAdmonDatos()
    Private ReadOnly datosLibro As New LibroDiarioDatos()
    Private ReadOnly datosSorteo As New SorteosDatos()

    Public Function GetDetallesAdmon()
        Return datosAdmon.ObtenerDetallesAdmon()
    End Function

    Public Sub GuardarDetallesAdmon(idAdmon As Integer, ejercicio As Integer,
                                    idSorteo As Integer, numJugado As Integer, numjugado2 As Integer,
                                    fecha As Date, pagoAbono As String, modo As String, importe As Double,
                                    pagoPremios As Double, importedecimos As Double)

        Dim stabla As DataTable = DetallesAdmonEntidad.TablaDetallesAdmon
        Dim fila As DataRow = stabla.NewRow

        'fila("Id") = id
        fila("IdAdmon") = idAdmon
        fila("Ejercicio") = ejercicio
        fila("IdSorteo") = idSorteo
        fila("NumJugado") = numJugado
        fila("NumJugado2") = numjugado2
        fila("Fecha") = fecha
        fila("PagoAbono") = pagoAbono
        fila("Modo") = modo
        fila("ImportePagado") = importe
        fila("PagoPremios") = pagoPremios
        fila("ImporteDecimos") = importedecimos
        stabla.Rows.Add(fila)
        datosAdmon.InsertarDetallesAdmon(fila)

    End Sub

    Public Function GetTotalPagadoSorteo(idsorteo As Integer)
        Return datosAdmon.TotalPagadoSorteo(idsorteo)
    End Function

    Public Function GetTotalPagadoAdmon(ejercicio As Integer)
        Return datosAdmon.TotalPagadoAdmon(ejercicio)
    End Function

    Public Function GetImporteDecimos(ejercicio As Integer, idsorteo As Integer)
        Return datosAdmon.ImporteDecimosSorteo(ejercicio, idsorteo)
    End Function

    Public Function GetImporteDecimosEjercicio(ejercicio As Integer)
        Return datosAdmon.ImporteDecimosEjercicio(ejercicio)
    End Function

    Public Function GetPremiosabonadoEjercicio(ejercicio As Integer)
        Return datosAdmon.AbonadoPremiosEjercicio(ejercicio)
    End Function

    Public Function GetPremiosAbonadoSorteo(ejercicio As Integer, idsorteo As Integer)
        Return datosAdmon.ABonadoPremiosSorteo(ejercicio, idsorteo)
    End Function

    Public Sub RegistrarMovimientoYLibroDiario(idAdmon As Integer, ejercicio As Integer, idSorteo As Integer,
                                            numJugado As Integer, numJugado2 As Integer, fecha As Date,
                                            pagoAbono As String, modo As String, importePago As Double,
                                            pagoPremios As Double, importeDecimos As Double)

        ' 1. La capa de negocio abre el único canal de comunicación con Aiven
        Using conn As MySqlConnection = ConexionDatos.GetLoteriaConexion()
            Dim transaccion As MySqlTransaction = Nothing

            Try
                conn.Open()
                ' Iniciamos el paraguas transaccional
                transaccion = conn.BeginTransaction()

                ' =========================================================================
                ' PASO A: Empaquetamos la fila del detalle y llamamos a DATOS
                ' =========================================================================
                Dim tablaAdmon As DataTable = Entidades.DetallesAdmonEntidad.TablaDetallesAdmon()
                Dim filaAdmon As DataRow = tablaAdmon.NewRow()
                filaAdmon("IdAdmon") = idAdmon
                filaAdmon("Ejercicio") = ejercicio
                filaAdmon("IdSorteo") = idSorteo
                filaAdmon("NumJugado") = numJugado
                filaAdmon("NumJugado2") = numJugado2
                filaAdmon("Fecha") = fecha
                filaAdmon("PagoAbono") = pagoAbono
                filaAdmon("Modo") = modo
                filaAdmon("ImportePagado") = Math.Round(importePago, 2)
                filaAdmon("PagoPremios") = Math.Round(pagoPremios, 2)
                filaAdmon("ImporteDecimos") = Math.Round(importeDecimos, 2)

                ' 🔥 DATOS EJECUTA EL SQL: Le pasamos la fila, la conexión y la transacción
                datosAdmon.InsertarDetallesAdmonTransaccional(filaAdmon, conn, transaccion)

                ' =========================================================================
                ' PASO B: Calculamos las matemáticas de la caja y llamamos a DATOS
                ' =========================================================================
                ' Recuperamos el saldo vivo actual usando tu función optimizada de Datos
                Dim saldoActual As Double = datosLibro.GetDebeHaberLibroDiario(ejercicio)

                Dim debe, haber, importeLibro As Double
                Dim entradaSalida, cajaBanco, concepto As String

                If pagoAbono = "P" Then
                    entradaSalida = "S"
                    concepto = "Pago loteria a admon " & idAdmon & ", sorteo " & idSorteo
                    debe = importePago : haber = 0
                    saldoActual -= importePago
                    importeLibro = importePago
                Else
                    entradaSalida = "E"
                    concepto = "Abono admon " & idAdmon & " de premio lotería, sorteo " & idSorteo
                    debe = 0 : haber = pagoPremios
                    saldoActual += pagoPremios
                    importeLibro = pagoPremios
                End If

                cajaBanco = If(modo = "T", "B", "C")

                Dim tablaLibro As DataTable = Entidades.LibroDiarioEntidad.TablaLibroDiario()
                Dim filaLibro As DataRow = tablaLibro.NewRow()
                filaLibro("Ejercicio") = ejercicio
                filaLibro("Fecha") = fecha
                filaLibro("EntradaSalida") = entradaSalida
                filaLibro("Importe") = Math.Round(importeLibro, 2)
                filaLibro("CajaBanco") = cajaBanco
                filaLibro("Concepto") = concepto
                filaLibro("Debe") = Math.Round(debe, 2)
                filaLibro("Haber") = Math.Round(haber, 2)
                filaLibro("Saldo") = Math.Round(saldoActual, 2)

                ' 🔥 DATOS EJECUTA EL SQL del Libro Diario
                datosLibro.InsertarApunteTransaccional(filaLibro, conn, transaccion)

                ' =========================================================================
                ' PASO C: Calculamos el nuevo acumulado del sorteo y llamamos a DATOS
                ' =========================================================================
                Dim pagadoSorteoActual As Double = datosSorteo.ImportePagadoSorteo(ejercicio, idSorteo)
                Dim nuevoTotalPagado As Double = pagadoSorteoActual + importeLibro

                ' 🔥 DATOS EJECUTA EL UPDATE del Sorteo
                datosSorteo.ActualizarPagadoTransaccional(idSorteo, Math.Round(nuevoTotalPagado, 2), conn, transaccion)

                ' 🔥 ¡COMMIT!: Si la capa de datos procesó los 3 métodos con éxito, consolidamos en la nube
                transaccion.Commit()

            Catch ex As Exception
                ' Si algo falla a mitad de camino, la capa de Negocio ordena el aborto total
                If transaccion IsNot Nothing Then transaccion.Rollback()
                Throw New Exception("Operación contable rechazada por seguridad. Base de datos intacta: " & ex.Message)
            End Try
        End Using
    End Sub

End Class
