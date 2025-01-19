using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SOIngredient dataIngredient;
    [SerializeField] float availableDistance;
    [SerializeField] private float scaleNear = 1.2f;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = dataIngredient.spriteIngredient;

    }

    private void Update()
    {
        if (checkNear())
        {
            gameObject.transform.localScale = new Vector3(scaleNear, scaleNear, scaleNear);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
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
