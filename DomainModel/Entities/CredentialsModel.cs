using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CredentialsModel
    {
        public string AccessToken { get; set; }
        public int Duration { get; set; }
        public string UserID { get; set; }

    }
}
