Imports Negocio
Public Class FrmResumenVendedor
    Public ReadOnly eje As New EjercicioActivoNegocio
    Public ReadOnly vendor As New VendedoresNegocio
    Public ReadOnly detallevendor As New DetalleVentasLoteriaNegocio
    Private Sub FrmDetallesVentaVendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Neje As String = eje.GetEjercicioActivo.ToString
        Me.LblEjercicio.Text = Neje

        Me.TxtBuscar.Visible = False
        Me.DgvVendedores.Visible = False
        Me.DgvVendedores.Height = 200
        Me.DgvVendedores.Width = 400
        Me.DgvVendedores.DataSource = vendor.GetlistaVendedores(Neje)
        Me.DgvVendedores.Columns(0).Visible = False
        Me.DgvVendedores.Columns(4).Visible = False
        Me.DgvVendedores.Columns(5).Visible = False
        Me.DgvVendedores.Columns(6).Visible = False
        Me.DgvVendedores.Columns(7).Visible = False
        Me.DgvVendedores.Columns(8).Visible = False
        Me.DgvVendedores.Columns(9).Visible = False

        Me.DgvVendedores.Columns(1).Width = 50
        Me.DgvVendedores.Columns(2).Width = 100
        Me.DgvVendedores.Columns(3).Width = 220

    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnSelectVendor_Click(sender As Object, e As EventArgs) Handles BtnSelectVendor.Click
        Me.TxtBuscar.Visible = True
        Me.DgvVendedores.Visible = True
        Me.DgvVendedores.Height = 250
        Me.DgvVendedores.Refresh()
        Me.TxtBuscar.Focus()

    End Sub

    Private Sub DgvVendedores_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvVendedores.CellContentClick
        Me.DgvVendedores.Visible = False
        Me.TxtCodVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Codigo").Value
        Me.TxtNomVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Nombre").Value
        Me.TxtApeVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Apellidos").Value
        Me.TxtAsignada.Text = Me.DgvVendedores.CurrentRow.Cells("LoteriaAsignada").Value
        Me.TxtVendida.Text = Me.DgvVendedores.CurrentRow.Cells("Liquidado").Value
        If Me.DgvVendedores.CurrentRow.Cells("Saldo").Value < 0 Then
            Me.TxtSaldo.Text = Me.DgvVendedores.CurrentRow.Cells("Saldo").Value
            Me.TxtBeneficios.Text = 0
        End If
        If Me.DgvVendedores.CurrentRow.Cells("Saldo").Value > 0 Then
            Me.TxtSaldo.Text = 0
            Me.TxtBeneficios.Text = Me.DgvVendedores.CurrentRow.Cells("Saldo").Value
        ElseIf Me.DgvVendedores.CurrentRow.Cells("Saldo").Value = 0 Then
            Me.TxtSaldo.Text = 0
            Me.TxtBeneficios.Text = 0
        End If
        Me.DgvVendedores.Height = 22
        Me.TxtBuscar.Visible = False

        ' Usamos Val() para limpiar de forma segura el texto antes de convertir a entero
        Dim nCodigo As Integer = CInt(Val(Me.TxtCodVendor.Text))
        Dim nEje As Integer = CInt(Val(Me.LblEjercicio.Text))

        Try
            ' 1. Consultamos el resumen a la capa de Negocio
            Dim Resultado As DataTable = detallevendor.ObtenerResumenTotalVendor(nCodigo, nEje)

            ' 2. Enlazamos la tabla contenedora al DataGridView
            Me.DgvSorteosVendor.DataSource = Resultado
            Me.DgvSorteosVendor.Refresh()
            'Else
            '    Me.DgvSorteosVendor.DataSource = Nothing
            '    MsgBox("No existen movimientos de lotería para el vendedor y sorteo especificados.",
            '           MsgBoxStyle.Information, "Sin Registros")
            'End If

        Catch ex As Exception
            MsgBox("Error al conectar con la capa de negocio: " & ex.Message,
                   MsgBoxStyle.Critical, "Error de Datos")
        End Try

    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        Dim Neje As Integer = eje.GetEjercicioActivo
        Dim vista As New DataView(vendor.GetlistaVendedores(Neje))

        If TxtBuscar.Text <> "" Then
            vista.RowFilter = $"Apellidos LIKE '%{TxtBuscar.Text}%'"
        End If

        DgvVendedores.DataSource = vista

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New FrmResumenSorteosReport
        With frm
            .TxtVendedor.Text = Me.TxtCodVendor.Text & ". " & Me.TxtNomVendor.Text & " " & TxtApeVendor.Text
            .TxtAsignada.Text = Me.TxtAsignada.Text
            .TxtVendida.Text = Me.TxtVendida.Text
            .TxtSaldo.Text = Me.TxtSaldo.Text
            .TxtBeneficios.Text = Me.TxtBeneficios.Text
        End With
        frm.ShowDialog()
    End Sub
End Class