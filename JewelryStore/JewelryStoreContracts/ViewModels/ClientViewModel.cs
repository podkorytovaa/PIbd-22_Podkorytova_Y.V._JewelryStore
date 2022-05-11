using JewelryStoreContracts.Attributes;

namespace JewelryStoreContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 60)]
        public int Id { get; set; }

        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [Column(title: "Логин", width: 150)]
        public string Login { get; set; }

        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
