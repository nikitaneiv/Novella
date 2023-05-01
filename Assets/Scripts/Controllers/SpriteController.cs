using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    
    [SerializeField] private Image sprite;
    
    private RectTransform rect;
    private Animator animator;
    
    private const string HIDE = "HideSpeaker";
    private const string SHOW = "ShowSpeaker";
    
    private float durationRotate = 0.1f;

    private readonly static Vector3 seeRight = new Vector3(0, 180, 0);
    private readonly static Vector3 seeLeft = new Vector3(0, 0, 0);
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    public void Show(Vector2 coords)
    {
        animator.SetTrigger(SHOW);
        rect.localPosition = coords;
    }

    public void Hide()
    {
        animator.SetTrigger(HIDE);
    }

    public void RotationSpriteRight()
    {
        sprite.rectTransform.DOLocalRotate(seeRight, durationRotate, RotateMode.Fast);
    }
    public void RotationSpriteLeft()
    {
        sprite.rectTransform.DOLocalRotate(seeLeft, durationRotate, RotateMode.Fast);
    }

    public void Move(Vector2 coords, float speed )
    {
        StartCoroutine(MoveCoroutine(coords, speed));
    }

    private IEnumerator MoveCoroutine(Vector2 coords, float speed)
    {
        while (rect.localPosition.x != coords.x || rect.localPosition.y != coords.y)
        {
            rect.localPosition = Vector2.MoveTowards(rect.localPosition, coords, Time.deltaTime * 1000f * speed);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
