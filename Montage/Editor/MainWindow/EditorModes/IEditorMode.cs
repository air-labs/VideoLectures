﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public interface IEditorMode
    {
        Response CheckTime(int ms);
        Response MouseClick(int ms, MouseButtonEventArgs button);
        Response ProcessKey(KeyEventArgs key);
    }
}
