> ### 演示项目 GitHub 地址 : [RingsStartUp](https://github.com/CuteLeon/HackSystem/tree/master/RingsStartUp)
***

> ### 1>. 首先在解决方案管理器里 StartUps 解决方案文件夹内新建项目，
    项目类型为 Visual C#>Windows 经典桌面>类库 (.Net Framework)；
	框架版本为 .Net Framework 4.0；
***
> ### 2>. 进入项目属性，
	生成目录设置为：
	Debug => ..\Debug\StartUps\
	Release => ..\Release\StartUps\
***

> ### 3>. 为项目添加引用，
	添加引用 => StartUpTemplate.dll
	在 项目>引用 里选中StratUpTemplate，属性窗口内 复制本地 => false；
***

> ### 4>. 设置解决方案项目依赖
	为 RingsStartUp 项目设置 StartUpTeamplate 项目依赖
***

> ### 5>. 下面以 RingsStartUp 作为开发演示：
	此项目内将包含多个StartUp插件
***

> ### 6>. 项目 添加>新建项>Visual C#>资源文件
	命名为 RingsStartUpResource
***

> ### 7>. 项目内新建文件夹，（只包含单个StratUp插件时也可以不新建文件夹）
	名为 RainbowRing，把 Class1 迁移到此文件夹中，
	但要注意 Class 所处命名空间仍需在解决方案根层，即 namespace RingsStartUp；
	重命名 Class1 类为 RainbowRingClass
	这个也将作为插件的 Type名称
	RainbowRing 文件夹内新建 Form，
	名称为 RainbowRingForm
	名称空间改为 namespace RingsStartUp；
***

> ### 8>. RainbowRingClass 内引用 StartUpTemplate
	RainbowRingClass 内引入命名空间 using StartUpTemplate；
	RainbowRingClass 继承并实现 StartUpTemplateClass 抽象类；
	在构造函数内赋值插件的 名称、描述、预览；
	注意！
	不必再构造函数里 new 出 Form，而一定要在 CreateStartUpForm 方法内返回有效的 Form 对象，
``` csharp
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
	public class RainbowRingClass : StartUpTemplateClass
	{
		public RainbowRingClass()
		{
			Name = "彩虹环";
			Description = "彩虹环启动画面 - Leon";
			Preview = RingsStartUpResource.RainbowStartUpPreview;
		}

		protected override Form CreateStartUpForm()
		{
			return new RainbowRingForm() { ParentStartUp  =this };
		}
	}
}
```
***

> ### 9>. RainbowStartUpForm 增加 StartUpTemplateClass 类型字段
	public StartUpTemplateClass ParentStartUp = null;
	用于记录窗口所属 StartUp
***

> ### 10>. 发挥想象力，设计喜欢的 StartUp 界面
***

> ### 11>. 加载完成事件
	启动画面加载完成后要调用 ParentStartUp.OnStartUpFinished(s, e);
	并关闭 StartUpForm；
	简单的实现方案：
``` csharp
public RainbowRingForm()
{
    InitializeComponent();
    CheckForIllegalCrossThreadCalls = false;
    this.FormClosing += new FormClosingEventHandler((Leon, Mathilda) => { ParentStartUp?.OnStartUpFinished(EventArgs.Empty); });
}
```

    当加载结束时：
``` csharp
ThreadPool.QueueUserWorkItem(new WaitCallback(
    (ILoveU) => {
        while (this.Opacity > 0)
        {
            Thread.Sleep(100);
            this.Opacity -= 0.1;
        }

        this.Close();
    }));
```
***

12>. 使用插件，
	可以在 HackSystem 启动画面处设置使用此 StartUp 插件；
	也可以直接修改 Config 文件以使用此 StartUp 插件：
``` xml
<add key="StartUpFile" value="RingStartUp.dll" />
<add key="StartUpName" value="RainbowRingClass" />
```

***

> ### 结束
via : Leon.ID#QQ.COM