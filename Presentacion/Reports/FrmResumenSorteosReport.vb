Imports Negocio
Imports Microsoft.Reporting.WinForms

Public Class FrmResumenSorteosReport
    Public Property EjercicioActivo As Integer
    Public ReadOnly liquiNegocio As New DetalleVentasLoteriaNegocio
    Public ReadOnly vendor As New VendedoresNegocio
    Public ReadOnly eje As New EjercicioActivoNegocio

    Private Sub FrmResumenSorteosReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConfigurarAyudaFormulario(Me, Me.HelpProvider1, "26")

        EjercicioActivo = eje.GetEjercicioActivo
        Me.TxtBuscar.Visible = False
        Me.DgvVendedores.Visible = False

        ' 1. Rescate gráfico por código: forzamos el acople del visor al contenedor inferior
        If Not Me.PanelVisorReport.Controls.Contains(Me.ReportViewer1) Then
            Me.PanelVisorReport.Controls.Add(Me.ReportViewer1)
        End If
        Me.ReportViewer1.Dock = DockStyle.Fill
        Me.ReportViewer1.Visible = True

        If Val(TxtCodigoVendor.Text) > 0 Then
            Me.RbtnVendedor.Checked = True
            Me.RbtnGlobal.Checked = False
            CargarDatosVendor(CInt(Val(TxtCodigoVendor.Text)))
        Else
            ' Marcamos por defecto la opción global
            Me.RbtnGlobal.Checked = True
            Me.TxtVendedor.Enabled = False
        End If


        ' Renderizamos la primera carga global automática
        Filtrar()

    End Sub

    Private Sub Filtrar()
        Dim codigoVendedor As Integer = 0

        ' Si está seleccionada la opción de vendedor, capturamos su ID con nuestro escudo Val()
        If Me.RbtnVendedor.Checked Then
            If String.IsNullOrWhiteSpace(Me.TxtVendedor.Text) Then
                MsgBox("Por favor, introduce un código de vendedor válido.", MsgBoxStyle.Exclamation)
                Me.TxtCodigoVendor.Focus()
                Exit Sub
            End If
            codigoVendedor = CInt(Val(Me.TxtCodigoVendor.Text.ToString()))
        End If

        ProcesarCargaInforme(codigoVendedor)
    End Sub

    Private Sub ProcesarCargaInforme(idVendor As Integer)
        Try
            ' 1. RECAUDACIÓN DINÁMICA: Traemos los datos de Aiven según el filtro seleccionado
            Dim dtDatos As DataTable = liquiNegocio.GetLiquidacionReporte(Me.EjercicioActivo, idVendor)

            ' 2. Sincronizamos el motor de Microsoft ReportViewer
            Me.ReportViewer1.LocalReport.DataSources.Clear()

            Dim rutaRaizApp As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaRdlc As String = System.IO.Path.Combine(rutaRaizApp, "Reports", "RptResumenSorteosVendor.rdlc")
            Me.ReportViewer1.LocalReport.ReportPath = rutaCompletaRdlc

            ' 3. Acoplamos los datos al DataSet del folio
            Dim rds As New ReportDataSource("DataSet1", dtDatos)
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)

            ' =========================================================================
            ' 🔥 EL PASO DE PARÁMETROS: Capturamos las cajas de texto y las enviamos al archivo .rdlc
            ' =========================================================================
            ' Creamos una lista dinámica de parámetros para el visor
            Dim listaParametros As New List(Of ReportParameter)()

            ' Si es consulta global, pasamos textos genéricos. Si es vendedor, pasamos sus casillas.
            If Me.RbtnGlobal.Checked Then
                listaParametros.Add(New ReportParameter("p_vendedor", "TODOS LOS VENDEDORES"))
                listaParametros.Add(New ReportParameter("p_asignada", "---"))
                listaParametros.Add(New ReportParameter("p_vendida", "---"))
                listaParametros.Add(New ReportParameter("p_saldo", "---"))
                listaParametros.Add(New ReportParameter("p_beneficio", "---"))
                listaParametros.Add(New ReportParameter("p_ejercicio", EjercicioActivo))
            Else
                ' Usamos condicionales rápidos para evitar pasar casillas vacías a la visualización
                Dim vNombre As String = If(String.IsNullOrWhiteSpace(Me.TxtVendedor.Text), "No seleccionado", Me.TxtVendedor.Text)
                Dim vAsignada As String = If(String.IsNullOrWhiteSpace(Me.TxtAsignada.Text), "0.00", Me.TxtAsignada.Text)
                Dim vVendida As String = If(String.IsNullOrWhiteSpace(Me.TxtVendida.Text), "0.00", Me.TxtVendida.Text)
                Dim vSaldo As String = If(String.IsNullOrWhiteSpace(Me.TxtSaldo.Text), "0.00", Me.TxtSaldo.Text)
                Dim vBeneficio As String = If(String.IsNullOrWhiteSpace(Me.TxtBeneficios.Text), "0.00", Me.TxtBeneficios.Text)

                listaParametros.Add(New ReportParameter("p_vendedor", vNombre))
                listaParametros.Add(New ReportParameter("p_asignada", vAsignada))
                listaParametros.Add(New ReportParameter("p_vendida", vVendida))
                listaParametros.Add(New ReportParameter("p_saldo", vSaldo))
                listaParametros.Add(New ReportParameter("p_beneficio", vBeneficio))
                listaParametros.Add(New ReportParameter("p_ejercicio", EjercicioActivo))
            End If

            ' 🔥 INYECCIÓN ATÓMICA: Seteamos la lista completa dentro del informe local
            Me.ReportViewer1.LocalReport.SetParameters(listaParametros)

            ' 4. Redibujamos las celdas en el monitor
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            MsgBox("Error al generar el balance: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    ' Eventos estéticos para habilitar/deshabilitar la casilla de texto según el modo
    Private Sub RbtnGlobal_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnGlobal.CheckedChanged
        If Me.RbtnGlobal.Checked Then
            Me.TxtCodigoVendor.Clear()
            Me.TxtVendedor.Clear()
            Me.TxtAsignada.Clear()
            Me.TxtVendida.Clear()
            Me.TxtSaldo.Clear()
            Me.TxtBeneficios.Clear()
            ProcesarCargaInforme(0)
        End If
    End Sub

    Private Sub RbtnVendedor_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnVendedor.CheckedChanged
        If Me.RbtnVendedor.Checked Then Me.TxtVendedor.Enabled = True
        Dim nCod As Integer = CInt(Val(Me.TxtCodigoVendor.Text))
        ProcesarCargaInforme(nCod)
    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Me.TxtBuscar.Visible = True
        Me.DgvVendedores.Visible = True
        Me.DgvVendedores.Height = 200
        Me.DgvVendedores.Width = 400
        Me.DgvVendedores.DataSource = vendor.GetlistaVendedores(EjercicioActivo)
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

    Private Sub DgvVendedores_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvVendedores.CellContentClick
        Me.DgvVendedores.Visible = False
        Dim nCodigo As Integer = Me.DgvVendedores.CurrentRow.Cells("Codigo").Value
        Me.TxtCodigoVendor.Text = nCodigo
        Dim cNombre As String = Me.DgvVendedores.CurrentRow.Cells("Nombre").Value
        Dim cApellidos As String = Me.DgvVendedores.CurrentRow.Cells("Apellidos").Value
        Me.TxtVendedor.Text = cNombre & " " & cApellidos

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
        Me.RbtnVendedor.Checked = True
        Me.RbtnGlobal.Checked = False

        ' 2. Forzamos la ráfaga de actualización del ReportViewer pasando su ID real
        ProcesarCargaInforme(nCodigo)
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        Dim vista As New DataView(vendor.GetlistaVendedores(EjercicioActivo))

        If TxtBuscar.Text <> "" Then
            vista.RowFilter = $"Apellidos LIKE '%{TxtBuscar.Text}%'"
        End If

        DgvVendedores.DataSource = vista

    End Sub

    Private Sub CargarDatosVendor(codigo)
        Me.DgvVendedores.DataSource = vendor.GetVendorEjercicio(EjercicioActivo, codigo)

        Me.TxtAsignada.Text = Me.DgvVendedores.CurrentRow.Cells("LoteriaAsignada").Value
        Me.TxtVendida.Text = Me.DgvVendedores.CurrentRow.Cells("Liquidado").Value
        Dim nSaldo As Double = Me.DgvVendedores.CurrentRow.Cells("Saldo").Value
        If nSaldo < 0 Then
            Me.TxtSaldo.Text = nSaldo
            Me.TxtBeneficios.Text = 0
        ElseIf nSaldo = 0 Then
            Me.TxtSaldo.Text = 0
            Me.TxtBeneficios.Text = 0
        ElseIf nSaldo > 0 Then
            Me.TxtSaldo.Text = 0
            Me.TxtBeneficios.Text = nSaldo
        End If


    End Sub
End Class
