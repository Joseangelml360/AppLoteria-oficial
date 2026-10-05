Imports Datos
Imports Entidades
Public Class CalcularPremiosLoteriaNegocio
    Public ReadOnly sorteo As New SorteosDatos
    Public ReadOnly detalleadmon As New DetallesAdmonDatos

    Public Function CalcularImportePremioSorteo(ejercicio As Integer, idsorteo As Integer)

        Dim numdecimos As Integer = sorteo.DecimosComprados(idsorteo, ejercicio)
        Dim importedecimo As Double = sorteo.ImporteDecimo(idsorteo, ejercicio)
        Dim importepapeletapremiada As Double = sorteo.ImportePptaPremio(idsorteo, ejercicio)
        Dim importepapeleta As Double = sorteo.ImportePpta(idsorteo, ejercicio)

        'Cálculos
        Dim cociente = importepapeletapremiada / (importepapeleta * 0.8)
        Dim importetotalpremio = importedecimo * numdecimos * cociente
        Return importetotalpremio
    End Function

End Class
