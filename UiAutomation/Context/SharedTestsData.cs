using System.Collections.Generic;

namespace UiAutomation.Context
{
    public class SharedTestsData
    {
        public List<string> someTestsSharedData { get; set; }

        public SharedTestsData()
        { 
            someTestsSharedData = new List<string>();
        }
    }
}