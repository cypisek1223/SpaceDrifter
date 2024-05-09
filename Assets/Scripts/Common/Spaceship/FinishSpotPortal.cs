using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class FinishSpotPortal : FinishSpot
    {
        [SerializeField] OpenCloseAnimated portalGate;

        public override void Open()
        {
            base.Open();
            StartCoroutine(OpenGate());
        }

        IEnumerator OpenGate()
        {
            yield return new WaitForSeconds(0.1f);
            portalGate.Open();
        }
    } 
}
