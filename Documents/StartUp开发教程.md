> ### 演示项目 GitHub 地址 : [DefaultStartUp](https://github.com/CuteLeon/HackSystem/tree/master/DefaultStartUp)
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

> ### 3>. 为项目添加引用
	添加引用 => StartUpTemplate.dll
	在 项目>引用 里选中StartUpTemplate，属性窗口内 复制本地 => false；
***

> ### 4>. 设置解决方案项目依赖
	为 DefaultStartUp 项目设置 StartUpTeamplate 项目依赖
***

> ### 5>. StartUp类内引用 StartUpTemplate，继承并实现 StartUpTemplateClass 抽象类
	在构造函数内赋值插件的 名称、描述、预览；
	使用FileName属性返回程序集文件名称;
	注意，只需要在CreateStartUpForm()方法返回Form即可，不要在构造函数内创建Form对象，否则会因为创建Class对象而造成Form占用大量内存；

``` csharp
using StartUpTemplate;
using System.Windows.Forms;

namespace DefaultStartUp
{
    public class DefaultStartUpClass : StartUpTemplateClass
    {
        public DefaultStartUpClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠启动画面 - Leon";
            this.Preview = DefaultStartUpResource.DefaultStartUpPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new DefaultStartUpForm() { ParentStartUp = this };
        }
    }
}
```
***

> ### 6>. 发挥想象，设计喜欢的 StartUp 界面
***

> ### 7>. 启动完毕，触发Class.OnStartUpFinished()并关闭Form
	启动画面加载完毕，需要关闭Form才可以继续运行程序；
	推荐两种方法触发Class.OnStartUpFinished()：
	1>. Class内创建Form时，订阅FormClosing事件以触发OnStartUpFinished();，Form关闭时即可触发；
	2>.在Form内使用字段记录所属Class，在Form内根据用户操作适时触发Class.OnStartUpFinished();

``` csharp
public DefaultStartUpForm()
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

> ### 8>. 使用插件
	可以在 HackSystem 启动画面配置界面启用此插件；
	也可以直接修改 Config 文件以启用此插件：
``` xml
<add key="StartUpFile" value="DefaultStartUp.dll" />
<add key="StartUpName" value="DefaultStartUpClass" />
```

***

> ### 结束
via : Leon.ID@QQ.COM