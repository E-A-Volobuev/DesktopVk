using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using VkNet.Model.Attachments;
using System.IO;

namespace DesktopVk
{
    public class Autorize
    {
        public VkApi Api { get; set; }
        public User Person { get; set; }
        public ICollection<User> AllFriends { get; set; }

        public string Adress
        {
            get { return "C:\\Users\\usr\\Desktop\\фото.jpg"; }
        }


        public Autorize(string name, string password)
        {
            var services = new ServiceCollection();
            services.AddAudioBypass();
            Api = new VkApi(services);
            Api.Authorize(new ApiAuthParams
            {
                ApplicationId = 124563,
                Login = name,
                Password = password,
                Settings = Settings.All
            });
            Person = Api.Users.Get(new long[] { long.Parse(Api.UserId.ToString()) }, ProfileFields.All).FirstOrDefault();
            AllFriends= Api.Friends.Get(new FriendsGetParams { UserId = Api.UserId, Fields = ProfileFields.FirstName | ProfileFields.LastName });

            Download();


        }
        
        public void Download()
        {
            WebClient web = new WebClient();
            var photo = Person.PhotoMaxOrig;
            web.DownloadFile(photo, Adress);
        }


    }
}
