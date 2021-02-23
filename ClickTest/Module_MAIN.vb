Module Module_MAIN
    Public _KEYS As Class_KEYS

    Public MyArr(0 To 200) As Byte
    Public CurX As Single
    Public Len As Long
    Public Graph As Graphics
    Public brush As New SolidBrush(Color.White)
    Public Pen As New Pen(Color.White, 1)
    Public H As Single
    Public W As Single
    Public ONflag As Boolean = False
    Public AppName As String = Nothing

End Module
