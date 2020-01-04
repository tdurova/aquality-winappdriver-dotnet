﻿using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class TestWindow : Window, ITestForm
    {
        public TestWindow(By locator, string name) : base(locator, name)
        {
        }

        public new string ElementType => base.ElementType;
    }
}
