using Dapper;
using MatchActors.Contracts;
using MatchActors.Models;
using Newtonsoft.Json;
using Npgsql;

namespace MatchActors.Services;

public class MovieSearchService : IMovieSearchService
{
    public async Task<MatchActorsResponse> MovieSearch(MatchActorsRequest request, CancellationToken token)
    {
        var result = new List<string>();

        var key = "k_msuvty8y";

        var cs = @"Server=127.0.0.1:5432;Database=postgres;User ID=admin;Password=admin";

        using var con = new NpgsqlConnection(cs);

        con.Open();
        var q1 = "select actor_id from actors where name='" + request.Actor1 + "';";
        var q2 = "select actor_id from actors where name='" + request.Actor2 + "';";

        var val1 = con.ExecuteScalar<string>(q1);
        var val2 = con.ExecuteScalar<string>(q2);

        var clnt = new HttpClient();

        if (val1 == null)
        {
            var c = await clnt.GetAsync("https://imdb-api.com/en/API/SearchName/" + key + "/"+ request.Actor1);
            var res = await c.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<Data>(res);

            val1 = a.Results.FirstOrDefault(t => request.Actor1 == t.Title)?.Id;
        }

        if (val2 == null)
        {
            var c = await clnt.GetAsync("https://imdb-api.com/en/API/SearchName/" + key + "/" + request.Actor2);
            var res = await c.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<Data>(res);

            val2 = a.Results.FirstOrDefault(t => request.Actor2 == t.Title)?.Id;
        }

        if (val1 != null && val2 != null)
        {
            var m1 = await clnt.GetAsync("https://imdb-api.com/en/API/Name/" + key + "/" + val1);
            var res1 = await m1.Content.ReadAsStringAsync();
            var movs1 = JsonConvert.DeserializeObject<ActorData>(res1).CastMovies;

            var m2 = await clnt.GetAsync("https://imdb-api.com/en/API/Name/" + key + "/" + val2);
            var res2 = await m2.Content.ReadAsStringAsync();
            var movs2 = JsonConvert.DeserializeObject<ActorData>(res2).CastMovies;

            // фильтр MoviesOnly
            //if (request.MoviesOnly == true)
            //{
            //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
            //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
            //}

            foreach (var movies1 in movs1)
            {
                foreach (var movies2 in movs2)
                {
                    if (movies1.Id == movies2.Id)
                    {
                        result.Add(movies1.Title);
                    }
                }
            }
        }

        return new MatchActorsResponse { Movies = result };
    }
}
