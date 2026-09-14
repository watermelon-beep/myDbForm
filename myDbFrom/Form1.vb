Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Security.Cryptography
Public Class Form1

    Private form2 As New Form2()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        connect()
        readData()
        readData2()

        studentCounter.Text = studentCount().ToString()

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

        While sqldr.Read()

            Dim img As Image = Nothing

            If Not IsDBNull(sqldr("Image")) Then

                Dim imageBytes() As Byte = CType(sqldr("Image"), Byte())

                Using ms As New MemoryStream(imageBytes)
                    img = Image.FromStream(ms)
                End Using

                DataGridView1.Rows.Add(sqldr("student_id"),
                                   sqldr("student_name"),
                                   sqldr("student_age"),
                                   sqldr("student_grade"),
                                   sqldr("student_course"),
                                         img,
                                         "Edit",
                                         "Delete")

            End If
        End While

        sqlcom.Dispose()
        sqldr.Close()
    End Sub

    Sub readData2()
        DataGridView2.Rows.Clear()
        query = "SELECT student_name, student_age, student_course FROM student_record"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        sqldr = sqlcom.ExecuteReader

        While sqldr.Read
            DataGridView2.Rows.Add(sqldr("student_name"),
                                   sqldr("student_age"),
                                   sqldr("student_course"))
        End While

        sqlcom.Dispose()
        sqldr.Close()
    End Sub

    Sub saveData()

        Dim ms As New MemoryStream

        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)

        query = "INSERT INTO student_record (
                student_id,
                student_name, 
                student_age,
                student_grade,
                student_course,
                image
                )
                VALUES (@student_id, 
                        @student_name, 
                        @student_age, 
                        @student_grade,
                        @student_course,
                        @image)"

        sqlcom = New SqlClient.SqlCommand(query, sqlconn)

        With sqlcom.Parameters
            .AddWithValue("@student_id", idtxbx.Text)
            .AddWithValue("@student_name", nametxbx.Text)
            .AddWithValue("@student_age", agetxbx.Text)
            .AddWithValue("@student_grade", gradetxbx.Text)
            .AddWithValue("@student_course", coursecmbx.Text)
            .AddWithValue("@image", ms.ToArray())
        End With

        sqlcom.ExecuteNonQuery()
        sqlcom.Dispose()

        ms.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("added")
        saveData()
        readData()
        readData2()
        form2.readData3()
        studentCounter.Text = studentCount().ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        form2.Show()
    End Sub

    Sub viewImg()
        Dim img() As Byte

        query = "SELECT Image FROM student_record WHERE student_id = @student_id"
        sqlcom = New SqlClient.SqlCommand(query, sqlconn)
        sqldr = sqlcom.ExecuteReader

        While sqldr.Read
            If IsDBNull(sqldr("Image")) Then
                PictureBox1.Image = Nothing
            Else
                img = sqldr("Image")
            End If
        End While

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub
End Class
