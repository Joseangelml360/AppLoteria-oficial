Imports Datos
Imports System.Data

Public Class VersionLoteriaNegocio
    Private ReadOnly datosVersion As New VersionLoteriaDatos()

    Public Function ComprobarVersionSistema(versionLocal As Version) As (ExisteCambio As Boolean, VersionNueva As String, UrlZip As String, EsObligatoria As Boolean)
        Try
            Dim fila As DataRow = datosVersion.ObtenerUltimaVersion()

            If fila IsNot Nothing Then
                Dim versionCloudStr As String = fila("version_loteria").ToString().Trim()
                Dim urlZip As String = fila("url_zip").ToString().Trim()
                Dim esObligatoria As Boolean = Convert.ToBoolean(Val(fila("obligatoria").ToString()))

                Dim versionCloud As New Version(versionCloudStr)

                If versionCloud > versionLocal Then
                    Return (True, versionCloudStr, urlZip, esObligatoria)
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Omitiendo chequeo de versión por corte de red: " & ex.Message)
        End Try

        Return (False, "", "", False)
    End Function

    ' 🔥 SINCRONIZADO: Método que llama tu FrmGestionVersiones para llenar la rejilla
    Public Function ObtenerHistorialVersiones() As DataTable
        Return datosVersion.ObtenerHistorialVersiones()
    End Function

    ' 🔥 SINCRONIZADO: Método que procesa tu formulario visual al pulsar Publicar
    Public Sub PublicarVersion(versionStr As String, urlZip As String, esObligatoria As Boolean)
        If String.IsNullOrWhiteSpace(versionStr) OrElse String.IsNullOrWhiteSpace(urlZip) Then
            Throw New Exception("La versión y la URL del archivo ZIP son campos obligatorios.")
        End If

        Dim vCheck As Version = Nothing
        If Not Version.TryParse(versionStr, vCheck) Then
            Throw New Exception("El formato de versión introducido no es válido (Ej correcto: 1.0.0.0).")
        End If

        ' Estructuración manual y limpia del DataRow de la entidad
        Dim tabla As New DataTable()
        tabla.Columns.Add("version_loteria", GetType(String))
        tabla.Columns.Add("url_zip", GetType(String))
        tabla.Columns.Add("obligatoria", GetType(Integer))

        Dim fila As DataRow = tabla.NewRow()
        fila("version_loteria") = versionStr.Trim()
        fila("url_zip") = urlZip.Trim()
        fila("obligatoria") = If(esObligatoria, 1, 0)
        tabla.Rows.Add(fila)

        ' Enviamos a la capa de datos de forma directa
        datosVersion.PublicarVersion(fila)
    End Sub
End Class
