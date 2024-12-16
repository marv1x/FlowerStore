using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerStore.Class
{
    internal class UprClass
    {
    //    public class ClientManager
    //    {
    //        private readonly KursovoiEntities1 _context;

    //        // Конструктор, принимающий контекст базы данных
    //        public ClientManager(KursovoiEntities1 context)
    //        {
    //            _context = context;
    //        }

    //        // Метод для получения всех клиентов
    //        public List<Client> GetAllClients()
    //        {
    //            return _context.Client.ToList();
    //        }

    //        // Метод для поиска клиента по ID
    //        public Client GetClientById(int id)
    //        {
    //            return _context.Client.FirstOrDefault(c => c.ID == id);
    //        }

    //        // Метод для добавления нового клиента
    //        public void AddClient(Client client)
    //        {
    //            if (client == null) throw new ArgumentNullException(nameof(client));

    //            _context.Client.Add(client);
    //            _context.SaveChanges();
    //        }

    //        // Метод для обновления существующего клиента
    //        public void UpdateClient(Client client)
    //        {
    //            if (client == null) throw new ArgumentNullException(nameof(client));

    //            var existingClient = _context.Client.FirstOrDefault(c => c.ID == client.ID);
    //            if (existingClient != null)
    //            {
    //                existingClient.FirstName = client.FirstName;
    //                existingClient.LastName = client.LastName;
    //                existingClient.PhoneNumber = client.PhoneNumber;

    //                _context.SaveChanges();
    //            }
    //        }

    //        // Метод для удаления клиента
    //        public void DeleteClient(int id)
    //        {
    //            var client = _context.Client.FirstOrDefault(c => c.ID == id);
    //            if (client != null)
    //            {
    //                _context.Client.Remove(client);
    //                _context.SaveChanges();
    //            }
    //        }
    //    }
    }
    }
