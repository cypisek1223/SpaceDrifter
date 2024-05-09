using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class VelocityDisplay : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private TextMeshProUGUI displayVelText;
        [SerializeField] private TextMeshProUGUI displaySqrVelText;
        [SerializeField] private float frequency = 0.1f;
        [SerializeField] private bool disabled;

        private void OnEnable()
        {
            StopAllCoroutines();
            if (!disabled)
                StartCoroutine(DisplayVelocity());
        }

        private IEnumerator DisplayVelocity()
        {
            while (true)
            {
                displayVelText.text = rb.velocity.magnitude.ToString("N2");
                displaySqrVelText.text = rb.velocity.sqrMagnitude.ToString("N2");
                yield return new WaitForSeconds(frequency);
            }
        }
    } 
}
