using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

namespace SpaceDrifter2D
{
    public class PoolParticleManager : PoolManager<ParticleSystem>
    {
        //[SerializeField] private PrefabToPool[] pools;

        ////private Dictionary<Type, Pool<TPool>> typeToPool = new Dictionary<Type, Pool<TPool>>();

        ////protected override void Awake()
        ////{
        ////    base.Awake();

        ////    foreach(var p in pools)
        ////    {
        ////        typeToPool.Add( p.Prefab.GetType(), p.Pool );
        ////    }
        ////}

        ////public TPool GetInstance( Type emitterType )
        ////{
        ////    return typeToPool[emitterType].GetInstance();
        ////}

        //[Serializable]
        //public class PrefabToPool
        //{
        //    public Pool<TPool> Pool => pool;
        //    public ParticleEmmiter Prefab => prefab;

        //    [SerializeField] private Pool<TPool> pool;
        //    [SerializeField] private ParticleEmmiter prefab;
        //}
    } 
}
