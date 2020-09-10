using OpenQA.Selenium;

namespace Engine.WebDrivers
{
    internal interface IWebDriverStrategy
    {
        IWebDriver GetLocalWebDriverInstance();

        IWebDriver GetRemoteWebDriverInstance();
    }
}