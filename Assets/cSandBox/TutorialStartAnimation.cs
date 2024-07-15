using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartAnimation : MonoBehaviour
{
    
    public void BeforAnimation()
    {
        TutorialManager.tutorialManager.BeforeMenuAnimation();       
    }
}
