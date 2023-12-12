using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    public class UserData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public string? UserID { get; set; }

        [JsonProperty("body")]
        public string? Body { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }
    }
}
