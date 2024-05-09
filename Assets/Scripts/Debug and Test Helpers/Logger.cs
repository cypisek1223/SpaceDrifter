using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Logger : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] new private bool enabled = true;

        public void Log(string msg)
        {
            if (!enabled) return;

            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", 
                (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), 
                msg));
        }
    } 
}
