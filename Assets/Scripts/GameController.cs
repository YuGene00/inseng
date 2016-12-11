﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public enum JOBSTAGE
	{
		CHILD_STAGE,
		STUDENT_STAGE,
		UNIVERSITY_STAGE,
		UNEMPLOYED_STAGE_0,
		UNEMPLOYED_STAGE_1,
		CHICKEN_STAGE,
		WORKER_STAGE_0,
		WORKER_STAGE_1,
		SENIOR_STAGE,
	}

	public enum EVENTSTAGE
	{
		NORMAL_STATE,
		SPECIAL_STATE,
		SECTION_STATE,
	}

	private static GameController instance;  
	private static GameObject container;  

    public static void DeleteInstance() {
        instance = null;
        NormalEvent = null;
        SpecialEvent = null;
        SectionEvent = null;
        SpecialEndEvent = null;
        SectionEndEvent = null;
        DyingSeniEvent = null;
        DieEvent = null;
    }

	public static GameController GetInstance()  
	{  
		if( !instance )  
		{  
			container = new GameObject();  
			container.name = "GameController";  
			instance = container.AddComponent(typeof(GameController)) as GameController;  
		}  

		return instance;  
	}  

	public delegate void GameControllerHandler(JOBSTAGE stage);
	public static event GameControllerHandler ChangeStageEvnet;

	public delegate void GameControllerHandler2();
	public static event GameControllerHandler2 NormalEvent;
	public static event GameControllerHandler2 SpecialEvent;
	public static event GameControllerHandler2 SectionEvent;
	public static event GameControllerHandler2 SpecialEndEvent;
	public static event GameControllerHandler2 SectionEndEvent;
	public static event GameControllerHandler2 DyingSeniEvent;
	public static event GameControllerHandler2 DieEvent;

    public Text scoreText;
    AudioSource audioSource;
    public AudioClip coinSound;

	public JOBSTAGE nowStage = JOBSTAGE.CHILD_STAGE;
	public EVENTSTAGE nowEvent = EVENTSTAGE.NORMAL_STATE;
	public float gameSpeed = 800.0f;
    private int _score = 0;
    public int score {
        get {
            return _score;
        }
        set {
            _score = value;
            scoreText.text = _score.ToString();
            audioSource.Play();
        }
    }
    public int EatItemNumber = 0;

    public float[] sectionTime = {0.0f,10.0f,5.0f,10.0f,5.0f,10.0f,7.5f};

	private float timer = 0.0f;
	private int timerIndex = 0;

	void Start () 
	{
		timer = Time.time;
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("EffectSound/Coin") as AudioClip;
        audioSource.volume = 0.2f;
    }

	public float GetTime() 
	{
		return Time.time-timer;
	}

	void Update ()
	{
		if (Time.time-timer>sectionTime[timerIndex])
			ChangeSection();
	}

	void ChangeSection()
	{
		timer = Time.time;

		if (timerIndex.Equals (0)) // 0 -> 1 N
		{
			timerIndex++;

			nowEvent = EVENTSTAGE.NORMAL_STATE;

			if (NormalEvent != null)
				NormalEvent();
		}
		else if (timerIndex.Equals (1))  // 1 -> 2 S
		{
			timerIndex++;

			nowEvent = EVENTSTAGE.SPECIAL_STATE;

			if(SpecialEvent!=null)
				SpecialEvent();
		}
		else if (timerIndex.Equals (2))  // 2 -> 3 N
		{
			if (SpecialEndEvent != null)
				SpecialEndEvent ();

			if (nowStage.Equals (JOBSTAGE.CHILD_STAGE))
			{
				ChangeJob ();
				ChangeStage ();

				return;
			}

			timerIndex++;

			nowEvent = EVENTSTAGE.NORMAL_STATE;

			if (NormalEvent != null)
				NormalEvent();
		}
		else if (timerIndex.Equals (3))  // 3 -> 4 S
		{
			if (nowStage.Equals (JOBSTAGE.SENIOR_STAGE))
			{
				if (DyingSeniEvent != null)
					DyingSeniEvent ();
				return;
			}

			timerIndex++;

			nowEvent = EVENTSTAGE.SPECIAL_STATE;

			if(SpecialEvent!=null)
				SpecialEvent();
		}
		else if (timerIndex.Equals(4)) // 4 -> 5 N
		{
			if (SpecialEndEvent != null)
				SpecialEndEvent ();
			
			timerIndex++;

			nowEvent = EVENTSTAGE.NORMAL_STATE;

			if (NormalEvent != null)
				NormalEvent();
		}
		else if (timerIndex.Equals(5)) // 4 -> 5 SS
		{
			timerIndex++;

			if (nowStage.Equals (JOBSTAGE.STUDENT_STAGE) ||
				nowStage.Equals (JOBSTAGE.UNIVERSITY_STAGE) ||
				nowStage.Equals (JOBSTAGE.UNEMPLOYED_STAGE_0) ||
				nowStage.Equals (JOBSTAGE.UNEMPLOYED_STAGE_1) ||
				nowStage.Equals (JOBSTAGE.WORKER_STAGE_0) ||
				nowStage.Equals (JOBSTAGE.WORKER_STAGE_1) ||
				nowStage.Equals (JOBSTAGE.CHICKEN_STAGE))
			{
				nowEvent = EVENTSTAGE.SECTION_STATE;

				if (SectionEvent != null)
					SectionEvent();
			}
			else
			{
				ChangeJob ();
				ChangeStage();
				return;
			}
		}
		else if (timerIndex.Equals(6)) // end
		{
			if (SectionEndEvent != null)
				SectionEndEvent();
		}
	}

	public void GlobalChange(int index = 0)
	{
		ChangeJob (index);
		ChangeStage();
	}

	public void ChangeStage()
	{
		timerIndex = 0;

		if(ChangeStageEvnet!=null)
			ChangeStageEvnet (nowStage);
	}

	void ChangeJob(int index = 0)
	{
		switch (nowStage) 
		{
		case JOBSTAGE.CHILD_STAGE:
			nowStage = JOBSTAGE.STUDENT_STAGE;
			break;
		case JOBSTAGE.STUDENT_STAGE:
			if (index.Equals (0))
				nowStage = JOBSTAGE.UNIVERSITY_STAGE;
			else
				nowStage = JOBSTAGE.UNEMPLOYED_STAGE_0;
			break;

		case JOBSTAGE.UNIVERSITY_STAGE:
			if (index.Equals (0))
				nowStage = JOBSTAGE.UNEMPLOYED_STAGE_1;
			else
				nowStage = JOBSTAGE.WORKER_STAGE_0;
			break;
			
		case JOBSTAGE.UNEMPLOYED_STAGE_0:
			if (index.Equals (0))
				nowStage = JOBSTAGE.UNEMPLOYED_STAGE_1;
			else
				nowStage = JOBSTAGE.WORKER_STAGE_0;
			break;
			
		case JOBSTAGE.UNEMPLOYED_STAGE_1:
			if (index.Equals (0))
				nowStage = JOBSTAGE.WORKER_STAGE_1;
			else
				nowStage = JOBSTAGE.CHICKEN_STAGE;
			break;

		case JOBSTAGE.CHICKEN_STAGE:
			if (index.Equals (0))
				nowStage = JOBSTAGE.CHILD_STAGE;
			else
				nowStage = JOBSTAGE.SENIOR_STAGE;
			break;

		case JOBSTAGE.WORKER_STAGE_0:
			if (index.Equals (0))
				nowStage = JOBSTAGE.WORKER_STAGE_1;
			else
				nowStage = JOBSTAGE.CHICKEN_STAGE;
			break;

		case JOBSTAGE.WORKER_STAGE_1:
			if (index.Equals (0))
				nowStage = JOBSTAGE.CHILD_STAGE;
			else
				nowStage = JOBSTAGE.SENIOR_STAGE;
			break;

		case JOBSTAGE.SENIOR_STAGE:
			nowStage = JOBSTAGE.CHILD_STAGE;
			Debug.Log ("???");
			break;
		}
	}
}