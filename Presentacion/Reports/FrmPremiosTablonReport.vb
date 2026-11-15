Imports Negocio
Imports Microsoft.Reporting.WinForms
Imports System.Data

Public Class FrmPremiosTablonReport
    ' Variable global que le inyectaremos desde el MDIParent para saber qué año listar
    Public Property Ejercicio As Integer

    ' Instanciamos de forma limpia tu nueva capa de negocio específica
    Public ReadOnly premios As New SorteosNegocio()
    Public ReadOnly eje As New EjercicioActivoNegocio()

    Private Sub FrmPremiosTablonReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConfigurarAyudaFormulario(Me, Me.HelpProvider1, "22")

        Ejercicio = eje.GetEjercicioActivo

        Try
            ' =========================================================================
            ' 🔥 RESCATE GRÁFICO (Inyección obligatoria del visor en el formulario)
            ' =========================================================================
            If Not Me.Controls.Contains(Me.ReportViewer1) Then
                Me.Controls.Add(Me.ReportViewer1)
            End If

            ' Forzamos a que ocupe el 100% de la pantalla de forma impecable
            Me.ReportViewer1.Dock = DockStyle.Fill
            Me.ReportViewer1.Visible = True

            ' =========================================================================
            ' 📊 RECAUDACIÓN Y CARGA DE DATOS DESDE AIVEN
            ' =========================================================================
            ' 1. Extraemos el DataTable manual a través de tus 4 capas puras
            Dim dtDatos As DataTable = premios.ObtenerTablonPremios(Ejercicio)

            ' 2. Limpiamos y preparamos el motor del ReportViewer
            Me.ReportViewer1.LocalReport.DataSources.Clear()

            ' 3. 🔥 RUTA ASIGNADA: Apuntamos al archivo físico que acabas de diseñar
            ' (Recuerda verificar que el .rdlc esté en Propiedades como "Copiar siempre")
            Dim rutaRaizApp As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaRdlc As String = System.IO.Path.Combine(rutaRaizApp, "Reports", "RptPremiosTablon.rdlc")
            Me.ReportViewer1.LocalReport.ReportPath = rutaCompletaRdlc

            ' 4. Vinculamos los datos con el molde del DataSet del folio
            ' El primer parámetro "DataSet1" debe llamarse igual al de tu panel izquierdo
            Dim rds As New ReportDataSource("DataSet1", dtDatos)
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)

            ' 5. Pintamos el tablón de anuncios oficial en el monitor
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            MsgBox("Error al generar el listado de premios para el tablón: " & ex.Message,
                   MsgBoxStyle.Critical, "Fallo de Impresión")
        End Try
    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub
End Class
