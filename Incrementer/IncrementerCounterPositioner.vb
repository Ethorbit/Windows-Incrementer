Imports Microsoft.Win32

Public Class IncrementerCounterPositioner

    Private Sub IncrementerCounter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStuff()
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

                'Me.Location = New Point(1509, 0)
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
            End If
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form2.PositioningMode = False
        Dim ConfirmationDialog As Integer = MessageBox.Show("Set the new position here?", "Save new position?", MessageBoxButtons.YesNoCancel)
        If ConfirmationDialog = DialogResult.No Then
            Form2.PositioningMode = True
        ElseIf ConfirmationDialog = DialogResult.Yes Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", "True")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionX", Me.Left, RegistryValueKind.String)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CustomPositionY", Me.Top, RegistryValueKind.String)
            Form2.PositioningMode = False
            IncrementerCounter.SetStuff()
            Me.Hide()
        ElseIf ConfirmationDialog = DialogResult.Cancel Then
            Form2.PositioningMode = False
            Me.Hide()
        End If
    End Sub
End Class