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
            if (StartUpController.GetStartUpPlugin(@"..\..\..\Debug\StartUps\DefaultStartUp.dll", "DefaultStartUpClass") == null) Assert.Fail();
            if (StartUpController.GetStartUpPlugin(@"..\..\..\Debug\StartUps\FileNotExists.dll", "ClassNotFound") != null) Assert.Fail();
            if (StartUpController.GetStartUpPlugin(@"C:\Windows\System32\user32.dll", "llalalalla") != null) Assert.Fail();
        }
    }
}