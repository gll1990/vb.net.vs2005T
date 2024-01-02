Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Resources
Imports System.Runtime.CompilerServices

Namespace hasp_rehost.Properties
    <GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode(), CompilerGenerated()> _
    Friend Class Resources
        ' Methods
        Friend Sub New()
        End Sub


        ' Properties
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Friend Shared Property Culture() As CultureInfo
            Get
                Return Resources.resourceCulture
            End Get
            Set(ByVal value As CultureInfo)
                Resources.resourceCulture = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Friend Shared ReadOnly Property ResourceManager() As ResourceManager
            Get
                If Object.ReferenceEquals(Resources.resourceMan, Nothing) Then
                    Dim temp As New ResourceManager("hasp_rehost.Properties.Resources", GetType(Resources).Assembly)
                    Resources.resourceMan = temp
                End If
                Return Resources.resourceMan
            End Get
        End Property


        ' Fields
        Private Shared resourceCulture As CultureInfo
        Private Shared resourceMan As ResourceManager
    End Class
End Namespace

