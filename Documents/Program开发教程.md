> ### 演示项目 GitHub 地址 : [DefaultPorgrams](https://github.com/CuteLeon/HackSystem/tree/master/DefaultPorgrams)
***

> ### 1>. 首先在解决方案管理器里 Programs 解决方案文件夹内新建项目，
    项目类型为 Visual C#>Windows 经典桌面>类库 (.Net Framework)；
	框架版本为 .Net Framework 4.0；
***
> ### 2>. 进入项目属性，
	生成目录设置为：
	Debug => ..\Debug\Programs\
	Release => ..\Release\Programs\
***

> ### 3>. 为项目添加引用
	添加引用 => ProgramTemplate.dll
	在 项目>引用 里选中ProgramTemplate，属性窗口内 复制本地 => false；
***

> ### 4>. 设置解决方案项目依赖
	为 DefaultPrograms 项目设置 ProgramTeamplate 项目依赖
***

> ### 5>. Program类内引用 ProgramTemplate，继承并实现 ProgramTemplateClass 抽象类
	在构造函数内赋值插件的 名称、描述、预览；
	使用FileName属性返回程序集文件名称;
	注意，只需要在CreateProgramForm()方法返回Form即可，不要在构造函数内创建Form对象，否则会因为创建Class对象而造成Form占用大量内存；

>*外部类使用 ProgramForms 访问此插件创建的所有窗口，此字段在外部和子类为只读；*
>*子类实现 protected CreateProgramForm() 方法创建新窗口并返回；*
>*父类使用 GetNewProgramForm() (子类可覆写此方法，但仍需调用 base.GetNewProgramForm() )方法将新窗口加入插件窗口集合，并为新窗口注册事件=>{当新窗口关闭时，从窗口集合移除}；*
>*外部类调用 GetNewProgramForm() 获取新窗口以显示；*

``` csharp
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPrograms
{
    public class DefaultProgramClass : ProgramTemplateClass
    {
	    public DefaultProgramClass()
        {
            Name = "钢铁侠";
            Description = "钢铁侠 [via leon]";
            Icon = DefaultProgramResource.IronManIcon;
        }

        public override string FileName =>
            System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;


        protected override Form CreateProgramForm()
        {
            return new Form();
        }

		/* TODO: 其他抽象或可重写方法 ...
		*/

    }
}
```
***

> ### 6>. 发挥想象，设计喜欢的 Program
***

> ### 7>. 使用插件
	将编译好的程序插件放在 .\HackSystem\Programs\ 目录内，系统启动时即可扫描并创建此插件；

***

> ### 结束
via : Leon.ID@QQ.COM