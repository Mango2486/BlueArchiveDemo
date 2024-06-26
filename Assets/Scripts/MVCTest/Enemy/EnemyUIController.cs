using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using MVCTest.Enemy;
using UnityEngine;

namespace MVCTest
{
    public class EnemyUIController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        private EnemyViewTest enemyView;
        private EnemyModelTest enemyModel;
        
        public EnemyModelTest EnemyModel => enemyModel;

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
            return enemyModel.CurrentHp == 0;
        }
        
    }

}
