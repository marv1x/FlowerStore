using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerStore;

namespace FlowerStore
{
    // Расширяем существующий класс Client
    public partial class Client
    {
        // Добавляем свойство для полного имени
        public string FullName => $"{FirstName} {LastName}";
    }
}

