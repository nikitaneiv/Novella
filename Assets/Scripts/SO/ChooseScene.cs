using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChooseScene", menuName = "Data/NewChooseScene")]
[System.Serializable]
public class ChooseScene : GameScene
{
   [SerializeField] private List<ChooseLabel> labels;
   
   [System.Serializable]
   public struct ChooseLabel
   {
      [SerializeField] private string text;
      [SerializeField] private StoryScene nextScene;

      public string Text => text;

      public StoryScene NextScene => nextScene;
   }

   public List<ChooseLabel> Labels => labels;
}
