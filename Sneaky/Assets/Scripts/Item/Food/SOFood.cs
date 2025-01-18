using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "SOFood", menuName = "Scriptable Objects/SOFood")]
public class SOFood : ScriptableObject
{
    public List<SOIngredient> ingredientList;

}
