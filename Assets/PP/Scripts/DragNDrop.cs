using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform inspectArea;

    private float xMinBound;
    private float xMaxBound;
    private float yMinBound;
    private float yMaxBound;

    public UnityEvent OnInspectTableEnter;
    public UnityEvent OnInspectTableExit;

    private bool onTable;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        var inspectGO = GameObject.FindGameObjectWithTag("InspectAreaPP");
        inspectArea = inspectGO.GetComponent<RectTransform>();
        
        var pos = inspectArea.position;
        var halfWidth = inspectArea.rect.width / 2;
        var halfHeight = inspectArea.rect.height / 2;
        Debug.Log(pos);
        Debug.Log(halfWidth);
        Debug.Log(halfHeight);

        // x min bound = x position - width / 2
        // x max bound = x position + width / 2
        // y min bound = y position - height / 2
        // y max bound = y position + height / 2

        xMinBound = pos.x - halfWidth;
        xMaxBound = pos.x + halfWidth;
        
        yMinBound = pos.y - halfHeight;
        yMaxBound = pos.y + halfHeight;

        Debug.Log(new Vector2(xMinBound, yMinBound));
        Debug.Log(new Vector2(xMaxBound, yMaxBound));

    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        //transform.SetAsFirstSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        var position = eventData.position;

        var withinBounds = WithinBounds(position);
        Debug.Log(withinBounds);

        switch (withinBounds)
        {
            case true when !onTable:
                onTable = true;
                OnInspectTableEnter.Invoke();
                break;
            case false when onTable:
                onTable = false;
                OnInspectTableExit.Invoke();
                break;
        }
        
        /*if (withinBounds && !isPassportOpen)
        {
            OpenPassport();
        }
        else if (!withinBounds && isPassportOpen)
        {
            ClosePassport();
        }*/
    }

    private bool WithinBounds(Vector2 pos)
    {
        return (pos.x > xMinBound
            && pos.x < xMaxBound
            && pos.y > yMinBound
            && pos.y < yMaxBound
        );
    }

   
}
