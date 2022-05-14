﻿using System;

namespace JewelryStoreFileImplement.Models
{
    public class MessageInfo
    {
        public string MessageId { get; set; }
        public int? ClientId { get; set; }
        public string SenderName { get; set; }
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Checked { get; set; }
        public string ReplyText { get; set; }
    }
}
