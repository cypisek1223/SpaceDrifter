using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SignLight : PoolEmitter
    {
        [SerializeField] protected SpriteRenderer sprite;
        public string onTouchText = "";
        
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //Count score and position
                int maxScore = 5;
                Vector2 localPos = transform.InverseTransformPoint(collision.transform.position);
                int score = maxScore - (int)Mathf.Round(Mathf.Abs(localPos.x));
                localPos.y = 0;

                //Spawn Paricles
                var ps = PoolParticleManager.Instance.GetInstance(this.GetType());
                ps.GetComponent<TextMeshToParticleMaterial>().SetText(onTouchText);
                ps.transform.position = transform.position;
                ps.Play();
            }

            if (collision.CompareTag("Player"))
            {
                sprite.color = Color.black;
            }
        }
    } 
}
