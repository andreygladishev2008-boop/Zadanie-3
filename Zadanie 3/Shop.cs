using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_3
{
    public class Shop
    {
        // Поля и свойства из задания

        public string ShopName { get; set; }
        public int Sales { get; set; }
        public int Customers { get; set; }
        public double Money { get; set; }

        // Конструктор базового класса

        public Shop(string shop, int sales, double money , int customers)
        {
            ShopName = shop;
            Sales = sales;
            Money = money;
            Customers = customers;
        }

        // Функция определения качества объекта – Q по заданной формуле
        // Виртуальная (virtual), чтобы потомок мог её переопределить

        public virtual double GetQ()
        {
            // Формула: Q = выручка/ количество продаж за месяц
            return Money / Sales;
        }

        public virtual double GetQp(int customers)
        {
            Customers = customers;

            // Формула: Qp = 2 * Q
            if (customers > 50000) return 2 * GetQ();

            // Формула: Qp = 0.5 * Q
            else return 0.5 * GetQ();
        }

        // Вывод информации об объекте
        public virtual string GetInfo()
        {
            // Форматируем строку с помощью выравнивания для красивого отображения в ListBox
            return $"{ShopName,-8} | Продаж: {Sales,-12} | Покупателей: {Customers,-12} | Выручка: {Money,-12}руб. | Q: {GetQ(),7:F2} | Qp: {GetQp(Customers),7:F2}";
        }
    }
}
