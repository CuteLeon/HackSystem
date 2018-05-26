Imports System.Drawing
Imports System.Windows.Forms

Public Class TenTenGameForm
    Private Declare Function ReleaseCapture Lib "user32" () As Integer
    Private Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer

    Private Const TitleHeight As Integer = 70 '标题栏高度
    Private Const MarginSize As Integer = 2 '卡片之间的间距
    Private Const PaddingSize As Integer = 30 '裁片集中区域与窗体边框的距离
    Private Const CardSize As Integer = 26 '卡片尺寸
    Dim ScoreList() As Integer = {0, 10, 30, 60, 100, 150, 210}
    Dim MousePointInLabel As Point '用于记录鼠标拖动标签时鼠标坐标与标签起点差值
    Dim Score As Integer = 0 '分数
    Dim Moved As Boolean '定义一个标识，记录是否发生了移动，以确定操作是否有效
    Dim BlankColor As Color = Color.FromArgb(100, Color.DarkGray)
    Dim ObjectLabelLocation() As Point
    Dim CardColor() As Color = {
        Color.FromArgb(255, 121, 136, 191),
        Color.FromArgb(255, 254, 198, 61),
        Color.FromArgb(255, 87, 203, 132),
        Color.FromArgb(255, 236, 149, 72),
        Color.FromArgb(255, 152, 220, 83),
        Color.FromArgb(255, 230, 106, 130),
        Color.FromArgb(255, 90, 190, 226),
        Color.FromArgb(255, 76, 212, 174)
        }
    Dim CardData(9, 9) As Boolean
    Dim ColorData(9, 9) As Color
    Dim ObjectType(2) As Integer '记录新产生的三个物体类型在 ObjectModel 里的标识
    Dim ObjectColor(2) As Color '记录新产生的三个物体的颜色
    Dim ObjectImage(2) As Bitmap  '记录新产生的三个物体的图像
    Dim ObjectCount As Integer '还没有被放入游戏区的物体个数
    Dim ObjectLabels() As Label
    '记录每个魔性占用的行和列数，用以生成对应尺寸的图像
    Dim ObjectSize() As Size = {New Size(1, 1), New Size(1, 2), New Size(2, 1), New Size(2, 2), New Size(2, 2), New Size(2, 2), New Size(2, 2), New Size(1, 3), New Size(3, 1), New Size(2, 2), New Size(1, 4), New Size(4, 1), New Size(3, 3), New Size(3, 3), New Size(3, 3), New Size(3, 3), New Size(1, 5), New Size(5, 1), New Size(3, 3)}
    Dim ObjectModel As Point()() = {
        New Point() {New Point(0, 0)},'单个方格
        New Point() {New Point(0, 0), New Point(1, 0)},'垂直排列的两个方格
        New Point() {New Point(0, 0), New Point(0, 1)},'水平排量的两个方格
        New Point() {New Point(1, 0), New Point(0, 1), New Point(1, 1)},'缺少第二象限的三个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(1, 1)},'缺少第一象限的三个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(1, 1)},'缺少第三象限的三个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(0, 1)},'缺少第四象限的三个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(2, 0)},'垂直的三个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2)},'水平的三个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(0, 1), New Point(1, 1)},'四个紧凑的方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(2, 0), New Point(3, 0)},'垂直的四个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2), New Point(0, 3)},'水平的四个方格
        New Point() {New Point(0, 2), New Point(1, 2), New Point(2, 2), New Point(2, 1), New Point(2, 0)},'缺少第二象限的五个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(2, 0), New Point(2, 1), New Point(2, 2)},'缺少第一象限的五个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2), New Point(1, 2), New Point(2, 2)},'缺少第三象限的五个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(2, 0)},'缺少第四象限的五个方格
        New Point() {New Point(0, 0), New Point(1, 0), New Point(2, 0), New Point(3, 0), New Point(4, 0)},'垂直的五个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2), New Point(0, 3), New Point(0, 4)},'水平的五个方格
        New Point() {New Point(0, 0), New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 0), New Point(2, 1), New Point(2, 2)}'九个方格组成的超大正方形
    }

    Private Sub GameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.TenTenGameResource._1010
        '记录三个新物体的初始位置
        ObjectLabelLocation = {ObjectLabel0.Location, ObjectLabel1.Location, ObjectLabel2.Location}
        '记录储存三个新物体的控件
        ObjectLabels = {ObjectLabel0, ObjectLabel1, ObjectLabel2}
        '创建三个新物体
        CreateNewObject()
        '计算 MaskLabel 的坐标，使用控件当做蒙版模拟出圆角效果，不需要重复绘制，节省计算能力
        'PS操作: "色彩范围"选取10x10个灰色小矩形后，"选择"菜单>"修改">"平滑">输入"2"
        MaskLabel.Location = New Point(PaddingSize - 1, PaddingSize + TitleHeight - 1)
        '刷新界面
        DrawForm()
    End Sub

    ''' <summary>
    ''' 根据 CardData 数组刷新界面
    ''' </summary>
    Private Sub DrawForm()
        ScoreLabel.Text = Score.ToString
        Dim UnityBitmap As Bitmap = My.Resources.TenTenGameResource.Background
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmap)
            For IndexY As Integer = 0 To 9
                For IndexX As Integer = 0 To 9
                    UnityGraphics.FillRectangle(New SolidBrush(IIf(CardData(IndexY, IndexX), ColorData(IndexY, IndexX), BlankColor)), New RectangleF(PaddingSize + IndexX * (CardSize + MarginSize), PaddingSize + IndexY * (CardSize + MarginSize) + TitleHeight, CardSize, CardSize))
                    'UnityGraphics.DrawString(IndexX & "," & IndexY, Me.Font, Brushes.Red, PaddingSize + IndexX * (CardSize + MarginSize), PaddingSize + IndexY * (CardSize + MarginSize) + TitleHeight)
                Next
            Next
        End Using
        Me.BackgroundImage = UnityBitmap
        GC.Collect() '回收内存
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
    ''' 产生3个新的物体用于放置到游戏区中
    ''' </summary>
    Private Sub CreateNewObject()
        '重置剩余物体的数量，恢复显示新物体控件
        ObjectCount = 3
        ObjectLabel0.Show()
        ObjectLabel1.Show()
        ObjectLabel2.Show()
        '循环产生新物体
        For Index As Integer = 0 To 2
            '随机产生物体模型标识和颜色
            ObjectType(Index) = VBMath.Rnd * 18
            ObjectColor(Index) = CardColor(VBMath.Rnd * 7)
            'ObjectColor(Index) = Color.FromArgb(255, VBMath.Rnd * 255, VBMath.Rnd * 255, VBMath.Rnd * 255)
            '恢复控件的坐标，方便改变控件大小后重新居中对齐控件（首次生成时不需要恢复坐标）
            If ObjectImage(Index) IsNot Nothing Then ObjectLabels(Index).Location = New Point(ObjectLabels(Index).Left + ObjectImage(Index).Width / 4, ObjectLabels(Index).Top + ObjectImage(Index).Height / 4)
            '使用 ObjectSize 数组产生对应大小的物体图像
            ObjectImage(Index) = New Bitmap(
                ObjectSize(ObjectType(Index)).Width * CardSize + (ObjectSize(ObjectType(Index)).Width - 1) * MarginSize,
                ObjectSize(ObjectType(Index)).Height * CardSize + (ObjectSize(ObjectType(Index)).Height - 1) * MarginSize)
            '是控件适应图像并大小重新居中对齐
            ObjectLabels(Index).Size = New Size(ObjectImage(Index).Width / 2, ObjectImage(Index).Height / 2)
            ObjectLabels(Index).Location = New Point(ObjectLabels(Index).Left - ObjectImage(Index).Width / 4, ObjectLabels(Index).Top - ObjectImage(Index).Height / 4)
            '记录控件对齐后的坐标，将物体拖入游戏区失败时会把控件恢复到此处记录的坐标
            ObjectLabelLocation(Index) = ObjectLabels(Index).Location
            Using CardGraphics As Graphics = Graphics.FromImage(ObjectImage(Index))
                '绘制物体模型和颜色对应的物体图像
                For Each ObjectCell As Point In ObjectModel(ObjectType(Index))
                    CardGraphics.FillRectangle(New SolidBrush(ObjectColor(Index)), New Rectangle(
                        ObjectCell.Y * CardSize + (ObjectCell.Y) * MarginSize,
                        ObjectCell.X * CardSize + (ObjectCell.X) * MarginSize,
                        CardSize, CardSize))
                Next
            End Using
            '把生成的图像绘制到控件
            ObjectLabels(Index).Image = New Bitmap(ObjectImage(Index), ObjectImage(Index).Width / 2, ObjectImage(Index).Height / 2)
            '回收内存
            GC.Collect()
        Next
    End Sub

    Private Sub ObjectLabel_MouseDown(sender As Object, e As MouseEventArgs) Handles ObjectLabel0.MouseDown, ObjectLabel1.MouseDown, ObjectLabel2.MouseDown
        Dim ObjectLabel As Label = CType(sender, Label)
        '把当前拖动的控件带到Z顺序前面，即控件置前显示
        ObjectLabel.BringToFront()
        '将控件放大至适应游戏区的尺寸
        ObjectLabel.Size = ObjectImage(ObjectLabel.Tag).Size
        ObjectLabel.Image = ObjectImage(ObjectLabel.Tag)
        '记录鼠标按下时在控件内的坐标，绑定鼠标移动事件，允许鼠标拖动控件
        MousePointInLabel = e.Location
        AddHandler ObjectLabel.MouseMove, AddressOf ObjectLabel_MouseMove
    End Sub

    Private Sub ObjectLabel_MouseMove(sender As Object, e As MouseEventArgs)
        '允许鼠标拖动控件
        CType(sender, Label).Left = MousePosition.X - Me.Left - MousePointInLabel.X
        CType(sender, Label).Top = MousePosition.Y - Me.Top - MousePointInLabel.Y
    End Sub

    Private Sub ObjectLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles ObjectLabel0.MouseUp, ObjectLabel1.MouseUp, ObjectLabel2.MouseUp
        Dim ObjectLabel As Label = CType(sender, Label)
        '卸载控件的鼠标移动事件，不允许鼠标拖动控件
        RemoveHandler ObjectLabel.MouseMove, AddressOf ObjectLabel_MouseMove
        '首先要隐藏控件
        ObjectLabel.Hide()
        '恢复控件尺寸为游戏区对应尺寸的一半
        ObjectLabel.Size = New Size(ObjectImage(ObjectLabel.Tag).Width / 2, ObjectImage(ObjectLabel.Tag).Height / 2)
        'Label 控件不允许图像拉伸，需要自行缩放图像
        ObjectLabel.Image = New Bitmap(ObjectImage(ObjectLabel.Tag), ObjectImage(ObjectLabel.Tag).Width / 2, ObjectImage(ObjectLabel.Tag).Height / 2)
        '恢复 ObjectLabel 的位置
        ObjectLabel.Location = ObjectLabelLocation(ObjectLabel.Tag)
        '检测是否拖入成功
        If MoveToGameAera(ObjectLabel.Tag) Then
            '拖入游戏区成功时首先检查整行整列
            IsFullInLine()
            '刷新界面
            DrawForm()
            '剩余物体个数自减
            ObjectCount -= 1
            If ObjectCount = 0 Then CreateNewObject()
            '需要检测剩余的物体是否可以放入游戏区，以判断游戏是否结束
            If IsGameOver() Then GameOver()
        Else
            '拖入失败时恢复显示控件
            ObjectLabel.Show()
        End If
    End Sub

    ''' <summary>
    ''' 尝试将目标物体拖动进游戏区域
    ''' </summary>
    ''' <param name="Index">拖入的物体的标识</param>
    ''' <returns>是否拖入成功</returns>
    Private Function MoveToGameAera(ByVal Index As Integer) As Boolean
        Dim IndexX, IndexY As Integer '当前鼠标位置对应的 CardData 坐标
        Dim PointsInGameAera(0) As Point '记录拖入的物体在游戏区对应的坐标组，方便拖入成功时赋值
        Dim PointInGameAera As Point '用作游戏区里坐标
        '根据鼠标在游戏区的坐标判断在 CardData 数组所对应的坐标
        IndexY = Math.Round((MousePosition.X - PaddingSize - MousePointInLabel.X - Me.Left) / (CardSize + MarginSize))
        IndexX = Math.Round((MousePosition.Y - PaddingSize - TitleHeight - MousePointInLabel.Y - Me.Top) / (CardSize + MarginSize))
        '防止数组越界
        If IndexX < 0 OrElse IndexX > 9 OrElse IndexY < 0 OrElse IndexY > 9 Then Return False
        '检测当前位置是否放得下物体
        For Each ObjectCell As Point In ObjectModel(ObjectType(Index))
            PointInGameAera.X = IndexX + ObjectCell.X
            PointInGameAera.Y = IndexY + ObjectCell.Y
            '物体超出到游戏区边缘，当前坐标放不下物体，返回假
            If PointInGameAera.X < 0 OrElse PointInGameAera.X > 9 OrElse PointInGameAera.Y < 0 OrElse PointInGameAera.Y > 9 Then Return False
            If CardData(PointInGameAera.X, PointInGameAera.Y) Then
                '当前坐标放不下，立即返回假
                Return False
            Else
                '当前坐标可以放得下(整个物体不一定放的下)，先记录下坐标，方便如果放的下时对游戏区对应坐标赋值
                PointsInGameAera(UBound(PointsInGameAera)) = New Point(PointInGameAera.X, PointInGameAera.Y)
                '为 PointsInGameAera 数组扩展一个元素，方便记录下一个放的下的坐标
                ReDim Preserve PointsInGameAera(UBound(PointsInGameAera) + 1)
            End If
        Next
        '运行到这里没有 Return False 说明物体可以放入当前坐标，此时删去 PointsInGameAera 最后一个未赋值的元素
        ReDim Preserve PointsInGameAera(UBound(PointsInGameAera) - 1)
        '遍历之前记录的物体在游戏区对应的坐标
        For PointIndex As Integer = 0 To UBound(PointsInGameAera)
            '每放入一个坐标，加一分
            Score += 1
            '在游戏区储存物体的位置和颜色（相当于放入了物体）
            CardData(PointsInGameAera(PointIndex).X, PointsInGameAera(PointIndex).Y) = True
            ColorData(PointsInGameAera(PointIndex).X, PointsInGameAera(PointIndex).Y) = ObjectColor(Index)
        Next
        '物体可以放在当前坐标，返回真
        Return True
    End Function

    ''' <summary>
    ''' 检测是否存在整行或者整列
    ''' </summary>
    Private Sub IsFullInLine()
        Dim IndexX, IndexY As Integer '循环因子
        Dim IsFulled As Boolean '是否为整行或整列的标识
        Dim FulledLine(0) As Integer '记录为整行的行标识
        Dim FulledColumn(0) As Integer '记录为整列的列标识
        '检查整行
        For IndexY = 0 To 9
            IsFulled = True
            For IndexX = 0 To 9
                If Not CardData(IndexY, IndexX) Then
                    '不是整行时立即跳出内层循环
                    IsFulled = False : Exit For
                End If
            Next
            If IsFulled Then
                '为整行时记录下行号
                FulledLine(UBound(FulledLine)) = IndexY
                '为 FulledLine 数组扩展一个元素，方便记录下一个整行行号
                ReDim Preserve FulledLine(UBound(FulledLine) + 1)
            End If
        Next
        '删除 FulledLine 数组最后一个未赋值的元素
        ReDim Preserve FulledLine(UBound(FulledLine) - 1)
        '检查整列
        For IndexX = 0 To 9
            IsFulled = True
            For IndexY = 0 To 9
                If Not CardData(IndexY, IndexX) Then
                    '不是整列时立即跳出内层循环
                    IsFulled = False : Exit For
                End If
            Next
            If IsFulled Then
                '为整列时记录下列号
                FulledColumn(UBound(FulledColumn)) = IndexX
                '为 FulledColumn 数组扩展一个元素，方便记录下一个整列列号
                ReDim Preserve FulledColumn(UBound(FulledColumn) + 1)
            End If
        Next
        '删除 FulledColumn 数组最后一个未赋值的元素
        ReDim Preserve FulledColumn(UBound(FulledColumn) - 1)
        '计算得分（每次同时消失的行或列数不同，得分也不同，见数组 ScoreList）
        Score += ScoreList(FulledColumn.Count + FulledLine.Count)

        '清除整行或整列
        If FulledLine.Count > 0 OrElse FulledColumn.Count > 0 Then
            For IndexX = 0 To 9
                For IndexY = 0 To UBound(FulledLine)
                    CardData(FulledLine(IndexY), IndexX) = False
                    '用于显示整行消失动态效果
                Next
                For IndexY = 0 To UBound(FulledColumn)
                    CardData(IndexX, FulledColumn(IndexY)) = False
                Next
                Threading.Thread.Sleep(25)
                DrawForm()
                Me.Refresh()
            Next
        End If
    End Sub

    ''' <summary>
    ''' 检测游戏是否结束
    ''' </summary>
    ''' <returns></returns>
    Private Function IsGameOver() As Boolean
        Dim Index As Integer '物体循环因子
        Dim PointX, PointY As Integer '坐标循环因子
        Dim Result As Boolean '检测结果

        For Index = 0 To 2
            '跳过已经放入游戏区的物体
            If ObjectLabels(Index).Visible Then
                For PointY = 0 To 9
                    For PointX = 0 To 9
                        '逐个坐标检测是否可以放入游戏区
                        Result = CanPutItIn(PointX, PointY, ObjectType(Index))
                        '一旦可以放入立即跳出内层循环，节省计算能力，下同
                        If Result Then Exit For
                    Next
                    If Result Then Exit For
                Next
                If Result Then Exit For
            End If
        Next
        '返回检测结果的相反值
        Return Not Result
    End Function

    ''' <summary>
    ''' 检测 ObjectType 对应的物体能否放在 CardData(PointX,PointY) 里
    ''' </summary>
    ''' <param name="PointX"></param>
    ''' <param name="PointY"></param>
    ''' <param name="ObjectType"></param>
    ''' <returns>能否放置</returns>
    Private Function CanPutItIn(ByVal PointX As Integer, ByVal PointY As Integer, ByVal ObjectType As Integer) As Boolean
        Dim PointInGameAera As Point '在游戏区对应的坐标
        '遍历物体模型的坐标组
        For Each ObjectCell As Point In ObjectModel(ObjectType)
            '计算物体在游戏区内对应的坐标
            PointInGameAera.X = PointX + ObjectCell.X
            PointInGameAera.Y = PointY + ObjectCell.Y
            '物体超出到游戏区边缘，当前坐标放不下物体，返回假
            If PointInGameAera.X < 0 OrElse PointInGameAera.X > 9 OrElse PointInGameAera.Y < 0 OrElse PointInGameAera.Y > 9 Then Return False
            '游戏区目标坐标已经放入了物体，无法重复放入，返回假
            If CardData(PointInGameAera.X, PointInGameAera.Y) Then Return False
        Next
        '运行到这里没有返回假而跳出过程水命可以放入物体，返回真
        Return True
    End Function

    ''' <summary>
    ''' 游戏结束
    ''' </summary>
    Private Sub GameOver()
        '游戏结束的自定义过程，用以重置游戏
        MsgBox("得分：" & Score, 64, "游戏结束：")
        '分数清零
        Score = 0
        ScoreLabel.Text = "0"
        '重置游戏区
        ReDim CardData(9, 9)
        '重新产生三个物体
        CreateNewObject()
        '刷新界面
        DrawForm()
    End Sub

#Region "关闭按钮"

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub CloseButton_MouseDown(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseDown
        CloseButton.Image = My.Resources.TenTenGameResource.GameClose_2
    End Sub

    Private Sub CloseButton_MouseEnter(sender As Object, e As EventArgs) Handles CloseButton.MouseEnter
        CloseButton.Image = My.Resources.TenTenGameResource.GameClose_1
    End Sub

    Private Sub CloseButton_MouseLeave(sender As Object, e As EventArgs) Handles CloseButton.MouseLeave
        CloseButton.Image = My.Resources.TenTenGameResource.GameClose_0
    End Sub

    Private Sub CloseButton_MouseUp(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseUp
        CloseButton.Image = My.Resources.TenTenGameResource.GameClose_1
    End Sub

    Private Sub TenTenGameForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.BackgroundImage = Nothing
        Me.Dispose(True)
        GC.Collect()
    End Sub

#End Region
End Class
