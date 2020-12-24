using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        private void Start() 
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxHealth(float maxaHealth)
        {
            slider.maxValue = maxaHealth;
            slider.value = maxaHealth;
        }
        public void SetCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}