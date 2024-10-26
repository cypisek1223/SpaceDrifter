using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    [SerializeField] int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    private string sceneKey;
    private void Start()
    {
        sceneKey = $"{SceneManager.GetActiveScene().name}_FirstTime";

        if (PlayerPrefs.GetInt(sceneKey, 0) == 0)
        {

            PlayerPrefs.SetInt(sceneKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            //Debug.Log("Drugie odpalenie");
            //currentPage = 2;
            //targetPos += pageStep;
            //levelPagesRect.LeanMoveLocal(targetPos, 0.01f).setEase(tweenType);

        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(sceneKey, 0);
        PlayerPrefs.Save();
    }
    public void Next()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }
    public void Previous()
    {
        if (currentPage > 1)
        {
            targetPos -= pageStep;
            MovePage();
            currentPage--;           
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }
}
