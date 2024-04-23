using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text RobotTxt;
    public int robotCount;
    public GameObject[] robots;
    public GameObject[] items;
    bool isComplete = false;
    public event System.Action OnMissionComplete;

    // Start is called before the first frame update
    void Start()
    {

        robots = GameObject.FindGameObjectsWithTag("Enemy");
        items = GameObject.FindGameObjectsWithTag("Collectible");
        if (TitleController.instance.btnChoice == "Load")
        {
            LoadGame loadGame = FindObjectOfType<LoadGame>();
            SaveData missionData = loadGame.Load();
            if (missionData != null)
            {
                robotCount = missionData._robotCount;
                for (int i = 0; i < missionData.robotState.Length; i++)
                {
                    if (!missionData.robotState[i])
                    {
                        EnemyController enemyController = robots[i].GetComponent<EnemyController>();
                        enemyController.FixRobotOnly();
                    }
                }
                for (int i = 0; i < items.Length; i++)
                {
                    if (!missionData.itemState[i])
                    {
                        items[i].SetActive(false);
                    }
                }
            }
        }
        else
        {
            robotCount = robots.Length;
        }

        RobotTxt.text = robotCount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (robotCount <= 0 && !isComplete)
        {
            if (OnMissionComplete != null)
            {
                isComplete = true;
                OnMissionComplete();
            }

        }
    }

    public void ChangeRobotNum()
    {
        robotCount--;
        RobotTxt.text = robotCount.ToString();
    }
}
