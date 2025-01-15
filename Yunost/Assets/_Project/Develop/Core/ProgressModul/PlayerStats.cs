using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressModul
{
    public class PlayerStats : ISaveLoadObject
    {
        private string _healthKey = "health";
        private string _staminaKey = "stamina";
        private string _maxHealthKey = "max_health";
        private string _maxStaminaKey = "max_stamina";
        private string _xKey = "x";
        private string _zKey = "z";
        private string _rotYKey = "rot_y";

        public event Action HealthChanged;
        public event Action StaminaChanged;

        public PlayerStats(int health, int stamina, int maxHealth = 100, int maxStamina = 100)
        {
            PlayerPrefs.SetInt(_healthKey, health);
            PlayerPrefs.SetInt(_staminaKey, stamina);
            PlayerPrefs.SetInt(_maxHealthKey, maxHealth);
            PlayerPrefs.SetInt(_maxStaminaKey, maxStamina);
            PlayerPrefs.SetFloat(_xKey, -1);
            PlayerPrefs.SetFloat(_zKey, -1);
            PlayerPrefs.SetFloat(_rotYKey, -1);
        }

        public PlayerStats() { }

        public float X
        {
            get => PlayerPrefs.GetFloat(_xKey);
            set  {
                PlayerPrefs.SetFloat(_xKey, value);
            }

        }

        public float Z
        {
            get => PlayerPrefs.GetFloat(_zKey);
            set
            {
                PlayerPrefs.SetFloat(_zKey, value);
            }
        }

        public float RotY
        {
            get => PlayerPrefs.GetFloat(_rotYKey);
            set
            {
                PlayerPrefs.SetFloat(_rotYKey, value);
            }
        }

        
        public void HitHealth(int value)
        {
            Health = Health - value;
        }

        public void HitStamina(int value)
        {
            Stamina = Stamina - value;
        }

        public SaveLoadData GetSaveLoadData()
        {
            return new PlayerStatsSaveLoadData(ComponentSaveId, Health, Stamina, X, Z, RotY);
        }

        public void RestoreValues(SaveLoadData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 4)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (field)
            // [1] - (filed)
            // [2] - (field)
            // [3] - (filed)
            // [4] - (filed)

            Health = int.Parse(loadData.Data[0].ToString());

            Stamina = int.Parse(loadData.Data[1].ToString());

            X = float.Parse(loadData.Data[2].ToString());

            Z = float.Parse(loadData.Data[3].ToString());

            RotY = float.Parse(loadData.Data[4].ToString());
        }

        public void SetDefault()
        {
          
        }

        public int MaxHealth
        {
            get => PlayerPrefs.GetInt(_maxHealthKey);
        }

        public int MaxStamina
        {
            get => PlayerPrefs.GetInt (_maxStaminaKey);
        }

        public int Health
        {
            get => PlayerPrefs.GetInt(_healthKey);

            set
            {
                if (Health >= 0 && value <= MaxHealth) 
                { 
                    PlayerPrefs.SetInt(_healthKey, value);
                    if(HealthChanged != null)
                    {
                        HealthChanged();
                    }
                }
            }
        }

        public int Stamina
        {
            get => PlayerPrefs.GetInt(_staminaKey);

            set
            {
                if (Stamina >= 0 && value <= MaxStamina)
                {
                    PlayerPrefs.SetInt(_staminaKey, value);
                    if (StaminaChanged != null)
                    {
                        StaminaChanged();
                    }
                }
            }
        }

        public string ComponentSaveId => "PlayerStats";
    }
}


