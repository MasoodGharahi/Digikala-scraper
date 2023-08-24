using Digikala_scraper.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.HttpResults;
using PuppeteerSharp;
using System.Xml;

namespace Digikala_scraper.Utility
{
    public static class Scraper
    {

        public static async  Task<Product> GetAttributesAsync(string url)
        {
            Product prodoct;
            try
            {

                const string chromeLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                const string screenShotLocation = "wwwroot/screenshot.jpg";
                var options = new LaunchOptions()
                {
                    Headless = true,
                    //Google chrome location on your local machine
                    ExecutablePath = chromeLocation
                };
                //launch chrome instance
                var browser = await Puppeteer.LaunchAsync(options, null);
                //open new page in browser
                var page = await browser.NewPageAsync();
                await page.GoToAsync(url);
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1920,
                    Height = 1080
                });
                //wait 10 seconds to let the page load completley
                //if you have a fast internet connection you may decrease the time out
                await page.WaitForTimeoutAsync(10000);

                string content = await page.GetContentAsync();
                await page.ScreenshotAsync(screenShotLocation);
                await browser.CloseAsync();
                prodoct = new Product();
                //Extract Data from html            
                if (!String.IsNullOrEmpty(content))
                {                  
                    var doc = new HtmlDocument();
                    doc.LoadHtml(content);
                    //retrieve first <H1> tag that contains product name
                    HtmlNode h1 = doc.DocumentNode.SelectSingleNode("//h1");
                    if (!String.IsNullOrEmpty(h1.InnerText))
                        prodoct.Name = h1.InnerText;
                    //retrieve rate and convert in to english numbers
                    var rate = doc.DocumentNode.SelectSingleNode("//p[@class='mr-1 text-body-2']");
                    prodoct.Rate = Fixer.ToEnglishDouble(rate.InnerText);

                    //do the same with price
                    var price = doc.DocumentNode.SelectSingleNode("//div[@class='pos-relative w-full w-auto-lg px-4-lg pb-4-lg']//div[@class='w-full w-auto-lg z-3 bg-000 shadow-fab-button shadow-none-lg styles_BuyBoxFooter__actionWrapper__Hl4e7']//div//span[@class='color-800 ml-1 text-h4']");
                    prodoct.Price = Fixer.ToEnglishDouble(price.InnerText);
                }
                return prodoct;
            }
            catch
            {
                return new Product();
            }

        }
    }
}
