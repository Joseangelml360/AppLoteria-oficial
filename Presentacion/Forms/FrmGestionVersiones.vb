Imports Negocio
Imports System.Windows.Forms

Public Class FrmGestionVersiones
    Private ReadOnly vNegocio As New VersionLoteriaNegocio()

    Private Sub FrmGestionVersiones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarHistorialDgv()
    End Sub

    Private Sub CargarHistorialDgv()
        Try
            ' Cargamos el historial directamente desde el Negocio hacia la rejilla
            Me.DgvHistorialVersiones.DataSource = vNegocio.ObtenerHistorialVersiones()
        Catch ex As Exception
            MsgBox("Error al listar versiones: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub BtnPublicar_Click(sender As Object, e As EventArgs) Handles BtnPublicar.Click
        Try
            ' Capturamos los datos visuales y los enviamos al motor de Negocio
            vNegocio.PublicarVersion(
                Me.TxtNuevaVersion.Text,
                Me.TxtUrlZip.Text,
                Me.ChkObligatoria.Checked)

            MsgBox("¡Nueva versión desplegada con éxito total en la nube de Aiven!" & vbCrLf &
                   "Los administrativos recibirán la actualización al arrancar sus aplicaciones.",
                   MsgBoxStyle.Information, "Lanzamiento Exitoso")

            ' Limpieza de controles y recarga automática
            Me.TxtNuevaVersion.Clear()
            Me.TxtUrlZip.Clear()
            Me.ChkObligatoria.Checked = False
            CargarHistorialDgv()

        Catch ex As Exception
            MsgBox("No se pudo desplegar la versión: " & ex.Message, MsgBoxStyle.Exclamation, "Operación Cancelada")
        End Try
    End Sub
End Class
