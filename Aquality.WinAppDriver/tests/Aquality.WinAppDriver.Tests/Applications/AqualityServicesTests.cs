﻿using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Windows;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class AqualityServicesTests : TestWithApplication
    {
        private readonly CalculatorWindow calculatorWindow = new CalculatorWindow();

        [Test]
        public void Should_WorkWithCalculator()
        {
            AqualityServices.Application.Driver.FindElement(calculatorWindow.OneButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.PlusButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.TwoButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = AqualityServices.Application.Driver.FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.OneButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.PlusButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.TwoButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_GetCurrentApplicationFactory_AfterSetDefaultFactory()
        {
            var firstFactory = AqualityServices.Get<IApplicationFactory>();
            AqualityServices.SetDefaultFactory();
            var secondFactory = AqualityServices.Get<IApplicationFactory>();
            Assert.AreNotSame(firstFactory, secondFactory);
            AqualityServices.ApplicationFactory = firstFactory;
            Assert.AreSame(firstFactory, AqualityServices.Get<IApplicationFactory>());
            Assert.AreNotSame(secondFactory, AqualityServices.Get<IApplicationFactory>());
        }

        [Test]
        public void Should_GetCurrentApplication_AfterSetApplication()
        {
            IApplication firstApplication;
            using(var scope = AqualityServices.ServiceProvider.CreateScope())
            {
                firstApplication = scope.ServiceProvider.GetRequiredService<IApplication>();
            }

            // Creating a second instance of Application
            AqualityServices.Application = AqualityServices.ApplicationFactory.Application;

            using (var scope = AqualityServices.ServiceProvider.CreateScope())
            {
                var secondApplication = scope.ServiceProvider.GetRequiredService<IApplication>();
                Assert.AreNotSame(firstApplication, secondApplication);
                secondApplication.Driver.Quit();
            }

            // Switching back to a first instance of Application
            AqualityServices.Application = firstApplication as Application;

            using (var scope = AqualityServices.ServiceProvider.CreateScope())
            {
                Assert.AreSame(firstApplication, scope.ServiceProvider.GetRequiredService<IApplication>());
            }
        }

        [Test]
        public void Should_GetCurrentApplication_AfterQuit()
        {
            var firstApplication = AqualityServices.Application;
            firstApplication.Quit();
            var secondApplication = AqualityServices.Application;
            Assert.AreNotSame(firstApplication, secondApplication);
            using (var scope = AqualityServices.ServiceProvider.CreateScope())
            {
                var secondApplicationFromServiceProvider = scope.ServiceProvider.GetRequiredService<IApplication>();
                Assert.AreNotSame(firstApplication, secondApplicationFromServiceProvider);
                Assert.AreSame(secondApplication, secondApplicationFromServiceProvider);
            }
        }
    }
}
