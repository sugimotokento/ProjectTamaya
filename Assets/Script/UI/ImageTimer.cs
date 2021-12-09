using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour {
    private enum TimeIndex {
        MINUTE,
        SECOND,
        MILLISECOND,
        MAX,
    }

    [SerializeField] private ImageNumber[] imageNumbers;
    [SerializeField] private Image[] coronImage = new Image[2];
    [SerializeField] float time;
    [SerializeField] int space;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //ï™ÅAïbÅAè≠êîÇ≤Ç∆Ç…ï™ÇØÇÈ
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int decimals = (int)((time - Mathf.FloorToInt(time)) * 100);

        imageNumbers[(int)TimeIndex.MINUTE].transform.position = this.transform.position;
        imageNumbers[(int)TimeIndex.MINUTE].SetNumber(minutes);
        imageNumbers[(int)TimeIndex.MINUTE].SetSpace(space);

        coronImage[0].transform.position = this.transform.position+ Vector3.right * 2 * space;

        imageNumbers[(int)TimeIndex.SECOND].transform.position = this.transform.position+ Vector3.right * 3 * space;
        imageNumbers[(int)TimeIndex.SECOND].SetNumber(seconds);
        imageNumbers[(int)TimeIndex.SECOND].SetSpace(space);

        coronImage[1].transform.position = this.transform.position+ Vector3.right * 5 * space;

        imageNumbers[(int)TimeIndex.MILLISECOND].transform.position = this.transform.position+ Vector3.right * 6 * space;
        imageNumbers[(int)TimeIndex.MILLISECOND].SetNumber(decimals);
        imageNumbers[(int)TimeIndex.MILLISECOND].SetSpace(space);
    }

    public void SetTime(float t) {
        time = t;
    }
    public void SetSpace(int s) {
        space = s;
    }
}
