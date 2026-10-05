Imports Negocio
Public Class FrmDetallesVentaVendedor

    Public ReadOnly eje As New EjercicioActivoNegocio
    Public ReadOnly vendor As New VendedoresNegocio
    Private Sub FrmDetallesVentaVendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Neje As Integer = eje.GetEjercicioActivo

        Me.Height = 650

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


        Me.TxtCodVendor.Text = Me.DgvVendedores.Rows(0).Cells("Codigo").Value
        Me.TxtNomVendor.Text = Me.DgvVendedores.Rows(0).Cells("Nombre").Value
        Me.TxtApeVendor.Text = Me.DgvVendedores.Rows(0).Cells("Apellidos").Value

        'TODO: esta línea de código carga datos en la tabla 'dbcensosDataSet.detallesventaloteria' Puede moverla o quitarla según sea necesario.
        Me.detallesventaloteriaTableAdapter.Fill(Me.dbcensosDataSet.detallesventaloteria)

        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnFiltro_Click(sender As Object, e As EventArgs) Handles BtnFiltro.Click
        Dim Neje As Integer = eje.GetEjercicioActivo
        Me.detallesventaloteriaTableAdapter.FillByCodigo(Me.dbcensosDataSet.detallesventaloteria, Neje, CInt(Me.TxtCodVendor.Text))
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub BtnSelectVendor_Click(sender As Object, e As EventArgs) Handles BtnSelectVendor.Click
        Me.TxtBuscar.Visible = True
        Me.DgvVendedores.Visible = True
        Me.detallesventaloteriaTableAdapter.Fill(dbcensosDataSet.detallesventaloteria)
        Me.DgvVendedores.Refresh()
        Me.TxtBuscar.Focus()

    End Sub

    Private Sub DgvVendedores_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvVendedores.CellContentClick
        Me.DgvVendedores.Visible = False
        Me.TxtCodVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Codigo").Value
        Me.TxtNomVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Nombre").Value
        Me.TxtApeVendor.Text = Me.DgvVendedores.CurrentRow.Cells("Apellidos").Value
        Me.TxtBuscar.Visible = False
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        Dim Neje As Integer = eje.GetEjercicioActivo
        Dim vista As New DataView(vendor.GetlistaVendedores(Neje))

        If TxtBuscar.Text <> "" Then
            vista.RowFilter = $"Apellidos LIKE '%{TxtBuscar.Text}%'"
        End If

        DgvVendedores.DataSource = vista

    End Sub

End Class