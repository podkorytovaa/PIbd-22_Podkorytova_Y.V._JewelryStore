using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace JewelryStoreView
{
    public partial class FormWarehouse : Form
    {
        public int Id { set { id = value; } }
        private readonly IWarehouseLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> warehouseComponents;
        public FormWarehouse(IWarehouseLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WarehouseViewModel view = _logic.Read(new WarehouseBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.WarehouseName;
                        textBoxResponsibleFullName.Text = view.ResponsibleFullName.ToString();
                        warehouseComponents = view.WarehouseComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                warehouseComponents = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (warehouseComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var wc in warehouseComponents)
                    {
                        dataGridView.Rows.Add(new object[] { wc.Key, wc.Value.Item1, wc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxResponsibleFullName.Text))
            {
                MessageBox.Show("Заполните ФИО ответственного", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new WarehouseBindingModel
                {
                    Id = id,
                    WarehouseName = textBoxName.Text,
                    ResponsibleFullName = textBoxResponsibleFullName.Text,
                    WarehouseComponents = warehouseComponents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
