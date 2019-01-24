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


namespace InstaPublicacoes
{
    public class TodasASPublicacoes
    {
        public async void TotalDePublicacoes(string username, IInstaApi api)
        {
            IResult<InstaUser> userSearch = await api.GetUserAsync(username);
            Console.WriteLine($"USER:{userSearch.Value.FullName}\n\tFollowers: {userSearch.Value.FollowersCount}\n\t {userSearch.Value.IsVerified}");

            IResult<InstaMediaList> media = await api.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(5));
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



