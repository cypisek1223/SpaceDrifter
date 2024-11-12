using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    public class FuelBehaviour : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float fuel;

        public float Fuel { get => fuel; set => fuel = value; }
        public Image fuelBar;
        [SerializeField] float fuelDecreaseInterval;
        [SerializeField] bool taking = false;
        private void Update()
        {
            fuelBar.fillAmount = fuel;
            if (taking)
                WaistingFuel(fuel, fuelDecreaseInterval);
        }
        public void TakingFuel()
        {
            taking = true;
        }

        public void StopTanking()
        {
            taking = false;
        }
        public void AddedFuel(float power, float time)
        {
            fuel += power;
            fuel = Mathf.Clamp(fuel, 0, 1);
        }
        public void WaistingFuel(float amountFuel, float _fuelDecreaseInterval)
        {

            float fuelToDecrease = 1 * _fuelDecreaseInterval;
            amountFuel -= fuelToDecrease;
            amountFuel = Mathf.Max(amountFuel, 0);

            fuel = Mathf.Clamp(amountFuel, 0, 1);
        }
    }
}