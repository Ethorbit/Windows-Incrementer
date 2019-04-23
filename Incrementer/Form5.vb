Public Class Form5
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ControlBox = False
        Me.KeyPreview = True
        IncrementHotKeyText()
        DecrementHotKeyText()
    End Sub
    Sub IncrementHotKeyText()
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\WindowsIncrementer") Is Nothing Then
            My.Computer.Registry.CurrentUser.Close()
            TextBox2.Text = "  Select and press a key"
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKey", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                TextBox2.Text = "  Select and press a key"
            Else
                TextBox2.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKey", Nothing)
                My.Computer.Registry.CurrentUser.Close()
            End If
        End If
    End Sub
    Sub DecrementHotKeyText()
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\WindowsIncrementer") Is Nothing Then
            My.Computer.Registry.CurrentUser.Close()
            TextBox3.Text = "  Select and press a key"
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKey", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                TextBox3.Text = "  Select and press a key"
            Else
                TextBox3.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKey", Nothing)
                My.Computer.Registry.CurrentUser.Close()
            End If
        End If
    End Sub

    Private Sub Form5_KeyWasPressed(sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKey", e.KeyCode)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "IncrementHotKeyCode", e.KeyValue)
        TextBox2.Text = e.KeyCode.ToString()
    End Sub
    Private Sub TextBox2_FocusReceived(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        If TextBox2.Text = "  Select and press a key" Then
            TextBox2.Text = ""
        End If
    End Sub
    Private Sub TextBox2_FocusLost(sender As Object, e As EventArgs) Handles TextBox2.LostFocus
        If TextBox2.Text = "" Then
            TextBox2.Text = "  Select and press a key"
        End If
    End Sub


    Private Sub Form5_KeyWasPressed2(sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKey", e.KeyCode)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "DecrementHotKeyCode", e.KeyValue)
        TextBox3.Text = e.KeyCode.ToString()
    End Sub
    Private Sub TextBox2_FocusReceived2(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
        If TextBox3.Text = "  Select and press a key" Then
            TextBox3.Text = ""
        End If
    End Sub
    Private Sub TextBox2_FocusLost2(sender As Object, e As EventArgs) Handles TextBox3.LostFocus
        If TextBox3.Text = "" Then
            TextBox3.Text = "  Select and press a key"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ConfigurationPopup As DialogResult
        ConfigurationPopup = MessageBox.Show("Are you sure you want to finish the configuration?", "Finished?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ConfigurationPopup = vbYes Then
            OtherButtonPressed = True
            IncrementerForm.Show()
            IncrementerForm.Location() = Me.Location()
            Me.Hide()
        End If
    End Sub

    Public OtherButtonPressed As New Boolean
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OtherButtonPressed = True
        Form4.Show()
        Form4.Location() = Me.Location()
        Me.Hide()
    End Sub

    Private Sub Form_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        IncrementerForm.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class