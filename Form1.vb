Imports Aladdin.HASP
Public Class Form1
    Dim DogMsg As New HaspTool.HaspTool()

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            DogMsg.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not DogMsg.Probe() Then
                MsgBox("加密狗不匹配")
            Else
                MsgBox("success")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            TextBox2.Text = DogMsg.GetCurMsg()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            If Strings.Trim(TextBox1.Text).Length = 14 Then
                Dim str As String = Strings.Replace(Strings.Trim(TextBox1.Text), "-", ",")
                DogMsg.SetFile(str)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim sn As String = Strings.Trim(txtPO.Text)
        sn &= Mid(Strings.Trim(txtTpye.Text), 1, 1)
        sn &= Strings.Trim(txtCount.Text)
        sn &= Chr(Strings.Trim(txtYear.Text) + 64)
        Dim str As String = ChrW(CInt(Strings.Trim(txtYear.Text) + 64))
        sn &= Hex(Strings.Trim(txtMonth.Text))
        sn &= Strings.Trim(txtOrder.Text)
        TextBox1.Text = sn.ToUpper
        If Strings.Trim(TextBox1.Text).Length = 10 Then
            str = Mid(Strings.Trim(TextBox1.Text), 1, 2)
            For i As Byte = 3 To 9 Step 2
                str += "-" + Mid(Strings.Trim(TextBox1.Text), i, 2)
            Next
            TextBox1.Text = str
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            DogMsg.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            DogMsg = New HaspTool.HaspTool()

            Label7.Text = DogMsg.GetExpDate.ToString
            Label8.Text = DogMsg.GetCurTime.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            txtPW.Text = GetPassword(txtID.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetPassword(ByVal ID As String) As String
        Try
            ID = Strings.Trim(ID)
            Dim length As Integer = ID.Length
            Dim Pw(5) As String
            Dim intPw(5) As Long
            If length >= 6 Then
                For i As Byte = 0 To 5
                    intPw(i) = Mid(ID, i + 1, 1) * (length + 1 + i)
                Next
            Else
                For i As Byte = 0 To length - 1
                    intPw(i) = Mid(ID, i + 1, 1) * (length + 7 + i)
                Next
            End If
            Dim ret As String = String.Empty
            For i As Byte = 0 To Pw.Length - 1
                If intPw(i) <> 0 Then
                    Pw(i) = Mid(intPw(i).ToString, intPw(i).ToString.Length, 1)
                Else
                    Pw(i) = "8"
                End If
                ret += Pw(i)
            Next
            Return ret
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim scope As String = HaspDemo.defaultScope
            scope = HaspDemo.localScope
            Dim demo As HaspDemo = New HaspDemo(Me.TextBox1)
            demo.RunDemo(scope)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Path As String = "C:\LQ_Calbtn"
        If IO.Directory.Exists(Path) Then
            FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
        End If
    End Sub
End Class
