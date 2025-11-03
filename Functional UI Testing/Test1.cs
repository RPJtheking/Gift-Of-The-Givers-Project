using Microsoft.Playwright;

namespace Functional_UI_Testing
{
    [TestClass]
    public sealed class Test1
    {
        private static IPlaywright _pw = null!;
        private static IBrowser _browser = null!;

        //Put your local site URL
        private const string BaseURL = "https://giftofthegiversproject20251103101447-dpe7dycvazgzgfgg.southafricanorth-01.azurewebsites.net/";


        [ClassInitialize]
        public static async Task SetUp(TestContext _)
        {
            _pw = await Playwright.CreateAsync();
            _browser = await _pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        }

        [ClassCleanup]
        public static async Task CleanUp()
        {
            await _browser.CloseAsync();
            _pw.Dispose();
        }

        [TestMethod]
        public async Task CreateStudent_ShowOnIndex()
        {
            var page = await _browser.NewPageAsync();

            //1. Open student list
            await page.GotoAsync($"{BaseURL}/Donations");

            //2. Go to create Page
            await page.ClickAsync("a[href ='/Donations'/Create]");

            //3.Fill the form and submit
            await page.FillAsync("input[name= 'Email']", "Richard@gmail.com");
            await page.ClickAsync("button[type ='submit]'");

            //4.After submit,Scaffold redirects back to /Students
            await page.WaitForURLAsync("/Donations");

            // 5.Assert the new name is visible
            var html = await page.ContentAsync();
            StringAssert.Contains(html, "Richard@gmail.com");

            

        }
    }
}
