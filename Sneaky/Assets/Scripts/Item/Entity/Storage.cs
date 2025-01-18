using UnityEngine;
using UnityEngine.EventSystems;

public class Storage : MonoBehaviour, IPointerClickHandler
{

    private int level = GameManager.Instance.CurrentLevel;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (level != 3)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if(PlayerController.Instance.playerBag.checkFood())
            {
                gameObject.SetActive(false);
            }
        }
    }

 
}
