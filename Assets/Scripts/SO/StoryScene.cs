using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/NewStoryScene")]
[System.Serializable]
public class StoryScene : GameScene
{
    [SerializeField] private Sprite background;
    [SerializeField] private GameScene nextScene;
    [SerializeField] private List<Sentence> sentences;
   
    
    [System.Serializable]
    public struct Sentence
    {
        [SerializeField] private string text;
        [SerializeField] private Speaker speaker;
        [SerializeField] private List<Action> actions;
        [SerializeField] private AudioClip music;
        [SerializeField] private AudioClip sound;
        
        [System.Serializable]
        public struct Action
        {
            [SerializeField] private Speaker speaker;
            [SerializeField] private Type actionType;
            [SerializeField] private Vector2 coords;
            [SerializeField] private float moveSpeed;
            
            [System.Serializable]
            public enum Type
            {
                NONE, APPEAR, DISAPPEAR, ROTATELEFT, ROTATERIGHT, MOVE
            }

            public Speaker Speaker => speaker;

            public Type ActionType => actionType;

            public Vector2 Coords => coords;

            public float MoveSpeed => moveSpeed;
        }

        public AudioClip Music => music;

        public AudioClip Sound => sound;

        public List<Action> Actions => actions;

        public Speaker Speaker => speaker;

        public string Text => text;
    }

    public Sprite Background => background;

    public GameScene NextScene => nextScene;

    public List<Sentence> Sentences => sentences;
}

public class GameScene : ScriptableObject { }
