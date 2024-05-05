using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private Collider2D _collider2D;

    private void Start()
    {
        gate.SetActive(false);
        _collider2D = GetComponent<Collider2D>();
    }

    //Trigger when ruby walk through gate
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Turn on gate to stop ruby from come back
        gate.SetActive(true);
        
        //Turn off trigger
        _collider2D.enabled = false;
        
        //Start Level
        GameManager.Instance.LevelController.StartLevel();
    }
}
