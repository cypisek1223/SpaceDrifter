using UnityEngine;

namespace SpaceDrifter2D
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 axis;
        [SerializeField] private bool local = true;
        [SerializeField] private float speed = 1;

        private void Update()
        {
            transform.Rotate(axis, Time.deltaTime * speed, local ? Space.Self : Space.World);
        }
    } 
}
