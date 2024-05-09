using UnityEngine;

namespace SpaceDrifter2D
{
    public class DoubleGate : PoolEmitter
    {
        [SerializeField] protected SpriteRenderer sprite;

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
                ps.GetComponent<TextMeshToParticleMaterial>().SetText("+" + score);
                ps.transform.localPosition = transform.TransformPoint(localPos);
                ps.Play();

                //Update Score
                ScoreKeeper.Instance.Gate();
            }
        }
    } 
}
