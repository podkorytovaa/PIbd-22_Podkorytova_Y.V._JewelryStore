using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace JewelryStoreView
{
    public partial class FormMessagesInfo : Form
    {
        private readonly IMessageInfoLogic _logic;
        private bool isNext = false;
        private readonly int messagesOnPage = 3;
        private int currentPage = 0;

        public FormMessagesInfo(IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormMessagesInfo_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadData()
        {
            var list = _logic.Read(new MessageInfoBindingModel
            {
                ToSkip = currentPage * messagesOnPage,
                ToTake = messagesOnPage + 1
            });
            isNext = !(list.Count() <= messagesOnPage);
            if (isNext)
            {
                buttonNext.Enabled = true;
            }
            else
            {
                buttonNext.Enabled = false;
            }
            if (currentPage == 0)
            {
                buttonBack.Enabled = false;
            }
            if (list != null)
            {
                dataGridView.DataSource = list.Take(messagesOnPage).ToList();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                labelPage.Text = "Страница " + (currentPage + 1).ToString();
                buttonNext.Enabled = true;
                LoadData();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (isNext)
            {
                currentPage++;
                labelPage.Text = "Страница " + (currentPage + 1).ToString();
                buttonBack.Enabled = true;
                LoadData();
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormMessageInfo>();
                form.MessageId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                form.ShowDialog();
                LoadData();
            }
        }
    }
}
