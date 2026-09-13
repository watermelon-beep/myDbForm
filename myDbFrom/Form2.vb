Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        readData3()
    End Sub

    Public Sub readData3()
        DataGridView1.Rows.Clear()
        query = "SELECT student_id, student_name, student_grade FROM student_record"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        sqldr = sqlcom.ExecuteReader

        While sqldr.Read
            DataGridView1.Rows.Add(sqldr("student_id"), sqldr("student_name"), sqldr("student_grade"))
        End While

        sqlcom.Dispose()
        sqldr.Close()
    End Sub

    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub
End Class