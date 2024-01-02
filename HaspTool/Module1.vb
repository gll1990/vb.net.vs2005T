Module Module1
#Region "分区域读写"

#Region "底层函数"
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" _
        (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpDefault As String, _
        ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" _
    (ByVal lpSectionName As String, ByVal lpKeyName As String, _
     ByVal lpString As String, ByVal lpFileName As String) As Int32
#End Region

    '按区域读     1                      
    Private Function ReadIni(ByVal lpSectionName As String, ByVal lpKeyName As String, _
                             ByVal lpDefault As String, ByVal nSize As Integer, ByVal lpFilePath As String) As String
        Try
            Dim intReturned As Integer
            Dim strReturn As String = String.Empty
            strReturn = LSet(strReturn, nSize)
            intReturned = GetPrivateProfileString(lpSectionName, lpKeyName, lpDefault, strReturn, Len(strReturn), lpFilePath)
            strReturn = Microsoft.VisualBasic.Left(strReturn, InStr(strReturn, Chr(0)) - 1)
            If intReturned = strReturn.Length Then
                Return strReturn
            Else
                Return String.Empty
                '  Throw New AppException("00201", My.Resources.读配置文件错误)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '                                ReadIni(区域名,关键词,默认值,文件路径)   
    Public Function ReadIni(ByVal lpSectionName As String, ByVal lpKeyName As String, _
                           ByVal lpDefault As String, ByVal lpFilePath As String) As String
        Return ReadIni(lpSectionName, lpKeyName, lpDefault, 256, lpFilePath)
    End Function

    ' '按区域写
    Public Sub WriteIni(ByVal lpSectionName As String, ByVal lpKeyName As String, _
                             ByVal lpString As String, ByVal lpFilePath As String)
        Try
            Dim intReturned As Integer
            intReturned = WritePrivateProfileString(lpSectionName, lpKeyName, lpString, lpFilePath)
            If intReturned = 0 Then
                '   Throw New AppException("00202", My.Resources.写配置文件错误)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Module
