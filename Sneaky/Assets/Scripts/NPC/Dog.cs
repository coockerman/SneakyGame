using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] float availableDistance;
    [SerializeField] float maxTiming = 100;
    [SerializeField] float countTiming = 0;
    [SerializeField] float decreaseTiming;
    [SerializeField] float increaseTiming;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.keyIsPressed == true && checkNear() == true)
        {
            countTiming += Time.deltaTime * increaseTiming;
            if (countTiming > maxTiming)
            {
                GameManager.Instance.SetEndGame();
            }
            UIManager.Instance.UpdateSliderDog(countTiming / maxTiming);
            //slide 
        }
        else
        {
            if (countTiming > 0)
            {
                UIManager.Instance.UpdateSliderDog(countTiming / maxTiming);
                countTiming -= Time.deltaTime * decreaseTiming;
            }
            else
            {
                countTiming = 0;
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

}
