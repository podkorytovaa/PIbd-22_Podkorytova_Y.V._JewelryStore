using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStoreListImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
