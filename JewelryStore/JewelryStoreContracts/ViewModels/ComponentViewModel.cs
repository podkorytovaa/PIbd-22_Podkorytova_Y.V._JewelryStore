using System.ComponentModel;

namespace JewelryStoreContracts.ViewModels
{
    // Компонент, требуемый для изготовления изделия (драгоценности)
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }
    }
}
