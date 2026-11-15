Imports Datos
Imports System.Data

Public Class LiquidacionNegocio
    Private ReadOnly datosliquidacion As New LiquidacionDatos()

    Public Function GetLiquidacion() As DataTable
        Return datosliquidacion.ObtenerLiquidacion()
    End Function

    Public Function GetLiquidacionFilter(ejercicio As Integer) As DataTable
        Return datosliquidacion.GetLiquidacionFilter(ejercicio)
    End Function

    Public Function GetTotalesSorteos(ejercicio As Integer) As DataRow
        Return datosliquidacion.ObtenerTotalesSorteos(ejercicio)
    End Function

    ' =========================================================================
    ' ⚡ MOTOR DE ACTUALIZACIÓN EN TIEMPO REAL (La "Fotografía" de la Caja)
    ' =========================================================================
    Public Sub ActualizarTablaLiquidacion(ejercicio As Integer)
        ' 1. Descargamos las ráfagas acumuladas hasta este mismo segundo de la nube
        Dim rowSorteos As DataRow = datosliquidacion.ObtenerTotalesSorteos(ejercicio)
        Dim rowAdmon As DataRow = datosliquidacion.ObtenerTotalesAdmon(ejercicio)
        Dim rowVentas As DataRow = datosliquidacion.ObtenerTotalesVentasVendedores(ejercicio)

        ' Variables numéricas puras de control con escudo contra celdas vacías
        Dim comprado, vendido, jugadoEmpresa, pagadoAdmon, premios, ventasLoteria, abonoPremios As Double

        If rowSorteos IsNot Nothing Then
            comprado = Val(rowSorteos("Comprado").ToString())
            vendido = Val(rowSorteos("Vendido").ToString())
            jugadoEmpresa = Val(rowSorteos("JugadoEmpresa").ToString())
        End If

        If rowAdmon IsNot Nothing Then
            pagadoAdmon = Val(rowAdmon("Pagado").ToString())
            premios = Val(rowAdmon("Premios").ToString())
        End If

        If rowVentas IsNot Nothing Then
            ventasLoteria = Val(rowVentas("VentasLoteria").ToString())
            abonoPremios = Val(rowVentas("AbonoPremios").ToString())
        End If

        ' Descargamos los sumatorios directos escalares de vendedores
        Dim aCuotas As Double = datosliquidacion.ObtenerTotalACuotas(ejercicio)
        Dim aBeneficiosSocio As Double = datosliquidacion.ObtenerTotalBeneficiosSocios(ejercicio)

        ' 2. 🧠 ALGORITMO DE BALANCE DINÁMICO (Tus fórmulas de equilibrio)
        Dim saldo As Double = vendido + premios - pagadoAdmon - abonoPremios
        Dim deducciones As Double = aCuotas + aBeneficiosSocio + jugadoEmpresa
        Dim beneficioEmpresa As Double = saldo - deducciones

        ' 3. EMPAQUETADO EN LA ENTIDAD
        Dim tabla As DataTable = Entidades.LiquidacionEntidad.TablaLiquidacion()
        Dim filaNueva As DataRow = tabla.NewRow()

        filaNueva("Ejercicio") = ejercicio
        filaNueva("Comprado") = comprado
        filaNueva("Vendido") = vendido
        filaNueva("Premios") = premios
        filaNueva("AbonoPremios") = abonoPremios
        filaNueva("ACuotas") = aCuotas
        filaNueva("ABeneficiosSocio") = aBeneficiosSocio
        filaNueva("JugadoEmpresa") = jugadoEmpresa
        filaNueva("Saldo") = Math.Round(saldo, 2)
        filaNueva("BeneficioEmpresa") = Math.Round(beneficioEmpresa, 2)
        tabla.Rows.Add(filaNueva)

        ' 4. 🔥 DECISIÓN INTELIGENTE (INSERT O UPDATE AUTOMÁTICO)
        ' Comprobamos si el año actual ya tiene un registro en la tabla liquidación
        Dim tblCheck As DataTable = datosliquidacion.GetLiquidacionFilter(ejercicio)

        If tblCheck.Rows.Count > 0 Then
            ' Si ya existía una fotografía previa, machacamos los datos con el nuevo estado
            datosliquidacion.EditarLiquidacion(filaNueva)
        Else
            ' Si es la primera vez que se consulta este año, creamos el renglón
            datosliquidacion.InsertarLiquidacion(filaNueva)
        End If
    End Sub
End Class
