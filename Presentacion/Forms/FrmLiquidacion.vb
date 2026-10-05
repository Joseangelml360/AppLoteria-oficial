Imports Negocio
Public Class FrmLiquidacion
    Public ReadOnly negocio As New LiquidacionNegocio
    Public ReadOnly dato As New EjercicioActivoNegocio
    Private Sub FrmLiquidacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LblEjercicio.Text = dato.GetEjercicioActivo

        Dim negocio As New LiquidacionNegocio()
        Dim eje As Integer = CInt(Me.LblEjercicio.Text)

        Try
            ' 1. Ordenamos a la capa de negocio hacer toda la magia contable
            negocio.ActualizarTablaLiquidacion(eje)
            MsgBox("Liquidación calculada y guardada con éxito en la nube.", MsgBoxStyle.Information, "Éxito")

            ' 2. Refrescamos el DataGridView llamando a tu método local de carga
            CargarDatos()
        Catch ex As Exception
            MsgBox("Error al procesar el balance: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Public Sub CargarDatos()
        Me.DgvLiquidacion.DataSource = negocio.GetLiquidacion
        Me.DgvLiquidacion.Refresh()
        Me.CboEjercicios.DataSource = negocio.GetLiquidacion
        Me.CboEjercicios.DisplayMember = "Ejercicio"
        Me.CboEjercicios.ValueMember = "Ejercicio"
        Me.DgvLiquidacion.Columns(0).Width = 60
        Me.DgvLiquidacion.Columns(1).Width = 60
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CboEjercicios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboEjercicios.SelectedIndexChanged
        Dim valorSeleccionado As Integer

        ' Verifica si el ComboBox está usando un DataTable como origen de datos
        If TypeOf CboEjercicios.SelectedItem Is DataRowView Then
            valorSeleccionado = Convert.ToInt32(CType(CboEjercicios.SelectedItem, DataRowView)("Ejercicio"))
        Else
            valorSeleccionado = Convert.ToInt32(CboEjercicios.SelectedItem)
        End If

        Me.DgvLiquidacion.DataSource = negocio.GetLiquidacionFilter(valorSeleccionado)
    End Sub

    Private Sub BtnVerTodos_Click(sender As Object, e As EventArgs) Handles BtnVerTodos.Click
        CargarDatos()
        Me.DgvLiquidacion.Refresh()
    End Sub

    Public Sub CalcularLiquidacion()
        Dim negocio As New LiquidacionNegocio()
        Dim eje As Integer = CInt(Me.LblEjercicio.Text)

        Try
            ' 1. Ordenamos a la capa de negocio hacer toda la magia contable
            negocio.ActualizarTablaLiquidacion(eje)
            MsgBox("Liquidación calculada y guardada con éxito en la nube.", MsgBoxStyle.Information, "Éxito")

            ' 2. Refrescamos el DataGridView llamando a tu método local de carga
            CargarDatos()
        Catch ex As Exception
            MsgBox("Error al procesar el balance: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

End Class