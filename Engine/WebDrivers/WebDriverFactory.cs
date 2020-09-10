using Engine.WebDrivers.WebDriversStrategy;
using OpenQA.Selenium;
using Support.Enums;
using Support.LocalConfigs;
using Support.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Engine.WebDrivers
{
    internal class WebDriverFactory
    {
        private IDictionary<DriverTypes, IWebDriverStrategy> _driversInstances = new ConcurrentDictionary<DriverTypes, IWebDriverStrategy>(
            new Dictionary<DriverTypes, IWebDriverStrategy>()
            {
                { DriverTypes.CHROME, new ChromeDriverInstance() }
            });

        public IWebDriver NewDriverInstance()
        {
            // Also can be returned smth like RemoteDriverInstance, etc... 
            return GetLocalDriverInstance();
        }

        private IWebDriver GetLocalDriverInstance()
        {
            IWebDriverStrategy currentWebDriverStrategy;
            if (_driversInstances.TryGetValue(LocalConfigsEntityContainer.GetLocalConfigs().Driver.ToUpper().ToEnum<DriverTypes>(), out currentWebDriverStrategy))
            {
                return currentWebDriverStrategy.GetLocalWebDriverInstance();
            }
            else
            {
                throw new ArgumentException("Unsupported local driver type: " + LocalConfigsEntityContainer.GetLocalConfigs().Driver);
            }
        }
    }
}