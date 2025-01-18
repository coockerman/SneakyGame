using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]protected string nameEntity;
    protected bool isOpened = false;
  
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void openEntity()
    {
       isOpened = true;
       
    }

}
