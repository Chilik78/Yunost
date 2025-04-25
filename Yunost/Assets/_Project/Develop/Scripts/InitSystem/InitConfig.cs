using System;
using UnityEngine;

namespace ProgressModul
{
    [Serializable]
    public struct ActData
    {
        public string act;
        public string name;
    }

    [Serializable]
    public struct NPCData { 
        public string name;
        public int loyalty;
    }

    [CreateAssetMenu(fileName = "InitConfig", menuName = "InitConfig")]
    public class InitConfig : ScriptableObject
    {

        //Инициализационные параметры
        [SerializeField] private int _hp = 100;
        [SerializeField] private int _stamina = 100;
        [SerializeField] private int _mainIGT = 0;
        [SerializeField] private int _sideIGT = 0;
        [SerializeField] private int _hours = 0;
        [SerializeField] private int _minutes = 0;
        [SerializeField] private ActData _levelPositions; //TODO: Заглушка, появится после LevelManager
        [SerializeField] private string _positionMark = "start_game";
        [SerializeField] private NPCData[] _npcsLoyality; //TODO: Заглушка, появится после NPCManager

        public int HP { get => _hp; set => _hp = value; }
        public int Stamina { get => _stamina; set => _stamina = value; }
        public int MainIGT { get => _mainIGT; set => _mainIGT = value; }
        public int SideIGT { get => _sideIGT; set => _sideIGT = value; }
        public int Hours { get => _hours; set => _hours = value; }
        public int Minutes { get => _minutes; set => _minutes = value; }
        public ActData LevelPositions { get => _levelPositions; set => _levelPositions = value; } //TODO: Заглушка, появится после LevelManager
        public string PositionMark { get => _positionMark; set => _positionMark = value; }
        public NPCData[] NPCSLoyality { get => _npcsLoyality; set => _npcsLoyality = value; } //TODO: Заглушка, появится после NPCManager
    }
}
