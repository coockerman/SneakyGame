using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;



public class Pot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float availableDistance = 1f;
    public void OnPointerClick(PointerEventData eventData)
    {
       if(checkFood() && checkNear())
        {
            StartCoroutine(CoCoking());
        }
        else
        {
            if (!checkNear())
            {
                // text dialogue: not enough distance
            }else
            {
                // text dialogue: wrong food!
            }
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
    public bool checkFood()
    {
        return PlayerController.Instance.playerBag.checkFood();
    }

    private IEnumerator CoCoking()
    {

        //On animation cooking

        yield return new WaitForSeconds(3f);

        //change player Animation

        yield return null;

    }

}
