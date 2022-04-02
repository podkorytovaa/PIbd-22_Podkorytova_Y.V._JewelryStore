namespace JewelryStoreContracts.BindingModels
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string ClientFIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
