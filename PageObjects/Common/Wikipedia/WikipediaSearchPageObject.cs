using OpenQA.Selenium;
using PageObjects.Common;
using SeleniumExtras.PageObjects;

namespace PageObjects.PageObjects.Wikipedia
{
    public class WikipediaSearchPageObject : AbstractPageObject
    {
        public static string VIKIPEDIA_URL = "https://www.wikipedia.org/";

        [FindsBy(How = How.CssSelector, Using = "input[id='searchInput']")]
        public IWebElement SearchInputElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        public IWebElement SearchButton { get; set; }
    }
}