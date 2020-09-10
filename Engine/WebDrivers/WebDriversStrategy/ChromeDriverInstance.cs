using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Support.LocalConfigs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Engine.WebDrivers.WebDriversStrategy
{
    internal class ChromeDriverInstance : IWebDriverStrategy
    {
        public IWebDriver GetLocalWebDriverInstance()
        {
            var driverExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var driverService = ChromeDriverService.CreateDefaultService(driverExePath);

            ChromeOptions chromeOptions = new ChromeOptions();
            // Fix to work
            //SetBrowserLoggingCapabilities(ref chromeOptions);
            //SetLockProdFeedbackPopupOption(ref chromeOptions);

            // Investigate how to set dictionary to chrome options
            //Dictionary<string, object> chromePreferences = GetDefaultChromePrefs(Configurations.LOCAL_DOWNLOADED_FILES_DIR);
            //SetTurnOffSavePasswordOption(ref chromePreferences);

            var webDriver = new ChromeDriver(driverService, chromeOptions);

            driverService.HideCommandPromptWindow = true;
            webDriver.Manage().Window.Maximize();

            return webDriver;
        }

        public IWebDriver GetRemoteWebDriverInstance()
        {
            throw new System.NotImplementedException();
        }

        private void SetBrowserLoggingCapabilities(ref ChromeOptions options)
        {
            var perfLogPrefs = new ChromePerformanceLoggingPreferences();
            var tracingCategories = "toplevel,disabled-by-default-devtools.timeline.frame,blink.console,disabled-by-default-devtools.timeline,benchmark";
            perfLogPrefs.AddTracingCategories(new string[] { tracingCategories });
            options.PerformanceLoggingPreferences = perfLogPrefs;
            options.SetLoggingPreference("performance", LogLevel.All);
        }

        private void SetLockProdFeedbackPopupOption(ref ChromeOptions chromeOptions)
        {
            chromeOptions.AddArguments("--host-resolver-rules=" + "MAP www.qualtrics.com 127.0.0.1,"
                    + "MAP www.siteintercept.qualtrics.com  127.0.0.1," + "MAP s.qualtrics.com 127.0.0.1,"
                    + "MAP www.zneipf5yepteyvptx-alticorinc.siteintercept.qualtrics.com 127.0.0.1,"
                    + "MAP zneipf5yepteyvptx-alticorinc.siteintercept.qualtrics.com 127.0.0.1");
        }

        private Dictionary<string, object> GetDefaultChromePrefs(string defaultDirectory)
        {
            Dictionary<string, object> prefs = new Dictionary<string, object>();
            prefs.Add("profile.default_content_settings.popups", 0);
            prefs.Add("download.default_directory", defaultDirectory);
            prefs.Add("download.prompt_for_download", "false");
            prefs.Add("plugins.plugins_disabled", new String[] { "Chrome PDF Viewer" });
            prefs.Add("plugins.always_open_pdf_externally", LocalConfigsEntityContainer.GetLocalConfigs().IsOpenDownloadedFile);
            prefs.Add("profile.default_content_settings.geolocation", 2);
            return prefs;
        }

        private void SetTurnOffSavePasswordOption(ref Dictionary<string, object> prefs)
        {
            prefs.Add("credentials_enable_service", false);
            prefs.Add("profile.password_manager_enabled", false);
        }
    }
}