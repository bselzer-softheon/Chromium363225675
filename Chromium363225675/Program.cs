using PuppeteerSharp;
using PuppeteerSharp.Media;

public static class Program
{
    public static void Main(string[] args)
    {
        var launchOptions = new LaunchOptions()
        {
            Headless = true,
            ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
        };

        var mainHtml = File.ReadAllText("C:\\Users\\bselzer\\Downloads\\main.html");
        var footerHtml = File.ReadAllText("C:\\Users\\bselzer\\Downloads\\footer.html");

        using var browser = Puppeteer.LaunchAsync(launchOptions).GetAwaiter().GetResult();
        using var page = browser.NewPageAsync().GetAwaiter().GetResult();

        var navigationOptions = new NavigationOptions() { WaitUntil = [WaitUntilNavigation.Load, WaitUntilNavigation.Networkidle0] };
        page.SetContentAsync(mainHtml, navigationOptions).Wait();

        var pdfOptions = new PdfOptions()
        {
            DisplayHeaderFooter = true,
            HeaderTemplate = "<div></div>",
            FooterTemplate = footerHtml,
            PrintBackground = true,
            Format = PaperFormat.Letter,
            MarginOptions = new MarginOptions()
            {
                Top = "0.27in",
                Right = "0in",
                Bottom = "0.4in",
                Left = "0in"
            }
        };

        var pdf = page.PdfDataAsync(pdfOptions).GetAwaiter().GetResult();
        File.WriteAllBytes("C:\\Users\\bselzer\\Downloads\\output.pdf", pdf);
    }
}