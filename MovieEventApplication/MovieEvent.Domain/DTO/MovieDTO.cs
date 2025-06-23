using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DTO
{
    public class MovieDTO
    {
        [JsonPropertyName("#TITLE")]
        public string TITLE { get; set; }

        [JsonPropertyName("#YEAR")]
        public int YEAR { get; set; }

        [JsonPropertyName("#IMDB_ID")]
        public string IMDB_ID { get; set; }

        [JsonPropertyName("#RANK")]
        public int RANK { get; set; }

        [JsonPropertyName("#ACTORS")]
        public string ACTORS { get; set; }

        [JsonPropertyName("#AKA")]
        public string AKA { get; set; }

        [JsonPropertyName("#IMDB_URL")]
        public string IMDB_URL { get; set; }

        [JsonPropertyName("#IMDB_IV")]
        public string IMDB_IV { get; set; }

        [JsonPropertyName("#IMG_POSTER")]
        public string IMG_POSTER { get; set; }

        [JsonPropertyName("photo_width")]
        public int photo_width { get; set; }

        [JsonPropertyName("photo_height")]
        public int photo_height { get; set; }
    }



    //"#TITLE": "Andor",
    // "#YEAR": 2022,
    // "#IMDB_ID": "tt9253284",
    // "#RANK": 19,
    // "#ACTORS": "Diego Luna, Denise Gough",
    // "#AKA": "Andor (2022) ",
    // "#IMDB_URL": "https://imdb.com/title/tt9253284",
    // "#IMDB_IV": "https://IMDb.iamidiotareyoutoo.com/title/tt9253284",
    // "#IMG_POSTER": "https://m.media-amazon.com/images/M/MV5BNGI2MTJjMjUtMTJhOC00YTY2LTg1NjUtMTdmMjg4YTk2YjM5XkEyXkFqcGc@._V1_.jpg",
    // "photo_width": 1688,
    // "photo_height": 2500
}
