using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class FlyLateStart : MonoBehaviour
    {

        [SerializeField] private GameObject startPanel;
        [SerializeField] private TextMeshProUGUI startText;

        //private void Start()
        //{
        //    CancelCountDown();
        //}

        public void CancelCountDown()
        {
            StopAllCoroutines();
            startPanel.SetActive(false);
        }

        public void StartCountDown(Action callback)
        {
            startPanel.SetActive(true);
            StartCoroutine(CountDown(3, callback));
        }

        IEnumerator CountDown(int seconds, Action callback)
        {
            startText.text = seconds.ToString();
            yield return new WaitForSeconds(1.75f);

            while (seconds > 0)
            {
                startText.text = (--seconds).ToString();
                yield return new WaitForSeconds(1);
            }

            startPanel.SetActive(false);
            callback?.Invoke();
        }
    } 
}
