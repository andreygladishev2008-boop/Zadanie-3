using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_3
{
    public class ShopD : Shop
    {
        // Поля и свойства из задания

        public int Discount { get; set; }

        // Конструктор базового класса

        public ShopD(string shop, int sales, double money, int customers, int discount)
            : base(shop, sales, money, customers)
        {
            ShopName = shop;
            Sales = sales;
            Money = money;
            Customers = customers;
            Discount = discount;
        }

        // Функция определения качества объекта – Q по заданной формуле
        // Виртуальная (virtual), чтобы потомок мог её переопределить

        public override double GetQ()
        {
            return base.GetQ() - (base.GetQ() * (Discount / 100.0));
        }

        // Вывод информации об объекте
        public override string GetInfo()
        {
            // Форматируем строку с помощью выравнивания для красивого отображения в ListBox
            return base.GetInfo() + $" | Скидка: {Discount}%";
        }
    }
}
