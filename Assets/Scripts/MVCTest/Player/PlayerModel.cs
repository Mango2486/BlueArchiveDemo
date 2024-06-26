using Data;
using Unity.VisualScripting;
using UnityEngine;

namespace MVCTest.Player
{
    public class PlayerModel
    {
        public PlayerModel(PlayerData playerData)
        {
            SetData(playerData);
            Initialize();
        }

        public PlayerData playerData;
        
        public float MaxHp { get; private set; }
        
        public float CurrentHp { get; private set; }
        
        public float Shield { get; private set; }

        public float Armor { get; private set; }
        
        public float Speed { get; private set; }
        
        public float Atk { get; private set; }
        
        public float InvincibleTime { get; private set; }
        
        public float ShootInterval { get; private set; }

        private void Initialize()
        {
            MaxHp = playerData.maxHp;
            CurrentHp = MaxHp;
            Shield = playerData.shield;
            Armor = playerData.armor;
            Speed = playerData.speed;
            Atk = playerData.atk;
            InvincibleTime = playerData.invincibleTime;
            ShootInterval = playerData.shootInterval;
        }

        public delegate void UnityAction<PlayerModel>(PlayerModel playerModel);
        public event UnityAction<PlayerModel> Actions;

        private void UpdateInformation()
        {
            Actions?.Invoke(this);
        }

        public void GetHurt(float damage)
        {
            if (CurrentHp > 0)
            {
                CurrentHp -= damage;
                CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
            }
            UpdateInformation();
        }

        private void SetData(PlayerData playerData)
        {
            this.playerData = playerData;
        }
    }
}
