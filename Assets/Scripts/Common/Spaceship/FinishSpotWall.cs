using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class FinishSpotWall : FinishSpot
    {
        [Header("References")]
        [SerializeField] SpriteRenderer doorSprite;
        [SerializeField] Renderer lightPicture;

        [Header("Settings")]
        [SerializeField] Sprite spriteClosed;
        [SerializeField] Color closedColor;
        [SerializeField] Sprite spriteOpen;
        [SerializeField] Color openColor;

        private bool open;

        public override void Close()
        {
            base.Close();

            doorSprite.sprite = spriteClosed;
            lightPicture.material.color = closedColor;
        }

        public override void Open()
        {
            base.Open();

            doorSprite.sprite = spriteOpen;
            lightPicture.material.color = openColor;
        }
    } 
}
