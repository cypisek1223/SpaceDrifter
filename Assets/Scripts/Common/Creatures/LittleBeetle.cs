using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LittleBeetle : MonoBehaviour
    {
        public float speed = 1;
        public Vector2 min_max_state_duration = new Vector2(1,5);
        Rigidbody2D rb;
        SpriteRenderer s_renderer;
        Animator animator;
        Collider2D colider;
       
        CreatureState state;
        float state_duration;
        float state_end;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            s_renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            colider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            switch (state)
            {
                case CreatureState.Idle:
                    break;
                case CreatureState.Wander:
                    Wander();
                    break;
            }

            state_duration += Time.deltaTime;
            if(state_duration >= state_end)
            {
                ChangeStateRandomly();
            }
        }

        void ChangeStateRandomly()
        {
            state_duration = 0;
            state_end = Random.Range(min_max_state_duration.x, min_max_state_duration.y);

            int dice = Random.Range(0, 2);
            state = (CreatureState)dice;
            animator.speed = dice; // idle - stop animation, walk - play animation

            dice = Random.Range(0, 2);
            left = dice == 1;
        }

        bool left;
        RaycastHit2D[] ground_info = new RaycastHit2D[1];
        ContactFilter2D no_trigger_filter = new ContactFilter2D() { useTriggers = false };
        void Wander()
        {
            int dir = left ? -1 : 1;
            //rb.MovePosition(rb.position + Vector2.right * speed * Time.deltaTime * dir);
            transform.Translate(Vector2.right * speed * Time.deltaTime * dir);
            s_renderer.flipX = !left;

            if(colider.Raycast(Vector2.down, no_trigger_filter, ground_info, colider.bounds.extents.y + 0.5f) > 0)
            {
                transform.up = ground_info[0].normal;
            }
        }

        enum CreatureState { Idle, Wander }
    }
}
