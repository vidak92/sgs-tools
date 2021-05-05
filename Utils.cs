using UnityEngine;

namespace MijanTools
{
    public static class Utils
    {
        // Color strings.
        public static string GetColorTag(string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }

        public static string GetSpriteTag(string name, Color color)
        {
            return $"<sprite name=\"{name}\" color=\"{color.ToHex()}\">";
        }
    }
}