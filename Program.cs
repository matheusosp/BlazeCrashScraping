using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

var driver =  new ChromeDriver();
var js = (IJavaScriptExecutor)driver;
var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
driver.Navigate().GoToUrl("https://blaze.com/pt/games/crash");
Thread.Sleep(5000);
wait.Until(d => js.ExecuteScript("return document.readyState").Equals("complete"));
DateTime endTime = DateTime.Now.AddHours(15);
while (DateTime.Now < endTime)
{
    var div = driver.FindElement(By.ClassName("crash-previous")).GetAttribute("innerHTML");;
    Func<IWebDriver, bool> condition = driver =>
    {
        var div2 = driver.FindElement(By.ClassName("crash-previous")).GetAttribute("innerHTML");;
        return div != div2;
    };
    wait.Until(condition);

    var table = wait.Until(d => d.FindElement(By.ClassName("casino-table")));
    var linhas = table.FindElements(By.CssSelector("tbody tr.entry"));
    foreach (var linha in linhas)
    {
        try
        {
            var aposta = linha.FindElement(By.CssSelector(".bet")).Text.Trim();
            var lucroPerda = linha.FindElement(By.CssSelector(".profit")).Text.Trim();
            if (lucroPerda == "-")
            {
                lucroPerda = "-" + aposta;
            }

            using var writer = new StreamWriter("data.csv", append: true);
            writer.WriteLine($"{DateTime.Now},{aposta},{lucroPerda}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
    
}
driver.Quit();
Console.ReadLine();