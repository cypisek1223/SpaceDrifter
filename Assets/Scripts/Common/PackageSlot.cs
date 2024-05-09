using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class PackageSlot : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] SpriteRenderer doorSprite;
        [SerializeField] Renderer lightPicture;

        [Header("Settings")]
        [SerializeField] Transform targetPosition;
        [SerializeField] Sprite spriteClosed;
        [SerializeField] Color closedColor;
        [SerializeField] Sprite spriteOpen;
        [SerializeField] Color openColor;

        private bool open = true;

        private void Start()
        {
            //Close();
            LevelManager.Instance?.Register(this);
        }

        Transform package;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Package") && open)
            {
                package = collision.transform;
                LeanTween.move(collision.gameObject, targetPosition.position, 1).setEase(LeanTweenType.easeOutQuint);
                LeanTween.scale(collision.gameObject, Vector3.zero, 1).setEase(LeanTweenType.easeOutQuint).setOnComplete(DeliverPackage);
            }
        }

        private void DeliverPackage()
        {
            if (package)
            {
                Destroy(package.gameObject);
                package = null;
            }
            Close();
            LevelManager.Instance?.PackageDelivered(this);
        }

        public void Close()
        {
            open = false;

            doorSprite.sprite = spriteClosed;
            lightPicture.material.color = closedColor;
        }

        public void Open()
        {
            open = true;
            doorSprite.sprite = spriteOpen;
            lightPicture.material.color = openColor;
        }
    } 
}
