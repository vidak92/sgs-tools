using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

namespace MijanTools
{
    public static class Extensions
    {
        // Color strings.
        public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        // Int/enum.
        public static int ToInt<T>(this T value) where T : Enum
        {
            return (int)(object)value;
        }

        public static T ToEnum<T>(this int value) where T : Enum
        {
            return (T)(object)value;
        }

        // Int/bool.
        public static bool ToBool(this int i)
        {
            return i > 0 ? true : false;
        }

        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }

        // Resolution.
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

        // String.
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

        // Set alpha.
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

        // Particle systems.
        public static void SetStartAlpha(this ParticleSystem particleSystem, float alpha)
        {
            var mainModule = particleSystem.main;
            var color = mainModule.startColor.color;
            var newColor = new Color(color.r, color.g, color.b, alpha);
            mainModule.startColor = new MinMaxGradient(newColor);
        }

        public static void SetStartColor(this ParticleSystem particleSystem, Color color)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = color;
        }

        public static void SetStartColor(this ParticleSystem particleSystem, MinMaxGradient gradient)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = gradient;
        }

        public static void SetAlphaOverLifetime(this ParticleSystem particleSystem, float alpha)
        {
            var mainModule = particleSystem.colorOverLifetime;
            var color = mainModule.color.color;
            var newColor = new Color(color.r, color.g, color.b, alpha);
            mainModule.color = new MinMaxGradient(newColor);
        }

        public static void SetColorOverLifetime(this ParticleSystem particleSystem, Color color)
        {
            var colorOverLifetimeModule = particleSystem.colorOverLifetime;
            colorOverLifetimeModule.color = color;
        }

        public static void SetColorOverLifetime(this ParticleSystem particleSystem, MinMaxGradient gradient)
        {
            var colorOverLifetimeModule = particleSystem.colorOverLifetime;
            colorOverLifetimeModule.color = gradient;
        }

        // List/array index helpers.
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

        public static bool ContainsIndex<T>(this List<T> list, int index)
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
                var randomIndexOffset = Random.Range(1, array.Length - 1);
                return (indexToExclude + randomIndexOffset) % array.Length;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized array. Returning default value...");
            return default;
        }

        public static int GetRandomIndexExcluding<T>(this List<T> list, int indexToExclude)
        {
            if (!list.IsNullOrEmpty())
            {
                var randomIndexOffset = Random.Range(1, list.Count - 1);
                return (indexToExclude + randomIndexOffset) % list.Count;
            }
            Debug.Log("Trying to get a random index from an empty or uninitialized list. Returning default value...");
            return default;
        }

        // Set position.
        public static void SetPositionX(this Transform transform, float x)
        {
            var tempPosition = transform.position;
            tempPosition.x = x;
            transform.position = tempPosition;
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            var tempPosition = transform.position;
            tempPosition.y = y;
            transform.position = tempPosition;
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            var tempPosition = transform.position;
            tempPosition.z = z;
            transform.position = tempPosition;
        }

        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }

        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            transform.position = new Vector3(x, transform.position.y, z);
        }

        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            transform.position = new Vector3(transform.position.x, y, z);
        }

        public static void SetPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Add position.
        public static void AddPositionX(this Transform transform, float x)
        {
            var tempPosition = transform.position;
            tempPosition.x += x;
            transform.position = tempPosition;
        }

        public static void AddPositionY(this Transform transform, float y)
        {
            var tempPosition = transform.position;
            tempPosition.y += y;
            transform.position = tempPosition;
        }

        public static void AddPositionZ(this Transform transform, float z)
        {
            var tempPosition = transform.position;
            tempPosition.z += z;
            transform.position = tempPosition;
        }

        public static void AddPositionXY(this Transform transform, float x, float y)
        {
            var tempPosition = transform.position;
            tempPosition.x += x;
            tempPosition.y += y;
            transform.position = tempPosition;
        }

        public static void AddPositionXZ(this Transform transform, float x, float z)
        {
            var tempPosition = transform.position;
            tempPosition.x += x;
            tempPosition.z += z;
            transform.position = tempPosition;
        }

        public static void AddPositionYZ(this Transform transform, float y, float z)
        {
            var tempPosition = transform.position;
            tempPosition.y += y;
            tempPosition.z += z;
            transform.position = tempPosition;
        }

        public static void AddPositionXYZ(this Transform transform, float x, float y, float z)
        {
            var tempPosition = transform.position;
            tempPosition.x += x;
            tempPosition.y += y;
            tempPosition.z += z;
            transform.position = tempPosition;
        }

        // Clamp position.
        public static void ClampPositionX(this Transform transform, float minX, float maxX)
        {
            var tempPosition = transform.position;
            tempPosition.x = Mathf.Clamp(tempPosition.x, minX, maxX);
            transform.position = tempPosition;
        }

        public static void ClampPositionY(this Transform transform, float minY, float maxY)
        {
            var tempPosition = transform.position;
            tempPosition.y = Mathf.Clamp(tempPosition.y, minY, maxY);
            transform.position = tempPosition;
        }

        public static void ClampPositionZ(this Transform transform, float minZ, float maxZ)
        {
            var tempPosition = transform.position;
            tempPosition.z = Mathf.Clamp(tempPosition.z, minZ, maxZ);
            transform.position = tempPosition;
        }
        // TODO: Clamp position XY, XZ, YZ, XYZ.

        // Set local position.
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x = x;
            transform.localPosition = tempPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            var tempPosition = transform.localPosition;
            tempPosition.y = y;
            transform.localPosition = tempPosition;
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            var tempPosition = transform.localPosition;
            tempPosition.z = z;
            transform.localPosition = tempPosition;
        }

        public static void SetLocalPositionXY(this Transform transform, float x, float y)
        {
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }

        public static void SetLocalPositionXZ(this Transform transform, float x, float z)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, z);
        }

        public static void SetLocalPositionYZ(this Transform transform, float y, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, z);
        }

        public static void SetLocalPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        // Add local position.
        public static void AddLocalPositionX(this Transform transform, float x)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x += x;
            transform.localPosition = tempPosition;
        }

        public static void AddLocalPositionY(this Transform transform, float y)
        {
            var tempPosition = transform.localPosition;
            tempPosition.y += y;
            transform.localPosition = tempPosition;
        }

        public static void AddLocalPositionZ(this Transform transform, float z)
        {
            var tempPosition = transform.position;
            tempPosition.z += z;
            transform.position = tempPosition;
        }

        public static void AddLocalPositionXY(this Transform transform, float x, float y)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x += x;
            tempPosition.y += y;
            transform.localPosition = tempPosition;
        }

        public static void AddLocalPositionXZ(this Transform transform, float x, float z)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x += x;
            tempPosition.z += z;
            transform.localPosition = tempPosition;
        }

        public static void AddLocalPositionYZ(this Transform transform, float y, float z)
        {
            var tempPosition = transform.localPosition;
            tempPosition.y += y;
            tempPosition.z += z;
            transform.localPosition = tempPosition;
        }

        public static void AddLocalPositionXYZ(this Transform transform, float x, float y, float z)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x += x;
            tempPosition.y += y;
            tempPosition.z += z;
            transform.localPosition = tempPosition;
        }

        // Clamp local position.
        public static void ClampLocalPositionX(this Transform transform, float minX, float maxX)
        {
            var tempPosition = transform.localPosition;
            tempPosition.x = Mathf.Clamp(tempPosition.x, minX, maxX);
            transform.localPosition = tempPosition;
        }

        public static void ClampLocalPositionY(this Transform transform, float minY, float maxY)
        {
            var tempPosition = transform.localPosition;
            tempPosition.y = Mathf.Clamp(tempPosition.y, minY, maxY);
            transform.localPosition = tempPosition;
        }

        public static void ClampLocalPositionZ(this Transform transform, float minZ, float maxZ)
        {
            var tempPosition = transform.localPosition;
            tempPosition.z = Mathf.Clamp(tempPosition.z, minZ, maxZ);
            transform.localPosition = tempPosition;
        }
        // TODO: Clamp local position XY, XZ, YZ, XYZ.

        // Set rotation.
        public static void SetRotationX(this Transform transform, float eulerX)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationY(this Transform transform, float eulerY)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.y = eulerY;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationZ(this Transform transform, float eulerZ)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.z = eulerZ;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationXY(this Transform transform, float eulerX, float eulerY)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.y = eulerY;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationXZ(this Transform transform, float eulerX, float eulerZ)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.z = eulerZ;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationYZ(this Transform transform, float eulerY, float eulerZ)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.y = eulerY;
            tempEulerAngles.z = eulerZ;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRotationXYZ(this Transform transform, float eulerX, float eulerY, float eulerZ)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.y = eulerY;
            tempEulerAngles.z = eulerZ;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        // TODO: Add rotation.
        // TODO: Clamp rotation.

        // Set local rotation.
        public static void SetLocalRotationX(this Transform transform, float eulerX)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetLocalRotationY(this Transform transform, float eulerY)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.y = eulerY;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetLocalRotationZ(this Transform transform, float eulerZ)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.z = eulerZ;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetLocalRotationXY(this Transform transform, float eulerX, float eulerY)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.y = eulerY;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetLocalRotationXZ(this Transform transform, float eulerX, float eulerZ)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.z = eulerZ;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetRLocalotationYZ(this Transform transform, float eulerY, float eulerZ)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.y = eulerY;
            tempEulerAngles.z = eulerZ;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        public static void SetLocalRotationXYZ(this Transform transform, float eulerX, float eulerY, float eulerZ)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x = eulerX;
            tempEulerAngles.y = eulerY;
            tempEulerAngles.z = eulerZ;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        // TODO: Add local rotation.
        // TODO: Clamp local rotation.

        // Set local scale.
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x = x;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.y = y;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.z = z;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleXY(this Transform transform, float x, float y)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x = x;
            tempLocalScale.y = y;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleXZ(this Transform transform, float x, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x = x;
            tempLocalScale.z = z;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleYZ(this Transform transform, float y, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.y = y;
            tempLocalScale.z = z;
            transform.localScale = tempLocalScale;
        }

        public static void SetLocalScaleXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(x, y, z);
        }

        // Add local scale.
        public static void AddLocalScaleX(this Transform transform, float x)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x += x;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleY(this Transform transform, float y)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.y += y;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleZ(this Transform transform, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.z += z;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleXY(this Transform transform, float x, float y)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x += x;
            tempLocalScale.y += y;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleXZ(this Transform transform, float x, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x += x;
            tempLocalScale.z += z;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleYZ(this Transform transform, float y, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.y += y;
            tempLocalScale.z += z;
            transform.localScale = tempLocalScale;
        }

        public static void AddLocalScaleXYZ(this Transform transform, float x, float y, float z)
        {
            var tempLocalScale = transform.localScale;
            tempLocalScale.x += x;
            tempLocalScale.y += y;
            tempLocalScale.z += z;
            transform.localScale = tempLocalScale;
        }

        // Clamp local scale.
        public static void ClampLocalScaleX(this Transform transform, float minX, float maxX)
        {
            var tempScale = transform.localScale;
            tempScale.x = Mathf.Clamp(tempScale.x, minX, maxX);
            transform.localScale = tempScale;
        }

        public static void ClampLocalScaleY(this Transform transform, float minY, float maxY)
        {
            var tempScale = transform.localScale;
            tempScale.y = Mathf.Clamp(tempScale.y, minY, maxY);
            transform.localScale = tempScale;
        }

        public static void ClampLocalScaleZ(this Transform transform, float minZ, float maxZ)
        {
            var tempScale = transform.localScale;
            tempScale.z = Mathf.Clamp(tempScale.z, minZ, maxZ);
            transform.localScale = tempScale;
        }

        // Set anchored position.
        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public static void SetAnchoredPositionXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        // Add anchored position.
        public static void AddAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition += new Vector2(x, 0f);
        }

        public static void AddAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition += new Vector2(0f, y);
        }

        public static void AddAnchoredPositionXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition += new Vector2(x, y);
        }

        // Set size delta.
        public static void SetSizeDeltaX(this RectTransform rectTransform, float x)
        {
            rectTransform.sizeDelta = new Vector2(x, rectTransform.sizeDelta.y);
        }

        public static void SetSizeDeltaY(this RectTransform rectTransform, float y)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
        }
        
        public static void SetSizeDeltaXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.sizeDelta = new Vector2(x, y);
        }

        // Add size delta.
        public static void AddSizeDeltaX(this RectTransform rectTransform, float x)
        {
            rectTransform.sizeDelta += new Vector2(x, 0f);
        }

        public static void AddSizeDeltaY(this RectTransform rectTransform, float y)
        {
            rectTransform.sizeDelta += new Vector2(0f, y);
        }

        public static void AddSizeDeltaXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.sizeDelta += new Vector2(x, y);
        }
    }
}