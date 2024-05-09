using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] Collider2D deadlyTrigger;
        [SerializeField] Transform beam;

        public void TurnOn()
        {
            deadlyTrigger.enabled = true;
            beam.gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            deadlyTrigger.enabled = false;
            beam.gameObject.SetActive(false);
        }
    }
}
