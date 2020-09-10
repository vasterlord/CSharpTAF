using System;
using TechTalk.SpecFlow;
using UiAutomation.Context;
using UiAutomation.Steps.AssertionSteps.Wikipedia;
using Unity;

namespace BddTests.Steps.Asserts
{
    [Binding]
    public sealed class WikipediaAsserts
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly WikipediaAsserter _wikipediaAsserter;
        private readonly SharedTestsData _sharedTestsData;

        public WikipediaAsserts(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _wikipediaAsserter = ContextContainerConfig.GetContextContainer().Resolve<WikipediaAsserter>();
            _sharedTestsData = ContextContainerConfig.GetContextContainer().Resolve<SharedTestsData>();
        }

        [Then(@"the all found links should contain with (.*)")]
        public void AllSearchedLinksShouldContainText(string linkText)
        {
            _wikipediaAsserter.VerifySearchedLinksContainText(linkText);
            // For example: extract shared tests data and do something with that
            _sharedTestsData.someTestsSharedData.ForEach(value => Console.WriteLine(value));
        }
    }
}