using PageObjects.PageObjects.Wikipedia;
using System.Threading;
using UiAutomation.ActionSteps.Wikipedia;
using UiAutomation.Steps.AssertionSteps.Wikipedia;
using Unity;

namespace UiAutomation.Context
{
    public static class ContextContainerConfig
    {
        private static ThreadLocal<UnityContainer> _contextContainer = new ThreadLocal<UnityContainer>();

        private static UnityContainer RegisterContainerContext()
        {
            var contextContainer = new UnityContainer();

            // Register Page Objects 
            contextContainer.RegisterType<WikipediaSearchPageObject>();
            contextContainer.RegisterType<WikipediaArticlePageObject>();

            //Register Steps Definitions Layer
            contextContainer.RegisterType<WikipediaActions>();
            contextContainer.RegisterType<WikipediaAsserter>();

            //Register Shared Test Data Context
            contextContainer.RegisterSingleton<SharedTestsData>();

            return contextContainer;
        }

        public static UnityContainer GetContextContainer()
        {
            if (_contextContainer.Value == null)
            {
                _contextContainer.Value = RegisterContainerContext();
            }

            return _contextContainer.Value;
        }

    }
}