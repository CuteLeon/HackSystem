﻿using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackSystem.Host.Tests
{
    [TestClass()]
    public class AssemblyControllerTests
    {
        [TestMethod()]
        public void CreateAssemblyTest()
        {
            if (AssemblyController<object>.CreateAssembly(@"..\..\..\Debug\StartUps\DefaultStartUp.dll") == null) Assert.Fail();
            if (AssemblyController<object>.CreateAssembly(@"C:\Windows\system32\user32.dll") != null) Assert.Fail();
            if (AssemblyController<object>.CreateAssembly(@"FileNotExists.dll") != null) Assert.Fail();
        }

        [TestMethod()]
        public void CreateTypeInstanceTest()
        {
            Assembly TestAssembly = AssemblyController<object>.CreateAssembly(@"..\..\..\Debug\StartUps\DefaultStartUp.dll");
            int InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreateTypeInstance(TestAssembly))
            {
                System.Diagnostics.Debug.Print(Instance.GetType().Name);
                InstanceCount++;
            }
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount == 0) Assert.Fail();

            InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreateTypeInstance(TestAssembly, "test"))
                InstanceCount++;
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount != 0) Assert.Fail();

            InstanceCount = 0;
            foreach (var Instance in AssemblyController<object>.CreateTypeInstance(TestAssembly, "DefaultStartUpClass"))
                InstanceCount++;
            System.Diagnostics.Debug.Print(InstanceCount.ToString());
            if (InstanceCount != 1) Assert.Fail();
        }
    }
}