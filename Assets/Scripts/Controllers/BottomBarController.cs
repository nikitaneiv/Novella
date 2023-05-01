using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI personText;
    [SerializeField] private TextMeshProUGUI personName;
    [SerializeField] private GameObject spritesPrefab;
    
    private StoryScene currentScene;
    private Animator animator;
    
    private State state = State.COMPLETED;
    
    private int sentenceIndex = -1;
    private bool isHidden = false;

    private const string HIDE = "Hide";
    private const string SHOW = "Show";
    
    private Dictionary<Speaker, SpriteController> sprites;
    
    private void Start()
    {
        sprites = new Dictionary<Speaker, SpriteController>();
        animator = GetComponent<Animator>();
    }

    private enum State
    {
        PLAYING,
        COMPLETED
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.Sentences[++sentenceIndex].Text));
        personName.text = currentScene.Sentences[sentenceIndex].Speaker.SpeakerName;
        personName.color = currentScene.Sentences[sentenceIndex].Speaker.TextColor;
        ActSpeakers();
    }

    private void ActSpeakers()
    {
        List<StoryScene.Sentence.Action> actions = currentScene.Sentences[sentenceIndex].Actions;
        for(int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i]);
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action)
    {
        SpriteController controller = null;
        switch (action.ActionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                if (!sprites.ContainsKey(action.Speaker))
                {
                    controller = Instantiate(action.Speaker.Prefab.gameObject, spritesPrefab.transform)
                        .GetComponent<SpriteController>();
                    sprites.Add(action.Speaker, controller);
                }
                else
                {
                    controller = sprites[action.Speaker];
                }
                controller.Show(action.Coords);
                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                if (sprites.ContainsKey(action.Speaker))
                {
                    controller = sprites[action.Speaker];
                    controller.Move(action.Coords, action.MoveSpeed);
                }
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                if (sprites.ContainsKey(action.Speaker))
                {
                    controller = sprites[action.Speaker];
                    controller.Hide();
                }
                break;
            case StoryScene.Sentence.Action.Type.NONE:
                if (sprites.ContainsKey(action.Speaker))
                {
                    controller = sprites[action.Speaker];
                }
                break;
            case StoryScene.Sentence.Action.Type.ROTATERIGHT:
                if (sprites.ContainsKey(action.Speaker))
                {
                    controller = sprites[action.Speaker];
                    controller.RotationSpriteRight();
                }
                break;
            case StoryScene.Sentence.Action.Type.ROTATELEFT:
                if (sprites.ContainsKey(action.Speaker))
                {
                    controller = sprites[action.Speaker];
                    controller.RotationSpriteLeft();
                }
                break;
        }
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }
    
    private IEnumerator TypeText(string text)
    {
        personText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            personText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.Sentences.Count;
    }

    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger(HIDE);
            isHidden = true;
        }
    }

    public void Show()
    {
        animator.SetTrigger(SHOW);
        isHidden = false;
    }

    public void ClearText()
    {
        personText.text = "";
        personName.text = "";
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }
}