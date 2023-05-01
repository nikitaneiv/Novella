using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameScene currentScene;
    [SerializeField] private BottomBarController bottomBar;
    [SerializeField] private BackGroundController backGroundController;
    [SerializeField] private ChoseController choseController;
    [SerializeField] private AudioController audioController;

    private State state = State.IDLE;

    private enum State
    {
        IDLE,ANIMATE,CHOOSE
    }

    private void Start()
    {
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
            backGroundController.SetImage(storyScene.Background);
            PlayAudio(storyScene.Sentences[0]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    PlayScene((currentScene as StoryScene).NextScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                    PlayAudio((currentScene as StoryScene).Sentences[bottomBar.GetSentenceIndex()]);
                }
            }
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;
            backGroundController.SwitchImage(storyScene.Background);
            PlayAudio(storyScene.Sentences[0]);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            choseController.SetupChoose(scene as ChooseScene);
        }
    }

    private void PlayAudio(StoryScene.Sentence Sentence)
    {
        audioController.PlayAudio(Sentence.Music, Sentence.Sound);
    }
}