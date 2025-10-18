using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fanfic_bible
{
    public struct UserModel
    {
        public reader readerInfo;

        public List<issuance_key> issuances;

        public List<int> issuedBookIds;

    }
}
