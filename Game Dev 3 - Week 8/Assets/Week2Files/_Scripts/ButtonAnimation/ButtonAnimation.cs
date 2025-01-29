using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject settingsMenu;
    public Vector3 originalScale;
    public Vector3 hoverScale;

    private bool wasClicked = false;

    private void Start()
    {
        originalScale = transform.localScale;
        hoverScale = originalScale * 1.2f; // Scaling factor 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animate button scale when mouse hovers over


        transform.localScale = hoverScale;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore original button scale when mouse exits


        transform.localScale = originalScale;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Restore original button scale when clicked
        transform.localScale = originalScale;

    }


}
