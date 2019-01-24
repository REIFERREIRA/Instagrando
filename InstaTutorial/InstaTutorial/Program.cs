using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using InstaTutorial;
using System.Threading.Tasks;
using InstaPublicacoes;

namespace InstaTeste
{
    public class Program
    {
        #region Hidden
        private const string username = "bielvfa";
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

          

            api = InstaApiBuilder.CreateBuilder().SetUser(user)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    //.SetRequestDelay(TimeSpan.FromSeconds(8))
                    .Build();

            var loginRequest = await api.LoginAsync();

            if (loginRequest.Succeeded)
            {
                Console.WriteLine("Logado");

                //TodasASPublicacoes TotalPublicacoes = new TodasASPublicacoes();
                //TotalPublicacoes.TotalDePublicacoes(username, api);
                //InstaComentario ComentariosInstagram = new InstaComentario();
                //ComentariosInstagram.Comentarios(username, api);

                //DesseguirQuemEuSigoENaoMeSegue(username);
                //BloqueiaDesbloqueia(username);
                //await api.GetUserInfoByUsernameAsync("duduaudsonn");
                //var jesus = await api.GetUserInfoByUsernameAsync("duduaudsonn");
                //var jesus = await api.GetUserStoryFeedAsync(2200085541);
                //var jesus1 = await api.GetUserMediaAsync("ferreira_gabriel1996", PaginationParameters.MaxPagesToLoad(8));
                //List<MaisCurtidas> MaisCurtidas = await MaisCurtidasNasFotos("bielvfa");
                List<Seguidores> seguidores= await QUEMMESEGUE("bielvfa");
                Comentario(seguidores);
                //List<Seguidores> CemUltimos = await PessoasQueNaoMeSeguem();
                //PessoasNaoRelevantes(CemUltimos, MaisCurtidas); 

            }
            else
            {
                Console.WriteLine("Erro: {0}", loginRequest.Info.Message);
            }
        }

        public static async Task<List<MaisCurtidas>> MaisCurtidasNasFotos(string username)
            
        {
            List<MaisCurtidas> MaisCurtidas = new List<MaisCurtidas>();
            List<MaisCurtidas> MaisCurtidasAuxiliar = new List<MaisCurtidas>();
            var publicacoes = await api.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(8));
            foreach (var publicacao in publicacoes.Value.ToList())
            {
                var chaveprimaria = publicacao.Pk;
                var foto = await api.GetMediaLikersAsync(chaveprimaria);
                foreach (var curtida in foto.Value.ToList())
                {
                    if (MaisCurtidas?.Any() != false)
                    {
                        //var teucu = MaisCurtidas.Where(q => q.nome == curtida.FullName); armazena a variavel no teucu
                        if (MaisCurtidas.Any(q => q.nome == curtida.UserName))
                        {
                            MaisCurtidas.FindAll(q => q.nome == curtida.UserName).ForEach(q => q.curtidas = q.curtidas + 1);

                        }
                        else
                        {
                            MaisCurtidas c1 = new MaisCurtidas();
                            c1.curtidas++;
                            c1.nome = curtida.UserName;
                            MaisCurtidasAuxiliar.Add(c1);
                        }
                    }
                    else
                    {
                        MaisCurtidas c = new MaisCurtidas();
                        c.nome = curtida.UserName;
                        c.curtidas = 1;
                        MaisCurtidas.Add(c);
                    }
                }
                foreach (var item in MaisCurtidasAuxiliar)
                {
                    MaisCurtidas.Add(item);
                }
                MaisCurtidasAuxiliar.Clear();
            }

            List<MaisCurtidas> maisCurtidasOrdenado = MaisCurtidas.OrderByDescending(q => q.curtidas).ToList();
            foreach (var item in maisCurtidasOrdenado)
            {
                
                //Console.WriteLine("{0}  ---  {1}", item.nome, item.curtidas);
            }

