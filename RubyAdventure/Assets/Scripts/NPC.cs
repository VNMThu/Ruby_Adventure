using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject diablogBox;
    private float timeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        diablogBox.SetActive(false);
        timeDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeDisplay >= 0 ){
            timeDisplay -= Time.deltaTime;
            if(timeDisplay < 0){
                diablogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog (){
        timeDisplay = displayTime;
        diablogBox.SetActive(true);
    }
}
