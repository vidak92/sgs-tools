using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MijanTools
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ScreenShake))]
    public class ScreenShakeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var screenShake = target as ScreenShake;
            if (GUILayout.Button("Start Screenshake"))
            {
                screenShake.StartScreenShake(screenShake.ShakeIntensity);
            }
            EditorGUILayout.Separator();
            DrawDefaultInspector();
        }
    }
#endif

    public class ScreenShake : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float ShakeIntensity;
        public float MinDuration;
        public float MaxDuration;
        public float MinSpeed;
        public float MaxSpeed;
        public float MinDistance;
        public float MaxDistance;

        private float _shakeTimer;
        public bool _shake;
        private bool _targetReached;
        private bool _returnToInitialPosition;
        private Vector3 _targetCameraPosition;
        private Vector3 _initialCameraPosition;
        private float _t;
        private float _shakeSpeed;
        private float _shakeDuration;
        private float _shakeDistance;

        private Vector3 _defaultCameraPosition = new Vector3(0f, 0f, -10f);

        private void Update()
        {
            if (_shake)
            {
                var camera = CameraUtils.MainCamera;

                if (_targetReached)
                {
                    // New target.
                    _initialCameraPosition = camera.transform.position;
                    
                    _targetCameraPosition = (Vector3)Random.insideUnitCircle.normalized * _shakeDistance + _defaultCameraPosition;
                    _targetCameraPosition.z = _defaultCameraPosition.z;

                    _targetReached = false;
                    _t = 0f;
                }
                else
                {
                    // Move to target.
                    _t += Time.deltaTime * _shakeSpeed;
                    camera.transform.position = Vector3.Lerp(_initialCameraPosition, _targetCameraPosition, _t);
                    _targetReached = _t > 0.99f;

                    if (_returnToInitialPosition && _targetReached)
                    {
                        _shake = false;
                        _targetReached = false;
                        _shakeTimer = 0f;
                        camera.transform.position = _defaultCameraPosition;
                    }
                }

                if (!_returnToInitialPosition)
                {
                    _shakeTimer += Time.deltaTime;
                    if (_shakeTimer >= _shakeDuration && _targetReached)
                    {
                        _returnToInitialPosition = true;
                        _initialCameraPosition = camera.transform.position;
                        _targetCameraPosition = _defaultCameraPosition;
                        _targetReached = false;
                        _t = 0f;
                    }
                }
            }
        }

        public void StartScreenShake(float intensity)
        {
            intensity = Mathf.Clamp01(intensity);
            if (_shake && intensity < ShakeIntensity)
            {
                // Do not start shake if a more intense one is already running.
                return;
            }

            ShakeIntensity = intensity;
            _shakeTimer = 0f;
            _shake = true;
            _targetReached = true;
            _returnToInitialPosition = false;
            _shakeSpeed = Mathf.Lerp(MinSpeed, MaxSpeed, intensity);
            _shakeDuration = Mathf.Lerp(MinDuration, MaxDuration, intensity);
            _shakeDistance = Mathf.Lerp(MinDistance, MaxDistance, intensity);
        }
    }
}