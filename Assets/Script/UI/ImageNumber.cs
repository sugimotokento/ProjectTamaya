using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour {
    [SerializeField] private Sprite[] numberSprite;
    [SerializeField] private Image numImage;
    [SerializeField] private float space = 75;
    [SerializeField] private int num = 0;
    [SerializeField] private int digit = 1;

    private List<Image> images = new List<Image>();
    

    // Start is called before the first frame update
    void Start() {
        //�e���𒊏o����
        List<int> nums = new List<int>();
        int n = Mathf.Abs(num);
        while (n / 10 != 0) {
            nums.Add(n % 10);
            n = n / 10;
        }
        nums.Add(n % 10);


        int count = 0;
        //�ŏ���0�Ŗ��߂�
        int zeroCount = (digit - nums.Count);
        if (zeroCount < 0) zeroCount = 0;
        images.Add(numImage);
        for (int i = 0; i < zeroCount-1; ++i) {
            Image im = Instantiate(numImage, this.transform.position + Vector3.right * count * space, Quaternion.identity);
            im.transform.parent = this.gameObject.transform;
            im.sprite = numberSprite[0];
            images.Add(im);
            count++;
        }

        //num�̐��l��\�����Ă�����
        for (int i = 0; i < nums.Count; ++i) {
            Image im = Instantiate(numImage, this.transform.position + Vector3.right * count * space, Quaternion.identity);
            im.transform.parent = this.gameObject.transform;
            im.sprite = numberSprite[nums[nums.Count-i-1]];
            images.Add(im);
            count++;
        }

    }

    // Update is called once per frame
    void Update() {
        //�e���𒊏o����
        List<int> nums = new List<int>();
        int n = Mathf.Abs(num);
        while (n / 10 != 0) {
            nums.Add(n % 10);
            n = n / 10;
        }
        nums.Add(n % 10);


        int count = 0;
        //�ŏ���0�Ŗ��߂�
        int zeroCount = (digit - nums.Count);
        if (zeroCount < 0) zeroCount = 0;
        for (int i = 0; i < zeroCount; ++i) {
            images[count].sprite = numberSprite[0];
            images[count].transform.position = this.transform.position + Vector3.right * count * space;
            count++;
        }

        //num�̐��l��\�����Ă�����
        for (int i = 0; i < nums.Count; ++i) {
            images[count].sprite = numberSprite[nums[nums.Count - i - 1]];
            images[count].transform.position = this.transform.position + Vector3.right * count * space;
            count++;
        }

    }

    public void SetNumber(int n) {
        num = n;
    }
    public void SetSpace(int s) {
        space = s;
    }
}
