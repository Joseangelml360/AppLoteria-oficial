Imports System.Configuration
Imports MySql.Data.MySqlClient

Public Class ConexionDatos
    Public Shared Function GetLoteriaConexion() As MySqlConnection
        Return New MySqlConnection("server=YOUR_SERVER;port=24267;user id=YOUR_USER;password=YOUR_PASSWORD;persistsecurityinfo=True;database=dbcensos;sslmode=Required;certificatefile=C:\Datos\MySQL_Local\ca.pem;allowpublickeyretrieval=True")
    End Function

    Public Shared Function GetDbCensosConexion() As MySqlConnection
        Return New MySqlConnection("server=YOUR_SERVER;port=24267;user id=YOUR_USER;password=YOUR_PASSWORD;persistsecurityinfo=True;database=dbcensos;sslmode=Required;certificatefile=C:\Datos\MySQL_Local\ca.pem;allowpublickeyretrieval=True")
    End Function

End Class
