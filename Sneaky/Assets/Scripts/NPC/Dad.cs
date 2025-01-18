using UnityEngine;

public class Dad : MonoBehaviour
{
    
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
        if (PlayerController.Instance.keyIsPressed == true)
        {      
            countTiming += Time.deltaTime * increaseTiming;
            if(countTiming > maxTiming)
            {
                GameManager.Instance.SetEndGame();
            }
            UIManager.Instance.UpdateSliderDad(countTiming / maxTiming);
            //slide 
        }
        else if(PlayerController.Instance.keyIsPressed == false)
        {
            if(countTiming > 0)
            {
                UIManager.Instance.UpdateSliderDad(countTiming/maxTiming);
                countTiming -= Time.deltaTime * decreaseTiming;
            }
            else
            {
                countTiming = 0;
            }
            
        } 
    }
}
