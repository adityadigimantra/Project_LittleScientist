using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private RectTransform CopyObjRectTransform;
    private CanvasGroup CopyObjCanvasGroup;
    private CanvasGroup canvasGroup;
    public string currentElementName;
    public GameObject ElementsPanel;
    public GameObject parentGameObject;
    public GameObject copiedGameObject;
    private void Awake()
    {
        ElementsPanel = GameObject.Find("ElementsPanel");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    private void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.tag == "Original")
        {
            canvasGroup.alpha = 0.6f; // Adjust the transparency of the image when dragging
            canvasGroup.blocksRaycasts = false;
            copiedElementData();

        }
    }

    public void copiedElementData()
    {
        copiedGameObject = Instantiate(gameObject, transform.position, transform.rotation);
        copiedGameObject.transform.parent = ElementsPanel.transform;
        copiedGameObject.GetComponent<CanvasGroup>().alpha = 1f;
        copiedGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        copiedGameObject.GetComponent<BoxCollider2D>().enabled = true;
        copiedGameObject.name = gameObject.GetComponent<Element>().elementName;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
        if(copiedGameObject!=null)
        {
            copiedGameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GetCanvasScale();
        }
        //CopyObjRectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Reset the transparency of the image when dragging ends
        canvasGroup.blocksRaycasts = true;
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            return canvas.scaleFactor;
        else
            return 1f;
    }
}
