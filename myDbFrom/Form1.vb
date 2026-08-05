Imports System.Data.SqlClient

Public Class Form1
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        readData()
    End Sub

    Sub readData()
        DataGridView1.Rows.Clear()
        query = "SELECT * FROM student_record"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        sqldr = sqlcom.ExecuteReader

        While sqldr.Read
            DataGridView1.Rows.Add(sqldr("student_id"), sqldr("student_name"), sqldr("student_age"), sqldr("student_grade"))
        End While

        sqlcom.Dispose()
        sqldr.Close()
    End Sub
End Class
