using Engine.WebDrivers;
using System.Linq;
using TechTalk.SpecFlow;
using UiAutomation.ActionSteps.Wikipedia;
using UiAutomation.Context;
using Unity;

namespace BddTests.Steps.Definitions
{
    [Binding]
    public sealed class WikipediaDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly WikipediaActions _wikipediaActions;
        private readonly SharedTestsData _sharedTestsData;

        public WikipediaDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _wikipediaActions = ContextContainerConfig.GetContextContainer().Resolve<WikipediaActions>();
            _sharedTestsData = ContextContainerConfig.GetContextContainer().Resolve<SharedTestsData>();
        }


        [Given("the user navigates to wikipedia page")]
        public void NavigateToWikipediaPage()
        {
            _wikipediaActions.NavigateToWikipediaPage();
        }

        [When(@"the user is looking for information on request (.*)")]
        public void SearchDataViaWikipedia(string searchedRequest)
        {
            _wikipediaActions.SearchDataViaWikipedia(searchedRequest); 
            // For example: populate some shared data for Test Run
            _sharedTestsData.someTestsSharedData.AddRange(WebDriverManager.GetDriver().Manage().Cookies.AllCookies.Select(item => item.ToString()).ToList());
        }
    }
}