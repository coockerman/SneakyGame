using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SOIngredient dataIngredient;
    [SerializeField] float availableDistance;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = dataIngredient.spriteIngredient;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (checkNear() == true)
        {
            PlayerController.Instance.playerBag.listIngredientInBag.Add(dataIngredient);
            gameObject.SetActive(false);
        }
        else
        {
            //text dialogue: not enough distance
        }
    }

    bool checkNear()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position);
        
        if (distance < availableDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
