Imports Negocio
Imports System.IO

Public Class LogInForm
    Public ReadOnly eje As New EjercicioActivoNegocio
    Private ReadOnly vNegocio As New VersionLoteriaNegocio()
    Private Sub LogInForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarAyudaFormulario(Me, Me.HelpProvider1, "2")

        EjecutarControlVersionesBD()
        Me.LblVersion.Text = String.Format("Versión {0}", My.Application.Info.Version.ToString)
        Me.LblConexion.Text = ""
        Me.TxtEjercicio.Text = eje.GetEjercicioActivo
        Me.TxtNombre.Text = ""
        Me.TxtNombre.Focus()
        ValidarVersion()
    End Sub

    Private Sub ValidarVersion()
        ' 1. Obtener la versión que tiene este ejecutable corriendo ahora mismo
        Dim versionLocal As New Version(Application.ProductVersion)

        ' 2. Instanciamos la capa de negocio de versiones que ya dejamos lista
        Dim vNegocio As New VersionLoteriaNegocio()

        ' Consultamos Aiven a través del Negocio pasando nuestro número de versión
        Dim resultado = vNegocio.ComprobarVersionSistema(versionLocal)

        ' 3. Si la base de datos cloud dictamina que hay un salto de versión
        If resultado.ExisteCambio Then
            MessageBox.Show(
                $"Hay una nueva versión disponible ({resultado.VersionNueva}). " & vbCrLf &
                "La aplicación se actualizará de forma totalmente automática.",
                "Actualización disponible", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' 4. Localizamos el ejecutable independiente del actualizador en la carpeta raíz
            Dim rutaActualizador As String =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Actualizador.exe")

            If File.Exists(rutaActualizador) Then
                ' Configuración de arranque avanzada heredada de tu módulo de Secretaría
                Dim infoArranque As New ProcessStartInfo()
                infoArranque.FileName = rutaActualizador


                ' 🔥 MODIFICACIÓN: Pasamos dos argumentos separados por un espacio: "URL" y "NombreEjecutable"
                infoArranque.Arguments = """" & resultado.UrlZip & """ ""AppLoteria"""


                ' Forzamos el consentimiento de Administrador de Windows (Escudo UAC)
                infoArranque.Verb = "runas"

                ' 🛡️ ESCUDO CONTRA DIRECTIVAS RÍGIDAS DE WINDOWS
                Try
                    Process.Start(infoArranque)
                    ' Si el satélite arrancó con éxito, cerramos la app principal de inmediato
                    Application.Exit()
                    End
                Catch ex As Exception
                    ' Si el Windows del usuario tiene restricciones locales (AppLocker), avisamos
                    MessageBox.Show(
                        "Una directiva de seguridad de Windows impidió ejecutar la actualización." & vbCrLf & vbCrLf &
                        "Por favor, cierra el programa, haz clic derecho sobre 'AppLoteria.exe' " &
                        "y selecciona 'Ejecutar como administrador' para permitir el proceso." & vbCrLf &
                        "Detalle: " & ex.Message, "Control de Aplicaciones Bloqueado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Else
                MessageBox.Show(
                    "Error crítico: No se encuentra el archivo 'Actualizador.exe' en la carpeta raíz.",
                    "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim negocio As New UsuarioNegocio()
        'Dim nRol As Integer
        'Dim user As String

        Try
            ' Llamamos a la capa de negocio pasando los textos de las casillas
            Dim dt As DataTable = negocio.ValidarUsuario(Me.TxtNombre.Text, Me.TxtContraseña.Text)

            ' Si la tabla contiene exactamente 1 fila, las credenciales son correctas
            If dt.Rows.Count = 1 Then
                Dim fila As DataRow = dt.Rows(0)

                ' Extraemos el rol del usuario de la fila del DataSet en memoria
                Dim idRolUsuario As Integer = Val(fila("idrol"))
                ' 2. 🔥 NUEVO ESCUDO DE SEGURIDAD: Validamos que pertenezca al departamento de loterías
                ' Si el idrol no es 4 (Lotero) o 1 (administrador), bloqueamos el acceso proactivamente
                'If idRolUsuario <> 1 And idRolUsuario <> 4 Then
                '    MsgBox("Acceso denegado. Tu perfil de usuario no tiene permisos para gestionar el módulo de loterías.",
                '           MsgBoxStyle.Critical, "Permisos Insuficientes")

                '    ' Limpiamos la casilla de contraseña por seguridad y enfocamos el campo
                '    Me.TxtContraseña.Text = ""
                '    Me.TxtNombre.Focus()
                '    Exit Sub ' Frenamos la ejecución del Login inmediatamente
                'End If

                ' 3. SI EL ROL ES 4: Almacenamos las variables globales y concedemos el acceso
                nRol = idRolUsuario
                ' Almacenamos las variables globales de la sesión leyendo los campos reales
                nRol = Val(fila("idrol"))
                user = fila("nombre").ToString()

                Select Case nRol
                    Case 1 : cRol = "Administrador"
                    Case 2 : cRol = "Tesorero"
                    Case 3 : cRol = "Lotero Administrativo"
                    Case 4 : cRol = "Secretaría"
                    Case Else : cRol = "Consulta"
                End Select

                ' Ocultamos el login y abrimos el contenedor de las loterías
                Me.Hide()
                MDIParent1.Show()
            Else
                MsgBox("Usuario o contraseña incorrectos", MsgBoxStyle.Information, "Entrada Incorrecta")
            End If

        Catch ex As Exception
            MsgBox("No se pudo conectar con el servidor de loterías." & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Error de Conexión")
        End Try
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub

    Private Sub EjecutarControlVersionesBD()
        ' 1. Leemos la versión real configurada en las propiedades de este ejecutable local
        Dim vLocal As Version = My.Application.Info.Version

        ' 2. Consultamos la base de datos de Aiven a través del Negocio
        Dim resultado = vNegocio.ComprobarVersionSistema(vLocal)

        ' 3. Si se detecta un salto de versión en internet
        If resultado.ExisteCambio Then
            ' Mantenemos tu lógica: si es obligatoria o si el usuario acepta la voluntaria
            Dim procederDescarga As Boolean = False

            If resultado.EsObligatoria Then
                MessageBox.Show(
                    $"[ACTUALIZACIÓN CRÍTICA OBLIGATORIA]" & vbCrLf & vbCrLf &
                    $"Tu versión actual (v{vLocal}) ya no es compatible con las reglas contables vigentes." & vbCrLf &
                    $"Es obligatorio instalar la nueva versión (v{resultado.VersionNueva})." & vbCrLf & vbCrLf &
                    $"El sistema se cerrará para aplicar los nuevos archivos automáticamente.",
                    "Actualización Obligatoria", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                procederDescarga = True
            Else
                Dim respuesta As DialogResult = MessageBox.Show(
                    $"¡Nueva mejora disponible de AppLoteria (v{resultado.VersionNueva})!" & vbCrLf & vbCrLf &
                    "¿Deseas instalar la actualización de forma automática ahora mismo?",
                    "Mejora Disponible", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If respuesta = DialogResult.Yes Then
                    procederDescarga = True
                End If
            End If

            ' 🔥 DISPARO DEL MOTOR DE ACTUALIZACIÓN AUTOMÁTICA
            If procederDescarga Then
                Try
                    ' Calculamos la ruta física donde se encuentra el actualizador en la carpeta local
                    Dim rutaActualizador As String = System.IO.Path.Combine(Application.StartupPath, "Actualizador.exe")

                    If System.IO.File.Exists(rutaActualizador) Then
                        ' Construimos los argumentos esperados por tu Form1_Load: 
                        ' Argumento 1: Url de descarga del ZIP en GitHub
                        ' Argumento 2: Nombre de este binario para que sepa a quién reiniciar ("AppLoteria")
                        Dim argumentos As String = $"""{resultado.UrlZip}"" ""AppLoteria"""

                        ' Arrancamos el proceso del instalador de forma totalmente independiente
                        Dim infoInicio As New System.Diagnostics.ProcessStartInfo(rutaActualizador, argumentos)
                        System.Diagnostics.Process.Start(infoInicio)

                        ' 🚀 CIERRE LIMPIO DE CONEXIONES Y LIBERACIÓN TOTAL DE MEMORIA
                        ' Cerramos el formulario actual liberando los componentes gráficos
                        Me.Close()

                        ' Ordenamos a la aplicación finalizar descargando el pool de hilos de Aiven MySQL
                        Application.Exit()
                        Exit Sub
                    Else
                        ' Si el actualizador no existe, usamos tu plan de contingencia del navegador
                        MessageBox.Show("No se encontró el archivo 'Actualizador.exe'. Se procederá a la descarga manual desde el navegador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        System.Diagnostics.Process.Start(resultado.UrlZip)
                        Application.Exit()
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error al lanzar el actualizador automático: " & ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

End Class