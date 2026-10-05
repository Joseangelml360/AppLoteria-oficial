Imports System.Windows.Forms

Public Class MDIParent1

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Extraemos el icono de la aplicación para el formulario
        Me.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)

        ' 2. 🔥 CARGA DE DATOS EN LA BARRA Strip MODERNA
        ' Etiqueta izquierda (Índice 0): Muestra la fecha y hora actual de la sesión
        Me.ToolStripStatusLabel1.Text = "Fecha: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm")

        ' Etiqueta derecha (Índice 1): Al tener Spring=True, se alineará a la derecha
        ' Usamos tus variables globales (user y cRol) que capturamos en el formulario de LogIn
        Me.ToolStripStatusLabel2.Text = $"Usuario: {user} ({cRol})"
        Me.ToolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right

        ' 3. 🔒 SISTEMA DE ESCUDO CONTRA ACCESOS (Tu lógica de negocio basada en Select Case)
        ' Si el usuario no es Administrador Supremo (Rol <> 1), le bloqueamos tu nuevo panel de versiones
        ' Asegúrate de que el nombre coincida con tu botón del menú de versiones
        If nRol <> 1 Then
            VersionToolStripMenuItem.Enabled = False
            VersionToolStripMenuItem.Visible = False ' Lo ocultamos para mayor privacidad
        End If

        ' Evaluamos los accesos del resto de delegaciones de lotería
        Select Case nRol
            Case 3 ' Rol de Tesorero de la Falla
                ' Bloqueamos o habilitamos según vuestras directrices de lotería
                ApuntesToolStripMenuItem.Enabled = True
                LiquidaciónToolStripMenuItem.Enabled = False

            Case 4 ' Lotero Administrativo estándar
                ' Solo puede picar datos diarios, no ver balances finales ni configuraciones
                VendedoresToolStripMenuItem.Enabled = True
                LibroDeCajaToolStripMenuItem.Enabled = True
                SorteosToolStripMenuItem.Enabled = True
                AdministracionesToolStripMenuItem.Enabled = True

            Case 2 ' Rol de consulta o Secretaría-Loterías
                ' Solo modo lectura preventiva
                DetalleVendedorToolStripMenuItem.Enabled = False
                LibroDeCajaToolStripMenuItem.Enabled = False
                SorteosToolStripMenuItem.Enabled = False
                AdministracionesToolStripMenuItem.Enabled = False
                VendedoresToolStripMenuItem1.Enabled = True

            Case 5 ' Visita o Auditor externo
                ' Capado por completo, solo ve su ficha
                VendedoresToolStripMenuItem.Enabled = False
                LibroDeCajaToolStripMenuItem.Enabled = False
                SorteosToolStripMenuItem.Enabled = False
                AdministracionesToolStripMenuItem.Enabled = False
        End Select
        StatusStrip.Update()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Utilice My.Computer.Clipboard.GetText() o My.Computer.Clipboard.GetData para recuperar la información del Portapapeles.
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Cierre todos los formularios secundarios del principal.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub VendedoresToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles VendedoresToolStripMenuItem1.Click
        Dim frm As New FrmVendedores With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub DetalleVendedorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetalleVendedorToolStripMenuItem.Click
        Dim frm As New FrmDetalleVentasLoteria With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub NuevoSorteoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem.Click
        Dim frm As New FrmSorteos With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub TiposDeSorteoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TiposDeSorteoToolStripMenuItem.Click
        Dim frm As New FrmTipoSorteo With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub ApuntesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApuntesToolStripMenuItem.Click
        Dim frm As New FrmLibroCaja With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub LiquidaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiquidaciónToolStripMenuItem.Click
        Dim frm As New FrmLiquidacion With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub Administraciones_Click(sender As Object, e As EventArgs) Handles Administraciones.Click
        Dim frm As New FrmAdministraciones With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub DetallesAdmon_Click(sender As Object, e As EventArgs) Handles DetallesAdmon.Click
        Dim frm As New FrmDetallesAdmon With {.MdiParent = Me}
        frm.Show()
    End Sub

    Private Sub CerrarAplicaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarAplicaciónToolStripMenuItem.Click
        End
        Me.Close()

    End Sub

    Private Sub VersionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionToolStripMenuItem.Click
        Dim frm As New FrmGestionVersiones With {.MdiParent = Me}
        frm.Show()
    End Sub
End Class
