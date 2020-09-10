using OpenQA.Selenium;
using PageObjects.Common;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace PageObjects.PageObjects.Wikipedia
{
    public class WikipediaArticlePageObject : AbstractPageObject
    {
        [FindsBy(How = How.CssSelector, Using = "a[href]")]
        public IList<IWebElement> AllFoundLinks { get; set; }
    }
}