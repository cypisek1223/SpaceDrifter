using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    public class PlanetInfoPanel : SingleInstance<PlanetInfoPanel>
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject lockedPanel;

        int slideInHash = Animator.StringToHash("slideIn");

        public void SetPlanet(PlanetData planet)
        {
            nameText.text = planet.Name;
            nameText.color = planet.PrimaryColor;
            animator.SetTrigger(slideInHash);

            if (planet.Locked)
                lockedPanel.SetActive(true);
            else
                lockedPanel.SetActive(false);
        }
    } 
}
