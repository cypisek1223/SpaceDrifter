using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResteStatistic : MonoBehaviour
{

    public bool resetStatistics;
    void Start()
    {
        if (resetStatistics)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }

}
