using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SpaceDrifter2D
{
    public class SplashManager : Singleton<SplashManager>
    {
        private List<Splash> splashes = new List<Splash>();

        public void ResetSplashes() //Must be invoked before level is loaded, bcuz new splashes register then
        {
            splashes.Clear();
        }

        public void PaintAllSplashes( Material mat, Color primCol, Color secndCol)
        {

            foreach(var s in splashes)
            {
                s.SetMaterial(mat);
                s.Paint(primCol, secndCol);
            }
        }

        public void Register(Splash splash)
        {
            splashes.Add(splash);
        }
    }
}
