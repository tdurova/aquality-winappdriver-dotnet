﻿using System;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Windows;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementStateProviderTests : TestWithApplication
    {
        private static readonly CalculatorWindow CalculatorWindow = new CalculatorWindow();
        private static readonly ITextBox RightArgumentTextBox = CalculatorWindow.RightArgumentTextBox;
        private static readonly IButton MaximizeButton = CalculatorWindow.MaximizeButton;
        private static readonly IButton EmptyButton = CalculatorWindow.EmptyButton;

        private IElement notPresentLabel = ApplicationManager.GetRequiredService<IElementFactory>()
            .GetLabel(By.XPath("//*[@id='111111']"), "Not present element");

        [Test]
        public void Should_ReturnTrue_IfElementIsDisplayed()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotDisplayed()
        {
            Assert.IsFalse(MaximizeButton.State.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfElementDoesNotExist()
        {
            Assert.IsFalse(notPresentLabel.State.IsExist);
        }

        [Test]
        public void Should_ReturnTrue_IfElementExist()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsExist);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotClickable()
        {
            Assert.IsFalse(EmptyButton.State.IsClickable);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsClickable()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsClickable);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotEnabled()
        {
            Assert.IsFalse(EmptyButton.State.IsEnabled);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsEnabled()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsEnabled);
        }

        [Test]
        public void Should_ThrowException_IfWaitForClickableEnded()
        {
            Assert.Throws<WebDriverTimeoutException>(() => EmptyButton.State.WaitForClickable(TimeSpan.Zero));
        }

        [Test]
        public void Should_FinishWaitForClickable_AfterClickaleElementIsFound()
        {
            Assert.DoesNotThrow(() => RightArgumentTextBox.State.WaitForClickable(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForDisplayed_WhenElementIsNotDisplayed()
        {
            Assert.IsFalse(MaximizeButton.State.WaitForDisplayed(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForDisplayed_WhenElementIsDisplayed()
        {
            Assert.IsTrue(RightArgumentTextBox.State.WaitForDisplayed(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotDisplayed_WhenElementIsDisplayed()
        {
            Assert.IsFalse(RightArgumentTextBox.State.WaitForNotDisplayed(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotDisplayed_WhenElementIsNotDisplayed()
        {
            Assert.IsTrue(MaximizeButton.State.WaitForNotDisplayed(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForEnabled_WhenElementIsNotEnabled()
        {
            Assert.IsFalse(EmptyButton.State.WaitForEnabled(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForEnabled_WhenElementIsEnabled()
        {
            Assert.IsTrue(RightArgumentTextBox.State.WaitForEnabled(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotEnabled_WhenElementIsEnabled()
        {
            Assert.IsFalse(RightArgumentTextBox.State.WaitForNotEnabled(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotEnabled_WhenElementIsNotEnabled()
        {
            Assert.IsTrue(EmptyButton.State.WaitForNotEnabled(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForExist_WhenElementDoesNotExist()
        {
            Assert.IsFalse(notPresentLabel.State.WaitForExist(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForExist_WhenElementExists()
        {
            Assert.IsTrue(RightArgumentTextBox.State.WaitForExist(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotExist_WhenElementExists()
        {
            Assert.IsFalse(RightArgumentTextBox.State.WaitForNotExist(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotExist_WhenElementDoesNotExist()
        {
            Assert.IsTrue(notPresentLabel.State.WaitForNotExist(TimeSpan.Zero));
        }
    }
}