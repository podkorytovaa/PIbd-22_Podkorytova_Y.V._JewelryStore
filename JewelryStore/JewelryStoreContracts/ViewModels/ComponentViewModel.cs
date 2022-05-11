using JewelryStoreContracts.Attributes;

namespace JewelryStoreContracts.ViewModels
{
    // Компонент, требуемый для изготовления изделия (драгоценности)
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 60)]
        public int Id { get; set; }

        [Column(title: "Компонент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
