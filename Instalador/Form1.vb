Imports System.IO
Imports System.IO.Compression
Imports System.Net.Http
Imports System.Threading

Public Class Form1
    ' Almacenará la URL de GitHub que le pasa la app principal
    Private urlDescarga As String = ""

    ' 🔥 REPARACIÓN CRÍTICA: Cambiamos la Const por una Variable Dinámica.
    ' Mantenemos por defecto "AppGestion.exe" por compatibilidad con tu Secretaría vieja.
    Private appPrincipal As String = "AppGestion.exe"
    Private Const NombreZip As String = "update.zip"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Capturar los argumentos que le envía el LogIn de cualquier app
        Dim argumentos As String() = Environment.GetCommandLineArgs()

        ' El argumento 0 siempre es la ruta del propio ejecutable, el 1 es el parámetro real (URL)
        If argumentos.Length > 1 Then
            urlDescarga = argumentos(1)

            ' 🔥 DETECCIÓN INTELIGENTE: Si nos pasan un segundo argumento real (ej: "AppLoteria")
            ' lo asimilamos dinámicamente y calculamos el nombre real de su ejecutable.
            If argumentos.Length >= 3 Then
                Dim nombreLimpio As String = argumentos(2).Replace(".exe", "").Trim()
                appPrincipal = nombreLimpio & ".exe"
            Else
                appPrincipal = "AppGestion.exe"
            End If

            ' Iniciamos el proceso en un hilo secundario para no congelar la pantalla/ProgressBar
            Dim hiloActualizacion As New Thread(AddressOf IniciarActualizacion)
            hiloActualizacion.Start()
        Else
            MessageBox.Show("Este actualizador no se puede ejecutar directamente de forma manual.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End If
    End Sub

    Private Async Sub IniciarActualizacion()
        Try
            ' 2. Esperar 6 segundos para garantizar que el programa padre liberó sus archivos en Windows
            ActualizarEstado("Esperando que la aplicación principal se cierre...", 10)
            Thread.Sleep(6000)

            ' Ruta de la carpeta actual donde están ambos ejecutables
            Dim rutaInstalacion As String = AppDomain.CurrentDomain.BaseDirectory
            Dim rutaCompletaZip As String = Path.Combine(rutaInstalacion, NombreZip)

            ' Eliminar el ZIP anterior si quedó alguno huérfano por error
            If File.Exists(rutaCompletaZip) Then File.Delete(rutaCompletaZip)

            ' 3. Descargar el archivo .zip de GitHub Releases
            ActualizarEstado("Descargando actualización desde GitHub...", 40)
            Using cliente As New HttpClient()
                ' Forzamos la cabecera User-Agent que exige el servidor cloud de GitHub
                cliente.DefaultRequestHeaders.Add("User-Agent", "AppUniversal-Updater")

                Dim bytesZip As Byte() = Await cliente.GetByteArrayAsync(urlDescarga)
                File.WriteAllBytes(rutaCompletaZip, bytesZip)
            End Using

            ' ====================================================================
            ' 4. Extraer el archivo .zip sobreescribiendo los ficheros actuales
            ' ====================================================================
            ActualizarEstado("Instalando nuevos archivos...", 70)

            Using archivoZip As ZipArchive = ZipFile.OpenRead(rutaCompletaZip)
                For Each elemento As ZipArchiveEntry In archivoZip.Entries
                    ' Calculamos la ruta final de cada archivo en la carpeta
                    Dim rutaDestinoArchivo As String = Path.Combine(rutaInstalacion, elemento.FullName)

                    ' ESCUDO DE SEGURIDAD ABSOLUTO: Evita machacarse a sí mismo en la RAM
                    If elemento.Name.ToLower() = "actualizador.exe" Then
                        Continue For
                    End If

                    ' Si es una carpeta dentro del ZIP, la creamos
                    If elemento.FullName.EndsWith("/") OrElse elemento.FullName.EndsWith("\") Then
                        Directory.CreateDirectory(rutaDestinoArchivo)
                    Else
                        ' Aseguramos que la subcarpeta destino exista
                        Dim directorioPadre As String = Path.GetDirectoryName(rutaDestinoArchivo)
                        If Not Directory.Exists(directorioPadre) Then Directory.CreateDirectory(directorioPadre)

                        ' Sobreescribimos el archivo antiguo
                        elemento.ExtractToFile(rutaDestinoArchivo, overwrite:=True)
                    End If
                Next
            End Using

            ' ====================================================================
            ' 5. Limpieza del archivo temporal .zip
            ' ====================================================================
            ActualizarEstado("Finalizando instalación...", 90)

            ' Le damos 1 segundo de respiro al procesador
            Thread.Sleep(1000)

            ' MEJORA DE INTEGRIDAD: Envolvemos la eliminación en un Try-Catch propio y aislado
            Try
                If File.Exists(rutaCompletaZip) Then
                    File.Delete(rutaCompletaZip)
                End If
            Catch ex As Exception
                ' Falla silenciosa permitida por el sistema operativo
            End Try

            Thread.Sleep(500)

            ' ====================================================================
            ' 6. Volver a arrancar la aplicación principal ya actualizada
            ' ====================================================================
            ActualizarEstado("Reiniciando aplicación...", 100)

            ' Delegamos de forma segura la reapertura y el cierre al hilo principal
            Me.Invoke(Sub()
                          ' 🔥 DINÁMICO: Arrancamos la aplicación correcta usando la variable reparada
                          Process.Start(Path.Combine(rutaInstalacion, appPrincipal))

                          ' Pequeña pausa para asegurar la estabilidad del hilo de Windows
                          Thread.Sleep(100)

                          ' Cerramos definitivamente el actualizador liberando la RAM
                          Me.Close()
                      End Sub)

        Catch ex As Exception
            ' Este bloque general captura errores reales de la descarga o la extracción
            Me.Invoke(Sub()
                          MessageBox.Show("Error durante la actualización automática: " & ex.Message,
                                          "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error)
                          Me.Close()
                      End Sub)
        End Try
    End Sub

    ' Método auxiliar para actualizar la interfaz visual de forma segura entre hilos (Thread-Safe)
    Private Sub ActualizarEstado(texto As String, progreso As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() ActualizarEstado(texto, progreso))
        Else
            Label1.Text = texto
            ProgressBar1.Value = progreso
        End If
    End Sub
End Class