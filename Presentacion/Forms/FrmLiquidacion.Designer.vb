<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLiquidacion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CboEjercicios = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnVerTodos = New System.Windows.Forms.Button()
        Me.LblEjercicio = New System.Windows.Forms.Label()
        Me.DgvLiquidacion = New System.Windows.Forms.DataGridView()
        CType(Me.DgvLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Liquidación ejercicio"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.Button1.Location = New System.Drawing.Point(1150, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 30)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Salir"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CboEjercicios
        '
        Me.CboEjercicios.FormattingEnabled = True
        Me.CboEjercicios.Location = New System.Drawing.Point(561, 16)
        Me.CboEjercicios.Name = "CboEjercicios"
        Me.CboEjercicios.Size = New System.Drawing.Size(121, 24)
        Me.CboEjercicios.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(422, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Selecciona ejercicio"
        '
        'BtnVerTodos
        '
        Me.BtnVerTodos.Image = Global.Presentacion.My.Resources.Resources.eye
        Me.BtnVerTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnVerTodos.Location = New System.Drawing.Point(752, 12)
        Me.BtnVerTodos.Name = "BtnVerTodos"
        Me.BtnVerTodos.Size = New System.Drawing.Size(119, 30)
        Me.BtnVerTodos.TabIndex = 4
        Me.BtnVerTodos.Text = "Ver todos"
        Me.BtnVerTodos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnVerTodos.UseVisualStyleBackColor = True
        '
        'LblEjercicio
        '
        Me.LblEjercicio.AutoSize = True
        Me.LblEjercicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEjercicio.Location = New System.Drawing.Point(239, 13)
        Me.LblEjercicio.Name = "LblEjercicio"
        Me.LblEjercicio.Size = New System.Drawing.Size(77, 25)
        Me.LblEjercicio.TabIndex = 5
        Me.LblEjercicio.Text = "Label3"
        '
        'DgvLiquidacion
        '
        Me.DgvLiquidacion.AllowUserToAddRows = False
        Me.DgvLiquidacion.AllowUserToDeleteRows = False
        Me.DgvLiquidacion.AllowUserToResizeColumns = False
        Me.DgvLiquidacion.AllowUserToResizeRows = False
        Me.DgvLiquidacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvLiquidacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvLiquidacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvLiquidacion.Location = New System.Drawing.Point(18, 65)
        Me.DgvLiquidacion.MultiSelect = False
        Me.DgvLiquidacion.Name = "DgvLiquidacion"
        Me.DgvLiquidacion.ReadOnly = True
        Me.DgvLiquidacion.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DgvLiquidacion.RowTemplate.Height = 24
        Me.DgvLiquidacion.Size = New System.Drawing.Size(1212, 166)
        Me.DgvLiquidacion.TabIndex = 6
        '
        'FrmLiquidacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1242, 243)
        Me.Controls.Add(Me.DgvLiquidacion)
        Me.Controls.Add(Me.LblEjercicio)
        Me.Controls.Add(Me.BtnVerTodos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CboEjercicios)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLiquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidación ejercicio"
        CType(Me.DgvLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents CboEjercicios As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnVerTodos As Button
    Friend WithEvents LblEjercicio As Label
    Friend WithEvents DgvLiquidacion As DataGridView
End Class
