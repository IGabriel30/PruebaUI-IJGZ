using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;


namespace PruebaUIIJGZ
{
    [TestClass]
    public class ProductSearchTest
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            // Inicializa el controlador de Chrome
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestMethod]
        public void SearchProductApp()
        {
           
            // Navega a la página de búsqueda de productos
            driver.Navigate().GoToUrl("https://localhost:7252/ProductIJGZ");
            // Espera a que la aplicación carge completamente
            System.Threading.Thread.Sleep(2000); // Espera 2 segundos

    
            // Encuentra el cuadro de búsqueda
            var searchBox = driver.FindElement(By.Name("Nombre_Like"));
            // Ingresa el término de búsqueda
            searchBox.SendKeys("Mesa");

            // Envía la búsqueda
            searchBox.Submit();
            // Espera a que se carguen los resultados (puedes ajustar esto)
            System.Threading.Thread.Sleep(2000);
            // Verifica que los resultados contengan el producto buscado

            var searchResults = driver.FindElements(By.CssSelector("tbody tr"));

            Assert.IsTrue(searchResults.Count > 0, "No se encontraron productos en los resultados de la búsqueda.");

            // Verifica que al menos uno de los resultados contenga el término buscado
            bool productFound = false;
            foreach (var result in searchResults)
            {
                // Busca en la celda correspondiente al nombre del producto
                var productNameCell = result.FindElement(By.XPath("./td[1]")); // Obtener el primer <td>
                if (productNameCell.Text.Contains("Mesa")) // 
                {
                    productFound = true;
                    break;
                }
            }

            Assert.IsTrue(productFound, "El producto buscado no se encontró en los resultados.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cierra el navegador
            driver.Quit();
        }
    }
}
