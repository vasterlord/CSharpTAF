using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Support.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Engine.WebDrivers
{
    public class WebDriverManager
    {
        private const int IMPLICITLY_WAIT_TIMEOUT = 30;

        private static ThreadLocal<IWebDriver> webdriver = new ThreadLocal<IWebDriver>();

        private static readonly WebDriverFactory webDriverFactory = new WebDriverFactory();

        private static readonly string JS_DRAG_DROP_FUNCTION = string.Format("{0}{1}{2}",
            "function simulate(f,c,d,e){var b,a=null;for(b in eventMatchers)if(eventMatchers[b].test(c)){a=b;break}if(!a)return!1;document.createEvent?(b=document.createEvent(a),a==\"HTMLEvents\"?b.initEvent(c,!0,!0):b.initMouseEvent(c,!0,!0,document.defaultView,0,d,e,d,e,!1,!1,!1,!1,0,null),f.dispatchEvent(b)):",
            "(a=document.createEventObject(),a.detail=0,a.screenX=d,a.screenY=e,a.clientX=d,a.clientY=e,a.ctrlKey=!1,a.altKey=!1,a.shiftKey=!1,a.metaKey=!1,a.button=1,f.fireEvent(\"on\"+c,a));return!0} var eventMatchers={HTMLEvents:/^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,MouseEvents:/^(?:click|dblclick|mouse(?:down|up|over|move|out))$/}; ",
            "simulate(arguments[0],\"mousedown\",0,0); simulate(arguments[0],\"mousemove\",arguments[1],arguments[2]); simulate(arguments[0],\"mouseup\",arguments[1],arguments[2]); ");

        public static IWebDriver GetDriver()
        {
            return webdriver.Value != null ? webdriver.Value : webdriver.Value = webDriverFactory.NewDriverInstance();
        }

        private WebDriverManager()
        {
        }

        public static void NavigateToUrl(string url)
        {
            GetDriver().Navigate().GoToUrl(url);
        }

        public static string getCurrentUrl()
        {
            return GetDriver().Url;
        }

        public static void NavigateBack()
        {
            GetDriver().Navigate().Back();
        }

        public static void NavigateForward()
        {
            GetDriver().Navigate().Forward();
        }

        public static void Refresh()
        {
            GetDriver().Navigate().Refresh();
        }

        public static IAlert getAlert()
        {
            return GetDriver().SwitchTo().Alert();
        }

        public static void AcceptAlert()
        {
            GetDriver().SwitchTo().Alert().Accept();
        }

        public static void DismissAlert()
        {
            GetDriver().SwitchTo().Alert().Dismiss();
        }

        public static void Restart()
        {
            Console.WriteLine("Clearing cookies and stopping browser");
            IWebDriver driver = GetDriver();
            driver.Manage().Cookies.DeleteAllCookies();
            StopBrowser();
            GetDriver();
        }

        public static void MouseOver(IWebElement webElement)
        {
            Actions action = new Actions(GetDriver());
            action.MoveToElement(webElement).Build().Perform();
        }

        public static void DragAndDrop(IWebElement elementToDrag, IWebElement placeDropTo)
        {
            Actions action = new Actions(GetDriver());
            action.ClickAndHold(elementToDrag).MoveToElement(placeDropTo).Release(placeDropTo).Build().Perform();
        }

        public static void JsDragAndDrop(IWebElement elementToDrag, IWebElement placeDropTo)
        {
            string xto = placeDropTo.Location.X.ToString();
            string yto = placeDropTo.Location.Y.ToString();
            ExecuteScript(JS_DRAG_DROP_FUNCTION, new object[] { elementToDrag, xto, yto });
        }

        public static bool IsElementSelect(IWebElement element)
        {
            return element.TagName.Equals(CommonConstants.SELECT);
        }

        public static string getFieldValue(IWebElement element)
        {
            string result;
            if (element.TagName.Equals(CommonConstants.INPUT))
            {
                result = element.GetAttribute(CommonConstants.CSS_VALUE);
            }
            else if (element.TagName.Equals(CommonConstants.SELECT))
            {
                SelectElement selectedValue = new SelectElement(element);
                result = selectedValue.SelectedOption.Text;
            }
            else
            {
                result = element.Text;
            }
            return result;
        }

        public static string getAllCookiesAsString()
        {
            return string.Join(";", GetDriver().Manage().Cookies.AllCookies.Select(item => item.ToString()).ToArray());
        }

        public static string getCookieValue(string cookieName)
        {
            Cookie cookie = GetDriver().Manage().Cookies.AllCookies.Where(item => string.Equals(item.Name, cookieName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault(item => item == null);
            return cookie != null ? cookie.Value : $"Cookie value for name {cookieName} was not found";
        }

        public static void SwitchToLastTab()
        {
            if (!IsOnLastTab())
            {
                HashSet<string> windowHandles = new HashSet<string>(GetDriver().WindowHandles);
                windowHandles.Remove(GetDriver().CurrentWindowHandle);
                GetDriver().SwitchTo().Window(windowHandles.Last());
            }
        }

        public static bool IsOnLastTab()
        {
            HashSet<string> windowHandles = new HashSet<string>(GetDriver().WindowHandles);
            return GetDriver().CurrentWindowHandle.Equals(windowHandles.Last());
        }

        public static object ExecuteScript(string script, object[] args)
        {
            return ((IJavaScriptExecutor)GetDriver()).ExecuteScript(script, args);
        }

        public static void setAttribute(IWebElement element, string attName, string attValue)
        {
            ((IJavaScriptExecutor)GetDriver()).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);",
                    element, attName, attValue);
        }

        public static Actions getActions()
        {
            return new Actions(GetDriver());
        }


        public static int GetBrowserTabCount()
        {
            return GetDriver().WindowHandles.Count;
        }

        public static void TurnOnImplicitWaitSafely()
        {
            try
            {
                GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICITLY_WAIT_TIMEOUT);
            }
            catch (WebDriverException e)
            {
                Console.WriteLine($"Exception happened when try to set implicit wait: {e.Message}");
            }
        }

        public static void TurnOffImplicitWaitSafely()
        {
            SetImplicitWaitSafely(CommonConstants.ZERO);
        }

        public static void SetImplicitWaitSafely(int timeInSeconds)
        {
            try
            {
                GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeInSeconds);
            }
            catch (WebDriverException e)
            {
                Console.WriteLine("Exception happened when try to set implicit wait: " + e.Message);
            }
        }

        public static void StopBrowser()
        {
            GetDriver().Quit();
            GetDriver().Dispose();
            webdriver.Value = null;
            webdriver.Dispose();
            Console.WriteLine("Web driber has been closed!");
        }
    }
}