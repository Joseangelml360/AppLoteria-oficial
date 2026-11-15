Imports Negocio
Imports System.Data

Public Class FrmPagosVendedor
    Public ReadOnly detalle As New DetalleVentasLoteriaNegocio
    Public ReadOnly eje As New EjercicioActivoNegocio
    Public ReadOnly vendor As New VendedoresNegocio

    Public Property nEje As Integer
    Public Property nCodigo As Integer

    Private Sub FrmDetalleSorteoVendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarAyudaFormulario(Me, Me.HelpProvider1, "29")
        ' 🚀 GARANTÍA: El ejercicio contable se recupera siempre, pase lo que pase
        nEje = eje.GetEjercicioActivo
        Me.LblEjercicio.Text = nEje.ToString()
        Me.TxtBuscar.Visible = False
        Me.DgvVendedores.Visible = False

        ' Evaluamos si venimos con un vendedor preseleccionado (Vía automática o botón directo con contexto)
        If Not String.IsNullOrWhiteSpace(Me.TxtIdVendor.Text) AndAlso Val(Me.TxtIdVendor.Text) > 0 Then

            nCodigo = CInt(Val(Me.TxtIdVendor.Text))
            Me.LblCodigo.Text = nCodigo.ToString()

            Cargar(nCodigo, nEje)
        Else
            ' 🚀 Vía MDIParent o Botón directo sin contexto: Forzamos al usuario a elegir vendedor
            Me.TxtIdVendor.Text = "0"
            Me.LblCodigo.Text = "0"
            nCodigo = 0

            ' Activamos la visualización del buscador automático
            ' ActivarBuscadorVendedores()
        End If
    End Sub

    Private Sub Cargar(ByVal codigo As Integer, ByVal ejercicio As Integer)
        If codigo <= 0 Then Exit Sub

        Dim n, i As Integer
        Dim nDeuda As Double = 0

        ' 1. Poblamos el origen de datos en el DataGridView
        Me.DgvDetallePagos.DataSource = detalle.ObtenerPagosVendedor(codigo, ejercicio)

        ' 2. Capturamos el número total de filas reales
        i = DgvDetallePagos.Rows.Count()

        If i < 1 Then
            MsgBox("Este vendedor no tiene sorteos adeudados", MsgBoxStyle.Information)
            Limpiar(Me)
            Me.LblCodigo.Text = ""
            Exit Sub
        End If

        ' 3. Bucle de recálculo preventivo sobre celdas renderizadas
        For n = 0 To i - 1
            If DgvDetallePagos.Rows(n).Cells("Deuda").Value IsNot Nothing AndAlso Not IsDBNull(DgvDetallePagos.Rows(n).Cells("Deuda").Value) Then
                Dim stringValor As String = DgvDetallePagos.Rows(n).Cells("Deuda").Value.ToString().Replace(".", "")
                nDeuda += Val(stringValor)
            End If
        Next

        ' 4. Volcamos el sumatorio en formato contable
        Me.TxtDeuda.Text = FormatCurrency(nDeuda, 2)
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim ImporteEntregado, DeudaTotal As Double
        Dim CajaBanco, Concepto As String

        ImporteEntregado = Val(Me.TxtEntregaParcial.Text.Replace(".", ""))
        DeudaTotal = Val(Me.TxtDeuda.Text.Replace(".", ""))
        nCodigo = CInt(Val(Me.TxtIdVendor.Text))

        If nCodigo <= 0 Then
            MsgBox("Debe seleccionar un vendedor válido antes de guardar.", MsgBoxStyle.Critical, "Error de Validación")
            Exit Sub
        End If

        If ImporteEntregado <= 0 Then
            MsgBox("El importe a entregar debe ser mayor que cero.", MsgBoxStyle.Critical, "Error en el Pago")
            Me.TxtEntregaParcial.Focus()
            Exit Sub
        End If

        If ImporteEntregado > DeudaTotal Then
            Dim descAnticipo = MsgBox("El importe entregado supera la deuda total. El excedente se guardará como un anticipo a cuenta del sorteo activo." & vbCrLf & "¿Desea continuar?",
                                      MsgBoxStyle.OkCancel Or MsgBoxStyle.Question, "Confirmar Excedente")
            If descAnticipo = vbCancel Then Exit Sub
        End If

        CajaBanco = If(RbtnBanco.Checked, "B", "C")
        Concepto = "Pago de deuda parcial lotería FIFO " & TxtNombre.Text & " " & TxtApellidos.Text

        Dim exito As Boolean = False
        Try
            detalle.ProcesarLiquidacionMasivaFIFO(nEje, nCodigo, ImporteEntregado, CajaBanco, Concepto)

            MsgBox("Liquidación masiva distribuida por antigüedad, saldos de vendedor y caja consolidados con éxito.",
                   MsgBoxStyle.Information, "Éxito Atómico FIFO")

            exito = True
            Me.BtnGuardar.Enabled = False
            Me.DialogResult = DialogResult.OK
            Me.Close() ' 🚀 Cierre seguro del formulario modal

        Catch ex As Exception
            MsgBox("Operación abortada por seguridad de datos Cloud. Base de datos protegida: " & ex.Message,
                   MsgBoxStyle.Critical, "Error de Red / Concurrencia")
        Finally
            ' 🚀 ESCUDO: Solo refrescamos la interfaz si el formulario NO se está cerrando
            If Not exito Then
                Cargar(nCodigo, nEje)
                Limpiar(Me)
                Me.BtnGuardar.Enabled = True
            End If
        End Try
    End Sub

    Private Sub ActivarBuscadorVendedores()
        Me.TxtBuscar.Visible = True
        Me.DgvVendedores.Visible = True
        Me.DgvVendedores.DataSource = vendor.GetlistaVendedores(nEje)
        Me.DgvVendedores.Height = 400

        With DgvVendedores
            ' Ocultación limpia basada en índices existentes
            If .Columns.Count > 9 Then
                .Columns(0).Visible = False
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(1).Width = 40
                .Columns(2).Width = 80
                .Columns(3).Width = 200
            End If
        End With
        Me.TxtBuscar.Focus()
    End Sub

    Private Sub BtnAbrirVendedores_Click(sender As Object, e As EventArgs) Handles BtnAbrirVendedores.Click
        ActivarBuscadorVendedores()
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        ' 1. Obtenemos el origen de datos real de forma segura sin importar si es DataTable o DataView
        Dim dt As DataTable = Nothing

        If TypeOf DgvVendedores.DataSource Is DataTable Then
            dt = CType(DgvVendedores.DataSource, DataTable)
        ElseIf TypeOf DgvVendedores.DataSource Is DataView Then
            dt = CType(DgvVendedores.DataSource, DataView).Table
        End If

        ' 2. Si tenemos una tabla válida y hay columnas suficientes, aplicamos el filtro instantáneo
        If dt IsNot Nothing AndAlso dt.Columns.Count > 3 Then
            Dim textoBusqueda As String = TxtBuscar.Text.Trim().Replace("'", "''")

            ' Detectamos dinámicamente tus columnas por su posición física (1, 2 y 3)
            Dim colCodigo As String = dt.Columns(1).ColumnName
            Dim colNombre As String = dt.Columns(2).ColumnName
            Dim colApellidos As String = dt.Columns(3).ColumnName

            If String.IsNullOrWhiteSpace(textoBusqueda) Then
                ' Si borra el texto, limpiamos el filtro por completo para mostrar todos
                dt.DefaultView.RowFilter = ""
            Else
                ' 🔥 FILTRADO AUTOMÁTICO EN CALIENTE: Letra a letra
                If IsNumeric(textoBusqueda) Then
                    dt.DefaultView.RowFilter = $"{colCodigo} = {textoBusqueda}"
                Else
                    dt.DefaultView.RowFilter = $"{colApellidos} LIKE '%{textoBusqueda}%' OR {colNombre} LIKE '%{textoBusqueda}%'"
                End If
            End If

            ' Reasignamos el DataView modificado a la rejilla para refrescar la visualización
            DgvVendedores.DataSource = dt.DefaultView
        End If
    End Sub


    ' 🚀 EVENTO COMPLETADO: Captura el doble clic sobre la rejilla de búsqueda para seleccionar al vendedor
    Private Sub DgvVendedores_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvVendedores.CellClick
        If e.RowIndex >= 0 Then
            Dim filaSeleccionada As DataGridViewRow = DgvVendedores.Rows(e.RowIndex)

            ' Mapeamos los datos básicos a las etiquetas y cuadros de texto correspondientes
            Me.TxtIdVendor.Text = filaSeleccionada.Cells("Codigo").Value.ToString()
            Me.LblCodigo.Text = filaSeleccionada.Cells("Codigo").Value.ToString()
            Me.TxtNombre.Text = filaSeleccionada.Cells("Nombre").Value.ToString()
            Me.TxtApellidos.Text = filaSeleccionada.Cells("Apellidos").Value.ToString()

            ' Actualizamos la variable de ámbito global de la clase
            nCodigo = CInt(Val(Me.TxtIdVendor.Text))

            ' Ocultamos el buscador para limpiar la interfaz visual del usuario
            Me.TxtBuscar.Visible = False
            Me.DgvVendedores.Visible = False

            ' Cargamos las deudas pendientes bajo el criterio FIFO
            Cargar(nCodigo, nEje)
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Limpiar(Me)
        Me.TxtIdVendor.Text = nCodigo.ToString()
    End Sub

    Private Sub Limpiar(ByVal controlPadre As Control)
        For Each c As Control In controlPadre.Controls
            ' 1. Si es un TextBox, lo reseteamos a cero o vacío
            If TypeOf c Is TextBox Then
                ' Si prefieres que nombre y apellidos queden vacíos ("") en vez de "0", 
                ' puedes evaluar el nombre, pero ponerlos a "0" o "" limpia la pantalla por igual.
                If c.Name = "TxtNombre" OrElse c.Name = "TxtApellidos" Then
                    c.Text = ""
                Else
                    c.Text = "0"
                End If
            End If

            ' 2. Rastreamos de forma recursiva si el control tiene hijos (Paneles, GroupBox, etc.)
            If c.HasChildren Then
                Limpiar(c)
            End If
        Next
    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnRecargar_Click(sender As Object, e As EventArgs) Handles BtnRecargar.Click
        Dim codigo As Integer = CInt(Val(Me.LblCodigo.Text))
        Me.TxtIdVendor.Text = codigo.ToString()
        Cargar(codigo, nEje)
    End Sub

End Class
