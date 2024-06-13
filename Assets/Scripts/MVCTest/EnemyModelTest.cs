using UnityEngine;

namespace MVCTest
{   
    //MVC中的Model，数据层，只存放有关数据。
    public class EnemyModelTest
    {

        public EnemyModelTest(EnemyDataTest enemyData)
        {
            SetEnemyData(enemyData);
            Initialize();
        }
        
        
        
        public EnemyDataTest enemyData;
        public float MaxHealth { get; private set; }

        public float CurrentHealth { get; private set; }
        
        //用于接受需要与数据联动的方法
        public delegate void UnityAction<EnemyModelTest>(EnemyModelTest enemyModel);
        public event UnityAction<EnemyModelTest> Actions;

        
        private void Initialize()
        {
            MaxHealth = enemyData.maxHealth;
            CurrentHealth = enemyData.currentHealth;
        }
        
        //当数据更新的时候，通知UI也更新
        private void UpdateInformation()
        {   
            //事件订阅了某些方法则执行。
            Actions?.Invoke(this);
        }
        
        public void Hit()
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= 10;
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            }
            UpdateInformation();
        }

        private  void SetEnemyData(EnemyDataTest enemyData)
        {
            this.enemyData = enemyData;
        }

    }
}