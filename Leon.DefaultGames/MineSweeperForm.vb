Imports System.Drawing
Imports System.Windows.Forms

Public Class MineSweeperForm
    Private Declare Function ReleaseCapture Lib "user32" () As Integer
    Private Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer
    Private Const DefaultPixelFormat As Integer = Imaging.PixelFormat.Format32bppArgb

    Dim CloseBitmap As Bitmap = My.Resources.DefaultGameResource.GameClose_0 '初始关闭按钮
    Dim CloseRectangle As New Rectangle(308, 3, Me.CloseBitmap.Width, Me.CloseBitmap.Height) '关闭按钮的位置
    Dim CloseState As Boolean '关闭按钮的状态

    Dim CellBitmap As Bitmap '单元图像
    Dim CellRectangle As Rectangle '单元图像的位置
    Dim LastHLIndex As New PointF(-1, -1) '上一个高亮标识

    Dim BackgroundBitmap As Bitmap '背景图
    Dim BackgroundGraphics As Graphics '背景画笔

    Dim MinefieldBitmap As Bitmap '雷区图
    Dim MinefieldGraphics As Graphics '雷区画笔
    Dim MinefieldRectangle As Rectangle = New Rectangle(10, 27, 320, 320) '雷区位置

    Dim MineCount As Int16 = 20 '地雷总数
    Dim MineState(9, 9) As Boolean '布雷位置
    Dim CellState(9, 9) As Int16  '地雷状态(0：未知；1：标记；2：无雷；3：高亮；4：高亮_标记)
    Dim CellAroundCount(9, 9) As Int16 '周围地雷数
    Dim ClickTimes As Integer = 0 '点击次数

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        'Alpha通道绘图相关
        Get
            If Not Me.DesignMode Then
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or WS_EX_LAYERED
                Return cp
            Else
                Return MyBase.CreateParams
            End If
        End Get
    End Property

    Private Sub MineSweeperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '启用双缓冲绘图技术，加快绘制速度，防止闪烁
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        '初始化
        Me.ResetMinefield()
    End Sub

    Private Sub ResetMinefield()
        '初始化雷区状态
        Me.ClickTimes = 0
        ReDim Me.CellState(9, 9)
        ReDim Me.MineState(9, 9)
        ReDim Me.CellAroundCount(9, 9)
        '布雷
        Dim TempArrayLast As New ArrayList, MineRandom As New Random
        Dim MineIndex As Integer, X As Integer, Y As Integer
        '首次调用会出现0，预调用一次，忽略掉
        MineIndex = MineRandom.Next(100)
        '循环遍历布雷数量
        For Index As Integer = 1 To Me.MineCount
            '内循环防止出现重复坐标
            Do Until TempArrayLast.IndexOf(MineIndex) = -1
                MineIndex = MineRandom.Next(100)
            Loop
            '计算坐标的X、Y位置
            X = MineIndex \ 10
            Y = MineIndex Mod 10
            '存进临时布雷表
            TempArrayLast.Add(MineIndex)
            '记录布雷位置
            Me.MineState(X, Y) = True
        Next
        '根据状态绘制UI
        Me.DrawUI()
    End Sub

    Private Sub DrawUI()
        '初始化背景和画笔
        Me.BackgroundBitmap = My.Resources.DefaultGameResource.MineSweeperBackground
        Me.BackgroundGraphics = Graphics.FromImage(Me.BackgroundBitmap)
        Me.MinefieldBitmap = New Bitmap(320, 320, DefaultPixelFormat)
        Me.MinefieldGraphics = Graphics.FromImage(Me.MinefieldBitmap)
        '绘制关闭按钮
        Me.BackgroundGraphics.DrawImage(Me.CloseBitmap, Me.CloseRectangle)
        '绘制十行十列方格
        For RowIndex As Integer = 0 To 9
            For ColumnIndex As Integer = 0 To 9
                Me.CellRectangle = New Rectangle(RowIndex * 32, ColumnIndex * 32, 32, 32)
                Select Case Me.CellState(RowIndex, ColumnIndex)
                    Case 0 '未知
                        '作弊行：直接显示出地雷
                        'CellBitmap = IIf(MineState(RowIndex, ColumnIndex), My.Resources.DefaultGameResource.Mine, My.Resources.DefaultGameResource.Unknown)
                        Me.CellBitmap = My.Resources.DefaultGameResource.Unknown
                    Case 1 '标记
                        Me.CellBitmap = My.Resources.DefaultGameResource.Mark
                    Case 2 '已挖掘
                        Me.CellBitmap = My.Resources.DefaultGameResource.ResourceManager.GetObject("NoMine_" & Me.CellAroundCount(RowIndex, ColumnIndex))
                    Case 3 '高亮
                        Me.CellBitmap = My.Resources.DefaultGameResource.HighLight
                    Case 4 '标记_高亮
                        Me.CellBitmap = My.Resources.DefaultGameResource.HighLight_Mark
                End Select
                Me.MinefieldGraphics.DrawImage(Me.CellBitmap, Me.CellRectangle)
            Next
        Next
        Me.BackgroundGraphics.DrawImage(Me.MinefieldBitmap, Me.MinefieldRectangle)
        '应用到窗口并释放背景内存
        DrawImageModule.DrawImage(Me, Me.BackgroundBitmap)
        Me.MinefieldGraphics.Dispose()
        Me.BackgroundGraphics.Dispose()
    End Sub

    Private Sub MinefieldPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles MinefieldPanel.MouseMove
        '鼠标在雷区移动
        Dim NowIndex As PointF = New PointF(e.X \ 32, e.Y \ 32)
        If Me.LastHLIndex = New PointF(-1, -1) Then Me.LastHLIndex = NowIndex
        If NowIndex = Me.LastHLIndex Then Exit Sub
        If (NowIndex.X < 0 OrElse NowIndex.X > 9 Or
            NowIndex.Y < 0 OrElse NowIndex.Y > 9) Then Exit Sub
        If Me.CellState(Me.LastHLIndex.X, Me.LastHLIndex.Y) > 2 Then Me.CellState(Me.LastHLIndex.X, Me.LastHLIndex.Y) -= 3
        If Me.CellState(NowIndex.X, NowIndex.Y) < 2 Then Me.CellState(NowIndex.X, NowIndex.Y) += 3
        Me.LastHLIndex = NowIndex
        Me.DrawUI()
    End Sub

    Private Sub MinefieldPanel_MouseLeave(sender As Object, e As EventArgs) Handles MinefieldPanel.MouseLeave
        '设置激活的标识为空
        If Not (Me.LastHLIndex = New PointF(-1, -1)) Then
            If Me.CellState(Me.LastHLIndex.X, Me.LastHLIndex.Y) > 2 Then Me.CellState(Me.LastHLIndex.X, Me.LastHLIndex.Y) -= 3
            Me.LastHLIndex = New PointF(-1, -1)
            Me.DrawUI()
        End If
    End Sub

    Private Sub MinefieldPanel_MouseClick(sender As Object, e As MouseEventArgs) Handles MinefieldPanel.MouseClick
        Dim RowIndex As Integer = e.X \ 32, ColumnIndex As Integer = e.Y \ 32
        If e.Button = MouseButtons.Right Then
            '右键标记/取消标记
            If Me.CellState(RowIndex, ColumnIndex) = 3 Then
                '未被挖掘且未被标记
                Me.CellState(RowIndex, ColumnIndex) = 4
                Me.DrawUI()
            ElseIf Me.CellState(RowIndex, ColumnIndex) = 4 Then
                '未被挖掘且已被标记
                Me.CellState(RowIndex, ColumnIndex) = 3
                Me.DrawUI()
            End If
        ElseIf e.Button = MouseButtons.Left Then
            '左键挖掘
            If Me.CellState(RowIndex, ColumnIndex) = 2 Then Exit Sub '已被挖掘，不处理
            If Me.CellState(RowIndex, ColumnIndex) = 4 Then Me.CellState(RowIndex, ColumnIndex) = 3 : Me.DrawUI() : Exit Sub '被标记的不挖掘

            If Me.MineState(RowIndex, ColumnIndex) Then
                '有雷
                Me.GameOver(False)
            Else
                '无雷
                Me.SweepCell(RowIndex, ColumnIndex)
            End If
        End If
    End Sub

    Private Sub SweepCell(ByVal RowIndex As Integer, ColumnIndex As Integer)
        If Me.CellState(RowIndex, ColumnIndex) = 2 Then Exit Sub '已被挖掘，不处理
        Me.CellState(RowIndex, ColumnIndex) = 2
        Me.CellAroundCount(RowIndex, ColumnIndex) = Me.GetAroundCount(RowIndex, ColumnIndex)
        If Me.CellAroundCount(RowIndex, ColumnIndex) = 0 Then
            '周围无雷，自动排查周围区域
            For X As Int16 = RowIndex - 1 To RowIndex + 1
                If X < 0 Or X > 9 Then Continue For
                For Y As Int16 = ColumnIndex - 1 To ColumnIndex + 1
                    If Y < 0 Or Y > 9 Then Continue For
                    Me.SweepCell(X, Y)
                Next
            Next
        End If
        Me.ClickTimes += 1
        If Me.ClickTimes = 100 - Me.MineCount Then Me.GameOver(True) Else Me.DrawUI()
    End Sub

    Private Function GetAroundCount(ByVal RowIndex As Integer, ColumnIndex As Integer) As Integer
        Dim AroundCount As Int16 = 0
        For X As Int16 = RowIndex - 1 To RowIndex + 1
            If X < 0 Or X > 9 Then Continue For
            For Y As Int16 = ColumnIndex - 1 To ColumnIndex + 1
                If Y < 0 Or Y > 9 Then Continue For
                If Me.MineState(X, Y) Then AroundCount += 1
            Next
        Next
        Return AroundCount
    End Function

    Private Sub GameOver(ByVal GameResult As Boolean)
        RemoveHandler Me.MinefieldPanel.MouseMove, AddressOf Me.MinefieldPanel_MouseMove
        RemoveHandler Me.MinefieldPanel.MouseClick, AddressOf Me.MinefieldPanel_MouseClick
        RemoveHandler Me.MinefieldPanel.MouseLeave, AddressOf Me.MinefieldPanel_MouseLeave
        Me.BackgroundBitmap = My.Resources.DefaultGameResource.MineSweeperBackground
        Me.BackgroundGraphics = Graphics.FromImage(Me.BackgroundBitmap)
        Me.MinefieldBitmap = New Bitmap(320, 320, DefaultPixelFormat)
        Me.MinefieldGraphics = Graphics.FromImage(Me.MinefieldBitmap)
        If GameResult Then
            '胜利
            For RowIndex As Integer = 0 To 9
                For ColumnIndex As Integer = 0 To 9
                    Me.CellRectangle = New Rectangle(RowIndex * 32, ColumnIndex * 32, 32, 32)
                    Me.CellBitmap = My.Resources.DefaultGameResource.ResourceManager.GetObject(IIf(Me.MineState(RowIndex, ColumnIndex), "Right", "NoMine_" & Me.CellAroundCount(RowIndex, ColumnIndex)))
                    Me.MinefieldGraphics.DrawImage(Me.CellBitmap, Me.CellRectangle)
                Next
            Next
            Me.MinefieldGraphics.DrawImage(IIf(GameResult, My.Resources.DefaultGameResource.Win, My.Resources.DefaultGameResource.Lost), 0, 0, 320, 320)
        Else
            '失败
            For RowIndex As Integer = 0 To 9
                For ColumnIndex As Integer = 0 To 9
                    Me.CellRectangle = New Rectangle(RowIndex * 32, ColumnIndex * 32, 32, 32)
                    Me.CellBitmap = My.Resources.DefaultGameResource.ResourceManager.GetObject(IIf(Me.MineState(RowIndex, ColumnIndex), "Mine", IIf(Me.CellState(RowIndex, ColumnIndex) = 2, TruePart:="NoMine_" & Me.CellAroundCount(RowIndex, ColumnIndex), FalsePart:="Unknown")))
                    Me.MinefieldGraphics.DrawImage(Me.CellBitmap, Me.CellRectangle)
                Next
            Next
            Me.MinefieldGraphics.DrawImage(IIf(GameResult, My.Resources.DefaultGameResource.Win, My.Resources.DefaultGameResource.Lost), 0, 0, 320, 320)
        End If
        Me.BackgroundGraphics.DrawImage(Me.CloseBitmap, Me.CloseRectangle)
        Me.BackgroundGraphics.DrawImage(Me.MinefieldBitmap, Me.MinefieldRectangle)
        DrawImageModule.DrawImage(Me, Me.BackgroundBitmap)
        '注册双击事件，双击以重置游戏
        AddHandler Me.MinefieldPanel.MouseDoubleClick, AddressOf Me.MinefieldPanel_MouseDoubleClick
        Me.MinefieldGraphics.Dispose()
        Me.BackgroundGraphics.Dispose()
    End Sub

    Private Sub MineSweeperForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        '鼠标在窗体移动，判断鼠标是否在关闭按钮
        Dim IsMouseIn As Boolean = Me.CloseRectangle.Contains(e.X, e.Y)
        '如果已经处理过，不重复处理
        If IsMouseIn = Me.CloseState Then Exit Sub
        '初始化背景
        Me.BackgroundBitmap = My.Resources.DefaultGameResource.MineSweeperBackground
        Me.BackgroundGraphics = Graphics.FromImage(Me.BackgroundBitmap)
        '改变关闭按钮状态和处理状态
        If IsMouseIn Then
            '鼠标在关闭按钮
            Me.CloseBitmap = My.Resources.DefaultGameResource.GameClose_1
            Me.CloseState = True
        Else
            '鼠标不在关闭按钮
            Me.CloseBitmap = My.Resources.DefaultGameResource.GameClose_0
            Me.CloseState = False
        End If
        '绘制关闭按钮和雷区
        Me.BackgroundGraphics.DrawImage(Me.CloseBitmap, Me.CloseRectangle)
        Me.BackgroundGraphics.DrawImage(Me.MinefieldBitmap, Me.MinefieldRectangle)
        '应用到窗口并释放背景内存
        DrawImageModule.DrawImage(Me, Me.BackgroundBitmap)
        Me.BackgroundGraphics.Dispose()
    End Sub

    Private Sub MineSweeperForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Me.CloseRectangle.Contains(e.X, e.Y) Then
            '鼠标在关闭按钮
            '初始化背景
            Me.BackgroundBitmap = My.Resources.DefaultGameResource.MineSweeperBackground
            Me.BackgroundGraphics = Graphics.FromImage(Me.BackgroundBitmap)
            '设置关闭按钮
            Me.CloseBitmap = My.Resources.DefaultGameResource.GameClose_2
            '绘制关闭按钮和雷区
            Me.BackgroundGraphics.DrawImage(Me.CloseBitmap, Me.CloseRectangle)
            Me.BackgroundGraphics.DrawImage(Me.MinefieldBitmap, Me.MinefieldRectangle)
            '应用到窗口并释放背景内存
            DrawImageModule.DrawImage(Me, Me.BackgroundBitmap)
            Me.BackgroundGraphics.Dispose()
        Else
            '鼠标不在关闭按钮，实现鼠标拖动
            ReleaseCapture()
            SendMessageA(Me.Handle, &HA1, 2, 0&)
        End If
    End Sub

    Private Sub MineSweeperForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        '初始化背景
        Me.BackgroundBitmap = My.Resources.DefaultGameResource.MineSweeperBackground
        Me.BackgroundGraphics = Graphics.FromImage(Me.BackgroundBitmap)
        '设置关闭按钮
        Me.CloseBitmap = IIf(Me.CloseRectangle.Contains(e.X, e.Y), My.Resources.DefaultGameResource.GameClose_1, My.Resources.DefaultGameResource.GameClose_0)
        '绘制关闭按钮和雷区
        Me.BackgroundGraphics.DrawImage(Me.CloseBitmap, Me.CloseRectangle)
        Me.BackgroundGraphics.DrawImage(Me.MinefieldBitmap, Me.MinefieldRectangle)
        '应用到窗口并释放背景内存
        DrawImageModule.DrawImage(Me, Me.BackgroundBitmap)
        Me.BackgroundGraphics.Dispose()
    End Sub

    Private Sub MineSweeperForm_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        '点击关闭按钮，退出程序
        If Me.CloseRectangle.Contains(e.X, e.Y) Then Me.Close()
        RemoveHandler Me.MouseUp, AddressOf Me.MineSweeperForm_MouseUp
    End Sub

    Private Sub MinefieldPanel_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        '双击重设游戏后，解除事件注册
        Me.ResetMinefield()
        RemoveHandler Me.MinefieldPanel.MouseDoubleClick, AddressOf Me.MinefieldPanel_MouseDoubleClick
        AddHandler Me.MinefieldPanel.MouseMove, AddressOf Me.MinefieldPanel_MouseMove
        AddHandler Me.MinefieldPanel.MouseClick, AddressOf Me.MinefieldPanel_MouseClick
        AddHandler Me.MinefieldPanel.MouseLeave, AddressOf Me.MinefieldPanel_MouseLeave
    End Sub

    Private Sub MineSweeperForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.BackgroundBitmap?.Dispose()
        Me.Dispose(True)
        GC.Collect()
    End Sub
End Class
