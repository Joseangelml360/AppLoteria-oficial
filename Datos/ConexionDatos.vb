Imports System.Configuration
Imports MySql.Data.MySqlClient

Public Class ConexionDatos
    Public Shared Function GetLoteriaConexion() As MySqlConnection
        Return New MySqlConnection("server=mysql-324aad11-joseangelml360-9fa3.e.aivencloud.com;port=24267;user id=avnadmin;password=AVNS_E0buBjUV40iLpm4dsfw;persistsecurityinfo=True;database=dbcensos;sslmode=Required;certificatefile=C:\Datos\MySQL_Local\ca.pem;allowpublickeyretrieval=True")
    End Function

    Public Shared Function GetDbCensosConexion() As MySqlConnection
        Return New MySqlConnection("server=mysql-324aad11-joseangelml360-9fa3.e.aivencloud.com;port=24267;user id=avnadmin;password=AVNS_E0buBjUV40iLpm4dsfw;persistsecurityinfo=True;database=dbcensos;sslmode=Required;certificatefile=C:\Datos\MySQL_Local\ca.pem;allowpublickeyretrieval=True")
    End Function

End Class
