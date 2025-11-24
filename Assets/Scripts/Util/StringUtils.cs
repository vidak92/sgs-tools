using System.Text;
using SGSTools.Common;
using UnityEngine;

namespace SGSTools.Util
{
    public static class StringUtils
    {
        private static StringBuilder _stringBuilder;

        private static void InitStringBuilder()
        {
            if (_stringBuilder == null)
            {
                _stringBuilder = new StringBuilder();
            }
            _stringBuilder.Clear();
        }

        // Rich text tags.
        public static string GetTaggedString(this string text, string colorHex = "", bool bold = false, bool underline = false)
        {
            InitStringBuilder();

            _stringBuilder.Append(text);
            if (bold)
            {
                _stringBuilder.Insert(0, "<b>");
                _stringBuilder.Append("</b>");
            }
            if (underline)
            {
                _stringBuilder.Insert(0, "<u>");
                _stringBuilder.Append("</u>");
            }
            if (!string.IsNullOrEmpty(colorHex))
            {
                _stringBuilder.Insert(0, $"<color={colorHex}>");
                _stringBuilder.Append("</color>");
            }

            var taggedString = _stringBuilder.ToString();
            _stringBuilder.Clear(); // NOTE not really necessary to clear here
            return taggedString;
        }

        public static string GetTaggedString(this string text, Color? color = null, bool bold = false, bool underline = false)
        {
            var colorHex = color?.ToHex() ?? "";
            return GetTaggedString(text, colorHex, bold, underline);
        }

        public static string Underlined(this string text)
        {
            return $"<u>{text}</u>";
        }

        public static string GetSpriteTag(this string name, Color color)
        {
            return $"<sprite name=\"{name}\" color=\"{color.ToHex()}\">";
        }
    }
}