
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Grocery.Core.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        // Interne lijst van clients 
        private readonly List<Client> _clientList;

        public ClientRepository()
        {
            _clientList = new List<Client>
            {
                new Client(1, "M.J. Curie", "user1@mail.com", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08="),
                new Client(2, "H.H. Hermans", "user2@mail.com", "dOk+X+wt+MA9uIniRGKDFg==.QLvy72hdG8nWj1FyL75KoKeu4DUgu5B/HAHqTD2UFLU="),
                new Client(3, "A.J. Kwak", "user3@mail.com", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")
            };

            // Stel de rol van de admin-client in
            var admin = _clientList.FirstOrDefault(c => c.Id == 3);
            if (admin != null) admin.Role = Role.Admin;
        }

        public Client? Get(string email)
        {
            return _clientList.FirstOrDefault(c => c.EmailAddress.Equals(email));
        }

        public Client? Get(int id)
        {
            return _clientList.FirstOrDefault(c => c.Id == id);
        }

        public List<Client> GetAll()
        {
            return _clientList;
        }
    }
}

