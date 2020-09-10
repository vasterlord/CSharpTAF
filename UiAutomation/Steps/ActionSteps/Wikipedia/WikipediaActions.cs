using Engine.WebDrivers;
using PageObjects.PageObjects.Wikipedia;
using System;
using UiAutomation.Context;
using Unity;

namespace UiAutomation.ActionSteps.Wikipedia
{
    public class WikipediaActions
    {
        private readonly WikipediaSearchPageObject _wikipediaSearchPage;

        public WikipediaActions()
        {
            _wikipediaSearchPage = ContextContainerConfig.GetContextContainer().Resolve<WikipediaSearchPageObject>();
        }

        public void NavigateToWikipediaPage()
        {
            Console.WriteLine($"Open: {WikipediaSearchPageObject.VIKIPEDIA_URL}");
            WebDriverManager.NavigateToUrl(WikipediaSearchPageObject.VIKIPEDIA_URL);
        }

        public void SearchDataViaWikipedia(string searchedRequest)
        {
            Console.WriteLine($"Search '{searchedRequest}' data on Wikipedia Page");
            _wikipediaSearchPage.SearchInputElement.SendKeys(searchedRequest);
            _wikipediaSearchPage.SearchButton.Click();
        }
    }
}