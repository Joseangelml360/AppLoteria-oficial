Imports Negocio
Imports Microsoft.Reporting.WinForms
Public Class FrmVendedoresReport
    Public Property nEje As Integer
    Public ReadOnly eje As New EjercicioActivoNegocio
    Public ReadOnly vendor As New VendedoresNegocio

    Private Sub FrmVendedoresReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nEje = eje.GetEjercicioActivo

        Try
            ' =========================================================================
            ' Sacamos el control de la bandeja de abajo y lo metemos dentro del formulario
            If Not Me.Controls.Contains(Me.ReportViewer1) Then
                Me.Controls.Add(Me.ReportViewer1)
            End If

            ' Forzamos a que ocupe el 100% del alto y ancho de la ventana MDI
            Me.ReportViewer1.Dock = DockStyle.Fill
            Me.ReportViewer1.Visible = True

            ' 2. 🔥 RECAUDACIÓN MANUAL EN RÁFAGA: Traemos el DataTable real
            Dim dtDatosReales As DataTable = vendor.GetlistaVendedores(nEje)

            ' 3. Limpiamos y pre3paramos el motor del ReportViewer de Microsoft
            Me.ReportViewer1.LocalReport.DataSources.Clear()

            ' 🔥 RUTA FISICA DEL FOLIO: Apuntamos al archivo de diseño que acabas de dibujar
            Dim rutaRaizApp As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaRdlc As String = System.IO.Path.Combine(rutaRaizApp, "Reports", "RptVendedores.rdlc")
            Me.ReportViewer1.LocalReport.ReportPath = rutaCompletaRdlc

            ' 4. 🔥 EL ENGRANJE ATÓMICO (Mapeo de Datos)
            ' El primer parámetro debe llamarse EXACTAMENTE IGUAL al nombre del 
            ' Conjunto de Datos (DataSet) que elegiste en el asistente del RDLC
            Dim rds As New ReportDataSource("DataSet1", dtDatosReales)
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)

            ' 5. Pintamos el folio en el monitor del lotero administrativo
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            MsgBox("Error al generar la vista previa del Libro Diario: " & ex.Message,
               MsgBoxStyle.Critical, "Fallo de Impresión")
        End Try


    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub
End Class