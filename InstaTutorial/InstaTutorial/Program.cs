using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharper;


namespace InstaTutorial
{
    class Program
    {

        #region Hidden
        private const string username = "ferreira_gabriel1996";
        private const string password = "498776498776";
        #endregion

        private static UserSessionData user;
        private static IInstaApi api;


        static void Main(string[] args)
        {
            user = new UserSessionData();
            user.UserName = username;
            user.Password = password;

            Login();
            Console.Read();
        }
        public static async void Login()
        {
            api = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                //.SetRequestDelay(TimeSpan.FromSeconds(8))
                .Build();


            var loginRequest = await api.LoginAsync();
            if (loginRequest.Succeeded)
            {
                Console.WriteLine("logged in");
                PullUserPosts("ferreira_gabriel1996");
            }
            else
                Console.WriteLine("error logging in \n" + loginRequest.Info.Message);
        }
        public static async void PullUserPosts(string userToScrape)
        {
            IResult<InstaUser> userSearch = await api.GetUserAsync(userToScrape);
            Console.WriteLine($"USER:{userSearch.Value.FullName}\n\tFollowers: {userSearch.Value.FollowersCount}\n\t {userSearch.Value.IsVerified}");

            IResult<InstaMediaList> media = await api.GetUserMediaAsync(userToScrape, PaginationParameters.MaxPagesToLoad(5));
            List<InstaMedia> mediaList = mediaList = media.Value.ToList();

            for (int i = 0; i < mediaList.Count; i++)
            {
                InstaMedia m = mediaList[i];
                if (m != null && m.Caption != null)
                {
                    string captionText = m.Caption.Text;
                    if (captionText != null)
                    {
                        if (m.MediaType == InstaMediaType.Image)
                        {
                            for (int X = 0; X < m.Images.Count; X++)
                            {
                                if (m.Images[X] != null && m.Images[X].URI != null)
                                {
                                    Console.WriteLine($"\n\t{captionText}");
                                    string uri = m.Images[X].URI;

                                    Console.Write($"{uri}\n\t");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}