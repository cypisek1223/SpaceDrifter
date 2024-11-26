using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScen : MonoBehaviour
{
   public void EndAnimation()
    {
        SceneManager.LoadScene("Initialisation");
    }
}
