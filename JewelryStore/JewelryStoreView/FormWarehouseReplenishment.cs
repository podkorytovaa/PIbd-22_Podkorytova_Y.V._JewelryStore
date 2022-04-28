using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JewelryStoreView
{
    public partial class FormWarehouseReplenishment : Form
    {
        public int WarehouseId
        {
            get { return Convert.ToInt32(comboBoxWarehouse.SelectedValue); }
            set { comboBoxWarehouse.SelectedValue = value; }
        }

        public int ComponentId
        {
            get
            {
                return Convert.ToInt32(comboBoxComponent.SelectedValue);
            }
            set
            {
                comboBoxComponent.SelectedValue = value;
            }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        private readonly IWarehouseLogic _warehouseLogic;

        public FormWarehouseReplenishment(IWarehouseLogic warehouseLogic, IComponentLogic componentLogic)
        {
            InitializeComponent();
            _warehouseLogic = warehouseLogic;

            List<ComponentViewModel> listComponent = componentLogic.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }

            List<WarehouseViewModel> listWarehouse = warehouseLogic.Read(null);
            if (listWarehouse != null)
            {
                comboBoxWarehouse.DisplayMember = "WarehouseName";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = listWarehouse;
                comboBoxWarehouse.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _warehouseLogic.AddComponent(new WarehouseBindingModel { Id = WarehouseId }, ComponentId, Count);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
