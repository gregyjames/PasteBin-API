using System;
using PasteBin_API;

namespace PasteBinTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var core = new PasteBinCore("DEVAPIKEY");
            var user = new PasteBinCore.User("USERNAME", "PASSWORD", core);

            //Gets the 10 most recent posts for user
            var postes = user.getUserPosts(10);

            //Displays title of first 2 posts
            Console.WriteLine(postes.Paste[0].Paste_title);
            Console.WriteLine(postes.Paste[1].Paste_title);

            Console.ReadLine();
        }
    }
}
