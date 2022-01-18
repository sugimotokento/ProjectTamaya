using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelpAction : MonoBehaviour
{
    private int EnemyCnt;
    private bool AllFalse;
    private int FalseCnt;

    // Start is called before the first frame update
    void Start()
    {
        EnemyCnt = transform.childCount;
        FalseCnt = EnemyCnt;
        AllFalse = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool help;
        FalseCnt = 0;
        EnemyCnt = transform.childCount;
        for (int i = 0; i < EnemyCnt; i++)
        {
            help = transform.GetChild(i).gameObject.GetComponent<Enemy>().isHelp;
            if (AllFalse == true)
            {
                if (help == true)
                {
                    for (int j = 0; j < EnemyCnt; j++)
                    {
                        transform.GetChild(j).gameObject.GetComponent<Enemy>().isHelp = true;
                    }
                    i = EnemyCnt;
                    AllFalse = false;
                }
            }
            else
            {
                if (help == false)
                {
                    for (int j = 0; j < EnemyCnt; j++)
                    {
                        transform.GetChild(j).gameObject.GetComponent<Enemy>().isHelp = false;
                    }
                    AllFalse = true;

                    FalseCnt++;
                    if (FalseCnt == EnemyCnt)
                    {
                        AllFalse = true;
                    }
                }
            }
        
        }
    }
}
