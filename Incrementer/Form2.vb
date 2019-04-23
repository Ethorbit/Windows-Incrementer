Imports Microsoft.Win32

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label4.Parent = Label1
        Label4.Hide()
        Label2.Parent = Label1
        Me.ControlBox = False
        TabPage1.Text = "Preset Positions"
        TabPage2.Text = "Custom Position"
        SetSelectedText()
    End Sub

    Sub SetSelectedText()
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\WindowsIncrementer") Is Nothing Then
            My.Computer.Registry.CurrentUser.Close()
            ComboBox1.SelectedText = "None"
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", "None") = "" Then
                My.Computer.Registry.CurrentUser.Close()
                ComboBox1.SelectedText = "None"
            Else
                ComboBox1.SelectedText = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", "None")
                My.Computer.Registry.CurrentUser.Close()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form3.Show()
        Form3.Location = New Point(Me.Location())
        Me.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        IncrementerForm.Show()
        IncrementerForm.Location = Me.Location()
        Me.Close()
    End Sub

    Private Sub Form_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex <> My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionIndex", 0) Then
            Dim ConfirmationDialog As Integer = MessageBox.Show("Set the new position to: " & ComboBox1.SelectedItem, "Save & Apply changes?", MessageBoxButtons.YesNo)
            If ConfirmationDialog = DialogResult.No Then
                ComboBox1.SelectedIndex = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionIndex", 0)
            Else
                Label4.Show()
                SavedTextTimer.Start()
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionPreset", ComboBox1.SelectedText)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "PositionIndex", ComboBox1.SelectedIndex)

                If ComboBox1.SelectedItem = "Top Left" Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", 0, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", 0, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", "False")
                    IncrementerCounter.SetStuff()

                ElseIf ComboBox1.SelectedItem = "Top Right" Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Screen.PrimaryScreen.WorkingArea.Right - Me.Width, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Screen.PrimaryScreen.WorkingArea.Top, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", "False")
                    IncrementerCounter.SetStuff()

                ElseIf ComboBox1.SelectedItem = "Bottom Right" Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", Screen.PrimaryScreen.WorkingArea.Width - Me.Width(), RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Screen.PrimaryScreen.WorkingArea.Height - Me.Height(), RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", "False")
                    IncrementerCounter.SetStuff()

                ElseIf ComboBox1.SelectedItem = "Bottom Left" Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSX", 0, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height, RegistryValueKind.String)
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "UseCustomPosition", "False")
                    IncrementerCounter.SetStuff()
                End If
            End If
        End If
    End Sub

    Private Sub SavedTextTimer_Tick(sender As Object, e As EventArgs) Handles SavedTextTimer.Tick
        Label4.Hide()
    End Sub

    Public PositioningMode As New Boolean
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PositioningMode = True
        IncrementerCounterPositioner.Show()
        PositionModeTimer.Start()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        IncrementerForm.Show()
        Form5.Hide()
        Form4.Hide()
        Form3.Hide()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        IncrementerForm.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PositionModeTimer_Tick(sender As Object, e As EventArgs) Handles PositionModeTimer.Tick
        If PositioningMode = True Then
            IncrementerCounterPositioner.Location = New Point(MousePosition.X - 50, MousePosition.Y - 50)
            PositionModeTimer.Start()

            'My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterPOSY", Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height, RegistryValueKind.String)
        End If
    End Sub
End Class