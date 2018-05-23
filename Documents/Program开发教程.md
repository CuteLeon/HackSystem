外部类使用 ProgramForms 访问此插件创建的所有窗口，此属性在外部和子类为只读
子类实现 protected CreateProgramForm() 方法创建新窗口并返回
父类使用 GetNewProgramForm() (子类可覆写此方法，但仍需调用 base.GetNewProgramForm() )方法将新窗口加入插件窗口集合，并为新窗口注册事件=>{当新窗口关闭时，从窗口集合移除}
外部类调用 GetNewProgramForm() 获取新窗口以显示