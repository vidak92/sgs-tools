using SGSTools.Common;
using SGSTools.Util;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SGSTools.Components
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ObjectShaker))]
    public class ObjectShakerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var screenShake = target as ObjectShaker;
            if (GUILayout.Button("Start Shake"))
            {
                screenShake.StartShake(screenShake.ShakeIntensity);
            }
            if (GUILayout.Button("Stop Shake"))
            {
                screenShake.StopShake();
            }
            EditorGUILayout.Separator();
            DrawDefaultInspector();
        }
    }
#endif

    public class ObjectShaker : MonoBehaviour
    {
        [Space]
        [Tooltip("If checked, shakes Camera.main and TargetObject is ignored")]
        public bool TargetIsMainCamera;
        public GameObject TargetObject;

        [Space]
        [Range(0f, 1f)]
        [Tooltip("Interpolate values from XXXRange fields")]
        public float ShakeIntensity;
        
        [Space]
        [Tooltip("Use (0, 0) for infinite duration")]
        public FloatRange DurationRange;
        public FloatRange SpeedRange;
        public FloatRange DistanceRange;

        private float _shakeTimer;
        private bool _shake;
        private bool _targetReached;
        private bool _returnToDefaultPosition;
        private Vector3 _targetPosition;
        private Vector3 _initialPosition;
        private Vector3 _defaultPosition;
        private float _t;
        private float _shakeSpeed;
        private float _shakeDuration;
        private float _shakeDistance;

        public bool IsShaking => _shake;

        private Transform TargetTransform => TargetIsMainCamera ? CameraUtils.MainCamera.transform : TargetObject.transform;

        private void Update()
        {
            if (_shake)
            {
                var targetTransform = TargetTransform;

                if (_targetReached)
                {
                    // New target.
                    _initialPosition = targetTransform.position;
                    
                    _targetPosition = (Vector3)Random.insideUnitCircle.normalized * _shakeDistance + _defaultPosition;
                    _targetPosition.z = _defaultPosition.z;

                    _targetReached = false;
                    _t = 0f;
                }
                else
                {
                    // Move to target.
                    _t += Time.deltaTime * _shakeSpeed;
                    targetTransform.position = Vector3.Lerp(_initialPosition, _targetPosition, _t);
                    _targetReached = _t > 0.99f; // TODO epsilon variable

                    if (_returnToDefaultPosition && _targetReached)
                    {
                        _shake = false;
                        _targetReached = false;
                        _shakeTimer = 0f;
                        targetTransform.position = _defaultPosition;
                    }
                }

                if (!_returnToDefaultPosition && _shakeDuration > 0f)
                {
                    _shakeTimer += Time.deltaTime;
                    if (_shakeTimer >= _shakeDuration && _targetReached)
                    {
                        _returnToDefaultPosition = true;
                        _initialPosition = targetTransform.position;
                        _targetPosition = _defaultPosition;
                        _targetReached = false;
                        _t = 0f;
                    }
                }
            }
        }

        public void StartShake(float intensity = 1f)
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
            _returnToDefaultPosition = false;
            _shakeSpeed = SpeedRange.GetValueAt(intensity);
            _shakeDuration = DurationRange.GetValueAt(intensity);
            _shakeDistance = DistanceRange.GetValueAt(intensity);
            _defaultPosition = TargetTransform.position;
        }

        public void StopShake()
        {
            _returnToDefaultPosition = true;
            _initialPosition = TargetTransform.position;
            _targetPosition = _defaultPosition;
            _targetReached = false;
            _t = 0f;
        }
    }
}