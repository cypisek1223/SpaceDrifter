using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class ScrewButton : MonoBehaviour
    {
        public float speed = 1;
        
        public SpriteRenderer screw, screwGizmo;
        public ProgressBar progressBar;
        public UnityEvent onOpened;

        Rigidbody2D attachedPlayer;

        float rot;

        bool opened;

        private void Start()
        {
            progressBar.gameObject.SetActive(false);
        }

        private void Update()
        {
            if(attachedPlayer)
            {
                rot -= InputHandler.Instance.LeftRight * Time.deltaTime * speed;
                Quaternion currRot = Quaternion.Euler(0, 0, rot);
                screw.transform.rotation = currRot;
                attachedPlayer.rotation = rot;

                float open01 = Mathf.Abs(rot);
                progressBar.SetFill(open01 / 360);

                if(open01 >= 360)
                {
                    Detach();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (opened) return;
            if(collision.CompareTag("Player"))
            {
                attachedPlayer = collision.GetComponentInParent<Rigidbody2D>();
                Attach();
                rot = 0;
            }
        }


        public void Attach()
        {
            attachedPlayer.bodyType = RigidbodyType2D.Static;
            attachedPlayer.position = transform.position;
            LeanTween.scale(screwGizmo.gameObject, Vector3.one * 1.75f, 0.5f).setEase(LeanTweenType.easeInOutBounce);
            progressBar.gameObject.SetActive(true);
        }

        public void Detach()
        {
            progressBar.gameObject.SetActive(false);
            attachedPlayer.bodyType = RigidbodyType2D.Dynamic;
            attachedPlayer = null;
            opened = true;

            LeanTween.scale(screwGizmo.gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutBounce);

            onOpened.Invoke();
        }
    }
}
