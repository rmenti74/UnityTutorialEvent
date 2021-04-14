using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerBool() {
        EventManager.TriggerEvent<bool>("bool_trigger", true);
	}

    public void OnTriggerInt() {
        EventManager.TriggerEvent<int>("int_trigger", 555);
    }
}
