namespace JewelryStoreContracts.BindingModels
{
    // Данные от клиента, для создания заказа
    public class CreateOrderBindingModel
    {
        public int JewelId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
