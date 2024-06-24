using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCTest
{
    public class EnemyUIController : MonoBehaviour
    {
        [SerializeField] private EnemyDataTest enemyData;
        private EnemyViewTest enemyView;
        private EnemyModelTest enemyModel;

        private void Awake()
        {
            enemyView = GetComponentInChildren<EnemyViewTest>();
        }

        private void OnEnable()
        {
            InitialEnemyModel();
            enemyModel.Actions += OnEnemyHit;
        }


        private void OnDisable()
        {
            enemyModel.Actions -= OnEnemyHit;
        }
        
        private void OnEnemyHit(EnemyModelTest enemyModel)
        {
            enemyView.UpdateUI(this.enemyModel);
        }

        private void InitialEnemyModel()
        {
            enemyModel = new EnemyModelTest(enemyData);
            enemyView.UpdateUI(enemyModel);
        }

        public void Hit( float atk)
        {
            enemyModel.Hit(atk);
        }

        public bool EnemyDie()
        {
            return enemyModel.CurrentHealth == 0;
        }
        
    }

}
