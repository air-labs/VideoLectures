using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    class KeySet
    {
        Key[] Keys;
        public KeySet(params Key[] keys)
        {
            Keys = keys;
        }
        public bool Is(Key k)
        {
            return Keys.Any(z => z == k);
        }
    }

    class KeyMap
    {


    }

}
