using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ChoseLabelController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;
    
    private StoryScene scene;
    private TextMeshProUGUI textMesh;
    private ChoseController controller;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
    }
    
    public float GetHeight()
    {
        return textMesh.rectTransform.sizeDelta.y * textMesh.rectTransform.localScale.y;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChoseController controller, float y)
    {
        scene = label.NextScene;
        textMesh.text = label.Text;
        this.controller = controller;
    
        Vector3 position = textMesh.rectTransform.localPosition;
        position.y = y;
        textMesh.rectTransform.localPosition = position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.PerformChoose(scene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = defaultColor;
    }
}

