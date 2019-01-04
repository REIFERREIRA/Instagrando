using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaTeste
{
    class Program
    {
        private const string username = "";
        private const string password = "";
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
            api = InstaApiBuilder.CreateBuilder().SetUser(user)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    //.SetRequestDelay(TimeSpan.FromSeconds(8))
                    .Build();

            var loginRequest = await api.LoginAsync();

            if (loginRequest.Succeeded)
            {
                Console.WriteLine("Logado");
                //DesseguirQuemEuSigoENaoMeSegue(username);
                //BloqueiaDesbloqueia(username);
            }
            else
            {
                Console.WriteLine("Erro: {0}", loginRequest.Info.Message);
            }
        }
        public static async void MaisCurtidasNasFotos(string username)
        {

        }
        
        //public static async void DesseguirQuemEuSigoENaoMeSegue(string username)
        //{


        //    var quemMeSegue = await api.GetUserFollowersAsync(username, PaginationParameters.MaxPagesToLoad(5));
        //    var quemEuSigo = await api.GetUserFollowingAsync(username, PaginationParameters.MaxPagesToLoad(5));

        //    foreach (var carinhaQueToSeguindo in quemEuSigo.Value.ToList())
        //    {
        //        var xx = quemMeSegue.Value.Any(q => q.Pk == carinhaQueToSeguindo.Pk);
        //        if (!xx)
        //        {
        //            await api.UnFollowUserAsync(carinhaQueToSeguindo.Pk);
        //        }
        //    }
        //}

        //public static async void curtifoto(string username)
        //{
        //    var viuMinhaUltimaFoto = await api.
        //    var ultimaFoto = await api.SendDirectMessage
        //}
        //public static async void BloqueiaDesbloqueia(string username)
        //{
        //    var quemMeSegue = await api.GetUserFollowersAsync(username, PaginationParameters.MaxPagesToLoad(5));
        //    List<Usuarios> usuario = new List<Usuarios>();
        //    foreach (var carinhaQueMeSegue in quemMeSegue.Value.ToList())
        //    {

        //        var todasAsPostagensDoNego = await api.GetUserMediaAsync(carinhaQueMeSegue.UserName, PaginationParameters.MaxPagesToLoad(5));
        //        var quantidadeDePosts = todasAsPostagensDoNego.Value.Count;




        //        var asudhiusahd = await api.

        //        var idDoCara = carinhaQueMeSegue.Pk;

        //        var quemSegueOCarinha = await api.GetUserFollowersAsync(carinhaQueMeSegue.UserName, PaginationParameters.MaxPagesToLoad(5));
        //        var quantidadeDeNegoQueSegueOCarinha = quemSegueOCarinha.Value.Count;

        //        var quantidadeCurtidaQueOCaraTemQueTer = quantidadeDeNegoQueSegueOCarinha * 0.14;
        //        var quantidadeCurtidasNasUltimas5Fotos = 0;

        //        for (int i = 0; i < 5; i++)
        //        {
        //            quantidadeCurtidasNasUltimas5Fotos += todasAsPostagensDoNego.Value[i].LikesCount;
        //        }




        //        if (quantidadeCurtidasNasUltimas5Fotos < quantidadeCurtidaQueOCaraTemQueTer + 50)
        //        {


        //        }



        //        if(quantidadeDePosts == 0 || carinhaQueMeSegue.ProfilePictureId == null || )
        //        {
        //            await api.BlockUserAsync(idDoCara);
        //            await api.UnBlockUserAsync(idDoCara);
        //        }

        //    }
        //}

    }

}

