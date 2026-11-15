<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmResumenVendedor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmResumenVendedor))
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.DgvVendedores = New System.Windows.Forms.DataGridView()
        Me.TxtCodVendor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNomVendor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtApeVendor = New System.Windows.Forms.TextBox()
        Me.BtnSelectVendor = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblEjercicio = New System.Windows.Forms.Label()
        Me.TxtAsignada = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSaldo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtVendida = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtBeneficios = New System.Windows.Forms.TextBox()
        Me.DgvSorteosVendor = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnSalir = New System.Windows.Forms.Button()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSorteosVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtBuscar
        '
        Me.TxtBuscar.Location = New System.Drawing.Point(492, 20)
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(203, 22)
        Me.TxtBuscar.TabIndex = 0
        '
        'DgvVendedores
        '
        Me.DgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVendedores.Location = New System.Drawing.Point(224, 50)
        Me.DgvVendedores.Name = "DgvVendedores"
        Me.DgvVendedores.RowHeadersWidth = 51
        Me.DgvVendedores.RowTemplate.Height = 24
        Me.DgvVendedores.Size = New System.Drawing.Size(471, 33)
        Me.DgvVendedores.TabIndex = 1
        '
        'TxtCodVendor
        '
        Me.TxtCodVendor.Location = New System.Drawing.Point(27, 115)
        Me.TxtCodVendor.Name = "TxtCodVendor"
        Me.TxtCodVendor.Size = New System.Drawing.Size(63, 22)
        Me.TxtCodVendor.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Código"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(119, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Nombre"
        '
        'TxtNomVendor
        '
        Me.TxtNomVendor.Location = New System.Drawing.Point(119, 115)
        Me.TxtNomVendor.Name = "TxtNomVendor"
        Me.TxtNomVendor.Size = New System.Drawing.Size(157, 22)
        Me.TxtNomVendor.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(316, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Apellidos"
        '
        'TxtApeVendor
        '
        Me.TxtApeVendor.Location = New System.Drawing.Point(316, 115)
        Me.TxtApeVendor.Name = "TxtApeVendor"
        Me.TxtApeVendor.Size = New System.Drawing.Size(297, 22)
        Me.TxtApeVendor.TabIndex = 7
        '
        'BtnSelectVendor
        '
        Me.BtnSelectVendor.Location = New System.Drawing.Point(316, 12)
        Me.BtnSelectVendor.Name = "BtnSelectVendor"
        Me.BtnSelectVendor.Size = New System.Drawing.Size(170, 30)
        Me.BtnSelectVendor.TabIndex = 11
        Me.BtnSelectVendor.Text = "Seleccionar Vendedor"
        Me.BtnSelectVendor.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Resumen Vendedor"
        '
        'LblEjercicio
        '
        Me.LblEjercicio.AutoSize = True
        Me.LblEjercicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEjercicio.Location = New System.Drawing.Point(33, 50)
        Me.LblEjercicio.Name = "LblEjercicio"
        Me.LblEjercicio.Size = New System.Drawing.Size(57, 17)
        Me.LblEjercicio.TabIndex = 13
        Me.LblEjercicio.Text = "Label5"
        '
        'TxtAsignada
        '
        Me.TxtAsignada.Location = New System.Drawing.Point(25, 177)
        Me.TxtAsignada.Name = "TxtAsignada"
        Me.TxtAsignada.Size = New System.Drawing.Size(100, 22)
        Me.TxtAsignada.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 17)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Lotería asignada"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(321, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 17)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Saldo"
        '
        'TxtSaldo
        '
        Me.TxtSaldo.Location = New System.Drawing.Point(324, 177)
        Me.TxtSaldo.Name = "TxtSaldo"
        Me.TxtSaldo.Size = New System.Drawing.Size(100, 22)
        Me.TxtSaldo.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(173, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 17)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Lotería vendida"
        '
        'TxtVendida
        '
        Me.TxtVendida.Location = New System.Drawing.Point(176, 177)
        Me.TxtVendida.Name = "TxtVendida"
        Me.TxtVendida.Size = New System.Drawing.Size(100, 22)
        Me.TxtVendida.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(470, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 17)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Beneficios"
        '
        'TxtBeneficios
        '
        Me.TxtBeneficios.Location = New System.Drawing.Point(473, 177)
        Me.TxtBeneficios.Name = "TxtBeneficios"
        Me.TxtBeneficios.Size = New System.Drawing.Size(100, 22)
        Me.TxtBeneficios.TabIndex = 20
        '
        'DgvSorteosVendor
        '
        Me.DgvSorteosVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSorteosVendor.Location = New System.Drawing.Point(28, 228)
        Me.DgvSorteosVendor.Name = "DgvSorteosVendor"
        Me.DgvSorteosVendor.RowHeadersWidth = 51
        Me.DgvSorteosVendor.RowTemplate.Height = 24
        Me.DgvSorteosVendor.Size = New System.Drawing.Size(1154, 294)
        Me.DgvSorteosVendor.TabIndex = 22
        '
        'Button1
        '
        Me.Button1.Image = Global.Presentacion.My.Resources.Resources.DocumentoAzul
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(839, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(148, 35)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "Imprimir Informe"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnSalir
        '
        Me.BtnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSalir.Location = New System.Drawing.Point(1092, 11)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(90, 35)
        Me.BtnSalir.TabIndex = 10
        Me.BtnSalir.Text = "Salir   "
        Me.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'FrmResumenVendedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 534)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DgvSorteosVendor)
        Me.Controls.Add(Me.LblEjercicio)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnSelectVendor)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.DgvVendedores)
        Me.Controls.Add(Me.TxtBuscar)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtBeneficios)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtVendida)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSaldo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtAsignada)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtApeVendor)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtNomVendor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtCodVendor)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmResumenVendedor"
        Me.Text = "Informe resumen de Venedor"
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvSorteosVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvVendedores As DataGridView
    Friend WithEvents TxtCodVendor As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtNomVendor As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtApeVendor As TextBox
    Friend WithEvents BtnSalir As Button
    Friend WithEvents BtnSelectVendor As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LblEjercicio As Label
    Friend WithEvents TxtAsignada As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSaldo As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtVendida As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtBeneficios As TextBox
    Friend WithEvents DgvSorteosVendor As DataGridView
    Friend WithEvents Button1 As Button
End Class
