using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Editor
{
    public enum EditorKeys
    {
        Unknown,

        Pause,
        Forward,
        Backward,
        SpeedUp,
        SpeedDown,

        GeneralScreen,
        GeneralFace,
        GeneralDrop,
        GeneralReset,
        GeneralNextChunk,
        GeneralPreviousChunk,
        GeneralLeftBorderToLeft,
        GeneralLeftBorderToRight,
        GeneralRightBorderToLeft,
        GeneralRightBorderToRight,


        BorderLeftForderToLeft,
        BorderLeftBorderToRight,
        BorderRightBorderToLeft,
        BorderRightBorderToRight
    }

    public static class KeyMap
    {
        Dictionary<Key, EditorKeys> map = new Dictionary<Key, EditorKeys>
        {
        };

        public static EditorKeys GetKey(Key key)
        {
            return EditorKeys.Unknown;
        }

    }
}
