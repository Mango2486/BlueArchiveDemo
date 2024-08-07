using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVCTest.Player
{
   public class PlayerView : MonoBehaviour
   {
      //一样，获取所有UI组件
      [Header("角色状态条")]
      [SerializeField] private Image hpBar;
      [SerializeField] private Image expBar;
      [SerializeField] private TextMeshProUGUI expText;

      public void UpdateHpUI(PlayerModel model)
      {
         hpBar.fillAmount = model.CurrentHp / model.PropDictionary["MaxHp"];
      }

      public void UpdateExpUI(PlayerModel model)
      {
         expBar.fillAmount = model.Exp / model.MaxExp[model.Level];
         //等级提升了或者是1级初始化则更新等级
         if (int.Parse(expText.text) < model.Level || model.Level == 1)
         {
            expText.text = $"{model.Level}";
            //呼出Buff界面
            if (model.Level > 1)
            {
              BuffUIController.Instance.ShowBuffUI();
            }
         }
      }
   }
}
