Imports Aladdin.HASP

Public Class HaspTool

    Public Sub New()
        Try
            intID = 323
            vendorCode = "Br6Ww2uZuss7Hco9teNicUYGrrOskAlLG2AvQSLB6UKQaTBLAOtpAiP5ohh5O9PjPdtYeVPNlh0yPvUw" +
                         "oFcfthlFUJXdW1CdS8a117lAVQcA5Hd46ZSqBma6+0rxORvsBcdrpIETET3UTzitiRnxZtoj3o652QwI" +
                          "yv+4Ixv7kxkxay9w3noKK2Yj/KvBtWfWJmQBkNuN0M4HFj6TZF8dwiUgV59PwSXMbVln8AatawtJFBuC" +
                          "rMK4v0IaXGnYK4oOp1c+44C4QP/KjMKUtgmIlun8WD1oqMEVJuxIn38O6AVt2TBgPOADbSmBCWdHmXTs" +
                          "9QuMiDHL+GfN25r6b7uDhAGTpQQWBiFw8pAg1By/vYVF/qNZd5l6Mm5K/oPwbkKfPblpAiL5Ms6y6JNJ" +
                          "bIbwRfqzB4VL5NPGqi0u7a5FFBiKekRar6S2Fd6LjAXc0EAqLqpGaaNGDla8cbG2CA34wKLDVPOA7gic" +
                          "UzqXPapgroPvHYAZ9ehEvmnwZH/mThRWTiw5HIcJ7+HtnaIYAnJzi1ZL4g+x2eDYq3evL4AI5uSF64IS" +
                          "KNeb4h69kkUcc7Kw07dooldOjPuIbJ9sCzjAwDQSnxdMiDkkCH4caxDUoPThM/1KuTY862MQjCvuNeqV" +
                          "P0VKJh9z0HGs0alkHxeYEBWZEFclHA3AZTVroXfI126ZfWhACWrnTyNT5wDcikt5Qjn8yDBeIcbVjLsJ" +
                          "K0cABwENE4UH8JQjJLIRB0f6nuSDSrZNm59Fef0cuY+G4sgIJfidUnwg4ZJ9pcXVqUPsbBs6icaCIRmA" +
                          "ZuAik76GYcOqFjEmgFPCe7n+dmdSWFC630H4Ec0J8FYqOpqrX7PT0xC89gP85BzuELSHMKH4DTD0DSgY" +
                           "0ucojmx+ggLxo5+SghHQHacpa35lM77CUpvz7xp2NqtYTjNW8CV/dz4qdq4FX3yNLO2gltCyzsIIR/hg" +
                           "6KcHp67GMDKBl2jL1wjPoA=="

            Dim feature As Aladdin.HASP.HaspFeature = HaspFeature.Default
            hasp = New Hasp(feature)
        Catch ex As Exception
            Throw New Exception("409004-" + ex.Message)
        End Try
    End Sub

    'Public Function IsPerpetual() As Boolean
    '    Try
    '        hasp.GetType()
    '    Catch ex As Exception
    '        Throw New Exception("408011-" + ex.Message)
    '    End Try
    'End Function

    ''' <summary>
    ''' 注册检查
    ''' </summary>
    ''' <returns>返回True--正确注册；返回False--错误注册</returns>
    ''' <remarks>建议加载程序使用</remarks>
    Public Function Probe() As Boolean
        Try
            If Not hasp.IsLoggedIn Then
                'hasp = New Hasp(HaspFeature.Default)
                'Dim scope As String = "<?xml version=""1.0"" encoding=""UTF-8"" ?> " + _
                '                          "<haspscope> " + _
                '                          "   <license_manager hostname =""localhost"" /> " + _
                '                          "</haspscope>"
                'Dim status As HaspStatus = hasp.Login(vendorCode, scope)

                status = hasp.Login(vendorCode)
                If (HaspStatus.StatusOk <> status) Then
                    Return False
                End If
            End If

            Dim strSNDog As String = GetDogSNCode()
            Dim strSNFile As String = GetFileSNCode()
            If IO.File.Exists(Path) Then
                FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
                If strSNDog = "8888888888" Then
                    Return True
                End If
                If strSNDog = strSNFile Then
                    Return True
                Else
                    Return False
                End If
            Else
                WriteIni("System", "SNCode", strSNDog, Path)
                FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
                Return True
            End If

        Catch ex As Exception
            Throw New Exception("409005-" + ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' 获取加密狗当前时间
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurTime() As Date
        Try
            Return GetHaspTime().AddHours(8)
        Catch ex As Exception
            Throw New Exception("409006-" + ex.Message)
        End Try
    End Function

    Public ReadOnly Property DogID As String
        Get
            Return HaspID
        End Get
    End Property

    ''' <summary>
    ''' 获取过期日期
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetExpDate() As Date
        Try
            Dim frm As New frmTip
            frm.Help_Load(Nothing, Nothing)
            Dim retdate As Date
            If frm.txtEndTime.Text = "永久" Then
                retdate = New Date(0)
            Else
                retdate = frm.txtEndTime.Text
            End If

            frm.frmTip_Disposed(Nothing, Nothing)
            Return retdate
            'frm.Dispose()
        Catch ex As Exception
            Throw New Exception("409007-" + ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' 显示加密狗相关信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ShowDialog() As System.Windows.Forms.DialogResult
        Try
            Dim frm As New frmTip
            Return frm.ShowDialog()
            'frm.Dispose()
        Catch ex As Exception
            Throw New Exception("409007-" + ex.Message)
        End Try
    End Function

    Public Function GetSN() As String
        Try
            Probe()
            Return GetFileSNCode()
        Catch ex As Exception
            Throw New Exception("409008-" + ex.Message)
        End Try
    End Function

    Public Sub Dispose()
        Try
            If Not hasp.IsLoggedIn Then
                hasp.Logout()
            End If
            hasp.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetFile(ByVal str As String)
        Try
            Dim strCode As String = Strings.Replace(str, ",", "")
            Dim bytArr(1) As Byte
            bytArr = Text.Encoding.Default.GetBytes(strCode)
            WriteDemo(HaspFileId.ReadWrite, bytArr)
        Catch ex As Exception
            Throw New Exception("409009-" + ex.Message)
        End Try
    End Sub

    Public Function GetCurMsg() As String
        Try
            Return GetDogSNCode()
        Catch ex As Exception
            Throw New Exception("409010-" + ex.Message)
        End Try
    End Function
End Class
