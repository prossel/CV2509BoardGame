using UnityEngine;

public class ActionButton : MonoBehaviour
{

    public Animator target;
    public string triggerName = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (target != null && triggerName != "")
        {
            target.SetTrigger(triggerName);
        }
    }
}
