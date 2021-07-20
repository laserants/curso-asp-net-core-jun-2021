using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Xunit;

namespace SeleniumTesting
{
    public class MoviesWebAppTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public MoviesWebAppTests()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [Fact]
        public void TestCorrectHeaderInIndex()
        {
            driver.Url = "https://localhost:44326/";
            IWebElement moviesLinkWebElement = driver.FindElement(By.XPath(".//a[text()='Peliculas']"));
            Thread.Sleep(5000);
            moviesLinkWebElement.Click();
            IWebElement header = wait.Until(driver => driver.FindElement(By.XPath(".//h3[text()='Lista de peliculas']")));
            driver.Quit();
        }

        [Fact]
        public void TestSearchMovie()
        {
            driver.Url = "https://localhost:44326/";
            //Click a movies index
            IWebElement moviesLinkWebElement = driver.FindElement(By.XPath(".//a[text()='Peliculas']"));
            moviesLinkWebElement.Click();
            Thread.Sleep(5000);
            //Esperar a que aparezca el input de Busqueda, obtener el web element y llenarlo
            IWebElement searchInput = wait.Until(driver => driver.FindElement(By.XPath(".//*[@name='SearchString']")));
            searchInput.Clear();
            searchInput.SendKeys("Lion");
            Thread.Sleep(5000);
            //Obtener web element del boton y hacer click para filtrar
            IWebElement filterButton = driver.FindElement(By.XPath(".//*[@value='Filtrar']"));
            filterButton.Click();
            Thread.Sleep(5000);
            //Buscar en la table la pelicula nueva y validar
            IWebElement movieRow = driver.FindElement(By.XPath(".//td[text()='The Lion King']"));
            Assert.True(movieRow != null);
            Assert.True(movieRow.Displayed);
            driver.Quit();
        }

        [Fact]
        public void TestCreateMovie()
        {
            driver.Url = "https://localhost:44326/";
            var title = "Mi nueva pelicula";
            var year = "2021";
            var rating = "99";
            Thread.Sleep(5000);
            //Click a movies index
            IWebElement moviesLinkWebElement = driver.FindElement(By.XPath(".//a[text()='Peliculas']"));
            moviesLinkWebElement.Click();
            Thread.Sleep(5000);
            //Obtener web element del boton y hacer click para ir a form de crear nueva pelicula
            IWebElement createNewMovieButton = driver.FindElement(By.XPath(".//a[text()='Agregar nueva pelicula']"));
            createNewMovieButton.Click();
            //Esperar a que aparezca el input de Titulo, obtener el web element y llenarlo
            IWebElement titleInput = wait.Until(driver => driver.FindElement(By.Id("Title")));
            titleInput.Clear();
            titleInput.SendKeys(title);
            Thread.Sleep(5000);
            //obtener el web element del input Year y llenarlo
            IWebElement yearInput = wait.Until(driver => driver.FindElement(By.Id("Year")));
            yearInput.Clear();
            yearInput.SendKeys(year);
            Thread.Sleep(5000);
            //obtener el web element del input Rating y llenarlo
            IWebElement ratingInput = wait.Until(driver => driver.FindElement(By.Id("Rating")));
            ratingInput.Clear();
            ratingInput.SendKeys(rating);
            Thread.Sleep(5000);
            //Obtener web element del boton y hacer click para crear pelicula
            IWebElement createButton = driver.FindElement(By.XPath(".//*[@value='Crear Pelicula']"));
            createButton.Click();
            Thread.Sleep(5000);
            //Buscar en la table la pelicula nueva y validar
            IWebElement movieRow = driver.FindElement(By.XPath($".//td[text()='{title}']"));
            Assert.True(movieRow != null);
            Assert.True(movieRow.Displayed);
            //Validar que aparezca el mensaje
            //IWebElement sucessMessage = driver.FindElement(By.XPath($".//div[contains(text(),'La pelicula se ha creado correctamente')]"));
            Assert.True(sucessMessage != null);
            driver.Quit();
        }
    }
}
