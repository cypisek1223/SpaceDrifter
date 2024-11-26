using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AreaHandler : MonoBehaviour
{
    public float detectionRadius = 5f; // Promieñ obszaru
    public LayerMask layerMask1;
    public LayerMask layerMask2;
    public LayerMask layerMask3;

    private HashSet<Collider2D> currentObjects = new HashSet<Collider2D>();

    private void Update()
    {
        LayerMask detectionLayers = layerMask1 | layerMask2 | layerMask3;
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayers);


        foreach (var obj in detectedObjects)
        {
            if (!currentObjects.Contains(obj))
            {
                currentObjects.Add(obj); 
                OnObjectEnter(obj); 
            }
        }


        currentObjects.RemoveWhere(obj =>
        {
            if (System.Array.IndexOf(detectedObjects, obj) == -1) 
            {
                OnObjectExit(obj);
                return true; 
            }
            return false; 
        });
    }

    private void OnObjectEnter(Collider2D obj)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }

    }

    private void OnObjectExit(Collider2D obj)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // Wizualizacja obszaru w edytorze
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnApplicationQuit()
    {
        //ZROBIC W INNYM SKRYPCIE
        PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}_FirstTime", 0);
        PlayerPrefs.Save();
    }
}
