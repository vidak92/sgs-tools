using UnityEngine;

namespace SGSTools.Extensions
{
    public static class TransformExt
    {
        // Set Position
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = transform.position.WithX(x);
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = transform.position.WithY(y);
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = transform.position.WithZ(z);
        }

        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            transform.position = transform.position.WithXY(x, y);
        }

        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            transform.position = transform.position.WithXZ(x, z);
        }

        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            transform.position = transform.position.WithYZ(y, z);
        }

        public static void SetPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Add Position
        public static void AddPositionX(this Transform transform, float x)
        {
            transform.position += new Vector3(x, 0f, 0f);
        }

        public static void AddPositionY(this Transform transform, float y)
        {
            transform.position += new Vector3(0f, y, 0f);
        }

        public static void AddPositionZ(this Transform transform, float z)
        {
            transform.position += new Vector3(0f, 0f, z);
        }

        public static void AddPositionXY(this Transform transform, float x, float y)
        {
            transform.position += new Vector3(x, y, 0f);
        }

        public static void AddPositionXZ(this Transform transform, float x, float z)
        {
            transform.position += new Vector3(x, 0f, z);
        }

        public static void AddPositionYZ(this Transform transform, float y, float z)
        {
            transform.position += new Vector3(0f, y, z);
        }

        public static void AddPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position += new Vector3(x, y, z);
        }

        // Set Local Position
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            transform.localPosition = transform.localPosition.WithX(x);
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            transform.localPosition = transform.localPosition.WithY(y);
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition = transform.localPosition.WithZ(z);
        }

        public static void SetLocalPositionXY(this Transform transform, float x, float y)
        {
            transform.localPosition = transform.localPosition.WithXY(x, y);
        }

        public static void SetLocalPositionXZ(this Transform transform, float x, float z)
        {
            transform.localPosition = transform.localPosition.WithXZ(x, z);
        }

        public static void SetLocalPositionYZ(this Transform transform, float y, float z)
        {
            transform.localPosition = transform.localPosition.WithYZ(y, z);
        }

        public static void SetLocalPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        // Add Local Position
        public static void AddLocalPositionX(this Transform transform, float x)
        {
            transform.localPosition += new Vector3(x, 0f, 0f);
        }

        public static void AddLocalPositionY(this Transform transform, float y)
        {
            transform.localPosition += new Vector3(0f, y, 0f);
        }

        public static void AddLocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition += new Vector3(0f, 0f, z);
        }
        
        public static void AddLocalPositionXY(this Transform transform, float x, float y)
        {
            transform.localPosition += new Vector3(x, y, 0f);
        }

        public static void AddLocalPositionXZ(this Transform transform, float x, float z)
        {
            transform.localPosition += new Vector3(x, 0f, z);
        }

        public static void AddLocalPositionYZ(this Transform transform, float y, float z)
        {
            transform.localPosition += new Vector3(0f, y, z);
        }

        public static void AddLocalPositionXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition += new Vector3(x, y, z);
        }

        // Set Rotation
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
        
        // Add Rotation
        public static void AddRotationX(this Transform transform, float x)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x += x;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddRotationY(this Transform transform, float y)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.y += y;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddRotationZ(this Transform transform, float z)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.z += z;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddRotationXY(this Transform transform, float x, float y)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x += x;
            tempEulerAngles.y += y;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddRotationXZ(this Transform transform, float x, float z)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.x += x;
            tempEulerAngles.z += z;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddRotationYZ(this Transform transform, float y, float z)
        {
            var tempEulerAngles = transform.rotation.eulerAngles;
            tempEulerAngles.y += y;
            tempEulerAngles.z += z;
            transform.rotation = Quaternion.Euler(tempEulerAngles);
        }

        // Set Local Rotation
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

        public static void SetLocalRotationYZ(this Transform transform, float eulerY, float eulerZ)
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
        
        // Add Local Rotation
        public static void AddLocalRotationX(this Transform transform, float x)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x += x;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddLocalRotationY(this Transform transform, float y)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.y += y;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddLocalRotationZ(this Transform transform, float z)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.z += z;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddLocalRotationXY(this Transform transform, float x, float y)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x += x;
            tempEulerAngles.y += y;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddLocalRotationXZ(this Transform transform, float x, float z)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.x += x;
            tempEulerAngles.z += z;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }
        
        public static void AddLocalRotationYZ(this Transform transform, float y, float z)
        {
            var tempEulerAngles = transform.localRotation.eulerAngles;
            tempEulerAngles.y += y;
            tempEulerAngles.z += z;
            transform.localRotation = Quaternion.Euler(tempEulerAngles);
        }

        // Set Local Scale
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            transform.localScale = transform.localScale.WithX(x);
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            transform.localScale = transform.localScale.WithY(y);
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            transform.localScale = transform.localScale.WithZ(z);
        }

        public static void SetLocalScaleXY(this Transform transform, float x, float y)
        {
            transform.localScale = transform.localScale.WithXY(x, y);
        }

        public static void SetLocalScaleXZ(this Transform transform, float x, float z)
        {
            transform.localScale = transform.localScale.WithXZ(x, z);
        }

        public static void SetLocalScaleYZ(this Transform transform, float y, float z)
        {
            transform.localScale = transform.localScale.WithXY(y, z);
        }

        public static void SetLocalScaleXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(x, y, z);
        }

        public static void SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }

        // Add Local Scale
        public static void AddLocalScaleX(this Transform transform, float x)
        {
            transform.localScale += new Vector3(x, 0f, 0f);
        }

        public static void AddLocalScaleY(this Transform transform, float y)
        {
            transform.localScale += new Vector3(0f, y, 0f);
        }

        public static void AddLocalScaleZ(this Transform transform, float z)
        {
            transform.localScale += new Vector3(0f, 0f, z);
        }

        public static void AddLocalScaleXY(this Transform transform, float x, float y)
        {
            transform.localScale += new Vector3(x, y, 0f);
        }

        public static void AddLocalScaleXZ(this Transform transform, float x, float z)
        {
            transform.localScale += new Vector3(x, 0f, z);
        }

        public static void AddLocalScaleYZ(this Transform transform, float y, float z)
        {
            transform.localScale += new Vector3(0f, y, z);
        }

        public static void AddLocalScaleXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localScale += new Vector3(x, y, z);
        }
        
        // Set Anchored Position
        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = rectTransform.anchoredPosition.WithX(x);
        }

        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = rectTransform.anchoredPosition.WithY(y);
        }

        public static void SetAnchoredPositionXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        // Add Anchored Position
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

        // Set Size Delta
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
        
        public static void SetSizeDelta(this RectTransform rectTransform, float size)
        {
            rectTransform.sizeDelta = new Vector2(size, size);
        }

        // Add Size Delta
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