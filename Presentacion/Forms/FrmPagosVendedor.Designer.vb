<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPagosVendedor
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPagosVendedor))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnSalir = New System.Windows.Forms.Button()
        Me.LblEjercicio = New System.Windows.Forms.Label()
        Me.DgvDetallePagos = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.RbtnBanco = New System.Windows.Forms.RadioButton()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.RbtnEfectivo = New System.Windows.Forms.RadioButton()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.BtnCancelar = New System.Windows.Forms.Button()
        Me.TxtEntregaParcial = New System.Windows.Forms.TextBox()
        Me.TxtDeuda = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtIdVendor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.BtnAñadir = New System.Windows.Forms.Button()
        Me.BtnAbrirVendedores = New System.Windows.Forms.Button()
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.TxtApellidos = New System.Windows.Forms.TextBox()
        Me.BtnRecargar = New System.Windows.Forms.Button()
        Me.DgvVendedores = New System.Windows.Forms.DataGridView()
        Me.LblCodigo = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.DgvDetallePagos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pagos multisorteo"
        '
        'BtnSalir
        '
        Me.BtnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSalir.Location = New System.Drawing.Point(1026, 12)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(90, 30)
        Me.BtnSalir.TabIndex = 1
        Me.BtnSalir.Text = "  Salir"
        Me.BtnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'LblEjercicio
        '
        Me.LblEjercicio.AutoSize = True
        Me.LblEjercicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEjercicio.Location = New System.Drawing.Point(243, 12)
        Me.LblEjercicio.Name = "LblEjercicio"
        Me.LblEjercicio.Size = New System.Drawing.Size(92, 29)
        Me.LblEjercicio.TabIndex = 2
        Me.LblEjercicio.Text = "Label2"
        '
        'DgvDetallePagos
        '
        Me.DgvDetallePagos.AllowUserToAddRows = False
        Me.DgvDetallePagos.AllowUserToDeleteRows = False
        Me.DgvDetallePagos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvDetallePagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvDetallePagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDetallePagos.Location = New System.Drawing.Point(281, 174)
        Me.DgvDetallePagos.MultiSelect = False
        Me.DgvDetallePagos.Name = "DgvDetallePagos"
        Me.DgvDetallePagos.RowHeadersWidth = 51
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DgvDetallePagos.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvDetallePagos.RowTemplate.Height = 24
        Me.DgvDetallePagos.Size = New System.Drawing.Size(835, 281)
        Me.DgvDetallePagos.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.BtnGuardar)
        Me.Panel1.Controls.Add(Me.BtnCancelar)
        Me.Panel1.Controls.Add(Me.TxtEntregaParcial)
        Me.Panel1.Controls.Add(Me.TxtDeuda)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TxtIdVendor)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(18, 173)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(257, 282)
        Me.Panel1.TabIndex = 4
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.RbtnBanco)
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Controls.Add(Me.RbtnEfectivo)
        Me.Panel4.Location = New System.Drawing.Point(24, 91)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(116, 89)
        Me.Panel4.TabIndex = 42
        '
        'RbtnBanco
        '
        Me.RbtnBanco.AutoSize = True
        Me.RbtnBanco.Location = New System.Drawing.Point(14, 61)
        Me.RbtnBanco.Name = "RbtnBanco"
        Me.RbtnBanco.Size = New System.Drawing.Size(69, 21)
        Me.RbtnBanco.TabIndex = 38
        Me.RbtnBanco.Text = "Banco"
        Me.RbtnBanco.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(11, 6)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(99, 17)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Modo de pago"
        '
        'RbtnEfectivo
        '
        Me.RbtnEfectivo.AutoSize = True
        Me.RbtnEfectivo.Checked = True
        Me.RbtnEfectivo.Location = New System.Drawing.Point(14, 34)
        Me.RbtnEfectivo.Name = "RbtnEfectivo"
        Me.RbtnEfectivo.Size = New System.Drawing.Size(79, 21)
        Me.RbtnEfectivo.TabIndex = 37
        Me.RbtnEfectivo.TabStop = True
        Me.RbtnEfectivo.Text = "Efectivo"
        Me.RbtnEfectivo.UseVisualStyleBackColor = True
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnGuardar.Image = Global.Presentacion.My.Resources.Resources.luzverde
        Me.BtnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnGuardar.Location = New System.Drawing.Point(141, 241)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(104, 30)
        Me.BtnGuardar.TabIndex = 18
        Me.BtnGuardar.Text = "Guardar"
        Me.BtnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnCancelar.Image = Global.Presentacion.My.Resources.Resources.undo16
        Me.BtnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancelar.Location = New System.Drawing.Point(15, 241)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(104, 30)
        Me.BtnCancelar.TabIndex = 17
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnCancelar.UseVisualStyleBackColor = True
        '
        'TxtEntregaParcial
        '
        Me.TxtEntregaParcial.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtEntregaParcial.Location = New System.Drawing.Point(141, 192)
        Me.TxtEntregaParcial.Name = "TxtEntregaParcial"
        Me.TxtEntregaParcial.Size = New System.Drawing.Size(67, 22)
        Me.TxtEntregaParcial.TabIndex = 14
        '
        'TxtDeuda
        '
        Me.TxtDeuda.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtDeuda.Location = New System.Drawing.Point(125, 46)
        Me.TxtDeuda.Name = "TxtDeuda"
        Me.TxtDeuda.ReadOnly = True
        Me.TxtDeuda.Size = New System.Drawing.Size(67, 22)
        Me.TxtDeuda.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Id Vendedor"
        '
        'TxtIdVendor
        '
        Me.TxtIdVendor.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtIdVendor.Location = New System.Drawing.Point(125, 18)
        Me.TxtIdVendor.Name = "TxtIdVendor"
        Me.TxtIdVendor.ReadOnly = True
        Me.TxtIdVendor.Size = New System.Drawing.Size(67, 22)
        Me.TxtIdVendor.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(32, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 17)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Total Deuda"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 195)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 17)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Entrega parcial"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Entrada de datos"
        '
        'TxtBuscar
        '
        Me.TxtBuscar.Location = New System.Drawing.Point(811, 48)
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(305, 22)
        Me.TxtBuscar.TabIndex = 21
        Me.TxtBuscar.Visible = False
        '
        'BtnAñadir
        '
        Me.BtnAñadir.Image = Global.Presentacion.My.Resources.Resources.InsertFila1
        Me.BtnAñadir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAñadir.Location = New System.Drawing.Point(283, 129)
        Me.BtnAñadir.Name = "BtnAñadir"
        Me.BtnAñadir.Size = New System.Drawing.Size(140, 30)
        Me.BtnAñadir.TabIndex = 18
        Me.BtnAñadir.Text = "   Añadir pago"
        Me.BtnAñadir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnAñadir.UseVisualStyleBackColor = True
        Me.BtnAñadir.Visible = False
        '
        'BtnAbrirVendedores
        '
        Me.BtnAbrirVendedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnAbrirVendedores.Location = New System.Drawing.Point(616, 46)
        Me.BtnAbrirVendedores.Name = "BtnAbrirVendedores"
        Me.BtnAbrirVendedores.Size = New System.Drawing.Size(190, 30)
        Me.BtnAbrirVendedores.TabIndex = 23
        Me.BtnAbrirVendedores.Text = "Seleccionar Vendedor"
        Me.BtnAbrirVendedores.UseVisualStyleBackColor = True
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(538, 129)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(177, 22)
        Me.TxtNombre.TabIndex = 24
        '
        'TxtApellidos
        '
        Me.TxtApellidos.Location = New System.Drawing.Point(721, 129)
        Me.TxtApellidos.Name = "TxtApellidos"
        Me.TxtApellidos.Size = New System.Drawing.Size(359, 22)
        Me.TxtApellidos.TabIndex = 25
        '
        'BtnRecargar
        '
        Me.BtnRecargar.Image = CType(resources.GetObject("BtnRecargar.Image"), System.Drawing.Image)
        Me.BtnRecargar.Location = New System.Drawing.Point(1086, 125)
        Me.BtnRecargar.Name = "BtnRecargar"
        Me.BtnRecargar.Size = New System.Drawing.Size(30, 30)
        Me.BtnRecargar.TabIndex = 26
        Me.BtnRecargar.Text = "Button1"
        Me.BtnRecargar.UseVisualStyleBackColor = True
        '
        'DgvVendedores
        '
        Me.DgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVendedores.Location = New System.Drawing.Point(616, 77)
        Me.DgvVendedores.Name = "DgvVendedores"
        Me.DgvVendedores.RowHeadersWidth = 51
        Me.DgvVendedores.RowTemplate.Height = 24
        Me.DgvVendedores.Size = New System.Drawing.Size(500, 28)
        Me.DgvVendedores.TabIndex = 27
        Me.DgvVendedores.Visible = False
        '
        'LblCodigo
        '
        Me.LblCodigo.AutoSize = True
        Me.LblCodigo.Enabled = False
        Me.LblCodigo.Location = New System.Drawing.Point(480, 132)
        Me.LblCodigo.Name = "LblCodigo"
        Me.LblCodigo.Size = New System.Drawing.Size(52, 17)
        Me.LblCodigo.TabIndex = 28
        Me.LblCodigo.Text = "Código"
        '
        'FrmPagosVendedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1143, 478)
        Me.Controls.Add(Me.LblCodigo)
        Me.Controls.Add(Me.DgvVendedores)
        Me.Controls.Add(Me.BtnRecargar)
        Me.Controls.Add(Me.TxtApellidos)
        Me.Controls.Add(Me.TxtNombre)
        Me.Controls.Add(Me.BtnAbrirVendedores)
        Me.Controls.Add(Me.TxtBuscar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnAñadir)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblEjercicio)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DgvDetallePagos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPagosVendedor"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pagos de Vendedor multisorteo"
        CType(Me.DgvDetallePagos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents BtnSalir As Button
    Friend WithEvents LblEjercicio As Label
    Friend WithEvents DgvDetallePagos As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TxtEntregaParcial As TextBox
    Friend WithEvents TxtDeuda As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtIdVendor As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnGuardar As Button
    Friend WithEvents BtnCancelar As Button
    Friend WithEvents BtnAñadir As Button
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents BtnAbrirVendedores As Button
    Friend WithEvents TxtNombre As TextBox
    Friend WithEvents TxtApellidos As TextBox
    Friend WithEvents BtnRecargar As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents RbtnBanco As RadioButton
    Friend WithEvents Label20 As Label
    Friend WithEvents RbtnEfectivo As RadioButton
    Friend WithEvents DgvVendedores As DataGridView
    Friend WithEvents LblCodigo As Label
    Friend WithEvents HelpProvider1 As HelpProvider
End Class
