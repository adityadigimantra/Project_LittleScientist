using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Copied_DragNDrop : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    //This Object Handles the Tap and Drag of Copied Elements 

    [Header("Element Data")]
    public string currentElementName;
    public GameObject thisObject;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public RectTransform ClampPanelRectTransform;
    public Vector2 InitialPosition;
    public Vector2 FinalPosition;
    public GameObject otherGameObject;

    private void Start()
    {
        currentElementName = gameObject.name;
        thisObject = this.gameObject;
        rectTransform = thisObject.GetComponent<RectTransform>();
        canvasGroup = thisObject.GetComponent<CanvasGroup>();
        LoadSetImageforthisElement();
        ClampPanelRectTransform = GameObject.Find("Boundary").GetComponent<RectTransform>();

    }
    public void LoadSetImageforthisElement()
    {
        string imagePath = "Elements/" + currentElementName;
        Sprite image = Resources.Load<Sprite>(imagePath);
        gameObject.transform.GetChild(2).GetComponent<Image>().sprite = image;
        gameObject.transform.GetChild(2).GetComponent<Image>().preserveAspect = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
 
        if (gameObject.tag=="NewCreatedElement")
        {
            gameObject.GetComponent<ElementPosition>().GetInitialPos(gameObject.transform.position);
        }
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Copied-On Pointer Up Called");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (gameObject.tag == "NewCreatedElement")
        {
            gameObject.GetComponent<ElementPosition>().GetFinalPos(gameObject.transform.position);
        }
        StartCombinationProcess();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Copied-On Drag Called");
        Vector2 mousePosition = eventData.position;
        Vector2 localPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ClampPanelRectTransform, mousePosition, eventData.pressEventCamera, out localPosition))
        {
            localPosition.x = Mathf.Clamp(localPosition.x, ClampPanelRectTransform.rect.min.x + thisObject.GetComponent<RectTransform>().rect.width, ClampPanelRectTransform.rect.max.x - thisObject.GetComponent<RectTransform>().rect.width);
            localPosition.y = Mathf.Clamp(localPosition.y, ClampPanelRectTransform.rect.min.y + thisObject.GetComponent<RectTransform>().rect.height, ClampPanelRectTransform.rect.max.y - thisObject.GetComponent<RectTransform>().rect.height);
            thisObject.GetComponent<RectTransform>().anchoredPosition = localPosition;
        }
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
    }
    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            return canvas.scaleFactor;
        else
            return 1f;
    }



    public void StartCombinationProcess()
    {
        Debug.Log("Element Creation Starting");

        if(gameObject.GetComponent<Element>().isCollided)
        {
            FindObjectOfType<combinationManager>().HandleCombination(gameObject.GetComponent<Element>().thisElementName, gameObject.GetComponent<Element>().OtherElementName);
            FindObjectOfType<ElementManager>().SetParentElements(this.gameObject, otherGameObject);
            if(PlayerPrefs.GetInt("ElementAlreadyPresent")==1)
            {
                //Need to play the Animation to the other gameobject not the one which is pointer.
                gameObject.GetComponent<WiggleAnimation>().startShake();
                StartCoroutine(MakeActiveExitsElement());
            }

            else if(PlayerPrefs.GetInt("NoCombinationFound") ==1)
            {
                otherGameObject.GetComponent<WiggleAnimation>().startShake();
                PlayerPrefs.SetInt("NoCombinationFound", 0);
                StartCoroutine(StartIfNoCombinationExists());
            }

        }

    }

    IEnumerator StartIfNoCombinationExists()
    {
        yield return new WaitForSeconds(1f); 
        otherGameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator MakeActiveExitsElement()
    {
        string imagePath = "Elements/" + PlayerPrefs.GetString("AlreadyPresentElement");
        Sprite image = Resources.Load<Sprite>(imagePath);
        otherGameObject.transform.GetChild(1).gameObject.SetActive(true);
        otherGameObject.transform.GetChild(1).GetComponent<Image>().sprite = image;
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("ElementAlreadyPresent", 0);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        otherGameObject.transform.GetChild(0).gameObject.SetActive(false);
        otherGameObject.transform.GetChild(1).gameObject.SetActive(false);

    }

    public void GetAnotherGameObject(GameObject other)
    {
        otherGameObject = other;

    }
}

