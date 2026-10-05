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
        Me.TxtNuevaVersion = New System.Windows.Forms.TextBox()
        Me.TxtUrlZip = New System.Windows.Forms.TextBox()
        Me.ChkObligatoria = New System.Windows.Forms.CheckBox()
        Me.BtnPublicar = New System.Windows.Forms.Button()
        Me.DgvHistorialVersiones = New System.Windows.Forms.DataGridView()
        CType(Me.DgvHistorialVersiones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtNuevaVersion
        '
        Me.TxtNuevaVersion.Location = New System.Drawing.Point(44, 67)
        Me.TxtNuevaVersion.Name = "TxtNuevaVersion"
        Me.TxtNuevaVersion.Size = New System.Drawing.Size(100, 22)
        Me.TxtNuevaVersion.TabIndex = 0
        '
        'TxtUrlZip
        '
        Me.TxtUrlZip.Location = New System.Drawing.Point(198, 67)
        Me.TxtUrlZip.Name = "TxtUrlZip"
        Me.TxtUrlZip.Size = New System.Drawing.Size(100, 22)
        Me.TxtUrlZip.TabIndex = 1
        '
        'ChkObligatoria
        '
        Me.ChkObligatoria.AutoSize = True
        Me.ChkObligatoria.Location = New System.Drawing.Point(44, 115)
        Me.ChkObligatoria.Name = "ChkObligatoria"
        Me.ChkObligatoria.Size = New System.Drawing.Size(100, 21)
        Me.ChkObligatoria.TabIndex = 2
        Me.ChkObligatoria.Text = "CheckBox1"
        Me.ChkObligatoria.UseVisualStyleBackColor = True
        '
        'BtnPublicar
        '
        Me.BtnPublicar.Location = New System.Drawing.Point(641, 115)
        Me.BtnPublicar.Name = "BtnPublicar"
        Me.BtnPublicar.Size = New System.Drawing.Size(75, 23)
        Me.BtnPublicar.TabIndex = 3
        Me.BtnPublicar.Text = "Button1"
        Me.BtnPublicar.UseVisualStyleBackColor = True
        '
        'DgvHistorialVersiones
        '
        Me.DgvHistorialVersiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistorialVersiones.Location = New System.Drawing.Point(44, 193)
        Me.DgvHistorialVersiones.Name = "DgvHistorialVersiones"
        Me.DgvHistorialVersiones.RowHeadersWidth = 51
        Me.DgvHistorialVersiones.RowTemplate.Height = 24
        Me.DgvHistorialVersiones.Size = New System.Drawing.Size(694, 150)
        Me.DgvHistorialVersiones.TabIndex = 4
        '
        'FrmGestionVersiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 406)
        Me.Controls.Add(Me.DgvHistorialVersiones)
        Me.Controls.Add(Me.BtnPublicar)
        Me.Controls.Add(Me.ChkObligatoria)
        Me.Controls.Add(Me.TxtUrlZip)
        Me.Controls.Add(Me.TxtNuevaVersion)
        Me.Name = "FrmGestionVersiones"
        Me.Text = "FrmGestionVersiones"
        CType(Me.DgvHistorialVersiones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtNuevaVersion As TextBox
    Friend WithEvents TxtUrlZip As TextBox
    Friend WithEvents ChkObligatoria As CheckBox
    Friend WithEvents BtnPublicar As Button
    Friend WithEvents DgvHistorialVersiones As DataGridView
End Class
