using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TitleController : MonoBehaviour
{
    public static TitleController instance;
    public string btnChoice;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        } 
    }    




}
