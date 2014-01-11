using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public static class Commands
    {
        public static RoutedUICommand Save = new RoutedUICommand("Сохранить", "Save", typeof(Commands),
            new InputGestureCollection(new ArrayList { new KeyGesture(Key.S,ModifierKeys.Control)  }));


        public static RoutedUICommand Export = new RoutedUICommand("Экспортировать", "Export", typeof(Commands),
            new InputGestureCollection(new ArrayList { new KeyGesture(Key.E, ModifierKeys.Control) }));
    }
}
