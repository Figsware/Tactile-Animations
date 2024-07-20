using System;
using System.Collections;
using UnityEngine;

namespace Tactile.Animations
{
    public static class TransformCoroutines
    {
        public static IEnumerator LocalTranslateCoroutine(this Transform transform, float time,
            Vector3 targetLocalPosition, Func<float, float> curve = null) => AnimationCoroutines.LerpCoroutine(
            time, curve ?? CurveFunctions.Linear, transform.localPosition, targetLocalPosition, Vector3.Lerp, pos => transform.localPosition = pos);

        public static IEnumerator TranslateCoroutine(this Transform transform, float time,
            Vector3 targetPosition, Func<float, float> curve = null) => AnimationCoroutines.LerpCoroutine(
            time, curve ?? CurveFunctions.Linear, transform.position, targetPosition, Vector3.Lerp, pos => transform.position = pos);
        
        public static IEnumerator ScaleCoroutine(this Transform transform, float time, Vector3 targetScale,
            Func<float, float> curve = null) => AnimationCoroutines.LerpCoroutine(time,
            curve ?? CurveFunctions.Linear, transform.localScale, targetScale, Vector3.Lerp,
            scale => transform.localScale = scale);

        public static IEnumerator LocalRotateCoroutine(this Transform transform, float time,
            Quaternion targetLocalRotation, Func<float, float> curve = null) =>
            AnimationCoroutines.LerpCoroutine(time, curve ?? CurveFunctions.Linear, transform.localRotation,
                targetLocalRotation, Quaternion.Lerp, rot => transform.localRotation = rot);

        public static IEnumerator RotateCoroutine(this Transform transform, float time,
            Quaternion targetRotation, Func<float, float> curve = null) =>
            AnimationCoroutines.LerpCoroutine(time, curve ?? CurveFunctions.Linear, transform.rotation,
                targetRotation, Quaternion.Lerp, rot => transform.rotation = rot);
    }
}