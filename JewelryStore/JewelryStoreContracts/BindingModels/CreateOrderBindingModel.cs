using System.Runtime.Serialization;

namespace JewelryStoreContracts.BindingModels
{
    // Данные от клиента, для создания заказа
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int JewelId { get; set; }

        [DataMember] 
        public int Count { get; set; }

        [DataMember] 
        public decimal Sum { get; set; }
    }
}
