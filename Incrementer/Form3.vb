Public Class Form3
    Private Sub Form3_Moved(sender As Object, e As EventArgs) Handles Me.Move
        Preview.Location = New Point(Me.Right())
        Preview.Top = Me.Top()
    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles Me.Activated
        Me.ControlBox = False
        Label2.Parent = Label1
        Label4.Parent = Label1
        Label4.Hide()
        Preview.Show()
    End Sub
    Private Sub Form3_Loaded(sender As Object, e As EventArgs) Handles Me.Load
        SetSelectedText()
    End Sub

    Sub SetSelectedText()
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\WindowsIncrementer") Is Nothing Then
            My.Computer.Registry.CurrentUser.Close()
            NumericUpDown1.Value = 59
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing) = "" Then
                My.Computer.Registry.CurrentUser.Close()
                NumericUpDown1.Value = 59
            Else
                NumericUpDown1.Value = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", Nothing)
                Preview.Label2.Font() = New Font("Segoe Marker", NumericUpDown1.Value())
                Preview.Label2.Top() = Preview.Top / NumericUpDown1.Value()
                Preview.Label2.Left() = Preview.Left / NumericUpDown1.Value()
                Preview.Height = Preview.Label2.Height + 150
                Preview.Width = Preview.Label2.Width + 150
                My.Computer.Registry.CurrentUser.Close()
            End If
        End If
    End Sub
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Value() <= 39 Then
            NumericUpDown1.Value = 40
            MsgBox("You can't have a value lower than 40!", vbCritical, "Cannot be below 40!")
        Else
            If NumericUpDown1.Value() <> 0 Then
                Preview.Label2.Font() = New Font("Segoe Marker", NumericUpDown1.Value())
                Preview.Label2.Top() = Preview.Top / NumericUpDown1.Value()
                Preview.Label2.Left() = Preview.Left / NumericUpDown1.Value()
                Preview.Height = Preview.Label2.Height + 150
                Preview.Width = Preview.Label2.Width + 150
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SetSelectedText()
        Preview.Close()
        Form4.Show()
        Form4.Location() = Me.Location()
        Me.Hide()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SetSelectedText()
        Preview.Close()
        Form2.Show()
        Form2.Location() = Me.Location()
        Me.Hide()
    End Sub

    Private Sub Form_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub SavedTextTimer_Tick(sender As Object, e As EventArgs) Handles SavedTextTimer.Tick
        Label4.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ConfirmationDialog As Integer = MessageBox.Show("Set the new size to: " & NumericUpDown1.Value(), "Save & Apply changes?", MessageBoxButtons.YesNo)
        If ConfirmationDialog = DialogResult.No Then

        Else
            Label4.Show()
            SavedTextTimer.Start()
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\WindowsIncrementer", "CounterSize", NumericUpDown1.Value)
            IncrementerCounter.SetStuff()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        IncrementerForm.Show()
        Preview.Hide()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class