using UnityEngine;
using UnityEngine.UI;

namespace MVCTest.Player
{
   public class PlayerView : MonoBehaviour
   {
      //一样，获取所有UI组件
      [SerializeField] private Image hpBar;


      public void UpdateUI(PlayerModel model)
      {
         hpBar.fillAmount = model.CurrentHp / model.MaxHp;
      }
   
   }
}
