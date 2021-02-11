using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHShareBack.Db
{
    public class Collection
    {
        public string Name;
        public List<DoujinInfos> Doujins;
        public List<string> AllowedUsers;
        public bool Private;

        public Collection() { }
    }
}
