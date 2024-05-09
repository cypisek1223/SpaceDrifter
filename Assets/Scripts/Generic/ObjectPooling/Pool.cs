using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public abstract class Pool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField] private int size;

        private Queue<T> queue;

        protected virtual void Start()
        {
            queue = new Queue<T>();
            for(int i=0; i<size; i++)
            {
                queue.Enqueue(Instantiate(prefab, transform));
            }
        }

        public T GetInstance()
        {
            var instance = queue.Dequeue();
            queue.Enqueue(instance);
            return instance;
        }

    } 
}
