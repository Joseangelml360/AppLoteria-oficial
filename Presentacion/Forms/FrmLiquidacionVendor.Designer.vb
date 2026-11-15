<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLiquidacionVendor
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnCargar = New System.Windows.Forms.Button()
        Me.TxtCodigo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DgvLiquidacionVendor = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtIdSorteo = New System.Windows.Forms.TextBox()
        CType(Me.DgvLiquidacionVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(713, 415)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Salir"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnCargar
        '
        Me.BtnCargar.Location = New System.Drawing.Point(698, 30)
        Me.BtnCargar.Name = "BtnCargar"
        Me.BtnCargar.Size = New System.Drawing.Size(90, 25)
        Me.BtnCargar.TabIndex = 1
        Me.BtnCargar.Text = "Cargar Datos"
        Me.BtnCargar.UseVisualStyleBackColor = True
        '
        'TxtCodigo
        '
        Me.TxtCodigo.Location = New System.Drawing.Point(258, 30)
        Me.TxtCodigo.Name = "TxtCodigo"
        Me.TxtCodigo.Size = New System.Drawing.Size(119, 22)
        Me.TxtCodigo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(201, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Código"
        '
        'DgvLiquidacionVendor
        '
        Me.DgvLiquidacionVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvLiquidacionVendor.Location = New System.Drawing.Point(12, 89)
        Me.DgvLiquidacionVendor.Name = "DgvLiquidacionVendor"
        Me.DgvLiquidacionVendor.RowHeadersWidth = 51
        Me.DgvLiquidacionVendor.RowTemplate.Height = 24
        Me.DgvLiquidacionVendor.Size = New System.Drawing.Size(776, 320)
        Me.DgvLiquidacionVendor.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(424, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Sorteo"
        '
        'TxtIdSorteo
        '
        Me.TxtIdSorteo.Location = New System.Drawing.Point(481, 35)
        Me.TxtIdSorteo.Name = "TxtIdSorteo"
        Me.TxtIdSorteo.Size = New System.Drawing.Size(119, 22)
        Me.TxtIdSorteo.TabIndex = 5
        '
        'FrmLiquidacionVendor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtIdSorteo)
        Me.Controls.Add(Me.DgvLiquidacionVendor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtCodigo)
        Me.Controls.Add(Me.BtnCargar)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FrmLiquidacionVendor"
        Me.Text = "FrmLiquidacionVendor"
        CType(Me.DgvLiquidacionVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents BtnCargar As Button
    Friend WithEvents TxtCodigo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DgvLiquidacionVendor As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtIdSorteo As TextBox
End Class
