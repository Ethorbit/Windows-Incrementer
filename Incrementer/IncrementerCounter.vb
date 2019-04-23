
Public Class IncrementerCounter
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short
    Public IncrementKey As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKeyCode", Nothing)
    Public DecrementKey As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKeyCode", Nothing)

    Private Sub KeyTimerLoop_Tick(sender As Object, e As EventArgs) Handles KeyTimerLoop.Tick
        If GetAsyncKeyState(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKeyCode", Nothing)) Then
            Dim IncrementThis As New Integer
            IncrementThis = Label1.Text
            Label1.Text = IncrementThis + My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementAmount", Nothing)
            SetLocation()
        End If
        If GetAsyncKeyState(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKeyCode", Nothing)) Then
            Dim DecrementThis As New Integer
            DecrementThis = Label1.Text
            Label1.Text = DecrementThis - My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementAmount", Nothing)
            SetLocation()
        End If
        KeyTimerLoop.Start()
    End Sub

    Private Sub IncrementerCounter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStuff()
        Me.KeyPreview = True
        KeyTimerLoop.Start()
    End Sub

    Private Const WS_EX_TRANSPARENT As Integer = &H20

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or WS_EX_TRANSPARENT
            Return cp
        End Get
    End Property

    Sub SetLocation()
        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", Nothing) = "False" Then
            Dim TextSize As New Integer
            TextSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing)

            Dim Preset As String
            Preset = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", Nothing)
            If Preset = "Top Right" Then
                Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) + My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) / 2.9 - Me.Width - 90
                Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing)
                My.Computer.Registry.CurrentUser.Close()
            ElseIf Preset = "Bottom Right" Then
                Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) + My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) / 2.9 - Me.Width - 90
                Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing) * 1.7 - TextSize
                My.Computer.Registry.CurrentUser.Close()
            ElseIf Preset = "Bottom Left" Then
                Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing)
                Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing) * 1.7 - TextSize
            ElseIf Preset = "Top Left" Then
                Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing)
                Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing)
            End If
        Else
            Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionX", Nothing)
            Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionY", Nothing)
        End If
    End Sub
    Sub SetStuff()
        'Me.Height = Label1.Height + 150
        ' Me.Width = Label1.Width + 150

        Label1.ForeColor = My.Settings.CounterColor
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\WindowsIncrementer") Is Nothing Then
            My.Computer.Registry.CurrentUser.Close()
            Label1.Text = 0
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterAmount", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                Label1.Text = 0
            Else
                Label1.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterAmount", Nothing)
                My.Computer.Registry.CurrentUser.Close()
            End If

            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                Label1.Font = New Font("Segoe Marker", 59)
            Else
                Label1.Font = New Font("Segoe Marker", My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing))
                My.Computer.Registry.CurrentUser.Close()
            End If

            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                Me.Location = New Point(0, 0)
            Else

                If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", Nothing) = "False" Then
                    Dim TextSize As New Integer
                    TextSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing)

                    Dim Preset As String
                    Preset = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", Nothing)
                    If Preset = "Top Right" Then
                        Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) + My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) / 2.9 - Me.Width - 90
                        Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing)
                        My.Computer.Registry.CurrentUser.Close()
                    ElseIf Preset = "Bottom Right" Then
                        Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) + My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing) / 2.9 - Me.Width - 90
                        Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing) * 1.7 - TextSize
                        My.Computer.Registry.CurrentUser.Close()
                    ElseIf Preset = "Bottom Left" Then
                        Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing)
                        Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing) * 1.7 - TextSize
                    ElseIf Preset = "Top Left" Then
                        Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Nothing)
                        Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Nothing)
                    End If
                Else
                    Me.Left = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionX", Nothing)
                    Me.Top = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionY", Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub


End Class