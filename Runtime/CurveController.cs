using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Tactile.Animations
{
    /// <summary>
    /// A curve controller combines an animation curve with a T value. It has an event so that 
    /// </summary>
    [Serializable]
    public class CurveController
    {
        [Tooltip("The current t value")]
        [SerializeField, Range(0,1)] private float t;
        [Tooltip("The curve that the values will be animated on")]
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private UnityEvent<float> onNewValue = new();

        public CurveController()
        {
            
        }

        public CurveController(AnimationCurve curve)
        {
            SetCurve(curve);
        }
        
        /// <summary>
        /// A unity event for when this curve has a new value.
        /// </summary>
        public UnityEvent<float> OnNewValue => onNewValue;
        
        /// <summary>
        /// The curve used by the curve controller.
        /// </summary>
        public AnimationCurve Curve => curve;

        /// <summary>
        /// The current T value for the curve.
        /// </summary>
        public float T
        {
            get => t;
            set => SetT(value);
        }

        /// <summary>
        /// The value of the curve.
        /// </summary>
        public float Value => curve.Evaluate(t);

        private void SetCurve(AnimationCurve newCurve)
        {
            curve = newCurve ?? throw new NullReferenceException(nameof(newCurve));
        }

        private void SetT(float newT)
        {
            t = Mathf.Clamp(newT, 0f, 1f);
            onNewValue.Invoke(Value);
        }
    }
}