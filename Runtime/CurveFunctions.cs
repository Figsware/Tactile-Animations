using System;
using UnityEngine;

namespace Tactile.Animations
{
    public static class CurveFunctions
    {
        public static float Linear(float x) => x;

        public static float Sin(float x) => (Mathf.Sin(Mathf.PI * (x - 0.5f)) + 1f) / 2f;
    }
}