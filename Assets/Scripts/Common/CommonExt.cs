﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MijanTools.Common
{ 
    public static class CommonExt
    {
        // Int
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        // LayerMask
        public static int ToIndex(this LayerMask layerMask)
        {
            var layer = Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
            return layer;
        }

        // GameObject
        public static bool IsInLayer(this GameObject gameObject, string layerName)
        {
            var layer = LayerMask.NameToLayer(layerName);
            return gameObject.layer == layer;
        }

        // Color
        public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        public static Color MultiplyIgnoringAlpha(this Color color, float multiplier)
        {
            return new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a);
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        // Int/enum
        public static int ToInt<T>(this T value) where T : Enum
        {
            return (int)(object)value;
        }

        public static T ToEnum<T>(this int value) where T : Enum
        {
            return (T)(object)value;
        }

        // Int/bool
        public static bool ToBool(this int i)
        {
            return i > 0 ? true : false;
        }

        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }

        // Resolution
        // Converts a string of the following format: "<width> x <height> @ <refresh_rate>Hz" to a Resolution struct.
        public static Resolution ToResolution(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (Screen.resolutions.ContainsIndex(Screen.resolutions.Length - 1))
                {
                    return Screen.resolutions[Screen.resolutions.Length - 1];
                }
                return Screen.currentResolution;
            }

            var indexOfX = s.IndexOf('x');
            var indexOfAt = s.IndexOf('@');
            var indexOfHz = s.IndexOf('H');
            var widthString = s.SubstringFromTo(0, indexOfX - 1);
            var heightString = s.SubstringFromTo(indexOfX + 1, indexOfAt - 1);
            var refreshRateString = s.SubstringFromTo(indexOfAt + 1, indexOfHz - 1);

            if (int.TryParse(widthString, out int width) &&
                int.TryParse(heightString, out int height) &&
                int.TryParse(refreshRateString, out int refreshRate))
            {
                Resolution resolution = new Resolution
                {
                    width = width,
                    height = height,
                    refreshRate = refreshRate
                };
                return resolution;
            }

            return Screen.currentResolution;
        }

        // String
        public static string SubstringFromTo(this string s, int startIndex, int endIndex)
        {
            if (startIndex <= endIndex && !string.IsNullOrEmpty(s) &&
                s.ContainsIndex(startIndex) && s.ContainsIndex(endIndex))
            {
                return s.Substring(startIndex, endIndex - startIndex + 1);
            }
            return "";
        }

        public static bool ContainsIndex(this string s, int index)
        {
            return !string.IsNullOrEmpty(s) && s.Length > index;
        }

        // Alpha
        public static void SetAlpha(this SpriteRenderer sprite, float alpha)
        {
            var color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }

        public static void SetAlpha(this LineRenderer line, float alpha)
        {
            var startColor = line.startColor;
            startColor.a = alpha;
            line.startColor = startColor;

            var endColor = line.endColor;
            endColor.a = alpha;
            line.endColor = endColor;
        }

        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void SetAlpha(this TMP_Text text, float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }

        public static void SetStartAlpha(this TrailRenderer trailRenderer, float alpha)
        {
            var color = trailRenderer.startColor;
            color.a = alpha;
            trailRenderer.startColor = color;
        }

        public static void SetEndAlpha(this TrailRenderer trailRenderer, float alpha)
        {
            var color = trailRenderer.endColor;
            color.a = alpha;
            trailRenderer.endColor = color;
        }

        // Particle Systems
        public static void SetStartAlpha(this ParticleSystem particleSystem, float alpha)
        {
            var mainModule = particleSystem.main;
            var color = mainModule.startColor.color;
            var newColor = new Color(color.r, color.g, color.b, alpha);
            mainModule.startColor = new ParticleSystem.MinMaxGradient(newColor);
        }

        public static void SetStartColor(this ParticleSystem particleSystem, Color color)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = color;
        }

        public static void SetStartColor(this ParticleSystem particleSystem, ParticleSystem.MinMaxGradient gradient)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = gradient;
        }

        public static void SetAlphaOverLifetime(this ParticleSystem particleSystem, float alpha)
        {
            var mainModule = particleSystem.colorOverLifetime;
            var color = mainModule.color.color;
            var newColor = new Color(color.r, color.g, color.b, alpha);
            mainModule.color = new ParticleSystem.MinMaxGradient(newColor);
        }

        public static void SetColorOverLifetime(this ParticleSystem particleSystem, Color color)
        {
            var colorOverLifetimeModule = particleSystem.colorOverLifetime;
            colorOverLifetimeModule.color = color;
        }

        public static void SetColorOverLifetime(this ParticleSystem particleSystem, ParticleSystem.MinMaxGradient gradient)
        {
            var colorOverLifetimeModule = particleSystem.colorOverLifetime;
            colorOverLifetimeModule.color = gradient;
        }
        
        // LineRenderer
        public static void SetColor(this LineRenderer lineRenderer, Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
        
        public static void SetWidth(this LineRenderer lineRenderer, float width)
        {
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }
        
        // TrailRenderer
        public static void SetColor(this TrailRenderer trailRenderer, Color color)
        {
            trailRenderer.startColor = color;
            trailRenderer.endColor = color;
        }
        
        public static void SetWidth(this TrailRenderer trailRenderer, float width)
        {
            trailRenderer.startWidth = width;
            trailRenderer.endWidth = width;
        }

        // Dictionary
        public static bool HasKey<T, U>(this Dictionary<T, U> dictionary, T key)
        {
            if (dictionary == null)
            {
                return false;
            }
            return dictionary.ContainsKey(key);
        }

        // List/array
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool ContainsIndex<T>(this T[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }

        public static bool ContainsIndex<T>(this T[,] array, int index1, int index2)
        {
            return index1 >= 0 && index1 < array.GetLength(0)
                && index2 >= 0 && index2 < array.GetLength(1);
        }

        public static bool ContainsIndex<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }

        public static bool ContainsIndex<T>(this IList<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }

        public static T GetRandomElement<T>(this T[] array)
        {
            if (!array.IsNullOrEmpty())
            {
                var randomIndex = Random.Range(0, array.Length);
                return array[randomIndex];
            }
            Debug.Log("Trying to get a random element from an empty or uninitialized array. Returning default value...");
            return default;
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            if (!list.IsNullOrEmpty())
            {
                var randomIndex = Random.Range(0, list.Count);
                return list[randomIndex];
            }
            Debug.Log("Trying to get a random element from an empty or uninitialized list. Returning default value...");
            return default;
        }

        public static int GetRandomIndexExcluding<T>(this T[] array, int indexToExclude)
        {
            if (!array.IsNullOrEmpty())
            {
                var randomIndexOffset = Random.Range(0, array.Length - 1);
                return (indexToExclude + randomIndexOffset) % array.Length;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized array. Returning default value...");
            return default;
        }

        public static int GetRandomIndexExcluding<T>(this List<T> list, int indexToExclude)
        {
            if (!list.IsNullOrEmpty())
            {
                var randomIndexOffset = Random.Range(0, list.Count - 1);
                return (indexToExclude + randomIndexOffset) % list.Count;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized list. Returning default value...");
            return default;
        }
        
        public static int GetRandomIndex<T>(this T[] array)
        {
            if (!array.IsNullOrEmpty())
            {
                var randomIndex = Random.Range(0, array.Length - 1);
                return randomIndex;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized array. Returning default value...");
            return default;
        }
        
        public static int GetRandomIndex<T>(this List<T> list)
        {
            if (!list.IsNullOrEmpty())
            {
                var randomIndex = Random.Range(0, list.Count - 1);
                return randomIndex;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized list. Returning default value...");
            return default;
        }

        // TODO: Add list/array extensions for sum, average, etc. 
        // Add support for int, float, Vector2, Vector3 or whatever else makes sense.
        
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                // int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]); // Swap elements
            }
        }
        
        // Weighted lists
        // TODO does this need to be an extension method?
        public static (int Index, T Item) GetRandomWeightedElement<T>(this List<(float Weight, T Item)> list)
        {
            var totalWeight = 0f;
            foreach (var item in list)
            {
                if (item.Weight <= 0f)
                {
                    continue;
                }
                
                totalWeight += item.Weight;
            }

            var random = Random.Range(0f, totalWeight);
            var currentWeight = 0f;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.Weight <= 0f)
                {
                    continue;
                }
                
                currentWeight += item.Weight;
                if (random < currentWeight)
                {
                    return (i, item.Item);
                }
            }

            Assert.IsTrue(true, "Failed to get random item from weighted list.");
            return (-1, default);
        }
    }
}