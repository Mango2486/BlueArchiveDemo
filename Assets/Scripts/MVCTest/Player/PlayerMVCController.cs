using System;
using UnityEngine;

namespace MVCTest.Player
{
    public class PlayerMVCController : MonoBehaviour
    {
        //装载数据的SO
        [SerializeField] private PlayerData playerData;
        [SerializeField]private PlayerView playerView;
        private PlayerModel playerModel;
        
        //因为是玩家UI,不存在说像对象池那样的使用场景，所以不需要OnEable这些。

        private void Awake()
        {
            InitializePlayerModel();
        }

        private void Start()
        {
            playerModel.Actions += OnPlayerHit;
        }

        
        private void OnDestroy()
        {
            playerModel.Actions -= OnPlayerHit;
        }

        private void OnPlayerHit(PlayerModel playerModel)
        {
            playerView.UpdateUI(this.playerModel);
        }
        private void InitializePlayerModel()
        {
            playerModel = new PlayerModel(playerData);
            playerView.UpdateUI(playerModel);
        }

        public void GetHit(float atk)
        {
            playerModel.GetHit(atk);
        }
    }
}
