> ### 演示项目 GitHub 地址 : [DefaultLogon](https://github.com/CuteLeon/HackSystem/tree/master/DefaultLogon)
***

> ### 1>. 首先在解决方案管理器里 Logons 解决方案文件夹内新建项目，
    项目类型为 Visual C#>Windows 经典桌面>类库 (.Net Framework)；
	框架版本为 .Net Framework 4.0；
***
> ### 2>. 进入项目属性，
	生成目录设置为：
	Debug => ..\Debug\Logons\
	Release => ..\Release\Logons\
***

> ### 3>. 为项目添加引用
	添加引用 => LogonTemplate.dll
	在 项目>引用 里选中LogonTemplate，属性窗口内 复制本地 => false；
***

> ### 4>. 设置解决方案项目依赖
	为 DefaultLogon 项目设置 LogonTeamplate 项目依赖
***

> ### 5>. Logon类内引用 LogonTemplate，继承并实现 LogonTemplateClass 抽象类
	在构造函数内赋值插件的 名称、描述、预览；
	使用FileName属性返回程序集文件名称;
	注意，只需要在CreateLogonForm()方法返回Form即可，不要在构造函数内创建Form对象，否则会因为创建Class对象而造成Form占用大量内存；

``` csharp
namespace HackSystem.LogonTemplate;
using System.Windows.Forms;

namespace Leon.DefaultLogon
{
    public class DefaultLogonClass : LogonTemplateClass
    {
        public DefaultLogonClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠登录画面 - Leon";
            this.Preview = DefaultLogonResource.DefaultLogonPreview;
        }

        public override string FileName => 
			System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLogonForm()
        {
            return new DefaultLogonForm() { ParentLogon = this };
        }
    }
}
```
***

> ### 6>. 发挥想象，设计喜欢的 Logon 界面
***

> ### 7>. 启动完毕，触发Class.OnLogonFinished()并关闭Form
	启动画面加载完毕，需要关闭Form才可以继续运行程序；
	推荐两种方法触发Class.OnLogonFinished()：
	1>. Class内创建Form时，订阅FormClosing事件以触发OnLogonFinished();，Form关闭时即可触发；
	2>.在Form内使用字段记录所属Class，在Form内根据用户操作适时触发Class.OnLogonFinished();

``` csharp
//在窗体定义布尔值变量记录是否允许窗口关闭
bool AllowToClose = false;

//窗体加载时订阅窗口即将关闭事件
private void HTMLLogonForm_Load(object sender, EventArgs e)
{
	//只允许密码验证代码置AllowToClose为true后才允许关闭窗口，否则无法直接关闭登录窗口
    this.FormClosing += new FormClosingEventHandler(
        (Leon, Mathilda) => {
            if (!AllowToClose)
                Mathilda.Cancel = true;
            else
                ParentLogon?.OnLogonFinished(EventArgs.Empty);
        });
}

private void CheckUserInfo(string UserName, string Password)
{
	//用户信息通过后，置AllowToClose为true
	if(UserName == "123" && Password == "456")
	{
		ThreadPool.QueueUserWorkItem(new WaitCallback(
            (ILoveU) => {
                try
                {
                    while (this.Opacity > 0)
                    {
                        Thread.Sleep(100);
                        this.Opacity -= 0.1;
                    }
                }
                catch { }
                AllowToClose = true;
                this.Close();
            }));
	}
}
```
***

> ### 8>. 使用插件
	可以在 HackSystem 启动画面配置界面启用此插件；
	也可以直接修改 Config 文件以启用此插件：
``` xml
<add key="LogonFile" value="DefaultLogon.dll" />
<add key="LogonName" value="DefaultLogonClass" />
```

***

> ### 结束
via : Leon.ID@QQ.COM