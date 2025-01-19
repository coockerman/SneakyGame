using Spine.Unity;
using UnityEngine;

public class Dad : MonoBehaviour
{
    
    [SerializeField] float maxTiming = 100;
    [SerializeField] float countTiming = 0;
    [SerializeField] float decreaseTiming;
    [SerializeField] float increaseTiming;
    [SerializeField] private GameObject buble;
    [SerializeField] private SkeletonAnimation skeletonBuble;
    [SerializeField] private float maxZoomBuble = 2;
    [SerializeField] private float minZoomBuble = 1;
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
                skeletonBuble.AnimationState.SetAnimation(0, "animation2", false);
            } 
        }
        else if(PlayerController.Instance.keyIsPressed == false)
        {
            if(countTiming > 0)
            {               
                countTiming -= Time.deltaTime * decreaseTiming;
            }
            else
            {
                countTiming = 0;
            }
            
        }
        buble.transform.localScale = Vector3.one * (minZoomBuble + (maxZoomBuble - minZoomBuble) * (countTiming / maxTiming));
        UIManager.Instance.UpdateSliderDad(countTiming / maxTiming);
    }

    
}
