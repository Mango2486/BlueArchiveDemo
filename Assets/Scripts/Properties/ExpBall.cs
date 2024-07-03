using Data;
using UnityEngine;

namespace Properties
{
   public class ExpBall : MonoBehaviour
   {
       public PropertiesData data;

       public void SelfDestroy()
       {
           Destroy(gameObject);
       }
   }
}
