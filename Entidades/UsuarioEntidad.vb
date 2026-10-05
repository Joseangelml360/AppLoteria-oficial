Imports System.Windows.Forms
Public Class UsuarioEntidad
    Public Shared Function TablaUsuario() As DataTable
        Dim Tabla As DataTable
        Try
            Tabla = New DataTable()
            Tabla.Columns.Add("idusuarios", GetType(Integer))
            Tabla.Columns.Add("nombre", GetType(String))
            Tabla.Columns.Add("password", GetType(String))
            Tabla.Columns.Add("idrol", GetType(Integer))
            Tabla.Columns.Add("activo", GetType(Boolean))
        Catch ex As Exception
            MessageBox.Show("Error al crear la tabla: " & ex.Message)
            Return Nothing
        End Try
        Return Tabla
    End Function
End Class
