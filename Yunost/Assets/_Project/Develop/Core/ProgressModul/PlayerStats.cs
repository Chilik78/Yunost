using System;
using UnityEngine;

namespace ProgressModul
{
    public class PlayerStats : ISaveLoadObject
    {
        private int _health = 0;
        private int _stamina = 0;
        private int _maxHealth = 0;
        private int _maxStamina = 0; 
        private float _x = -1;
        private float _z = -1;
        private float _rotY = -1;

        public event Action HealthChanged;
        public event Action StaminaChanged;

        public PlayerStats(int health, int stamina, int maxHealth = 100, int maxStamina = 100)
        {
            _health = health;
            _stamina = stamina;
            _maxHealth = maxHealth;
            _maxStamina = maxStamina;
        }

        public PlayerStats() { }

        public float X
        {
            get => _x;
            set  {
                _x = value;
            }

        }

        public float Z
        {
            get => _z;
            set
            {
                _z = value;
            }
        }

        public float RotY
        {
            get => _rotY;
            set
            {
                _rotY = value;
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
            if (loadData?.Data == null || loadData.Data.Length < 5)
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
            get => _maxHealth;
        }

        public int MaxStamina
        {
            get => _maxStamina;
        }

        public int Health
        {
            get => _health;

            set
            {
                if (value >= 0 && value <= MaxHealth) 
                { 
                    _health = value;
                    if(HealthChanged != null)
                    {
                        HealthChanged();
                    }
                }
            }
        }

        public int Stamina
        {
            get => _stamina;

            set
            {
                if (value >= 0 && value <= MaxStamina)
                {
                    _stamina = value;
                    if (StaminaChanged != null)
                    {
                        StaminaChanged();
                    }
                }
            }
        }

        public string ComponentSaveId => "PlayerStats";

        public void ClearAllListeners()
        {
            foreach (Delegate d in HealthChanged.GetInvocationList())
            {
                HealthChanged -= (Action)d;
            }

            foreach (Delegate d in StaminaChanged.GetInvocationList())
            {
                StaminaChanged -= (Action)d;
            }

            Debug.Log("Удалены все слушателе с PlayerStats");
        }
    }
}


