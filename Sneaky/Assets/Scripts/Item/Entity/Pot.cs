using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;



public class Pot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float availableDistance = 1f;
    [SerializeField] private float scaleNear = 1.2f;
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
        SoundManager.Instance.OnCookingSFX();
        yield return new WaitForSeconds(3f);

        PlayerController.Instance.FinishGoal();

        yield return null;

    }

}
