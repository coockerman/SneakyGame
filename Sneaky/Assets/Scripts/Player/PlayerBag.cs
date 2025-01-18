using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    public List<SOIngredient> listIngredientInBag = new List<SOIngredient>();
    public bool checkFood()
    {
        if (GameManager.Instance.targetFood.ingredientList.Count <= 0)
        {
            Debug.Log("food khong can nguyen lieu");
            return false;
        }
        foreach (SOIngredient item in GameManager.Instance.targetFood.ingredientList)
        {
            if (!PlayerController.Instance.playerBag.listIngredientInBag.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
}
