using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasteBin_API
{
    public class PasteBinCore
    {
        public string APIKEY = "";

        public class User
        {
            public string _username = "";
            public string _password = "";
            public string _userKey  = "";
            private PasteBinCore _core;

            public User(string username, string password, PasteBinCore core)
            {
                _username = username;
                _password = password;
                _core = core;
                try {
                    _userKey = getUserKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public string getUserKey()
            {
                using (var client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add("api_dev_key", _core.APIKEY);
                    data.Add("api_user_name", _username);
                    data.Add("api_user_password", _password);
                    try {
                        var responce = client.UploadValues(new Uri("https://pastebin.com/api/api_login.php"), data);
                        return Encoding.ASCII.GetString(responce);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }

            public Pastes getUserPosts(int maxResults)
            {
                using (var client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add("api_dev_key", _core.APIKEY);
                    data.Add("api_user_key", getUserKey());
                    data.Add("api_option", "list");
                    data.Add("api_results_limit", maxResults.ToString());
                    try {
                        Parallel.Invoke(() => {
                            var responce = client.UploadValues(new Uri("https://pastebin.com/api/api_post.php"), data);

                            var s = "<Pastes>\n" + Encoding.ASCII.GetString(responce) + "</Pastes>";
                            File.WriteAllText("Pastes.xml", s);
                        });

                        TextReader reader = new StreamReader(@"Pastes.xml");

                        XmlSerializer deserializer = new XmlSerializer(typeof(Pastes));
                        object obj = deserializer.Deserialize(reader);
                        Pastes XmlData = (Pastes)obj;

                        return XmlData;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }

            public void DeleteUserPost(string PasteKey, User currentUser)
            {
                using (var client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add("api_option", "delete");
                    data.Add("api_paste_key", PasteKey);
                    data.Add("api_dev_key", _core.APIKEY);
                    data.Add("api_user_key", getUserKey());
                    try
                    {
                        client.UploadValuesAsync(new Uri("https://pastebin.com/api/api_post.php"), data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    client.UploadValuesCompleted += (sender, args) => {
                        Console.WriteLine(Encoding.ASCII.GetString(args.Result));
                    };
                }
            }
        }

        public PasteBinCore(string APIKey)
        {
            APIKEY = APIKey;
        }

        public void PostAnnon(string PostName, string PostText)
        {
            using(var client = new WebClient())
            {
                var data = new NameValueCollection();
                data.Add("api_option", "paste");
                data.Add("api_paste_name", PostName);
                data.Add("api_dev_key", APIKEY);
                data.Add("api_paste_code", PostText);
                try {
                    client.UploadValuesAsync(new Uri("https://pastebin.com/api/api_post.php"), data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                client.UploadValuesCompleted += (sender, args) => {
                    Console.WriteLine(Encoding.ASCII.GetString(args.Result));
                };
            }
        }

        public void PostAsUser(string PostName, string PostText, User currentUser)
        {
            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                data.Add("api_option", "paste");
                data.Add("api_paste_name", PostName);
                data.Add("api_dev_key", APIKEY);
                data.Add("api_paste_code", PostText);
                data.Add("api_user_key", currentUser._userKey);
                try {
                    client.UploadValuesAsync(new Uri("https://pastebin.com/api/api_post.php"), data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                client.UploadValuesCompleted += (sender, args) => {
                    Console.WriteLine(Encoding.ASCII.GetString(args.Result));
                };
            }
        }
    }
}
