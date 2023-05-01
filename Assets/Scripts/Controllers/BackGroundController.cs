using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
   [SerializeField] private Image backGround1;
   [SerializeField] private Image backGround2;

   [SerializeField] private Animator animator;

   private bool isSwitched = false;
   
   private const string SWITCHFIRST = "SwitchFirst";
   private const string SWITCHTWO = "SwitchTwo";
   
   public void SwitchImage(Sprite sprite)
   {
      if (!isSwitched)
      {
         backGround2.sprite = sprite;
         animator.SetTrigger(SWITCHFIRST);
      }
      else
      {
         backGround1.sprite = sprite;
         animator.SetTrigger(SWITCHTWO);
      }

      isSwitched = !isSwitched;
   }

   public void SetImage(Sprite sprite)
   {
      if (!isSwitched)
      {
         backGround1.sprite = sprite;
      }
      else
      {
         backGround2.sprite = sprite;
      }
   }
}
