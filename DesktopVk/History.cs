using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace DesktopVk
{
    public partial class History : Form
    {
        Autorize _account;
        Random random = new Random();
        long idSendMessage;
        //отправитель
        User us;

        public History(MessageGetHistoryObject dialog, Autorize account,long id)
        {
            InitializeComponent();
            _account = account;
            idSendMessage = id;
            foreach(var s in dialog.Messages)
            {
                us = _account.Api.Users.Get(new long[] { long.Parse(s.FromId.ToString()) }).FirstOrDefault();
                listBox1.Items.Add(s.Date+" "+us.FirstName);
                listBox1.Items.Add(s.Text);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentPost();

        }
        public void CurrentPost()
        {
            _account.Api.Messages.Send(new MessagesSendParams
            {
                RandomId = random.Next(1, 100),
                UserId = idSendMessage,
                Message = richTextBox2.Text
            });
            //us = _account.Api.Users.Get(new long[] { long.Parse(idSendMessage.ToString()) }).FirstOrDefault(); id поправить
            string messages = $"{richTextBox2.Text}";
            listBox1.Items.Add(DateTime.Now + " " + us.FirstName);
            listBox1.Items.Add(messages);
            
        }

    }
}
