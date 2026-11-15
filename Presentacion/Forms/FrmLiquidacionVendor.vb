Imports Negocio

Public Class FrmLiquidacionVendor
    ' Mantienes tus instancias globales limpias
    Public ReadOnly vendor As New DetalleVentasLoteriaNegocio()
    Public ReadOnly eje As New EjercicioActivoNegocio()
    Public nEje As Integer

    Private Sub FrmLiquidacionVendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nEje = eje.GetEjercicioActivo()
    End Sub

    Private Sub Cargardatos()
        ' Usamos Val() para limpiar de forma segura el texto antes de convertir a entero
        Dim nCodigo As Integer = CInt(Val(Me.TxtCodigo.Text.ToString()))
        Dim nSorteo As Integer = CInt(Val(Me.TxtIdSorteo.Text.ToString()))

        Try
            ' 1. Consultamos el resumen a la capa de Negocio
            Dim filaResultado As DataRow = vendor.ObtenerResumenSorteoVendor(nSorteo, nCodigo, nEje)

            ' 2. 🛡️ ESCUDO DE CONTROL VISUAL: Si la consulta viene vacía de Aiven, avisamos al lotero
            If filaResultado IsNot Nothing Then
                ' 3. 🔥 EL TRUCO DEL CONTENEDOR: Creamos un DataTable idéntico en la RAM 
                ' copiando la estructura exacta del esquema de la fila
                Dim dtContenedor As DataTable = filaResultado.Table.Clone()

                ' Importamos la fila real dentro de nuestra tabla virtual
                dtContenedor.ImportRow(filaResultado)

                ' 4. Enlazamos la tabla contenedora al DataGridView
                Me.DgvLiquidacionVendor.DataSource = dtContenedor
                Me.DgvLiquidacionVendor.Refresh()
            Else
                Me.DgvLiquidacionVendor.DataSource = Nothing
                MsgBox("No existen movimientos de lotería para el vendedor y sorteo especificados.",
                       MsgBoxStyle.Information, "Sin Registros")
            End If

        Catch ex As Exception
            MsgBox("Error al conectar con la capa de negocio: " & ex.Message,
                   MsgBoxStyle.Critical, "Error de Datos")
        End Try
    End Sub

    Private Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        ' 🔥 REPARACIÓN CRÍTICA: Cambiamos "IsNot Nothing" por control legítimo de texto vacío
        If Not String.IsNullOrWhiteSpace(Me.TxtCodigo.Text) AndAlso
           Not String.IsNullOrWhiteSpace(Me.TxtIdSorteo.Text) Then

            ' Si ambas casillas contienen números escritos, procesamos la carga masiva
            Cargardatos()
        Else
            ' Si alguna de las dos está en blanco, saltará el escudo de aviso inmediatamente
            MsgBox("El número de sorteo y el código del vendedor no pueden estar vacíos.",
                   MsgBoxStyle.Exclamation, "Campos Requeridos")
            Me.TxtCodigo.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
