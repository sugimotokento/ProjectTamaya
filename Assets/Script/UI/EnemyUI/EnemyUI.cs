using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image eyeGauge;
    [SerializeField] private Image eyeVigilant;
    [SerializeField] private Image eyeDanger;

    private float maxAlertness = 100;
    private float Alertness;//Œx‰ú“x

    [SerializeField] private float add = 5;

    // Start is called before the first frame update
    void Start()
    {
        Alertness = 0;
        eyeGauge.color = Color.yellow;
        eyeVigilant.enabled = true;
        eyeDanger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        eyeGauge.fillAmount = Alertness / maxAlertness;

        if (Alertness >= 100)
        {
            eyeGauge.color = Color.red;
            eyeVigilant.enabled = false;
            eyeDanger.enabled = true;
        }
        else
        {
            eyeGauge.color = Color.yellow;
            eyeVigilant.enabled = true;
            eyeDanger.enabled = false;
        }
    }

    public void AddAlertness(float alert)
    {
        Alertness += alert;
    }

    public void SetAlertness(float alert)
    {
        Alertness = alert;
    }

    public void SetDanger()
    {
        Alertness = maxAlertness;
    }

    public float GetAlertness()
    {
        return Alertness;
    }
}
