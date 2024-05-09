using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    [DefaultExecutionOrder(-1000)]
    public class RemoveAtRuntime : MonoBehaviour
    {
        public bool scriptEnabled = true;
        private void Awake()
        {
            if(scriptEnabled)
                Destroy(gameObject);
        }
    } 
}
