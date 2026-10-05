Imports Datos
Imports System.Data

Public Class RecalcularSorteosNegocio
    Public ReadOnly datosventas As New DetalleVentasLoteriaDatos
    Public ReadOnly datossorteo As New SorteosDatos
    Public ReadOnly detallesadmon As New DetallesAdmonDatos

    Public Function RecalcularTodosSorteos(ejercicio As Integer) As (Double, Double, Double)
        Dim tblSorteos As DataTable = datossorteo.GetSorteoEjercicio(ejercicio)
        Dim importetotal, importepagado, saldo As Double
        Dim id, n As Integer

        Try
            For Each fila As DataRow In tblSorteos.Rows
                n += 1
                id = CInt(Val(fila("IdSorteo").ToString()))

                ' 🔥 PROTECCIÓN: Usamos Val() para evitar que un campo vacío rompa el bucle
                importetotal += Val(fila("ImporteTotal").ToString())
                importepagado += Val(fila("ImportePagado").ToString())
            Next
        Catch ex As Exception
            MsgBox("Error al recalcular el conjunto de sorteos: " & ex.Message)
        End Try

        ' 🔥 CORRECCIÓN: Si hay 1 sorteo o más (n >= 1), calculamos el saldo real del año
        If n >= 1 Then
            saldo = importepagado - importetotal
        Else
            importetotal = 0
            importepagado = 0
            saldo = 0
        End If

        Return (importetotal, importepagado, saldo)
    End Function

    Public Function CalcularSorteo(idsorteo As Integer, ejercicio As Integer) As (Double, Double, Double)
        Dim entregadas, devueltas As Integer
        Dim deuda, liquidado, saldo, importepapeleta, importepagado As Double

        Dim fila As DataRow = datosventas.GetResumenSorteo(idsorteo, ejercicio)

        ' 🔥 OPTIMIZACIÓN: Si la fila no existe, inicializamos a cero de forma predecible
        If fila IsNot Nothing Then
            entregadas = CInt(Val(fila("PapeletasEntregadas")))
            devueltas = CInt(Val(fila("PapeletasDevueltas")))
            liquidado = Val(fila("Liquidado")) * 0.8
            importepapeleta = Val(fila("ImportePapeleta"))
        Else
            entregadas = 0 : devueltas = 0 : liquidado = 0 : importepapeleta = 0
        End If

        deuda = datossorteo.ImporteTotal(ejercicio, idsorteo)
        importepagado = datossorteo.ImportePagadoSorteo(ejercicio, idsorteo)
        saldo = importepagado - deuda

        Return (deuda, importepagado, saldo)
    End Function

    Public Sub ActualizarSorteo(idsorteo As Integer, ejercicio As Integer)
        Dim entregadas, devueltas, pptasaventa, pptastotales, pptasempresa, numdecimos, num1, num2 As Integer
        Dim liquidado, importepapeleta, importetotal, loteriajugada, juegaempresa, importepptapremio,
            importepagado, importedecimo, Premio1, Premio2 As Double


        ' Extraer sumas de detalles venta loteria
        Dim fila As DataRow = datosventas.GetResumenSorteo(idsorteo, ejercicio)
        If fila IsNot Nothing Then
            entregadas = CInt(Val(fila("PapeletasEntregadas")))
            devueltas = CInt(Val(fila("PapeletasDevueltas")))
            liquidado = Val(fila("Liquidado")) * 0.8
            importepapeleta = Val(fila("ImportePapeleta"))
        Else
            MsgBox("No se encontraron datos de venta para este sorteo", MsgBoxStyle.Exclamation, "Atención")
            Exit Sub
        End If

        ' Descargamos todas las columnas del sorteo de una sola ráfaga en memoria
        Dim tblSorteo As DataTable = datossorteo.GetSorteoEjercicio(ejercicio)

        ' Creamos una variable para apuntar a la fila del sorteo correspondiente
        Dim filaSorteo As DataRow = Nothing

        ' Buscamos en la tabla el IdSorteo que queremos recalcular
        For Each r As DataRow In tblSorteo.Rows
            If CInt(Val(r("IdSorteo"))) = idsorteo Then
                filaSorteo = r
                Exit For
            End If
        Next

        ' 2. ESCUDO DE SEGURIDAD: Si el sorteo existe en Aiven, extraemos sus 5 valores
        If filaSorteo IsNot Nothing Then
            importedecimo = Val(filaSorteo("ImporteDecimo"))
            num1 = CInt(Val(filaSorteo("NumeroJugado")))
            num2 = CInt(Val(filaSorteo("NumeroJugado2")))
            Premio1 = Val(filaSorteo("ImportePremioNum1"))
            Premio2 = Val(filaSorteo("ImportePremioNum2"))
            importepagado = Val(filaSorteo("ImportePagado"))
            importepptapremio = Premio1 + Premio2
        Else
            ' Valores por defecto preventivos si el sorteo no se encontrara
            importedecimo = 0 : num1 = 0 : num2 = 0
            Premio1 = 0 : Premio2 = 0 : importepptapremio = 0
            MsgBox("Atención: No se encontraron las directrices del sorteo en el servidor.", MsgBoxStyle.Exclamation)
        End If


        ' Calcular nuevos valores para actualizar sorteo (Tu magnífico motor matemático)
        pptasaventa = entregadas - devueltas
        numdecimos = CInt(Math.Ceiling(pptasaventa * importepapeleta * 0.8 / importedecimo))
        importetotal = numdecimos * importedecimo

        If importepapeleta = 0 Then
            pptastotales = 0
        Else
            pptastotales = importetotal / (importepapeleta * 0.8)
        End If

        pptasempresa = pptastotales - pptasaventa
        juegaempresa = Math.Round(pptasempresa * importepapeleta * 0.8, 2)
        loteriajugada = pptastotales * importepapeleta

        ' Creación de la fila de la Entidad para pasarla a la capa de Datos
        Try
            Dim tabla As DataTable = Entidades.SorteosEntidad.TablaSorteos()
            Dim fila2 As DataRow = tabla.NewRow()

            fila2("IdSorteo") = idsorteo
            fila2("PapeletasEntregadas") = entregadas
            fila2("PapeletasDevueltas") = devueltas
            fila2("LoteriaJugada") = loteriajugada
            fila2("DecimosJugados") = numdecimos
            fila2("ImporteTotal") = importetotal
            fila2("JuegaEmpresa") = juegaempresa
            fila2("ImportePagado") = importepagado
            fila2("ImportePremioPapeleta") = importepptapremio
            fila2("NumeroJugado") = num1
            fila2("NumeroJugado2") = num2
            fila2("ImportePremioNum1") = Premio1
            fila2("ImportePremioNum2") = Premio2
            tabla.Rows.Add(fila2)

            ' Ejecutamos la actualización limpia en Aiven desde la capa de Datos
            datossorteo.EditarSorteo(fila2)
        Catch ex As Exception
            MsgBox("Error al actualizar sorteos desde detalles venta de loteria: " & ex.Message)
        End Try
    End Sub
End Class
