using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image eyeGauge;
    [SerializeField] private Image eyeFrame;
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

        eyeFrame.enabled = false;
        eyeGauge.enabled = false;
        eyeVigilant.enabled = false;
        eyeDanger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool help;
        help = transform.parent.parent.gameObject.GetComponent<Enemy>().isHelp;

        eyeGauge.fillAmount = Alertness / maxAlertness;

        if (Alertness >= 100)
        {
            eyeGauge.color = Color.red;

            eyeFrame.enabled = true;
            eyeGauge.enabled = true;
            eyeVigilant.enabled = false;
            eyeDanger.enabled = true;
        }
        else if (Alertness > 0 || help == true)
        {
            eyeGauge.color = Color.yellow;

            eyeFrame.enabled = true;
            eyeGauge.enabled = true;
            eyeVigilant.enabled = true;
            eyeDanger.enabled = false;
        }
        else
        {
            eyeFrame.enabled = false;
            eyeGauge.enabled = false;
            eyeVigilant.enabled = false;
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
