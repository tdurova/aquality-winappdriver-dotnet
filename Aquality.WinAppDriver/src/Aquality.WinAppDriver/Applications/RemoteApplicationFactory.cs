﻿using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class RemoteApplicationFactory : ApplicationFactory
    {
        private readonly Uri driverServerUri;
        private readonly IDriverSettings driverSettings;
        private readonly ITimeoutConfiguration timeoutConfiguration;
        private readonly LocalizationLogger localizationLogger;

        public RemoteApplicationFactory(Uri driverServerUri, IDriverSettings driverSettings, ITimeoutConfiguration timeoutConfiguration, LocalizationLogger localizationLogger) : base(localizationLogger)
        {
            this.driverServerUri = driverServerUri;
            this.driverSettings = driverSettings;
            this.timeoutConfiguration = timeoutConfiguration;
            this.localizationLogger = localizationLogger;
        }

        public override Application Application
        {
            get
            {
                localizationLogger.Info("loc.application.driver.service.remote", driverServerUri);
                var driver = GetDriver(driverServerUri, driverSettings.AppiumOptions, timeoutConfiguration.Command);
                driver.FileDetector = new LocalFileDetector();
                return new Application(driver, timeoutConfiguration, localizationLogger);
            }
        }
    }
}