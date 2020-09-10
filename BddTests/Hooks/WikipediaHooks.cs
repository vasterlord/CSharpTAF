using Engine.WebDrivers;
using Support.Utils;
using TechTalk.SpecFlow;

namespace BddTests.Hooks
{

    [Binding]
    public class WikipediaHooks
    {

        [BeforeScenario(Order = 1)]
        [Scope(Tag = "beforeScenario")]
        public static void SetUpHook()
        {
            //InitAllureDirectory();
            //ReadAllureConfig();
            LoggerUtils.LoadLoggerConfig();
        }

        [AfterScenario(Order = 1)]
        public static void TearDownHook()
        {

            WebDriverManager.StopBrowser();
        }

        // TODO: investigate how to work with SpecFlow.Allure

        //public static void ReadAllureConfig()
        //{
        //    Environment.SetEnvironmentVariable(
        //        AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
        //        Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME)); 

        //    var config = AllureLifecycle.Instance.JsonConfiguration;
        //}

        //[OneTimeSetUp]
        //public static void InitAllureDirectory()
        //{
        //    Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //}
    }

}