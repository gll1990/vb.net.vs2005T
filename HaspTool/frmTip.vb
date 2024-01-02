Imports Aladdin.HASP
Imports System.Windows.Forms

Public Class frmTip

    Public Sub frmTip_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Timer1.Stop()
            Timer1.Dispose()
            Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Help_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim InfoArr As ArrayList = GetHaspMsg()
            If InfoArr Is Nothing Then
                GettingFailed()
                txtSN.Text = FomateSNCode(GetFileSNCode())
            Else
                If InfoArr.Count = 0 Then
                    GettingFailed()
                Else
                    Dim expDateArr As New ArrayList
                    Dim IsUpdateSn As Boolean = True
                    For i As Integer = 0 To InfoArr.Count - 1
                        Dim item As InfoXml = InfoArr(i)
                        If intID = item.ID Then
                            txtHaspID.Text = HaspID
                            txtHaspID.Visible = True
                            lblStartTime.Visible = True
                            txtSN.Text = FomateSNCode(GetDogSNCode()) ' FomateSNCode(GetFileSNCode())
                            'If GetDogSNCode() <> GetFileSNCode() Then
                            '    txtStatue.Text = "加密狗不匹配"
                            '    txtStartTime.Text = "####-##-##"
                            '    txtEndTime.Text = "####-##-##"
                            '    Exit For
                            'End If
                            If item.license_type = LicenseType.perpetual Then
                                txtStatue.Text = "已激活"
                                'txtHaspID.Text = "永久"
                                txtEndTime.Text = "永久"
                                'txtStartTime.Visible = False
                                'lblStartTime.Visible = False
                            ElseIf item.license_type = LicenseType.trial Then
                                txtStatue.Text = "试用"
                                If item.time_start = -1 Then
                                    txtHaspID.Text = "未使用"
                                    Dim nowDate As Date = GetHaspTime()
                                    Dim ed As Date = nowDate.AddSeconds(item.total_time)
                                    txtEndTime.Text = ed.AddHours(8).ToString
                                Else
                                    Dim org As Date = New Date(1970, 1, 1, 0, 0, 0)
                                    Dim st As Date = org.AddSeconds(item.time_start)
                                    Dim ed As Date = st.AddSeconds(item.total_time)
                                    Dim nowDate As Date = GetHaspTime()
                                    If (nowDate - st).TotalSeconds > item.total_time Then
                                        txtStatue.Text = "已过期"
                                    End If
                                    txtEndTime.Text = ed.AddHours(8).ToString
                                    txtHaspID.Visible = True
                                    lblStartTime.Visible = True
                                End If
                            ElseIf item.license_type = LicenseType.expiration Then
                                Dim org As Date = New Date(1970, 1, 1, 0, 0, 0)
                                Dim exp As Date = org.AddSeconds(item.total_time)
                                Dim expMax As Date = exp
                                If expDateArr.Count > 0 Then
                                    For j As Byte = 0 To expDateArr.Count - 1
                                        If expMax < expDateArr.Item(j) Then
                                            expMax = expDateArr.Item(j)
                                        End If
                                    Next
                                End If
                                expDateArr.Add(exp)
                                exp = expMax
                                If GetDogSNCode() <> GetFileSNCode() Then
                                    txtStatue.Text = "加密狗不匹配"
                                    If GetFileSNCode() = "8888888888" And IsUpdateSn Then
                                        If MsgBox("是否更新SN码？", MsgBoxStyle.YesNo, "调试人员确认SN码") = MsgBoxResult.Yes Then
                                            Dim frmSet As New frmSetSN
                                            frmSet.strCode = GetDogSNCode()
                                            If frmSet.ShowDialog() = vbOK Then
                                                If NewSN <> String.Empty Then
                                                    Try
                                                        FileSystem.SetAttr(Path, FileAttribute.Normal)
                                                        WriteIni("System", "SNCode", NewSN, Path)
                                                        FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
                                                        MsgBox("设置成功！")
                                                        Help_Load(Nothing, Nothing)
                                                    Catch ex As Exception
                                                        Throw New Exception("408022-写入失败!")
                                                    End Try
                                                    SetFileSNCode(Strings.Trim(NewSN))
                                                Else
                                                    MsgBox("输入错误！")
                                                    Exit Sub
                                                End If
                                            End If
                                            frmSet.Dispose()
                                        Else
                                            IsUpdateSn = False
                                        End If
                                    End If
                                Else
                                    txtStatue.Text = "试用"
                                End If
                                Dim nowDate As Date = GetHaspTime()
                                If nowDate > exp Then
                                    txtStatue.Text = "已过期"
                                End If
                                txtEndTime.Text = exp.AddHours(8).ToString
                                'txtStartTime.Visible = False
                                'lblStartTime.Visible = False
                            Else

                            End If
                            'If not txtStatue.Text = "已过期" Then
                            '    Exit For
                            'End If
                        Else
                            GettingFailed()
                        End If
                    Next
                    expDateArr.Clear()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            GettingFailed()
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            txtTime.Text = GetHaspTime().AddHours(8)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GettingFailed()
        Try
            txtStatue.Text = "获取失败"
            'txtStartTime.Text = "####-##-##"
            txtEndTime.Text = "####-##-##"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Dim IsBusy As Boolean = False
    Private Sub frmTip_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If txtStatue.Text = "加密狗不匹配" Or txtStatue.Text = "获取失败" Then
                If e.KeyCode = Keys.F2 And Not IsBusy And txtStatue.Text = "加密狗不匹配" Then
                    IsBusy = True
                    Dim strPass As String = InputBox("修改加密狗SN，请输入密码")
                    Dim Pw As String = GetPassword(HaspID)
                    If strPass = Pw Then
                        Dim frmSet As New frmSetSN
                        frmSet.strCode = GetFileSNCode()
                        If frmSet.ShowDialog() = vbOK Then
                            If NewSN <> String.Empty Then
                                Try
                                    Dim bytArr(1) As Byte
                                    bytArr = System.Text.Encoding.Default.GetBytes(Strings.Trim(NewSN))
                                    WriteDemo(HaspFileId.ReadWrite, bytArr)
                                    MsgBox("设置成功！")
                                    Help_Load(Nothing, Nothing)
                                Catch ex As Exception
                                    Throw New Exception("408022-写入失败!")
                                End Try
                                SetFileSNCode(Strings.Trim(NewSN))
                            Else
                                MsgBox("输入错误！")
                                Exit Sub
                            End If
                        End If
                        frmSet.Dispose()
                    Else
                        MsgBox("密码错误！")
                        Exit Sub
                    End If
                ElseIf e.KeyCode = Keys.F9 And Not IsBusy Then
                    If (txtStatue.Text = "获取失败" And HaspID <> String.Empty) Or txtStatue.Text = "加密狗不匹配" Then
                        IsBusy = True
                        Dim strPass As String = InputBox("修改电脑SN，请输入密码")
                        Dim Pw As String = GetPassword(HaspID)
                        If strPass = Pw Then
                            Dim frmSet As New frmSetSN
                            frmSet.strCode = GetDogSNCode()
                            If frmSet.ShowDialog() = vbOK Then
                                If NewSN <> String.Empty Then
                                    Try
                                        FileSystem.SetAttr(Path, FileAttribute.Normal)
                                        WriteIni("System", "SNCode", NewSN, Path)
                                        FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
                                        MsgBox("设置成功！")
                                        Help_Load(Nothing, Nothing)
                                    Catch ex As Exception
                                        Throw New Exception("408022-写入失败!")
                                    End Try
                                    SetFileSNCode(Strings.Trim(NewSN))
                                Else
                                    MsgBox("输入错误！")
                                    Exit Sub
                                End If
                            End If
                            frmSet.Dispose()
                        Else
                            MsgBox("密码错误！")
                            Exit Sub
                        End If
                    End If
                End If
            End If
            IsBusy = False
        Catch ex As Exception
            IsBusy = False
            MsgBox("408023-设置错误")
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
End Class