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
        public static string GetTaggedString(this string text, Color? color = null, bool bold = false, bool underline = false)
        {
            InitStringBuilder();

            _stringBuilder.Append(text);
            if (color != null)
            {
                _stringBuilder.Insert(0, $"<color={color.Value.ToHex()}>");
                _stringBuilder.Append("</color>");
            }
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

            var taggedString = _stringBuilder.ToString();
            _stringBuilder.Clear(); // NOTE not really necessary to clear here
            return taggedString;
        }

        public static string GetSpriteTag(this string name, Color color)
        {
            return $"<sprite name=\"{name}\" color=\"{color.ToHex()}\">";
        }
    }
}