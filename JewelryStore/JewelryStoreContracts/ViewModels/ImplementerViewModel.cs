using JewelryStoreContracts.Attributes;

namespace JewelryStoreContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 60)]
        public int Id { get; set; }

        [Column(title: "Исполнитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Время на заказ", width: 80)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 80)]
        public int PauseTime { get; set; }
    }
}
