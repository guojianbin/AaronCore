using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Aaron.Core.Security.EDO
{
    public class EDObjectContext
    {
        public static ISerializable CreateInstance()
        {
            return null ?? new EDObject(Rijndael.Create());
        }
    }
}
