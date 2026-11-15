Imports Negocio
Imports Microsoft.Reporting.WinForms
Imports System.Data

Public Class FrmLibroDiarioReport
    ' Variable global que le inyectaremos al abrir la pantalla para saber qué año listar
    Public Property EjercicioActivo As Integer
    'Public ReadOnly libronegocio As New LibroDiarioNegocio
    Private Sub FrmVisorReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConfigurarAyudaFormulario(Me, Me.HelpProvider1, "24")

        Me.Label1.Text = EjercicioActivo
        ' 1. Instanciamos la capa de negocio que ya lee de Aiven de forma manual
        Dim libroNegocio As New LibroDiarioNegocio()

        Try
            ' =========================================================================
            ' 🔥 EL RESCATE GRÁFICO (Inyección manual del control en el formulario)
            ' =========================================================================
            ' Sacamos el control de la bandeja de abajo y lo metemos dentro del formulario
            If Not Me.Controls.Contains(Me.ReportViewer1) Then
                Me.Controls.Add(Me.ReportViewer1)
            End If

            ' Forzamos a que ocupe el 100% del alto y ancho de la ventana MDI
            Me.ReportViewer1.Dock = DockStyle.Fill
            Me.ReportViewer1.Visible = True

            ' 2. 🔥 RECAUDACIÓN MANUAL EN RÁFAGA: Traemos el DataTable real
            Dim dtDatosReales As DataTable = libroNegocio.GetLibroDiario(Me.EjercicioActivo)

            ' 3. Limpiamos y pre3paramos el motor del ReportViewer de Microsoft
            Me.ReportViewer1.LocalReport.DataSources.Clear()

            ' 🔥 RUTA FISICA DEL FOLIO: Apuntamos al archivo de diseño que acabas de dibujar
            Dim rutaRaizApp As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaRdlc As String = System.IO.Path.Combine(rutaRaizApp, "Reports", "RptLibroDiario.rdlc")
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
