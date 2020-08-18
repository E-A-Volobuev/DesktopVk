using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace DesktopVk
{
    public partial class Form2 : Form
    {

        Autorize account;
        //Выбранный пользователь
        User SelectUser;
 
        //для изменения масштаба картинок
        Bitmap bitt;
        public Form2(Autorize aut)
        {
            InitializeComponent();
            account = aut;
            Post();
            label1.Text = $"{account.Person.FirstName} {account.Person.LastName}";
            pictureBox1.Image = SizePhoto(account.Adress);

            foreach( User friend in account.AllFriends)
            {
                listBox1.Items.Add(friend.FirstName + " " + friend.LastName);
            }
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }
       
        //сведения о выделенном объекте
        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// получаем id выделенного объекта
            int index = (int)listBox1.SelectedIndex;
            SelectUser = account.AllFriends.ElementAt(index);

        }
        //уменьшение аватарки для иконки в форме
        public Bitmap SizePhoto(string adress)
        {
            System.Drawing.Image photo = System.Drawing.Image.FromFile(adress);
            Size size = new Size(100, 100);
            bitt = new Bitmap(photo, size);
            return bitt;
        }


        //увеличение картинки при клике
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Size size = new Size(500, 500);
            System.Drawing.Image pic = System.Drawing.Image.FromFile(account.Adress);
            bitt = new Bitmap(pic,size);
            FormPhoto photo = new FormPhoto((Bitmap)pic);
            photo.ShowDialog();
        }
        //диалоги
        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = account.Api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                UserId = SelectUser.Id,
                Count = 20,

            });
            
            History history = new History(dialog, account, SelectUser.Id);
            history.ShowDialog();
        }
        //входящие письма
        public void Post()
        {
            var messGets = account.Api.Messages.GetConversations(new GetConversationsParams
            {
                Count = 5,
                Filter = GetConversationFilter.All
            });

            foreach (var mes in messGets.Items)
            {
                User us = account.Api.Users.Get(new long[] { long.Parse(mes.LastMessage.FromId.ToString()) }, ProfileFields.All).FirstOrDefault();
                listBox2.Items.Add(mes.LastMessage.Date);
                listBox2.Items.Add(us.FirstName + " " + us.LastName+":");
                listBox2.Items.Add(mes.LastMessage.Text);
            }
        }

       
    }
}
