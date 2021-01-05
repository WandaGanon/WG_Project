using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG 
{
    public class PlayerDamage : MonoBehaviour
    {
        [Header("Damage Stats")]
        [Tooltip("Damage positivo o negativo")]
        public bool Positivo;
        [Tooltip("Damage constante")]
        public bool damageConstante;
        [Tooltip("Damage CoolDown")]
        public float damageCD = 1.0f;
        [Tooltip("Total Damage")]
        public int totalDamage = 25;
        
        [SerializeField]
        [Tooltip("Que tipo de NPC elemental es")]
            private TypeOfElemental elementalType;
        [SerializeField]
        [Tooltip("Tipo de daño sufrido al NPC")]
            private TypeOfDamage damageType;
        public enum TypeOfDamage
        {
            normal, burn, cold, electricity, posion, blind, bleed, stun, confused
        }
        public enum TypeOfElemental
        {
            normal, fire, water, wind, rock, light, darkness
        }
        
        float timer;
        bool playerInRange;
        
        /* ------ */
        private void Start()
        {
            Positivo = false;   
            damageConstante = false;
            playerInRange = false;
        }
        private void Update()
        {
            if(damageConstante == true)
            {
                timer += Time.deltaTime;
            }
            else {}
        }
        //CUANDO EL PERSONAJE ENTRA EN EL COLLIDER EL TIEMPO SE REINICIA, EL DAÑO ES DE UNA UNICA VEZ
        void OnTriggerEnter(Collider other) 
        {
            timer = 0f;
            playerInRange = true;
            CharacterBase _characterStats = other.GetComponent<CharacterBase>();
            if(damageConstante == false && playerInRange)
            {
                if (_characterStats != null)
                {
                    if (Positivo == false)
                    {
                        timer = 0f;
                        _characterStats.TakeDamage(totalDamage);
                    }
                    if (Positivo == true)
                    {
                        timer = 0f;
                        _characterStats.HealDamage(totalDamage);
                    }
                }
            }
        }
        //EL PERSONAJE SALE DEL TRIGGER POR LO QUE EL TIEMPO SE REINICIA
        void OnTriggerExit()
        {
            timer = 0f;
            playerInRange = false;
        }
        //MIENTRAS EL PERSONAJE ENTRE EN EL COLLIDER, EL FLOAT DE timer Y EL DE damageCD HARAN DAÑO 
        void OnTriggerStay(Collider other) 
        {
            CharacterBase _characterStats = other.GetComponent<CharacterBase>();
            if (_characterStats != null && playerInRange)
            {
                if(timer >= damageCD && playerInRange)
                {
                    if (Positivo == false)
                    {
                        timer = 0f;
                        _characterStats.TakeDamage(totalDamage);
                    }
                    if (Positivo == true)
                    {
                        timer = 0f;
                        _characterStats.HealDamage(totalDamage);
                    }
                }
            }
        }
        
    }
}
