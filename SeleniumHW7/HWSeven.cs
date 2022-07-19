using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V85.Page;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHW7
{
    [TestFixture]

    public class HWSeven
    {
        IWebDriver Driver;

        [SetUp]

        public void Preconditions()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Driver.Manage().Window.Maximize();

        }

        [Test]

        public void TestOne()
        {
           
            Driver.SwitchTo().Frame("frame-top");


            Driver.SwitchTo().Frame("frame-left");
            IWebElement leftElement = Driver.FindElement(By.TagName("body"));
            Assert.AreEqual("LEFT", leftElement.Text);
            string probaleft = leftElement.Text;
            Console.WriteLine("Text field " + probaleft);


            Driver.SwitchTo().ParentFrame();


            Driver.SwitchTo().Frame("frame-middle");

            IWebElement middleElement = Driver.FindElement(By.Id("content"));
            Assert.AreEqual("MIDDLE", middleElement.Text);
            string probaMiddle = middleElement.Text;
            Console.WriteLine("Text field " + probaMiddle);

            Driver.SwitchTo().ParentFrame();

            Driver.SwitchTo().Frame("frame-right");
            IWebElement rightElement = Driver.FindElement(By.TagName("body"));
            string probaright = rightElement.Text;
            Console.WriteLine("Text field " + probaright);
                      

        }


        [Test]

        public void TestTwoo()
        {

            
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.open()");


            Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            Driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/iframe");


            Driver.SwitchTo().Frame("mce_0_ifr");

            IWebElement textFieldElement = Driver.FindElement(By.Id("tinymce"));
            Assert.AreEqual("Your content goes here.", textFieldElement.Text);
            Console.WriteLine(textFieldElement.Text);


            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            Driver.SwitchTo().Frame("frame-top");


            Driver.SwitchTo().Frame("frame-middle");

            IWebElement middleElement = Driver.FindElement(By.Id("content"));
            Assert.AreEqual("MIDDLE", middleElement.Text);
            string probaMiddle = middleElement.Text;
            Console.WriteLine("Text field " + probaMiddle);

            Driver.SwitchTo().DefaultContent();

            Driver.SwitchTo().Frame("frame-bottom");
            IWebElement BottomElement = Driver.FindElement(By.TagName("body"));
            Assert.AreEqual("BOTTOM", BottomElement.Text);
            string probaBottom = BottomElement.Text;
            Console.WriteLine("Text field " + probaBottom);

        }

        [TearDown]
        public void CloseAll()
        {
            Driver.Quit();
            Driver.Dispose();
        }



    }
}

