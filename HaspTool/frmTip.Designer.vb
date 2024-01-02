<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTip
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblStartTime = New System.Windows.Forms.Label()
        Me.txtHaspID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEndTime = New System.Windows.Forms.TextBox()
        Me.lblState = New System.Windows.Forms.Label()
        Me.txtStatue = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTime = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSN = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblStartTime
        '
        Me.lblStartTime.AutoSize = True
        Me.lblStartTime.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblStartTime.ForeColor = System.Drawing.Color.White
        Me.lblStartTime.Location = New System.Drawing.Point(22, 126)
        Me.lblStartTime.Name = "lblStartTime"
        Me.lblStartTime.Size = New System.Drawing.Size(63, 14)
        Me.lblStartTime.TabIndex = 0
        Me.lblStartTime.Text = "HaspID："
        '
        'txtHaspID
        '
        Me.txtHaspID.BackColor = System.Drawing.Color.Black
        Me.txtHaspID.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtHaspID.ForeColor = System.Drawing.Color.White
        Me.txtHaspID.Location = New System.Drawing.Point(90, 122)
        Me.txtHaspID.Name = "txtHaspID"
        Me.txtHaspID.ReadOnly = True
        Me.txtHaspID.Size = New System.Drawing.Size(166, 23)
        Me.txtHaspID.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(10, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "到期时间："
        '
        'txtEndTime
        '
        Me.txtEndTime.BackColor = System.Drawing.Color.Black
        Me.txtEndTime.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtEndTime.ForeColor = System.Drawing.Color.White
        Me.txtEndTime.Location = New System.Drawing.Point(90, 64)
        Me.txtEndTime.Name = "txtEndTime"
        Me.txtEndTime.ReadOnly = True
        Me.txtEndTime.Size = New System.Drawing.Size(166, 23)
        Me.txtEndTime.TabIndex = 1
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.White
        Me.lblState.Location = New System.Drawing.Point(10, 10)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(77, 14)
        Me.lblState.TabIndex = 0
        Me.lblState.Text = "状态提示："
        '
        'txtStatue
        '
        Me.txtStatue.BackColor = System.Drawing.Color.Black
        Me.txtStatue.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtStatue.ForeColor = System.Drawing.Color.White
        Me.txtStatue.Location = New System.Drawing.Point(90, 6)
        Me.txtStatue.Name = "txtStatue"
        Me.txtStatue.ReadOnly = True
        Me.txtStatue.Size = New System.Drawing.Size(166, 23)
        Me.txtStatue.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(10, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 14)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "当前时间："
        '
        'txtTime
        '
        Me.txtTime.BackColor = System.Drawing.Color.Black
        Me.txtTime.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtTime.ForeColor = System.Drawing.Color.White
        Me.txtTime.Location = New System.Drawing.Point(90, 93)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.ReadOnly = True
        Me.txtTime.Size = New System.Drawing.Size(166, 23)
        Me.txtTime.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(38, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "SN码："
        '
        'txtSN
        '
        Me.txtSN.BackColor = System.Drawing.Color.Black
        Me.txtSN.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtSN.ForeColor = System.Drawing.Color.White
        Me.txtSN.Location = New System.Drawing.Point(90, 35)
        Me.txtSN.Name = "txtSN"
        Me.txtSN.ReadOnly = True
        Me.txtSN.Size = New System.Drawing.Size(166, 23)
        Me.txtSN.TabIndex = 1
        '
        'frmTip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(267, 157)
        Me.Controls.Add(Me.txtTime)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEndTime)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtStatue)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.txtSN)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtHaspID)
        Me.Controls.Add(Me.lblStartTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTip"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Help"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblStartTime As System.Windows.Forms.Label
    Friend WithEvents txtHaspID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEndTime As System.Windows.Forms.TextBox
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents txtStatue As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSN As System.Windows.Forms.TextBox
End Class
