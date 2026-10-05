<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDetallesVentaVendedor
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
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDetallesVentaVendedor))
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.DgvVendedores = New System.Windows.Forms.DataGridView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BtnSelectVendor = New System.Windows.Forms.Button()
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.TxtApeVendor = New System.Windows.Forms.TextBox()
        Me.TxtNomVendor = New System.Windows.Forms.TextBox()
        Me.TxtCodVendor = New System.Windows.Forms.TextBox()
        Me.BtnFiltro = New System.Windows.Forms.Button()
        Me.BtnSalir = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.detallesventaloteriaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dbcensosDataSet = New Presentacion.dbcensosDataSet()
        Me.detallesventaloteriaTableAdapter = New Presentacion.dbcensosDataSetTableAdapters.detallesventaloteriaTableAdapter()
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.detallesventaloteriaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcensosDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource1.Name = "DataSetDetalleVentas"
        ReportDataSource1.Value = Me.detallesventaloteriaBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion.RptDetallesVendedor.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 121)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1248, 245)
        Me.ReportViewer1.TabIndex = 0
        '
        'DgvVendedores
        '
        Me.DgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvVendedores.Location = New System.Drawing.Point(642, 64)
        Me.DgvVendedores.Name = "DgvVendedores"
        Me.DgvVendedores.RowHeadersWidth = 51
        Me.DgvVendedores.RowTemplate.Height = 24
        Me.DgvVendedores.Size = New System.Drawing.Size(291, 23)
        Me.DgvVendedores.TabIndex = 17
        Me.DgvVendedores.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Presentacion.My.Resources.Resources.Usuario
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 60)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'BtnSelectVendor
        '
        Me.BtnSelectVendor.Image = CType(resources.GetObject("BtnSelectVendor.Image"), System.Drawing.Image)
        Me.BtnSelectVendor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSelectVendor.Location = New System.Drawing.Point(474, 32)
        Me.BtnSelectVendor.Name = "BtnSelectVendor"
        Me.BtnSelectVendor.Size = New System.Drawing.Size(162, 30)
        Me.BtnSelectVendor.TabIndex = 19
        Me.BtnSelectVendor.Text = "Buscar Vendedor"
        Me.BtnSelectVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSelectVendor.UseVisualStyleBackColor = True
        '
        'TxtBuscar
        '
        Me.TxtBuscar.Location = New System.Drawing.Point(642, 36)
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(291, 22)
        Me.TxtBuscar.TabIndex = 18
        Me.TxtBuscar.Visible = False
        '
        'TxtApeVendor
        '
        Me.TxtApeVendor.Location = New System.Drawing.Point(823, 93)
        Me.TxtApeVendor.Name = "TxtApeVendor"
        Me.TxtApeVendor.Size = New System.Drawing.Size(213, 22)
        Me.TxtApeVendor.TabIndex = 16
        '
        'TxtNomVendor
        '
        Me.TxtNomVendor.Location = New System.Drawing.Point(672, 93)
        Me.TxtNomVendor.Name = "TxtNomVendor"
        Me.TxtNomVendor.Size = New System.Drawing.Size(145, 22)
        Me.TxtNomVendor.TabIndex = 15
        '
        'TxtCodVendor
        '
        Me.TxtCodVendor.Location = New System.Drawing.Point(609, 93)
        Me.TxtCodVendor.Name = "TxtCodVendor"
        Me.TxtCodVendor.Size = New System.Drawing.Size(56, 22)
        Me.TxtCodVendor.TabIndex = 14
        '
        'BtnFiltro
        '
        Me.BtnFiltro.Image = Global.Presentacion.My.Resources.Resources.filtro
        Me.BtnFiltro.Location = New System.Drawing.Point(1140, 85)
        Me.BtnFiltro.Name = "BtnFiltro"
        Me.BtnFiltro.Size = New System.Drawing.Size(120, 30)
        Me.BtnFiltro.TabIndex = 13
        Me.BtnFiltro.Text = "Aplicar filtro"
        Me.BtnFiltro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnFiltro.UseVisualStyleBackColor = True
        '
        'BtnSalir
        '
        Me.BtnSalir.Image = Global.Presentacion.My.Resources.Resources.door_out
        Me.BtnSalir.Location = New System.Drawing.Point(1180, 12)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(80, 30)
        Me.BtnSalir.TabIndex = 12
        Me.BtnSalir.Text = "Salir"
        Me.BtnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(78, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(253, 25)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Informe Detalle vendedor"
        '
        'detallesventaloteriaBindingSource
        '
        Me.detallesventaloteriaBindingSource.DataMember = "detallesventaloteria"
        Me.detallesventaloteriaBindingSource.DataSource = Me.dbcensosDataSet
        '
        'dbcensosDataSet
        '
        Me.dbcensosDataSet.DataSetName = "dbcensosDataSet"
        Me.dbcensosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'detallesventaloteriaTableAdapter
        '
        Me.detallesventaloteriaTableAdapter.ClearBeforeFill = True
        '
        'FrmDetallesVentaVendedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 378)
        Me.Controls.Add(Me.DgvVendedores)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.BtnSelectVendor)
        Me.Controls.Add(Me.TxtBuscar)
        Me.Controls.Add(Me.TxtApeVendor)
        Me.Controls.Add(Me.TxtNomVendor)
        Me.Controls.Add(Me.TxtCodVendor)
        Me.Controls.Add(Me.BtnFiltro)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "FrmDetallesVentaVendedor"
        Me.Text = "FrmDetallesVentaVendedor"
        CType(Me.DgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.detallesventaloteriaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcensosDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents detallesventaloteriaBindingSource As BindingSource
    Friend WithEvents dbcensosDataSet As dbcensosDataSet
    Friend WithEvents detallesventaloteriaTableAdapter As dbcensosDataSetTableAdapters.detallesventaloteriaTableAdapter
    Friend WithEvents DgvVendedores As DataGridView
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents BtnSelectVendor As Button
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents TxtApeVendor As TextBox
    Friend WithEvents TxtNomVendor As TextBox
    Friend WithEvents TxtCodVendor As TextBox
    Friend WithEvents BtnFiltro As Button
    Friend WithEvents BtnSalir As Button
    Friend WithEvents Label1 As Label
End Class
