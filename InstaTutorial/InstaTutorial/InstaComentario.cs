using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using InstaTutorial;

namespace InstaTutorial
{
    public class InstaComentario
    {
        public static async void XXXXXXXXXX(string username, IInstaApi api)
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

            var maisCurtidasOrdenado = MaisCurtidas.OrderByDescending(q => q.curtidas);
            foreach (var item in maisCurtidasOrdenado)
            {
                Console.WriteLine("{0}  ---  {1}", item.nome, item.curtidas);
            }

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







