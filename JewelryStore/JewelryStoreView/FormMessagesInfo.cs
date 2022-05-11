﻿using JewelryStoreContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JewelryStoreView
{
    public partial class FormMessagesInfo : Form
    {
        private readonly IMessageInfoLogic _logic;

        public FormMessagesInfo(IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormMessagesInfo_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConfigGrid(_logic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
