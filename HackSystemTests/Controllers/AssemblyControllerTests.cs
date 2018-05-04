using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace HackSystem.Tests
{
    [TestClass()]
    public class AssemblyControllerTests
    {
        [TestMethod()]
        public void CreateAssemblyTest()
        {
            if (AssemblyController<object>.CreateAssembly(@"..\..\..\Debug\StartUps\DefaultStartUp.dll") == null) Assert.Fail();
            if (AssemblyController<object>.CreateAssembly(@"C:\Windows\system32\user.dll") != null) Assert.Fail();
            if (AssemblyController<object>.CreateAssembly(@"FileNotExists.dll") != null) Assert.Fail();
        }

        [TestMethod()]
        public void CreatePluginInstanceTest()
        {
            Assembly TestAssembly = AssemblyController<object>.CreateAssembly(@"..\..\..\Debug\StartUps\DefaultStartUp.dll");
            int InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreatePluginInstance(TestAssembly))
            {
                System.Diagnostics.Debug.Print(Instance.GetType().Name);
                InstanceCount++;
            }
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount == 0) Assert.Fail();

            InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreatePluginInstance(TestAssembly, "test"))
                InstanceCount++;
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount != 0) Assert.Fail();

            InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreatePluginInstance(TestAssembly, "DefaultStartUpClass"))
                InstanceCount++;
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount != 1) Assert.Fail();
        }
    }
}