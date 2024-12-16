using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerStore.Class
{
    internal class ControlClass
    {
        public static bool AddOrder(int productIndex, int workerIndex, int clientIndex, string quantityText, string costText)
        {
            // Проверка на незаполненные поля
            if (productIndex == -1 || workerIndex == -1 || clientIndex == -1)
                return false;

            // Проверка на корректность числовых значений
            if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
                return false;

            if (!decimal.TryParse(costText, out decimal cost) || cost <= 0)
                return false;

            // Здесь логика добавления заказа в БД или коллекцию
            return true;
        }
    }
}
