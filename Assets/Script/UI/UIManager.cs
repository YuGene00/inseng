using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    //enum
    public enum AlarmType {
        SPECIAL, BRANCH
    }

	//singleton
    public static UIManager instance = null;

    //inspector
    public Text scoreText;
    public GameObject remainTimeStrings;
    public Text remainTimeText;
    public Image missionAlarmImage;
    public Text missionName;
    public Text missionDetail;
    public Sprite[] alarmSprite;
    public float alarmTime = 1f;
    public float appearTime = 0.5f;
    public float disappearTime = 1f;
    public float unitTime = 0.1f;

    //caching
    GameObject alarmObj;
    CanvasGroup alarmCanvasGroup;

    //variable
    WaitForSeconds waitForAlarm;
    WaitForSeconds waitUnitTime;

    void Awake() {
        instance = this;
        Caching();
        Initialize();
    }

    void Caching() {
        alarmObj = missionAlarmImage.gameObject;
        alarmCanvasGroup = alarmObj.GetComponent<CanvasGroup>();
    }

    void Initialize() {
        waitForAlarm = new WaitForSeconds(alarmTime);
        waitUnitTime = new WaitForSeconds(unitTime);
    }

    public void SetScore(int score) {
        scoreText.text = score.ToString();
    }

    public void SetRemainTimeActive(bool active) {
        remainTimeStrings.SetActive(active);
    }

    public void SetRemainTime(float remainTime) {
        remainTimeText.text = remainTime.ToString("0.0");
    }

    public void SetMissionAlarmWithType(AlarmType type) {
        missionAlarmImage.sprite = alarmSprite[(int)type];
        StartCoroutine("DisplayAlarm");
    }

    IEnumerator DisplayAlarm() {
        yield return StartCoroutine("subAppear");
        alarmCanvasGroup.alpha = 1f;
        yield return waitForAlarm;
        yield return StartCoroutine("subDisappear");
        alarmObj.SetActive(false);
    }

    IEnumerator subAppear() {
        alarmObj.SetActive(true);
        alarmCanvasGroup.alpha = 0f;
        float appearPerUnitTime = unitTime / appearTime;
        while (alarmCanvasGroup.alpha < 1f) {
            alarmCanvasGroup.alpha += appearPerUnitTime;
            yield return waitUnitTime;
        }
    }

    IEnumerator subDisappear() {
        float disappearPerUnitTime = unitTime / disappearTime;
        while (alarmCanvasGroup.alpha > 0f) {
            alarmCanvasGroup.alpha -= disappearPerUnitTime;
            yield return waitUnitTime;
        }
    }

    public void SetMissionNameAndDetail(string missionName, string missionDetail) {
        this.missionName.text = missionName;
        this.missionDetail.text = missionDetail;
    }
}
