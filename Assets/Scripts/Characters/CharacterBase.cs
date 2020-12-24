using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
//seleccionara siempre al padre por default
[SelectionBase]
//permite que las funciones trabajen en el modo editor
[ExecuteInEditMode]
public class CharacterBase : MonoBehaviour
    {
        public HealthBar healthbar;
        private void Start() 
        {
            /* maxHealth = SetMaxHealthFromHealthLevel(); */
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth); 
        }
        private void Update()
        {
            LimitLife();
            /* SetMaxHealthFromHealthLevel(); */
        }
        /* private float SetMaxHealthFromHealthLevel()
        {
            maxHealth = CharacterLevel * 10;
            return maxHealth;
        }  */
        public void TakeDamage (float negativeDamage)
        {
            currentHealth = currentHealth - negativeDamage;
            healthbar.SetCurrentHealth(currentHealth);
        }
        public void HealDamage (float positiveDamage)
        {
            if (currentHealth != 100)
            {
                currentHealth = currentHealth + positiveDamage;
                healthbar.SetCurrentHealth(currentHealth);
            }
            else{}
        }
        public void LimitLife()
        {
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else{}
        }

        [TextArea(1, 1)]
            public string Anotaciones = "Se puede clickear el script en si para acceder a sus funciones extras.";
        [Header("Basic Character Stats")]
        [Range(0, 100)]
            [Tooltip("Level Character")]
        public float CharacterLevel = 1;
        [Range(0, 200)]
        [Tooltip("Vida del NPC")]
            public float maxHealth;
        [Range(0, 200)]
        [Tooltip("Vida actual NPC")]
            public float currentHealth;

        [Range(0, 100)]
        [Tooltip("Mana del NPC")]
           public float mana;
        [Range(0, 100)]
        [Tooltip("Energia del NPC")]
            public float energy;
        [Range(0, 100)]
        [Tooltip("Armadura del NPC")]
            public int armor;
        [Range(0, 100)]
        [Tooltip("Nivel del NPC")]
            public int level;
        [Tooltip("Se le puede atacar al NPC aliado?")]
            public bool friendlyFire;
        [Tooltip("El NPC esta muerto?")]
            public bool death;
        [Tooltip("Se le puede matar?")]
            public bool Inmortal;
    [Header("Estados de Alerta, Tipos de Elementos y Tipos de Daño")]
        [SerializeField]
        [Tooltip("Estado de alerta del NPC")]
            private StatusAlert behaviour;
        [SerializeField]
        [Tooltip("Que tipo de NPC elemental es")]
            private TypeOfElemental elementalType;
        [SerializeField]
        [Tooltip("Tipo de daño sufrido al NPC")]
            private TypeOfDamage damageType;
        /*
        -   En normal el personaje estara al 100% de su vida y fuera del estado de combate.
        -   En upset el personaje estara sospechoso buscando al enemigo.
        -   En fighting el personaje estara intentado golpear al enemigo.
        */
        public enum StatusAlert
        {
            normal, upset, fighting
        }
        /*
        - En normal el personaje solo sufre daño basico.
        - En burn el personaje se estara quemando sufriendo daño por segundo.
        - En cold el personaje no se movera.
        - En electricity el personaje no se movera y recibira daño por segundo.
        - En poison el personaje recibira daño por segundo.
        - En blind el personaje no podra ver.
        - En bleed el personaje recibira daño si se mueve.
        - En stun el personaje estara inabilitado para hacer cualquier accion.
        - En confused el personaje tendra los botones alterados.

        */
        public enum TypeOfDamage
        {
            normal, burn, cold, electricity, posion, blind, bleed, stun, confused
        }
        /*
        - En normal el personaje no tiene ningun elemento.
        - En fire el personaje es invulnerable a los daño tipo fuego.
        - En water el personaje es invulnerable a los daño tipo agua.
        - En wind el personaje es invulnerable a los daño tipo viento.
        - En rock el personaje es invulnerable a los daño tipo tierra.
        - En light el personaje es invulnerable a los daño tipo luz.
        - En darkness el personaje es invulnerable a los daño tipo oscuridad.
        */
        public enum TypeOfElemental
        {
            normal, fire, water, wind, rock, light, darkness
        }
        [ContextMenu("Ejecutar ResetPositionChacter")]
        public void ResetPositionChacter()
        {
            transform.position = Vector3.zero;
        }
    }
}