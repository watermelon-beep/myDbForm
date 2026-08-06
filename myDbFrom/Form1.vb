Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D

Public Class Form1
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.BorderStyle = BorderStyle.Fixed3D
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.RowHeadersVisible = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        RoundDataGrid(DataGridView1, 20)

        connect()
        readData()
    End Sub

    Private Sub RoundDataGrid(dataGrid As DataGridView, radius As Integer)

        Dim path As New GraphicsPath()

        path.StartFigure()
        path.AddArc(0, 0, radius, radius, 180, 90)
        path.AddArc(dataGrid.Width - radius, 0, radius, radius, 270, 90)
        path.AddArc(dataGrid.Width - radius, dataGrid.Height - radius, radius, radius, 0, 90)
        path.AddArc(0, dataGrid.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()

        dataGrid.Region = New Region(path)
    End Sub

    Sub readData()
        DataGridView1.Rows.Clear()
        query = "SELECT * FROM student_record"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        sqldr = sqlcom.ExecuteReader

        While sqldr.Read
            DataGridView1.Rows.Add(sqldr("student_id"), sqldr("student_name"), sqldr("student_age"), sqldr("student_grade"), sqldr("student_course"))
        End While

        sqlcom.Dispose()
        sqldr.Close()
    End Sub

    Sub saveData()
        query = "Insert into student_record (student_id, student_name, student_age, student_grade, student_course) values (@student_id, @student_name, @student_age, @student_grade, @student_course)"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        With sqlcom.Parameters
            .AddWithValue("@student_id", idtxbx.Text)
            .AddWithValue("@student_name", nametxbx.Text)
            .AddWithValue("@student_age", agetxbx.Text)
            .AddWithValue("@student_grade", gradetxbx.Text)
            .AddWithValue("@student_course", coursecmbx.Text)
        End With

        sqlcom.ExecuteNonQuery()
        sqlcom.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("added")
        saveData()
        readData()
    End Sub
End Class
