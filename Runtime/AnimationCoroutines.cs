using System;
using System.Collections;
using UnityEngine;

namespace Tactile.Animations
{
    public static class AnimationCoroutines
    {
        public static IEnumerator AnimationCurveCoroutine(float time, Func<float, float> curve,
            Action<float> animation)
        {
            var elapsed = 0f;
            while (elapsed <= time)
            {
                var t = curve(elapsed / time);

                animation(t);
                
                elapsed += Time.deltaTime;
                yield return null;
            }

            animation(curve(1f));
        }
        
        public static IEnumerator LerpCoroutine<T>(float time, Func<float, float> curve,
            Func<float, T> lerp, Action<T> animation)
        {
            yield return AnimationCurveCoroutine(time, curve, t =>
            {
                var now = lerp(t);
                animation(now);
            });
        }
        public static IEnumerator LerpCoroutine<T>(float time, Func<float, float> curve, T from, T to,
            Func<T, T, float, T> lerp, Action<T> animation)
        {
            yield return AnimationCurveCoroutine(time, curve, t =>
            {
                var now = lerp(from, to, t);
                animation(now);
            });
        }
    }
}