            return maisCurtidasOrdenado;
        }
        public static async Task<List<Seguidores>> QUEMMESEGUE(string username)
        {
            List<Seguidores> Seguidores = new List<Seguidores>();
            var segui = await api.GetCurrentUserFollowersAsync(PaginationParameters.MaxPagesToLoad(8));
           
            foreach (var item in segui.Value.ToList())
            {
                Seguidores s = new Seguidores();
                s.meseguem = item.UserName;
                Seguidores.Add(s);
                
                
            }
            foreach(var item in Seguidores)
            {
               // Console.WriteLine(item.meseguem);
            }
            return Seguidores;
        }
        public static async Task<List<Seguidores>> PessoasQueNaoMeSeguem()
        {
            List<Seguidores> Seguidores = new List<Seguidores>();
            var segui = await api.GetCurrentUserFollowersAsync(PaginationParameters.MaxPagesToLoad(8));
            foreach (var item in segui.Value.ToList())
            {
                Seguidores s = new Seguidores();
                s.meseguem = item.UserName;
                s.id = item.Pk;
                Seguidores.Add(s);
                
            }
            //var xx = Seguidores.Take(100);
            List<Seguidores> ultimosCem = Seguidores.Skip(Math.Max(0, Seguidores.Count() - 400)).ToList();

            List<Seguidores> CemUltimos = new List<Seguidores>();
            foreach (var item in ultimosCem)
            {
  
                CemUltimos.Add(item);
               // Console.WriteLine(item.meseguem);
            }
            return ultimosCem;
            
        }

        public static async void PessoasNaoRelevantes(List<Seguidores> CemUltimos, List<MaisCurtidas> MaisCurtidas)
        {
            List<Seguidores> NaoImportantes = new List<Seguidores>();
            //var teucu = MaisCurtidas.Where(q => q.nome == curtida.FullName); armazena a variavel no teucu
            foreach (var item in CemUltimos)
            {
                if (MaisCurtidas.Any(q => q.nome == item.meseguem) == false)
                {
                    Seguidores curtida = new Seguidores();
                    curtida.meseguem = item.meseguem;
                    curtida.id = item.id;


                    //await api.BlockUserAsync(item.id);
                   // await api.UnBlockUserAsync(item.id);


                    NaoImportantes.Add(curtida);
                    //Console.WriteLine("{0}  ---  {1}", item.id, item.meseguem);


                }
            }
           
        }

        public static async void Comentario(List<Seguidores> seguidores)
        {
            var x1 = "ab";
            var x2= "ab";
            var x3 = "ab";
            List<Seguidores> TresNomes = new List<Seguidores>();
            int cont = 0;
            foreach ( var item in seguidores)
            {
                //var resto = cont % 3;
                if( cont == 0)
                {
                Seguidores seguir = new Seguidores();
                seguir.meseguem = item.meseguem;
                 x1 = seguir.meseguem;
                    
                    
                //var Comentario = await api.CommentMediaAsync("1864758539671613875", "@" + TresNomes);
                }
                
                if (cont == 1)
                {
                    Seguidores seguir = new Seguidores();
                    seguir.meseguem = item.meseguem;
                    x2 = seguir.meseguem;
                    
                    //var Comentario = await api.CommentMediaAsync("1864758539671613875", "@" + TresNomes);
                }
                
                if (cont == 2)
                {
                    Seguidores seguir = new Seguidores();
                    seguir.meseguem = item.meseguem;
                    x3 = seguir.meseguem;
                    Console.WriteLine("o primeiro" + x1 + "o segundo" + x2 + "o terceiro" + x3);
                    cont = -1;
                    var Comentario = await api.CommentMediaAsync("1864758539671613875", "@" + x1 +"  " + "@" + x2 + "  " + "@" + x3);
                }

               
                cont = cont + 1;
            }

           
            

        }

        //var xx = MaisCurtidas.Where(q => q.curtidas > 2);


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



