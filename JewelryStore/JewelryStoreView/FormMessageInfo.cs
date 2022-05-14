using JewelryStoreBusinessLogic.MailWorker;
using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Windows.Forms;

namespace JewelryStoreView
{
    public partial class FormMessageInfo : Form
    {
        private readonly IMessageInfoLogic _messageLogic;
        private readonly IClientStorage _clientStorage;
        private readonly AbstractMailWorker _mailWorker;
        private string messageId;
        public string MessageId { set { messageId = value; } }

        public FormMessageInfo(IMessageInfoLogic messageLogic, IClientStorage clientStorage, AbstractMailWorker mailWorker)
        {
            InitializeComponent();
            _messageLogic = messageLogic;
            _clientStorage = clientStorage;
            _mailWorker = mailWorker;
        }

        private void FormMessageInfo_Load(object sender, EventArgs e)
        {
            if (messageId != null)
            {
                try
                {
                    MessageInfoViewModel mes = _messageLogic.Read(new MessageInfoBindingModel { MessageId = messageId })?[0];
                    if (mes != null)
                    {
                        if (!mes.Checked)
                        {
                            _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                            {
                                //ClientId = _clientLogic.Read(new ClientBindingModel { Login = mes.SenderName })?[0].Id,
                                MessageId = messageId,
                                FromMailAddress = mes.SenderName,
                                Subject = mes.Subject,
                                Body = mes.Body,
                                DateDelivery = mes.DateDelivery,
                                Checked = true,
                                ReplyText = mes.ReplyText
                            });
                        }
                        labelSenderName.Text = /*"Отправитель: " + */mes.SenderName;
                        labelDateDelivery.Text = /*"Дата письма: " + */mes.DateDelivery.ToString();
                        labelSubject.Text = /*"Заголовок: " +*/ mes.Subject;
                        labelBody.Text = /*"Текст: " +*/ mes.Body;
                        textBoxReplyText.Text = mes.ReplyText;
                        if (!string.IsNullOrEmpty(mes.ReplyText))
                        {
                            buttonReply.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonReply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxReplyText.Text))
            {
                MessageBox.Show("Введите текст для ответа на письмо", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                {
                    ClientId = _clientStorage.GetElement(new ClientBindingModel { Login = labelSenderName.Text })?.Id,
                    MessageId = messageId,
                    FromMailAddress = labelSenderName.Text,
                    Subject = labelSubject.Text,
                    Body = labelBody.Text,
                    DateDelivery = DateTime.Parse(labelDateDelivery.Text),
                    Checked = true,
                    ReplyText = textBoxReplyText.Text
                });

                _mailWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = labelSenderName.Text,
                    Subject = "Ответ: " + labelSubject.Text,
                    Text = textBoxReplyText.Text
                });

                
                MessageBox.Show("Ответ отправлен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
