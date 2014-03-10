using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public interface IEditorMode
    {
        WindowCommand CheckTime();
        WindowCommand MouseClick(int SelectedLocation, MouseButtonEventArgs button);
        WindowCommand ProcessKey(KeyEventArgs key);
    }
}
