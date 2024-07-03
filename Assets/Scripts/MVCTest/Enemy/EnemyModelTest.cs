using Data;
using UnityEngine;

namespace MVCTest
{   
    //MVC中的Model，数据层，只存放有关数据。
    public class EnemyModelTest
    {

        public EnemyModelTest(EnemyData enemyData)
        {
            SetEnemyData(enemyData);
            Initialize();
        }
        
        private EnemyData enemyData;
        public float MaxHp { get; private set; }

        public float CurrentHp { get; private set; }
        
        public float Atk { get; private set; }
        
        public float Speed { get; private set; }
        
        public float Armor { get; private set; }
        
        public float Shield { get; private set; }
        
        //用于接受需要与数据联动的方法
        public delegate void UnityAction<EnemyModelTest>(EnemyModelTest enemyModel);
        public event UnityAction<EnemyModelTest> Actions;

        
        private void Initialize()
        {
            MaxHp = enemyData.maxHp;
            CurrentHp = MaxHp;
            Atk = enemyData.atk;
            Speed = enemyData.speed;
            Armor = enemyData.armor;
            Shield = enemyData.shield;
        }
        
        //当数据更新的时候，通知UI也更新
        private void UpdateInformation()
        {   
            //事件订阅了某些方法则执行。
            Actions?.Invoke(this);
        }
        
        public void Hit()
        {
            if (CurrentHp > 0)
            {
                CurrentHp -= 20f;
                CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
            }
            UpdateInformation();
        }

        public void Hit(float damage)
        {
            if (CurrentHp > 0)
            {
                CurrentHp -= damage;
                CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
            }
            UpdateInformation();
        }

        private  void SetEnemyData(EnemyData enemyData)
        {
            this.enemyData = enemyData;
        }

    }
}