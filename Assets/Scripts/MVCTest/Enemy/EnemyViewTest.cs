using UnityEngine;
using UnityEngine.UI;

namespace MVCTest.Enemy
{
   public class EnemyViewTest : MonoBehaviour
   {
      [SerializeField]private Image hpBar;

      private void Update()
      {  
         //TODO:保持血条位置不会随着人物旋转而旋转
         //暂时挂一个Update
         transform.rotation = Quaternion.identity;


      }

      public void UpdateUI(EnemyModelTest model)
      {
         // ReSharper disable once PossibleLossOfFraction
         hpBar.fillAmount = model.CurrentHp / model.MaxHp;
      }
   
  
   }
}
