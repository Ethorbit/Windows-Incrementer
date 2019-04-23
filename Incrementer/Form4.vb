Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ControlBox = False
        OtherButtonPressed = False
        ForceClosed = False
        Label2.BackColor = My.Settings.CounterColor
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ColorDialogResult As DialogResult
        ColorDialogResult = ColorDialog1.ShowDialog()

        If ColorDialogResult = Windows.Forms.DialogResult.OK Then
            My.Settings.CounterColor = ColorDialog1.Color
            My.Settings.Save()
            Label2.BackColor = My.Settings.CounterColor
            IncrementerCounter.SetStuff()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OtherButtonPressed = True
        Form5.Show()
        Form5.Location = Me.Location
        Me.Hide()
    End Sub
    Public OtherButtonPressed As New Boolean
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OtherButtonPressed = True
        Form3.Show()
        Form3.Location = Me.Location
        Me.Hide()
    End Sub

    Public ForceClosed As New Boolean
    Private Sub Form4_Closed(sender As Object, e As EventArgs) Handles Me.FormClosing
        ForceClosed = True
        IncrementerForm.Show()
        IncrementerForm.Location = Me.Location
        Form2.Close()
        Form3.Close()
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