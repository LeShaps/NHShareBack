using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHShareBack.Db
{
    public class User
    {
        public string id;
        public string FriendlyId;
        public List<Collection> Collections;
        public DateTime LastUpdate;
    }
}
