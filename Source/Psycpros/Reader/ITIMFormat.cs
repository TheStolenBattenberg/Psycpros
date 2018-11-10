using System;
using System.Collections.Generic;
using System.IO;

using Psycpros.Psycode;

namespace Psycpros.Reader {
    struct TIMClut {
        short[] u16Colour;

        public TIMClut(short[] clutData) {
            u16Colour = clutData;
        }
    }

    class ITIMFormat {

    }
}
