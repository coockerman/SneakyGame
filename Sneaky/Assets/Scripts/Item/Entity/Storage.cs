using UnityEngine;
using UnityEngine.EventSystems;

public class Storage : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);

    }

}
