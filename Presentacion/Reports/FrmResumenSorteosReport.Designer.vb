<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmResumenSorteosReport
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
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.PanelFiltroDatos = New System.Windows.Forms.Panel()
        Me.TxtCodigoVendor = New System.Windows.Forms.TextBox()
        Me.DgvVendedores = New System.Windows.Forms.DataGridView()
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtBeneficios = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtVendida = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSaldo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtAsignada = New System.Windows.Forms.TextBox()
        Me.BtnSalir = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtVendedor = New System.Windows.Forms.TextBox()
        Me.RbtnVendedor = New System.Windows.Forms.RadioButton()
        Me.RbtnGlobal = New System.Windows.Forms.RadioButton()
        Me.PanelVisorReport = New System.Windows.Forms.Panel()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.PanelFiltroDatos.SuspendLayout()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(396, 246)
        Me.ReportViewer1.TabIndex = 0
        '
        'PanelFiltroDatos
        '
        Me.PanelFiltroDatos.Controls.Add(Me.TxtCodigoVendor)
        Me.PanelFiltroDatos.Controls.Add(Me.DgvVendedores)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtBuscar)
        Me.PanelFiltroDatos.Controls.Add(Me.BtnBuscar)
        Me.PanelFiltroDatos.Controls.Add(Me.Label8)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtBeneficios)
        Me.PanelFiltroDatos.Controls.Add(Me.Label7)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtVendida)
        Me.PanelFiltroDatos.Controls.Add(Me.Label6)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtSaldo)
        Me.PanelFiltroDatos.Controls.Add(Me.Label5)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtAsignada)
        Me.PanelFiltroDatos.Controls.Add(Me.BtnSalir)
        Me.PanelFiltroDatos.Controls.Add(Me.Label2)
        Me.PanelFiltroDatos.Controls.Add(Me.Label1)
        Me.PanelFiltroDatos.Controls.Add(Me.TxtVendedor)
        Me.PanelFiltroDatos.Controls.Add(Me.RbtnVendedor)
        Me.PanelFiltroDatos.Controls.Add(Me.RbtnGlobal)
        Me.PanelFiltroDatos.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFiltroDatos.Location = New System.Drawing.Point(0, 0)
        Me.PanelFiltroDatos.Name = "PanelFiltroDatos"
        Me.PanelFiltroDatos.Size = New System.Drawing.Size(1104, 167)
        Me.PanelFiltroDatos.TabIndex = 0
        '
        'TxtCodigoVendor
        '
        Me.TxtCodigoVendor.Location = New System.Drawing.Point(91, 48)
        Me.TxtCodigoVendor.Name = "TxtCodigoVendor"
        Me.TxtCodigoVendor.Size = New System.Drawing.Size(60, 22)
        Me.TxtCodigoVendor.TabIndex = 33
        '
        'DgvVendedores
        '
        Me.DgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVendedores.Location = New System.Drawing.Point(627, 48)
        Me.DgvVendedores.Name = "DgvVendedores"
        Me.DgvVendedores.RowHeadersWidth = 51
        Me.DgvVendedores.RowTemplate.Height = 24
        Me.DgvVendedores.Size = New System.Drawing.Size(369, 34)
        Me.DgvVendedores.TabIndex = 32
        '
        'TxtBuscar
        '
        Me.TxtBuscar.Location = New System.Drawing.Point(657, 10)
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(339, 22)
        Me.TxtBuscar.TabIndex = 31
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(501, 6)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(148, 30)
        Me.BtnBuscar.TabIndex = 30
        Me.BtnBuscar.Text = "Buscar Vendedor"
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(695, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 17)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Beneficios"
        '
        'TxtBeneficios
        '
        Me.TxtBeneficios.Location = New System.Drawing.Point(698, 139)
        Me.TxtBeneficios.Name = "TxtBeneficios"
        Me.TxtBeneficios.Size = New System.Drawing.Size(100, 22)
        Me.TxtBeneficios.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(398, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 17)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Lotería vendida"
        '
        'TxtVendida
        '
        Me.TxtVendida.Location = New System.Drawing.Point(401, 139)
        Me.TxtVendida.Name = "TxtVendida"
        Me.TxtVendida.Size = New System.Drawing.Size(100, 22)
        Me.TxtVendida.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(546, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 17)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Saldo"
        '
        'TxtSaldo
        '
        Me.TxtSaldo.Location = New System.Drawing.Point(549, 139)
        Me.TxtSaldo.Name = "TxtSaldo"
        Me.TxtSaldo.Size = New System.Drawing.Size(100, 22)
        Me.TxtSaldo.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(247, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 17)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Lotería asignada"
        '
        'TxtAsignada
        '
        Me.TxtAsignada.Location = New System.Drawing.Point(250, 139)
        Me.TxtAsignada.Name = "TxtAsignada"
        Me.TxtAsignada.Size = New System.Drawing.Size(100, 22)
        Me.TxtAsignada.TabIndex = 22
        '
        'BtnSalir
        '
        Me.BtnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSalir.Location = New System.Drawing.Point(990, 12)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(102, 35)
        Me.BtnSalir.TabIndex = 11
        Me.BtnSalir.Text = "Salir   "
        Me.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Vendedor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(430, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Informe Resumen Sorteos Global/Vendedor"
        '
        'TxtVendedor
        '
        Me.TxtVendedor.Location = New System.Drawing.Point(157, 48)
        Me.TxtVendedor.Name = "TxtVendedor"
        Me.TxtVendedor.Size = New System.Drawing.Size(313, 22)
        Me.TxtVendedor.TabIndex = 2
        '
        'RbtnVendedor
        '
        Me.RbtnVendedor.AutoSize = True
        Me.RbtnVendedor.Location = New System.Drawing.Point(11, 118)
        Me.RbtnVendedor.Name = "RbtnVendedor"
        Me.RbtnVendedor.Size = New System.Drawing.Size(155, 21)
        Me.RbtnVendedor.TabIndex = 1
        Me.RbtnVendedor.TabStop = True
        Me.RbtnVendedor.Text = "Resumen Vendedor"
        Me.RbtnVendedor.UseVisualStyleBackColor = True
        '
        'RbtnGlobal
        '
        Me.RbtnGlobal.AutoSize = True
        Me.RbtnGlobal.Location = New System.Drawing.Point(11, 91)
        Me.RbtnGlobal.Name = "RbtnGlobal"
        Me.RbtnGlobal.Size = New System.Drawing.Size(134, 21)
        Me.RbtnGlobal.TabIndex = 0
        Me.RbtnGlobal.TabStop = True
        Me.RbtnGlobal.Text = "Resumen Global"
        Me.RbtnGlobal.UseVisualStyleBackColor = True
        '
        'PanelVisorReport
        '
        Me.PanelVisorReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelVisorReport.Location = New System.Drawing.Point(0, 167)
        Me.PanelVisorReport.Name = "PanelVisorReport"
        Me.PanelVisorReport.Size = New System.Drawing.Size(1104, 586)
        Me.PanelVisorReport.TabIndex = 1
        '
        'FrmResumenSorteosReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 753)
        Me.Controls.Add(Me.PanelVisorReport)
        Me.Controls.Add(Me.PanelFiltroDatos)
        Me.Name = "FrmResumenSorteosReport"
        Me.Text = "FrmResumenSorteosReport"
        Me.PanelFiltroDatos.ResumeLayout(False)
        Me.PanelFiltroDatos.PerformLayout()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents PanelFiltroDatos As Panel
    Friend WithEvents TxtVendedor As TextBox
    Friend WithEvents RbtnVendedor As RadioButton
    Friend WithEvents RbtnGlobal As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnSalir As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtBeneficios As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtVendida As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSaldo As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtAsignada As TextBox
    Friend WithEvents PanelVisorReport As Panel
    Friend WithEvents DgvVendedores As DataGridView
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents TxtCodigoVendor As TextBox
    Friend WithEvents HelpProvider1 As HelpProvider
End Class
