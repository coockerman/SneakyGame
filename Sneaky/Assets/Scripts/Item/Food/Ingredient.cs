using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SOIngredient dataIngredient;

    

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = dataIngredient.spriteIngredient;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // add ingredient to bag
    }
}
