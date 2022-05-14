namespace JewelryStoreView
{
    partial class FormMessageInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSenderName = new System.Windows.Forms.Label();
            this.labelDateDelivery = new System.Windows.Forms.Label();
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelBody = new System.Windows.Forms.Label();
            this.textBoxReplyText = new System.Windows.Forms.TextBox();
            this.buttonReply = new System.Windows.Forms.Button();
            this.labelReplyText = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSenderName
            // 
            this.labelSenderName.AutoSize = true;
            this.labelSenderName.Location = new System.Drawing.Point(15, 15);
            this.labelSenderName.Name = "labelSenderName";
            this.labelSenderName.Size = new System.Drawing.Size(0, 20);
            this.labelSenderName.TabIndex = 0;
            // 
            // labelDateDelivery
            // 
            this.labelDateDelivery.AutoSize = true;
            this.labelDateDelivery.Location = new System.Drawing.Point(15, 55);
            this.labelDateDelivery.Name = "labelDateDelivery";
            this.labelDateDelivery.Size = new System.Drawing.Size(0, 20);
            this.labelDateDelivery.TabIndex = 1;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(15, 95);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(0, 20);
            this.labelSubject.TabIndex = 2;
            // 
            // labelBody
            // 
            this.labelBody.AutoSize = true;
            this.labelBody.Location = new System.Drawing.Point(15, 135);
            this.labelBody.Name = "labelBody";
            this.labelBody.Size = new System.Drawing.Size(0, 20);
            this.labelBody.TabIndex = 3;
            // 
            // textBoxReplyText
            // 
            this.textBoxReplyText.Location = new System.Drawing.Point(70, 298);
            this.textBoxReplyText.Name = "textBoxReplyText";
            this.textBoxReplyText.Size = new System.Drawing.Size(450, 27);
            this.textBoxReplyText.TabIndex = 4;
            // 
            // buttonReply
            // 
            this.buttonReply.Location = new System.Drawing.Point(300, 360);
            this.buttonReply.Name = "buttonReply";
            this.buttonReply.Size = new System.Drawing.Size(100, 30);
            this.buttonReply.TabIndex = 5;
            this.buttonReply.Text = "Ответить";
            this.buttonReply.UseVisualStyleBackColor = true;
            this.buttonReply.Click += new System.EventHandler(this.buttonReply_Click);
            // 
            // labelReplyText
            // 
            this.labelReplyText.AutoSize = true;
            this.labelReplyText.Location = new System.Drawing.Point(15, 300);
            this.labelReplyText.Name = "labelReplyText";
            this.labelReplyText.Size = new System.Drawing.Size(55, 20);
            this.labelReplyText.TabIndex = 6;
            this.labelReplyText.Text = "Ответ: ";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(420, 360);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 30);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormMessageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 403);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelReplyText);
            this.Controls.Add(this.buttonReply);
            this.Controls.Add(this.textBoxReplyText);
            this.Controls.Add(this.labelBody);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.labelDateDelivery);
            this.Controls.Add(this.labelSenderName);
            this.Name = "FormMessageInfo";
            this.Text = "Письмо";
            this.Load += new System.EventHandler(this.FormMessageInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSenderName;
        private System.Windows.Forms.Label labelDateDelivery;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelBody;
        private System.Windows.Forms.TextBox textBoxReplyText;
        private System.Windows.Forms.Button buttonReply;
        private System.Windows.Forms.Label labelReplyText;
        private System.Windows.Forms.Button buttonCancel;
    }
}