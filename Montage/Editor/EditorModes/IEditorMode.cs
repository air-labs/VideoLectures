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
        WindowCommand CheckTime(WindowState state);
        WindowCommand MouseClick(WindowState state, int SelectedLocation, MouseButtonEventArgs button);
        WindowCommand ProcessKey(WindowState state, KeyEventArgs key);
    }
}
