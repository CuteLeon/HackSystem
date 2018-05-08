using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StartUpTemplate;

namespace HackSystem.Tests
{
    [TestClass()]
    public class StartUpControllerTests
    {
        [TestMethod()]
        public void ScanStartUpPluginsTest()
        {
            int StartUpCount = 0;
            foreach (StartUpTemplateClass Instance in StartUpController.ScanStartUpPlugins(@"..\..\..\Debug\StartUps\"))
            {
                StartUpCount++;
            }
            if (StartUpCount == 0) Assert.Fail();
        }

        [TestMethod()]
        public void GetStartUpPluginTest()
        {
            StartUpTemplateClass TestClass = null;
            TestClass = StartUpController.GetStartUpPlugin(@"..\..\..\Debug\StartUps\ScientistStartUp.dll", "ScientistStartUpClass");
            if(TestClass == null) Assert.Fail();
            if (TestClass.FileName != @"ScientistStartUp.dll") Assert.Fail();

            TestClass = StartUpController.GetStartUpPlugin(@"..\..\..\Debug\StartUps\DefaultStartUp.dll", "DefaultStartUpClass");
            if(TestClass == null) Assert.Fail();
            if (TestClass.FileName != @"DefaultStartUp.dll") Assert.Fail();

            TestClass = StartUpController.GetStartUpPlugin(@"..\..\..\Debug\StartUps\FileNotExists.dll", "ClassNotFound");
            if(TestClass != null) Assert.Fail();

            TestClass = StartUpController.GetStartUpPlugin(@"C:\Windows\System32\user32.dll", "llalalalla");
            if(TestClass != null) Assert.Fail();
        }
    }
}