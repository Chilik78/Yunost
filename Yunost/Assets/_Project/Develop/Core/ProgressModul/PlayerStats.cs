using Global;
using System;

namespace ProgressModul
{
    public class PlayerStats
    {
        private int _health;
        private int _stamina;
        private int _maxHealth;
        private int _maxStamina;

        public event Action HealthChanged;
        public event Action StaminaChanged;

        public PlayerStats(int health, int stamina, int maxHealth = 100, int maxStamina = 100)
        {
            _health = health;
            _stamina = stamina;
            _maxHealth = maxHealth;
            _maxStamina = maxStamina;
        }

        public void hitHealth(int value)
        {
            Health = _health - value;
        }

        public void hitStamina(int value)
        {
            Stamina = _stamina - value;
        }

        public int Health
        {
            get => _health;

            set
            {
                if (_health >= 0 && value <= _maxHealth) 
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
                if (_stamina >= 0 && value <= _maxStamina)
                {
                    _stamina = value;
                    if (StaminaChanged != null)
                    {
                        StaminaChanged();
                    }
                }
            }
        }
    }
}


