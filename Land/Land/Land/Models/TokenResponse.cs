using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Land.Models
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "acces_token")]
        public string AccesToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = ".issued")]
        public string Issued { get; set; }
        [JsonProperty(PropertyName = "expires")]
        public string Expires { get; set; }
        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        public bool IsRemembered
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}
