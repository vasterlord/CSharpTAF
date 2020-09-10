using log4net;
using NUnit.Framework;
using PageObjects.PageObjects.Wikipedia;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UiAutomation.Context;
using Unity;

namespace UiAutomation.Steps.AssertionSteps.Wikipedia
{
    public class WikipediaAsserter
    {
        private readonly WikipediaArticlePageObject _wikipediaArticlePage;

        private static readonly ILog LOG = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public WikipediaAsserter()
        {
            _wikipediaArticlePage = ContextContainerConfig.GetContextContainer().Resolve<WikipediaArticlePageObject>();
        }

        public void VerifySearchedLinksContainText(string linkText)
        {
            LOG.Info($"All found links should contain {linkText}");
            List<string> linksResults = _wikipediaArticlePage.AllFoundLinks.Where(item => item.Displayed && item.Enabled)
                .Select(item => item.GetAttribute("href")).ToList();

            Assert.That(linksResults, Has.All.Contains("/").Or.Contain(linkText), $"Each link should contain {linkText}");
        }
    }
}