Public Class MainForm


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _KEYS = New Class_KEYS(Me)
        AppName = "ClickTest"
        Me.Text = AppName

        Len = MyArr.Length - 1

        For i = 0 To Len
            MyArr(i) = 0
        Next
    End Sub

    Shared Sub UpdateArr(Optional ByVal Status As Byte = 0)
        If ONflag = False Then Exit Sub

        For i = 0 To Len - 1
            MyArr(i) = MyArr(i + 1)
        Next

        If Status = 0 Then
            MyArr(Len) = MyArr(Len)
        Else
            If MyArr(Len - 1) = 2 And Status Then
                MyArr(Len - 1) = 1
                MyArr(Len) = Status
            Else
                MyArr(Len) = Status
            End If

            'MyArr(Len) = Status
            Console.WriteLine(Status)
        End If

        MainForm.Pic1.Refresh()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateArr(0)
    End Sub

    Private Sub Pic1_Paint(ByVal sender As Object, ByVal Graph As System.Windows.Forms.PaintEventArgs) Handles Pic1.Paint
        Dim PicPoint As System.Drawing.Point
        Dim W = (Graph.Graphics.ClipBounds.Width - 1)
        Dim H = (Graph.Graphics.ClipBounds.Height - 1) * -1
        Dim StepX As Single = W / Len

        PicPoint.X = 0
        PicPoint.Y = (H * -1) - 128

        'Graph.Graphics.DrawImage(My.Resources.apple_touch_icon_precomposed, PicPoint)

        Graph.Graphics.ScaleTransform(1, -1)
        Graph.Graphics.TranslateTransform(0, H)

        For i = 0 To Len - 1
            Dim first As Single = getVals(MyArr(i), H)
            Dim last As Single = getVals(MyArr(i + 1), H)
            If first <> last Then
                If last > first Then Graph.Graphics.DrawLine(Pen, i * StepX, first, (i) * StepX, last)
                If first > last Then Graph.Graphics.DrawLine(Pen, i * StepX, first, (i) * StepX, last)
                Graph.Graphics.DrawLine(Pen, i * StepX, last, (i + 1) * StepX, last)
            Else
                Graph.Graphics.DrawLine(Pen, i * StepX, first, (i + 1) * StepX, last)
            End If
        Next
    End Sub

    Function getVals(MyVal, H) As Single
        Select Case MyVal
            Case 0
                Return 0
            Case 1
                Return H * -1
            Case 2
                Return 0
        End Select

        Return 100
    End Function

    Private Sub Pic1_Click(sender As Object, e As EventArgs) Handles Pic1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Pic1.Focus()

        If Button1.Text = "Start" Then
            If _KEYS.ThreadState = False Then _KEYS.ThreadStart()
            Button1.Text = "Stop"
            Timer1.Enabled = True
            ONflag = True

            _KEYS._Clear()
            For i = 1 To 15599
                _KEYS._Add(i, Keys.None, "SetEvent", i)
            Next

        Else
            _KEYS.ThreadStop()
            Button1.Text = "Start"
            Timer1.Enabled = False
            ONflag = False
        End If
    End Sub
End Class
