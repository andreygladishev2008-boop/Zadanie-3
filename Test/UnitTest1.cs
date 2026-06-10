using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie_3;
using System;

namespace Test
{
    [TestClass]
    public class ShopTests
    {
        [TestMethod]
        public void GetQ_ValidData_CalculatesCorrectly()
        {
            // Arrange — Подготовка данных
            var shop = new Shop("Тест", 10, 500.0, 100);
            double expectedQ = 50.0; // 500 выручка / 10 продаж

            // Act — Выполнение действия
            double actualQ = shop.GetQ();

            // Assert — Проверка результата
            Assert.AreEqual(expectedQ, actualQ, 0.001);
        }

        [TestMethod]
        public void GetQp_CustomersMoreThan50000_ReturnsDoubleQ()
        {
            // Arrange — Подготовка данных
            var shop = new Shop("Тест", 10, 500.0, 100);
            double baseQ = 50.0;
            double expectedQp = 2 * baseQ; // 100.0

            // Act — Выполнение действия с 60000 покупателей
            double actualQp = shop.GetQp(60000);

            // Assert — Проверка результата
            Assert.AreEqual(expectedQp, actualQp, 0.001);
        }

        [TestMethod]
        public void GetQp_CustomersLessThan50000_ReturnsHalfQ()
        {
            // Arrange — Подготовка данных
            var shop = new Shop("Тест", 10, 500.0, 100);
            double baseQ = 50.0;
            double expectedQp = 0.5 * baseQ; // 25.0

            // Act — Выполнение действия с 20000 покупателей
            double actualQp = shop.GetQp(20000);

            // Assert — Проверка результата
            Assert.AreEqual(expectedQp, actualQp, 0.001);
        }
    }
}
