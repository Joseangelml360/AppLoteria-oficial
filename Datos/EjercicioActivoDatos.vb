Imports MySql.Data.MySqlClient
Imports System.Data

Public Class EjercicioActivoDatos
    Public Property EjercicioActivoEntidad As Object

    Public Function ObtenerEjercicio() As Integer
        Dim query As String = "SELECT anoeje FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToInt32(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener el año del ejercicio activo: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerLoteriaMayores() As Double
        Dim query As String = "SELECT loteriamayor FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener la lotería de mayores: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerLoteriaJuveniles() As Double
        Dim query As String = "SELECT loteriajuvenil FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener la lotería de juveniles: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerOrdMayor() As Double
        Dim query As String = "SELECT ordinariomayor FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener la cuota ordinaria de mayores: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerOrdJuvenil() As Double
        Dim query As String = "SELECT ordinariojuvenil FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener la cuota ordinaria de juveniles: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerNavMayor() As Double
        Dim query As String = "SELECT navidadmayor FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener el importe de Navidad de mayores: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerNavJuvenil() As Double
        Dim query As String = "SELECT navidadjuvenil FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener el importe de Navidad de juveniles: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerReyesMayor() As Double
        Dim query As String = "SELECT infmayor FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener el importe de Reyes de mayores: " & ex.Message)
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerReyesJuvenil() As Double
        Dim query As String = "SELECT infjuvenil FROM ejercicio LIMIT 1"

        Using conn As MySqlConnection = ConexionDatos.GetDbCensosConexion()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado Is Nothing OrElse Convert.IsDBNull(resultado) Then Return 0
                    Return Convert.ToDouble(resultado)
                Catch ex As Exception
                    Throw New Exception("Error al obtener el importe de Reyes de juveniles: " & ex.Message)
                End Try
            End Using
        End Using
    End Function
End Class
