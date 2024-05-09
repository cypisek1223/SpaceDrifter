using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D.MathFun
{
    public enum Function { Identity, Inverse, Sinus }

    public static class FunctionLibrary
    {
        private static Func<float, float>[] functions = { Identity, Inverse, Sinus };
        public static Func<float, float> GetFunction(Function f)
        {
            return functions[(int)f];
        }

        private static float Identity(float x)
        {
            return x;
        }

        private static float Inverse(float x)
        {
            return 1 - x;
        }

        private static float Sinus(float x)
        {
            return Mathf.Sin(x);
        }
    }

    [Serializable]
    public class Function1D
    {
        [SerializeField] private Function f;
        [SerializeField] float inputOffset;
        [SerializeField] float inputMultiplier = 1;
        [SerializeField] float outputOffset;
        [SerializeField] float outputMultiplayer = 1;

        public float Evaluate(float x)
        {
            x = (x + inputOffset) * inputMultiplier;
            float output = (FunctionLibrary.GetFunction(f)(x) + outputOffset) * outputMultiplayer;
            return output;
        }
    }
}
