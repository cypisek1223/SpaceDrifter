using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SpriteScroller : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float speed = 0.1f;
        private void Update()
        {
            sprite.material.mainTextureOffset += Vector2.up * speed * Time.deltaTime;
        }
    } 
}
