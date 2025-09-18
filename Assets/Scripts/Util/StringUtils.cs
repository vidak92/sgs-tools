using SGSTools.Common;
using UnityEngine;

namespace SGSTools.Util
{
    public static class StringUtils
    {
        // Rich text tags.
        // TODO rename to GetColorTagged
        public static string GetColorTag(string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }

        // TODO rename to GetSpriteTagged
        public static string GetSpriteTag(string name, Color color)
        {
            return $"<sprite name=\"{name}\" color=\"{color.ToHex()}\">";
        }

        public static string GetColorTagged(this string text, Color color)
        {
            return GetColorTag(text, color);
        }
    }
}