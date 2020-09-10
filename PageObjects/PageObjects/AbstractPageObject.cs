using Engine.WebDrivers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjects.Common
{
    public abstract class AbstractPageObject
    {
        protected IWebDriver webDriver = WebDriverManager.GetDriver();

        public AbstractPageObject()
        {
            PageFactory.InitElements(webDriver, this);
        }
    }
}