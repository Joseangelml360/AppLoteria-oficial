Imports System.Data
Imports Datos

Public Class UsuarioNegocio
    Private ReadOnly datos As New UsuarioDatos()

    Public Function ValidarUsuario(nombre As String, contraseña As String) As DataTable
        ' Validaciones previas de negocio básicas antes de ir a la base de datos
        If String.IsNullOrWhiteSpace(nombre) OrElse String.IsNullOrWhiteSpace(contraseña) Then
            Return New DataTable() ' Devuelve una tabla vacía directamente sin sobrecargar Aiven
        End If

        Return datos.BuscarUsuario(nombre, contraseña)
    End Function
End Class
