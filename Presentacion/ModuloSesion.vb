Imports System.IO
Imports System.Windows.Forms
Public Module ModuloSesion
    ' 🔥 VARIABLES GLOBALES DE SESIÓN: Accesibles desde cualquier pantalla del proyecto
    Public IdRolUsuario As String = ""
    Public NombreRolActivo As String = ""
    Public IdRolActivo As Integer = 0
    Public user As String
    Public cRol As String
    Public nRol As Integer

    ' 🔥Cargar en cada formulario del HelpProvider1 el archivo de Ayuda TutorialAppLoteria.chm
    Public Sub ConfigurarAyudaFormulario(ByVal formulario As Form, ByVal helpProvider As HelpProvider, ByVal idTemaHelpNDoc As String)
        Try
            ' 1. Calculamos la ruta elástica exacta de la carpeta donde corre el EXE
            Dim rutaRaiz As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaChm As String = Path.Combine(rutaRaiz, "TutorialAppLoteria.chm")

            ' 2. Seteamos las directivas canónicas en el HelpProvider
            helpProvider.HelpNamespace = rutaCompletaChm

            ' 3. Vinculamos el ID de tema de HelpNDoc de forma automatizada al contenedor del Form
            helpProvider.SetHelpNavigator(formulario, HelpNavigator.TopicId)
            helpProvider.SetHelpKeyword(formulario, idTemaHelpNDoc)
            helpProvider.SetShowHelp(formulario, True)

        Catch ex As Exception
            ' Un escudo silencioso para que un fallo en la ayuda nunca tumbe la suite contable
            Console.WriteLine("Error al inyectar la ayuda contextual: " & ex.Message)
        End Try
    End Sub

End Module
