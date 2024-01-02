Imports Aladdin.HASP
Module MainModule

    Public vendorCode As String
    Public status As Aladdin.HASP.HaspStatus
    Public hasp As Hasp
    Public intID As Integer
    Public HaspID As String
    Public Path As String = "D:\DSDSystem.ini"
    Public NewSN As String = String.Empty
    Public IsSetNew As Boolean = False

    Public Function FomateSNCode(ByVal strCode As String) As String
        If strCode.Length = 10 Then
            Dim ret As String = Strings.Mid(strCode, 1, 2)
            For i As Byte = 2 To strCode.Length - 2 Step 2
                ret += "-" & Strings.Mid(strCode, i + 1, 2)
            Next
            Return ret
        Else
            Throw New Exception("408019--SNCode Length Error")
        End If
    End Function
 
    Public Function ReadDemo(ByVal fileId As HaspFileId) As String
        'sanity check
        If (Nothing Is hasp) Then
            Throw New Exception("408013- hasp is nothing")
        End If
        If (Not hasp.IsLoggedIn()) Then
            status = hasp.Login(vendorCode)
            If (HaspStatus.StatusOk <> status) Then
                Throw New Exception("408014- Failed to Login in ReadDemo,status: " & status.ToString)
            End If
        End If


        'Verbose("Read/Write Demo")

        'Get a file object to a key's memory file.
        'please note: the file object is tightly connected
        'to its key object. logging out from a key also
        'invalidates the file object.
        'doing the following will result in an invalid
        'file object:
        'hasp.login(...)
        'Dim file As HaspFile = hasp.GetFile()
        'hasp.Logout()
        'Debug.Assert(file.IsValid()) will assert
        Dim file As HaspFile = hasp.GetFile(fileId)
        If (Not file.IsLoggedIn()) Then
            'Not logged into a key - nothing left to do.
            Throw New Exception("408009-Failed to get file object in Login")

        End If

        'Verbose("Reading contents of file: " + file.FileId.ToString())

        'Verbose("Retrieving the size of the file")
        If (Not hasp.IsLoggedIn()) Then
            status = hasp.Login(vendorCode)
            If (HaspStatus.StatusOk <> status) Then
                Throw New Exception("408014- Failed to Login in ReadDemo,status: " & status.ToString)
            End If
        End If

        'get the file's size
        Dim size As Int32 = 0
        status = file.FileSize(size)
        'ReportStatus(status)
        If status <> HaspStatus.StatusOk Then
            Dim status1 As Aladdin.HASP.HaspStatus = hasp.Login(vendorCode)
            If (HaspStatus.StatusOk <> status1) Then
                Throw New Exception("408054- Failed to Login in ReadDemo,status: " & status.ToString & "," & status1.ToString)
            End If
            status = file.FileSize(size)
        End If
        If (HaspStatus.StatusOk <> status) Then
            Throw New Exception("408010-Failed to get file object in GetSize, Status: " & status.ToString)
        End If

        'Verbose("Size of the file is: " + size.ToString() + " Bytes")

        'read the contents of the file into a buffer
        'allocate sufficient buffer
        Dim bytes() As Byte = New Byte(size - 1) {}

        'Verbose("Reading data")
        status = file.Read(bytes, 0, bytes.Length)
        'ReportStatus(status)

        If (HaspStatus.StatusOk <> status) Then
            Throw New Exception("408011-Failed to get file object in Read, Status: " & status.ToString)

        End If

        Dim Ret As String = String.Empty

        'For i As Byte = 0 To 4
        '    Ret += bytes(i).ToString("X2")
        'Next
        For i As Byte = 0 To 9
            Ret += Chr(bytes(i))
        Next
        Return Ret

    End Function

    Public Sub WriteDemo(ByVal fileId As HaspFileId, ByVal arrbytes() As Byte)
        'sanity check
        If (Nothing Is hasp) Then
            Throw New Exception("408015- hasp is nothing")
        End If

        If (Not hasp.IsLoggedIn()) Then
            status = hasp.Login(vendorCode)
            If (HaspStatus.StatusOk <> status) Then
                Throw New Exception("408016- Failed to Login in ReadDemo, Status: " & status.ToString)
            End If
        End If

        'Get a file object to a key's memory file.
        'please note: the file object is tightly connected
        'to its key object. logging out from a key also
        'invalidates the file object.
        'doing the following will result in an invalid
        'file object:
        'hasp.login(...)
        'Dim file As HaspFile = hasp.GetFile()
        'hasp.Logout()
        'Debug.Assert(file.IsValid()) will assert
        Dim file As HaspFile = hasp.GetFile(fileId)
        If (Not file.IsLoggedIn()) Then
            'Not logged into a key - nothing left to do.
            Throw New Exception("408017-Failed to get file object in Login")

        End If

        'now let's write some data into the file
        'Dim newBytes() As Byte = New Byte() {10, 2, 3, 4, 5, 6, 7}

        status = file.Write(arrbytes, 0, arrbytes.Length)
        'ReportStatus(status)
        If (HaspStatus.StatusOk <> status) Then
            Throw New Exception("408018-Failed to get file object in Write, Status: " & status.ToString)
        End If

    End Sub

    Public Sub SetFileSNCode(ByVal FileSNCode As String)
        Try
            FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Normal)
            WriteIni("System", "SNCode", FileSNCode, Path)
            FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
        Catch ex As Exception
            Throw New Exception("408020-写入失败!")
        End Try
    End Sub

    Public Function GetFileSNCode() As String
        Try
            Dim ret As String = String.Empty
            If IO.File.Exists(Path) Then
                ret = ReadIni("System", "SNCode", "0000000000", Path)
                If ret <> String.Empty Then
                    'If ret.Length = 10 Then
                    FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
                    Return ret
                    'End If
                End If
            End If
            ret = GetDogSNCode()
            WriteIni("System", "SNCode", ret, Path)
            FileSystem.SetAttr(Path, FileAttribute.System Or FileAttribute.Hidden Or FileAttribute.ReadOnly)
            Return ret
        Catch ex As Exception
            Throw New Exception("408019-File获取失败!")
        End Try
    End Function

    Public Function GetDogSNCode() As String
        Try
            Return ReadDemo(HaspFileId.ReadWrite)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHaspTime() As Date
        Try
            Dim time As Date = DateTime.Now
            If (Not hasp.IsLoggedIn()) Then
                status = hasp.Login(vendorCode)
                If (HaspStatus.StatusOk <> status) Then
                    Throw New Exception("408012-Failed to get hasp time in Login, Status: " & status.ToString)
                    'Return Nothing
                End If
            End If
            status = hasp.GetRtc(time)
            Return time
            'If (HaspStatus.StatusOk = status) Then
            '    status = hasp.GetRtc(time)
            '    If (HaspStatus.StatusOk <> status) Then
            '        ' ''handle error
            '        status = hasp.Logout()
            '    Else
            '        status = hasp.Logout()
            '    End If
            'End If
            Return time
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub WriteToTxt(ByVal FilePath As String, ByVal strContent As String)       '作为参考, 别处写时按此写即可
        Dim st As New IO.FileStream(FilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        st.Position = st.Length
        Dim sw As New IO.StreamWriter(st)
        Try
            sw.WriteLine(strContent)
        Finally
            sw.Close()
            st.Close()
        End Try
    End Sub

    Public Function ReadtxtFile(ByVal strFilePath As String) As String
        Dim sr As New IO.StreamReader(strFilePath)
        Try
            If IO.File.Exists(strFilePath) = False Then
                Return "NO FILE EXITS!"
            End If
            Dim strRead As String = sr.ReadToEnd()
            Return strRead
        Finally
            sr.Close()
            sr.Dispose()
        End Try
    End Function

    Public Function GetHaspMsg() As ArrayList
        Dim scope As String = "<?xml version=""1.0"" encoding=""UTF-8"" ?> " + _
                                          "<haspscope/>"

        Dim format As String = _
        "<?xml version=""1.0"" encoding=""UTF-8"" ?>" + _
        "<haspformat root=""hasp_info"">" + _
        "    <feature>" + _
        "        <attribute name=""id"" />" + _
        "        <element name=""license"" />" + _
        "        <hasp>" + _
        "          <attribute name=""id"" />" + _
        "          <attribute name=""type"" />" + _
        "        </hasp>" + _
        "    </feature>" + _
        "</haspformat>" + _
        ""
        If (Not hasp.IsLoggedIn()) Then
            status = hasp.Login(vendorCode)
            If (HaspStatus.StatusOk <> status) Then
                Throw New Exception("408018-Failed to get hasp time in Login, Status: " & status.ToString)
                'Return Nothing
            End If
        End If
        Dim info As String = Nothing
        status = Aladdin.HASP.Hasp.GetInfo(scope, format, vendorCode, info)
        If (HaspStatus.StatusOk <> status) Then
            Return Nothing
        Else
            Dim InfoArr As New ArrayList
            GetXmltoList(info, InfoArr)
            Return InfoArr
        End If
    End Function

    Public Sub GetXmltoList(ByVal strRead As String, ByRef XMLarray As ArrayList)
        Try
            '<?xml version="1.0" encoding="UTF-8" ?>
            '<hasp_info>
            '  <feature id="0">
            '    <license>
            '      <license_type>perpetual</license_type>
            '    </license>
            '    <hasp id="118553699" type="HASP-HL" />
            '  </feature>
            '  <feature id="404">
            '    <license>
            '      <license_type>expiration</license_type>
            '      <exp_date>1483228500</exp_date>
            '    </license>
            '    <hasp id="118553699" type="HASP-HL" />
            '  </feature>
            '</hasp_info>

            Dim Icount As Byte = 0
            Dim StrInfo() As String = Strings.Split(strRead, "<feature id=")
            For i As Integer = 0 To StrInfo.Length - 1
                Dim strFeature As String = StrInfo(i)
                If strFeature.Contains("</feature>") Then
                    Dim xmlTemp As InfoXml = New InfoXml
                    Dim strid As String = Strings.Mid(strFeature, 2, strFeature.IndexOf(">") - 2)
                    xmlTemp.ID = strid
                    If strFeature.Contains("<license_type>") Then
                        Dim strtype As String = Strings.Mid(strFeature, strFeature.IndexOf("<license_type>") + 15, strFeature.IndexOf("</license_type>") - strFeature.IndexOf("<license_type>") - 14)
                        Select Case strtype
                            Case "executions"
                                xmlTemp.license_type = LicenseType.executions
                            Case "trial"
                                xmlTemp.license_type = LicenseType.trial
                                If strFeature.Contains("<time_start>") Then
                                    Dim strTst As String = Strings.Mid(strFeature, strFeature.IndexOf("<time_start>") + 13, strFeature.IndexOf("</time_start>") - strFeature.IndexOf("<time_start>") - 12)
                                    If strTst = "uninitialized" Then
                                        xmlTemp.time_start = -1
                                    Else
                                        xmlTemp.time_start = strTst
                                        Dim strTtl As String = Strings.Mid(strFeature, strFeature.IndexOf("<total_time>") + 13, strFeature.IndexOf("</total_time>") - strFeature.IndexOf("<total_time>") - 12)
                                        xmlTemp.total_time = strTtl
                                    End If
                                Else

                                End If
                            Case "perpetual"
                                xmlTemp.license_type = LicenseType.perpetual
                            Case "expiration"
                                xmlTemp.license_type = LicenseType.expiration
                                If strFeature.Contains("<exp_date>") Then
                                    Dim strExp As String = Strings.Mid(strFeature, strFeature.IndexOf("<exp_date>") + 11, strFeature.IndexOf("</exp_date>") - strFeature.IndexOf("<exp_date>") - 10)
                                    xmlTemp.total_time = strExp
                                Else
                                    MsgBox("408003- exp_date 解析错误")
                                End If

                        End Select
                        If strFeature.Contains("<hasp id=") Then
                            Dim strHaspid As String = Strings.Mid(strFeature, strFeature.IndexOf("<hasp id=") + 11, strFeature.IndexOf(" type=") - strFeature.IndexOf("<hasp id=") - 11)
                            xmlTemp.HaspID = strHaspid
                            If xmlTemp.ID = intID Then
                                HaspID = strHaspid
                            End If
                        Else
                            MsgBox("408001-hasp id 解析错误")
                        End If
                    Else
                        MsgBox("408002-license_type 解析错误")
                    End If
                    XMLarray.Add(xmlTemp.Clone)

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetXmlToHash(ByRef strRead As String, ByRef InfoHT As Hashtable)
        Try
            Dim Icount As Byte = 0
            'Dim filepath As String = "C:\WINDOWS\RunTempStatue.txt"
            'If IO.File.Exists(filepath) Then IO.File.Delete(filepath)
            'WriteToTxt(filepath, strRead)
            'Dim strTemp As String = ReadtxtFile(filepath)
            InfoHT = New Hashtable
            Dim StrInfo() As String = Strings.Split(strRead, vbLf)
            For i As Byte = 0 To StrInfo.Length - 2
                StrInfo(i) = StrInfo(i).Trim()
                Console.WriteLine(StrInfo(i))
                'Console.WriteLine(StrInfo(i).Substring(1, 11))
                If StrInfo(i).Length = 16 Then
                    Icount += 1
                    InfoHT.Add(Icount & StrInfo(i).Substring(1, 10), StrInfo(i).Substring(13, 1))
                ElseIf StrInfo(i).Length >= 17 Then
                    If StrInfo(i).Substring(1, 11) = "feature id=" Then
                        Icount += 1
                        InfoHT.Add(Icount & StrInfo(i).Substring(1, 10), StrInfo(i).Substring(13, StrInfo(i).Length - 15))
                        Dim str As String = StrInfo(i).Substring(13, StrInfo(i).Length - 17 + 2)
                        If str = "" Then

                        Else

                        End If
                    End If
                End If
                'StrInfo(i) = StrInfo(i).Trim()
                If StrInfo(i).Length > 12 And StrInfo(i).Substring(1, 4) <> "hasp" And StrInfo(i).Substring(1, 4) <> "?xml" And StrInfo(i).Substring(1, 7) <> "feature" Then
                    Dim Config_Key As String = StrInfo(i).Substring(1, StrInfo(i).IndexOf(">") - 1)
                    Dim Config_val As String = StrInfo(i).Substring(StrInfo(i).IndexOf(">") + 1, (StrInfo(i).LastIndexOf("<") - StrInfo(i).IndexOf(">") - 1))
                    InfoHT.Add(Icount & Config_Key, Config_val)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetHash2List(ByVal InfoHT As Hashtable, ByRef InfoArr As ArrayList)
        Try
            Dim de As DictionaryEntry
            Dim Val_Counts As Byte = 1
            Dim hashname As String = ""
            Dim hashval As String = ""
            For Each de In InfoHT
                hashname = de.Key
                hashval = de.Value
                Dim key_name As Byte = hashname.Substring(0, 1)
                If Val_Counts < key_name Then
                    Val_Counts = key_name
                End If
            Next
            For byte_i As Byte = 1 To Val_Counts
                Dim info_Xml As New InfoXml
                info_Xml.counter_fix = -1
                info_Xml.counter_var = -1
                info_Xml.time_start = -1
                info_Xml.total_time = -1
                info_Xml.license_type = -1
                info_Xml.ID = -1
                For Each de In InfoHT
                    hashname = de.Key
                    hashval = de.Value
                    If hashname.Substring(1) = "feature id" And byte_i = hashname.Substring(0, 1) Then
                        info_Xml.ID = CInt(hashval)
                    End If
                    If hashname.Substring(1) = "license_type" And byte_i = hashname.Substring(0, 1) Then
                        Select Case hashval
                            Case "executions"
                                info_Xml.license_type = LicenseType.executions
                            Case "trial"
                                info_Xml.license_type = LicenseType.trial
                            Case "perpetual"
                                info_Xml.license_type = LicenseType.perpetual
                        End Select

                    ElseIf hashname.Substring(1) = "counter_fix" And byte_i = hashname.Substring(0, 1) Then
                        info_Xml.counter_fix = CInt(hashval)
                    ElseIf hashname.Substring(1) = "counter_var" And byte_i = hashname.Substring(0, 1) Then
                        info_Xml.counter_var = CInt(hashval)
                    ElseIf hashname.Substring(1) = "time_start" And byte_i = hashname.Substring(0, 1) Then
                        If hashval = "uninitialized" Then
                            info_Xml.time_start = -1
                        Else
                            info_Xml.time_start = CLng(hashval)
                        End If
                    ElseIf hashname.Substring(1) = "total_time" And byte_i = hashname.Substring(0, 1) Then
                        info_Xml.total_time = CLng(hashval)
                    End If
                Next
                InfoArr.Add(info_Xml)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Class InfoXml
        Public HaspID As String
        Public ID As Integer
        Public license_type As LicenseType
        Public counter_fix As Integer
        Public counter_var As Integer
        Public time_start As Long
        Public total_time As Long

        Public Function Clone() As InfoXml
            Dim Ret As New InfoXml
            Ret.HaspID = HaspID
            Ret.ID = ID
            Ret.license_type = license_type
            Ret.counter_fix = counter_fix
            Ret.counter_var = counter_var
            Ret.time_start = time_start
            Ret.total_time = total_time
            Return Ret
        End Function
    End Class

    Public Enum LicenseType
        executions
        trial
        perpetual
        expiration
    End Enum
End Module
