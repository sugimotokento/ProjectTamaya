using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1StartEvent : GameEvent {
    [SerializeField] GameObject visual;
    [SerializeField] Text text;
    [SerializeField] Text nameText;

    private string[] talkCharactor = new string[13];
    private string[] talk = new string[13];
    private string endText;
    private string drawText;

    private int talkIndex = 0;
    private int textIndex = 0;
    private float addTextIntervalTimer = 0;

    // Start is called before the first frame update
    void Start() {
        eventPos=(StageManager.instance.player.gameObject.transform.position - Vector3.forward * 5);

        drawText = "";
        endText = "_";
        talkCharactor[0] = "トシロー";
        talk[0] = "引退したばっかなのにトラブルに巻き込まれるって・・・\n悪いもんでも憑いてるのかもしれねえ、今度お祓いでも行くか";

        talkCharactor[1] = "？？？";
        talk[1] = "あの！！！！";

        talkCharactor[2] = "トシロー";
        talk[2] = "ん、誰だ？";

        talkCharactor[3] = "？？？";
        talk[3] = "初めまして、あなたは元OKPKのトシローさんですよね？";

        talkCharactor[4] = "トシロー";
        talk[4] = "ああ、だがなんでそんな事知っているんだ？";

        talkCharactor[5] = "ビショップ";
        talk[5] = "申し遅れました、ｱｰｼは新米OKPK隊員のビショップと言います！\n現役の頃のトシローさんの活躍っぷりに憧れていたんですっ";

        talkCharactor[6] = "トシロー";
        talk[6] = "なるほど、じゃあ今回の騒ぎは君に任せていいってことだな？";

        talkCharactor[7] = "ビショップ";
        talk[7] = "それが・・・ｱｰｼはまだ任務に当たったことがなく、\n怖くて動けないんです・・・";

        talkCharactor[8] = "トシロー";
        talk[8] = "ったく、昔の俺を見ているようだな・・・\nよし、今回は俺に任せろ！ビショップは着いてきてくれ!\n電磁棒は持ってるか？";

        talkCharactor[9] = "ビショップ";
        talk[9] = "っは、はい！どうぞ！";

        talkCharactor[10] = "トシロー";
        talk[10] = "新品でピッカピカの電磁棒だな！\n・・・ん？俺が持ってたのとも少し形とか違うみたいだ";

        talkCharactor[11] = "ビショップ";
        talk[11] = "新米隊員用の電磁棒にはARで再生される\n教育プログラムが組み込まれているんですっ";

        talkCharactor[12] = "トシロー";
        talk[12] = "へえ・・・丁寧なこった。初心に戻ってみるか";
    }

    // Update is called once per frame
    void Update() {
        if (isEvent == true) {
            //1文字ずつ表示する
            addTextIntervalTimer += Time.deltaTime;
            if (addTextIntervalTimer > 0.09f) {
                if (textIndex < talk[talkIndex].Length) {
                    drawText += talk[talkIndex][textIndex];
                    addTextIntervalTimer = 0;
                    textIndex++;
                    endText = "_";

                } else {
                    endText = "▼";
                }
            }

            //会話を進める
            if (Input.GetKeyDown(KeyCode.Space) && textIndex >= talk[talkIndex].Length) {
                //次の会話
                talkIndex++;
                textIndex = 0;
                drawText = "";
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                //今の会話を全て表示
                textIndex = talk[talkIndex].Length;
                drawText = talk[talkIndex];
            }


            //会話の終了
            if (talkIndex >= talk.Length) {
                talkIndex = talk.Length - 1;
                isEventEnd = true;
                isEvent = false;
                visual.SetActive(false);
            }
            SkipEvent();

            nameText.text = talkCharactor[talkIndex];
            text.text     = "「" + drawText + endText + "」";
        } else {
            visual.SetActive(false);
        }
    }
}
