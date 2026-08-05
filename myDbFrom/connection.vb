Imports System.Data.SqlClient

Module connection

    Public sqlconn As New SqlConnection
    Public sqlcom As New SqlCommand
    Public sqldr As SqlDataReader
    Public query As String

    Sub connect()
        Try
            If sqlconn.State = ConnectionState.Open Then sqlconn.Close()
            sqlconn.ConnectionString = "Server= .\SQLEXPRESS; Database = mydb; Trusted_Connection = True; MultipleActiveResultSets = True"
            sqlconn.Open()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

End Module
