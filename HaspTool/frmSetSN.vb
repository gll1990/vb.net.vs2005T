Imports System.Windows.Forms

Public Class frmSetSN
    Public strCode As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text <> TextBox2.Text Then
            MsgBox("两次输入不一致，请重新输入")
            Exit Sub
        End If
        If TextBox1.Text.Length <> 10 Then
            MsgBox("输入格式不正确，请重新输入")
            Exit Sub
        End If
        NewSN = TextBox1.Text
        Me.DialogResult = vbOK
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = vbCancel
        Me.Close()
    End Sub

    Private Sub frmSetSN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IO.File.Exists(Path) Then
                TextBox1.Text = strCode ' GetFileSNCode()
                TextBox2.Text = strCode ' GetFileSNCode()
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim IsBusy As Boolean = False
    Private Sub frmSetSN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control = True And e.KeyCode = Keys.S And Not IsBusy Then
            IsBusy = True
            TextBox1.ReadOnly = False
            TextBox2.ReadOnly = False
            IsBusy = False
        End If

    End Sub
End Class