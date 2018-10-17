Imports System.Drawing
Imports System.Windows.Forms

Public Class TwosGameForm
    Private Declare Function ReleaseCapture Lib "user32" () As Integer
    Private Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer

    Private Const TitleHeight As Integer = 40 '标题栏高度
    Private Const PaddingSize As Integer = 60 '裁片集中区域与窗体边框的距离
    Dim Score As Integer = 0 '分数
    Dim Moved As Boolean '定义一个标识，记录是否发生了移动，以确定操作是否有效
    Dim CardData(3, 3) As Long

    Private Sub GameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DirectiionKeyBitmap As Bitmap = My.Resources.DefaultGameResource.DirectiionKey
        DirectiionKeyBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.KeyUpLabel.Image = DirectiionKeyBitmap.Clone
        DirectiionKeyBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.KeyRightLabel.Image = DirectiionKeyBitmap.Clone
        DirectiionKeyBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.KeyDownLabel.Image = DirectiionKeyBitmap.Clone

        Me.CreatNewCard()
        Me.DrawForm()
    End Sub

    ''' <summary>
    ''' 根据 CardData 数组刷新界面
    ''' </summary>
    Private Sub DrawForm()
        Me.ScoreLabel.Text = Me.Score.ToString
        Dim UnityBitmap As Bitmap = My.Resources.DefaultGameResource.TwosBackground
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmap)
            For IndexY As Integer = 0 To 3
                For IndexX As Integer = 0 To 3
                    UnityGraphics.DrawImage(My.Resources.DefaultGameResource.ResourceManager.GetObject("Card" & Me.CardData(IndexY, IndexX)), PaddingSize + IndexX * 70, PaddingSize + IndexY * 74 + TitleHeight, 70, 74)
                Next
            Next
        End Using
        Me.BackgroundImage = UnityBitmap
        GC.Collect() '回收内存
    End Sub

    ''' <summary>
    ''' 相应卡片布局变化（先压缩空格，再合并卡片，再次压缩空格）
    ''' </summary>
    Private Sub ApplyCardsChange(ByVal Key As Keys)
        Dim IndexX, IndexY As Integer
        Me.Moved = False
        Select Case Key
            Case Keys.Up
                Me.RemoveBlock(Key)
                For IndexX = 0 To 3 Step 1
                    For IndexY = 0 To 2 Step 1
                        If Me.CardData(IndexY, IndexX) > 0 Then
                            If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY + 1, IndexX) Then
                                Me.CardData(IndexY, IndexX) = 2 * Me.CardData(IndexY, IndexX)
                                Me.Score += Me.CardData(IndexY, IndexX)
                                Me.CardData(IndexY + 1, IndexX) = 0
                                IndexY += 1 '合并后跳过一个卡片
                                Me.Moved = True
                            End If
                        End If
                    Next
                Next
                Me.RemoveBlock(Key)
            Case Keys.Down
                Me.RemoveBlock(Key)
                For IndexX = 0 To 3 Step 1
                    For IndexY = 3 To 1 Step -1
                        If Me.CardData(IndexY, IndexX) > 0 Then
                            If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY - 1, IndexX) Then
                                Me.CardData(IndexY, IndexX) = 2 * Me.CardData(IndexY, IndexX)
                                Me.Score += Me.CardData(IndexY, IndexX)
                                Me.CardData(IndexY - 1, IndexX) = 0
                                IndexY -= 1 '合并后跳过一个卡片
                                Me.Moved = True
                            End If
                        End If
                    Next
                Next
                Me.RemoveBlock(Key)
            Case Keys.Left
                Me.RemoveBlock(Key)
                For IndexY = 0 To 3 Step 1
                    For IndexX = 0 To 2 Step 1
                        If Me.CardData(IndexY, IndexX) > 0 Then
                            If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY, IndexX + 1) Then
                                Me.CardData(IndexY, IndexX) = 2 * Me.CardData(IndexY, IndexX)
                                Me.Score += Me.CardData(IndexY, IndexX)
                                Me.CardData(IndexY, IndexX + 1) = 0
                                IndexX += 1 '合并后跳过一个卡片
                                Me.Moved = True
                            End If
                        End If
                    Next
                Next
                Me.RemoveBlock(Key)
            Case Keys.Right
                Me.RemoveBlock(Key)
                For IndexY = 0 To 3 Step 1
                    For IndexX = 3 To 1 Step -1
                        If Me.CardData(IndexY, IndexX) > 0 Then
                            If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY, IndexX - 1) Then
                                Me.CardData(IndexY, IndexX) = 2 * Me.CardData(IndexY, IndexX)
                                Me.Score += Me.CardData(IndexY, IndexX)
                                Me.CardData(IndexY, IndexX - 1) = 0
                                IndexX -= 1 '合并后跳过一个卡片
                                Me.Moved = True
                            End If
                        End If
                    Next
                Next
                Me.RemoveBlock(Key)
        End Select

        If Me.Moved Then
            Me.CreatNewCard() '检测是否产生新的数字 2或4
            Me.DrawForm()       '重新刷新界面
        End If
    End Sub

    ''' <summary>
    ''' 将对应方向的空格压缩，使卡片紧凑
    ''' </summary>
    Private Sub RemoveBlock(ByVal Key As Keys)
        Dim IndexX, IndexY, Dis As Integer
        Select Case Key
            Case Keys.Up
                For IndexX = 0 To 3 Step 1
                    Dis = 1
                    For IndexY = 0 To 2 Step 1
                        If Me.CardData(IndexY, IndexX) = 0 Then
                            Do While IndexY + Dis < 4
                                If Me.CardData(IndexY + Dis, IndexX) > 0 Then
                                    Me.CardData(IndexY, IndexX) = Me.CardData(IndexY + Dis, IndexX)
                                    Me.CardData(IndexY + Dis, IndexX) = 0
                                    Me.Moved = True
                                    Exit Do
                                End If
                                Dis += 1
                            Loop
                        End If
                    Next
                Next
            Case Keys.Down
                For IndexX = 0 To 3 Step 1
                    Dis = -1
                    For IndexY = 3 To 1 Step -1
                        If Me.CardData(IndexY, IndexX) = 0 Then
                            Do While IndexY + Dis > -1
                                If Me.CardData(IndexY + Dis, IndexX) > 0 Then
                                    Me.CardData(IndexY, IndexX) = Me.CardData(IndexY + Dis, IndexX)
                                    Me.CardData(IndexY + Dis, IndexX) = 0
                                    Me.Moved = True
                                    Exit Do
                                End If
                                Dis -= 1
                            Loop
                        End If
                    Next
                Next
            Case Keys.Left
                For IndexY = 0 To 3 Step 1
                    Dis = 1
                    For IndexX = 0 To 2 Step 1
                        If Me.CardData(IndexY, IndexX) = 0 Then
                            Do While IndexX + Dis < 4
                                If Me.CardData(IndexY, IndexX + Dis) > 0 Then
                                    Me.CardData(IndexY, IndexX) = Me.CardData(IndexY, IndexX + Dis)
                                    Me.CardData(IndexY, IndexX + Dis) = 0
                                    Me.Moved = True
                                    Exit Do
                                End If
                                Dis += 1
                            Loop
                        End If
                    Next
                Next
            Case Keys.Right
                For IndexY = 0 To 3 Step 1
                    Dis = -1
                    For IndexX = 3 To 1 Step -1
                        If Me.CardData(IndexY, IndexX) = 0 Then
                            Do While IndexX + Dis > -1
                                If Me.CardData(IndexY, IndexX + Dis) > 0 Then
                                    Me.CardData(IndexY, IndexX) = Me.CardData(IndexY, IndexX + Dis)
                                    Me.CardData(IndexY, IndexX + Dis) = 0
                                    Me.Moved = True
                                    Exit Do
                                End If
                                Dis -= 1
                            Loop
                        End If
                    Next
                Next
        End Select
    End Sub

    ''' <summary>
    ''' 每一步移动之后需要为游戏产生一个新的卡片（新卡片是 2 或 4）
    ''' </summary>
    Private Sub CreatNewCard()
        Dim BlankList As New ArrayList
        Dim RndIndex As Integer
        Dim PointX, PointY As Integer
        For IndexY As Integer = 0 To 3
            For IndexX As Integer = 0 To 3
                If Me.CardData(IndexY, IndexX) = 0 Then
                    BlankList.Add(IndexY * 4 + IndexX)
                End If
            Next
        Next

        RndIndex = CType(BlankList(Int(VBMath.Rnd * BlankList.Count)), Integer)
        PointY = RndIndex \ 4 : PointX = RndIndex Mod 4
        Me.CardData(PointY, PointX) = IIf(VBMath.Rnd > 0.5, 4, 2)

        If BlankList.Count = 1 Then
            If Me.IsGameOver() Then
                '调用游戏结束函数
                Me.GameOver()
            End If
        End If
    End Sub

    Private Sub GameForm_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Me.ApplyCardsChange(e.KeyCode) '相应用户按键
    End Sub

    ''' <summary>
    ''' 判断是否还存在相同且相邻的卡片，以判断游戏是否结束
    ''' </summary>
    ''' <returns>游戏是否结束了</returns>
    Private Function IsGameOver() As Boolean
        For IndexY As Integer = 0 To 3
            For IndexX As Integer = 0 To 2
                If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY, IndexX + 1) Then
                    Return False
                End If
            Next
        Next
        For IndexX As Integer = 0 To 3
            For IndexY As Integer = 0 To 2
                If Me.CardData(IndexY, IndexX) = Me.CardData(IndexY + 1, IndexX) Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    ''' <summary>
    ''' 游戏结束
    ''' </summary>
    Private Sub GameOver()
        Me.DrawForm()
        MsgBox("游戏分数：" & vbCrLf & Me.Score, 64, "游戏结束")

        Me.ScoreLabel.Text = "0"
        Me.Score = 0
        ReDim Me.CardData(3, 3)
        Me.CreatNewCard()
        Me.DrawForm()
    End Sub

    ''' <summary>
    ''' 允许鼠标拖动窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GameForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, ScoreLabel.MouseDown
        ReleaseCapture()
        SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    ''' <summary>
    ''' 鼠标点击方向按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub KeyLabel_Click(sender As Object, e As EventArgs) Handles KeyUpLabel.Click, KeyDownLabel.Click, KeyLeftLabel.Click, KeyRightLabel.Click
        Me.ApplyCardsChange(CType(sender, Label).Tag)
    End Sub

#Region "关闭按钮"

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub CloseButton_MouseDown(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseDown
        Me.CloseButton.Image = My.Resources.DefaultGameResource.GameClose_2
    End Sub

    Private Sub CloseButton_MouseEnter(sender As Object, e As EventArgs) Handles CloseButton.MouseEnter
        Me.CloseButton.Image = My.Resources.DefaultGameResource.GameClose_1
    End Sub

    Private Sub CloseButton_MouseLeave(sender As Object, e As EventArgs) Handles CloseButton.MouseLeave
        Me.CloseButton.Image = My.Resources.DefaultGameResource.GameClose_0
    End Sub

    Private Sub CloseButton_MouseUp(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseUp
        Me.CloseButton.Image = My.Resources.DefaultGameResource.GameClose_1
    End Sub

    Private Sub GameForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.BackgroundImage = Nothing
        Me.Dispose(True)
        GC.Collect()
    End Sub
#End Region
End Class
