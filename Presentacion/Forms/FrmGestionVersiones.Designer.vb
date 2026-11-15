<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGestionVersiones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGestionVersiones))
        Me.TxtNuevaVersion = New System.Windows.Forms.TextBox()
        Me.TxtUrlZip = New System.Windows.Forms.TextBox()
        Me.ChkObligatoria = New System.Windows.Forms.CheckBox()
        Me.BtnPublicar = New System.Windows.Forms.Button()
        Me.DgvHistorialVersiones = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnSalir = New System.Windows.Forms.Button()
        CType(Me.DgvHistorialVersiones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtNuevaVersion
        '
        Me.TxtNuevaVersion.Location = New System.Drawing.Point(117, 70)
        Me.TxtNuevaVersion.Name = "TxtNuevaVersion"
        Me.TxtNuevaVersion.Size = New System.Drawing.Size(71, 22)
        Me.TxtNuevaVersion.TabIndex = 0
        '
        'TxtUrlZip
        '
        Me.TxtUrlZip.Location = New System.Drawing.Point(244, 70)
        Me.TxtUrlZip.Name = "TxtUrlZip"
        Me.TxtUrlZip.Size = New System.Drawing.Size(593, 22)
        Me.TxtUrlZip.TabIndex = 1
        '
        'ChkObligatoria
        '
        Me.ChkObligatoria.AutoSize = True
        Me.ChkObligatoria.Location = New System.Drawing.Point(878, 72)
        Me.ChkObligatoria.Name = "ChkObligatoria"
        Me.ChkObligatoria.Size = New System.Drawing.Size(99, 21)
        Me.ChkObligatoria.TabIndex = 2
        Me.ChkObligatoria.Text = "Obligatoria"
        Me.ChkObligatoria.UseVisualStyleBackColor = True
        '
        'BtnPublicar
        '
        Me.BtnPublicar.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnPublicar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnPublicar.Location = New System.Drawing.Point(1010, 64)
        Me.BtnPublicar.Name = "BtnPublicar"
        Me.BtnPublicar.Size = New System.Drawing.Size(90, 35)
        Me.BtnPublicar.TabIndex = 3
        Me.BtnPublicar.Text = "Publicar"
        Me.BtnPublicar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnPublicar.UseVisualStyleBackColor = True
        '
        'DgvHistorialVersiones
        '
        Me.DgvHistorialVersiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvHistorialVersiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistorialVersiones.Location = New System.Drawing.Point(27, 105)
        Me.DgvHistorialVersiones.Name = "DgvHistorialVersiones"
        Me.DgvHistorialVersiones.RowHeadersWidth = 51
        Me.DgvHistorialVersiones.RowTemplate.Height = 24
        Me.DgvHistorialVersiones.Size = New System.Drawing.Size(1073, 180)
        Me.DgvHistorialVersiones.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(185, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Gestion versiones"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Núm versión"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(214, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Url"
        '
        'BtnSalir
        '
        Me.BtnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSalir.Location = New System.Drawing.Point(1010, 294)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(90, 35)
        Me.BtnSalir.TabIndex = 8
        Me.BtnSalir.Text = "Salir  "
        Me.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'FrmGestionVersiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 339)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DgvHistorialVersiones)
        Me.Controls.Add(Me.BtnPublicar)
        Me.Controls.Add(Me.ChkObligatoria)
        Me.Controls.Add(Me.TxtUrlZip)
        Me.Controls.Add(Me.TxtNuevaVersion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmGestionVersiones"
        Me.Text = "Control de versiones"
        CType(Me.DgvHistorialVersiones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtNuevaVersion As TextBox
    Friend WithEvents TxtUrlZip As TextBox
    Friend WithEvents ChkObligatoria As CheckBox
    Friend WithEvents BtnPublicar As Button
    Friend WithEvents DgvHistorialVersiones As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnSalir As Button
End Class
