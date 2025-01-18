using UnityEngine;

public class Dad : MonoBehaviour
{
    [SerializeField] float maxTiming = 100;
    [SerializeField] float countTiming = 0;
    [SerializeField] float decreaseTiming = 0.5f;
    [SerializeField] float increaseTiming = 2f;
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
            //slide 
        }
        else
        {
            countTiming -= Time.deltaTime * decreaseTiming;
            if(countTiming < 0)
            {
                countTiming = 0;
            }
        }
    }
}
