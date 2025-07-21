﻿using SGSTools.Common;
using UnityEngine;

namespace SGSTools.Util
{
    public static class StringUtils
    {
        // Rich text tags.
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