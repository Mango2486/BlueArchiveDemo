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
            enemyModel = new EnemyModelTest(enemyData);
            enemyView = GetComponentInChildren<EnemyViewTest>();
        }

        private void Start()
        {
            enemyModel.Actions += OnEnemyHit;
        }


        private void OnDisable()
        {
            enemyModel.Actions -= OnEnemyHit;
        }
        
        private void OnEnemyHit(EnemyModelTest enemyModel)
        {
            enemyView.UpdateUI(enemyModel);
        }

        public void Hit()
        {
            enemyModel.Hit();
        }

        public bool EnemyDie()
        {
            return enemyModel.CurrentHealth == 0;
        }
        
    }

}
