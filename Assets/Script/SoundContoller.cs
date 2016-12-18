using UnityEngine;

public class SoundContoller : MonoBehaviour 
{
	public AudioSource soundSource = null;
	private AudioClip[] soundClip = null;

    void Awake() {
        LoadBGM();
    }

    void Start() {
        BindFuncToEvent();
    }

    void LoadBGM() {
        soundClip = Resources.LoadAll<AudioClip>("Sounds");
    }

    void BindFuncToEvent() {
        EventManager.instacne.AddFuncToEventForStart(Change0, EventManager.EventType.NORMAL);
        EventManager.instacne.AddFuncToEventForStart(Change1, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForStart(Change1, EventManager.EventType.BRANCH);
    }

	void Change0()
	{
		soundSource.clip = soundClip [0];
		soundSource.Play ();
	}

	void Change1()
	{
		soundSource.clip = soundClip [1];
		soundSource.Play ();
	}

	void Change2()
	{
		soundSource.clip = soundClip [2];
		soundSource.Play ();
	}
}