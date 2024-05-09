using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpaceDrifter2D
{
    public class LevelTilemap : MonoBehaviour
    {
        public TilemapRenderer Tilemap => tilemap;
        private TilemapRenderer tilemap;

        private void Start()
        {
            tilemap = GetComponent<TilemapRenderer>();
            LevelManager.Instance.Register(this);
        }
    } 
}
