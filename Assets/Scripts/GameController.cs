﻿using UnityEngine;
using System.Collections;

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

	public delegate void GameControllerHandler(JOBSTAGE stage);
	public static event GameControllerHandler ChangeStageEvnet;

	public delegate void GameControllerHandler2();
	public static event GameControllerHandler2 NormalEvent;
	public static event GameControllerHandler2 SpecialEvent;
	public static event GameControllerHandler2 SectionEvent;

	public JOBSTAGE nowStage = JOBSTAGE.CHILD_STAGE;

	public float[] sectionTime = null;

	private float timer = 0.0f;
	private int timerIndex = 0;

	void Awake()
	{
		if(ChangeStageEvnet!=null)
			ChangeStageEvnet (nowStage);
	}

	void Start () 
	{
		nowStage = JOBSTAGE.CHILD_STAGE;
		timer = Time.time;
	}

	void Update ()
	{
		if (Time.time-timer>sectionTime[timerIndex])
			ChangeSection();
	}

	void ChangeSection()
	{
		timer = Time.time;

		if (timerIndex.Equals (0)) 
		{
			timerIndex++;

			if (NormalEvent != null)
				NormalEvent();
		}
		else if (timerIndex.Equals (1)) 
		{
			timerIndex++;

			if(SpecialEvent!=null)
				SpecialEvent();
		}
		else if (timerIndex.Equals (2)) 
		{
			if (nowStage.Equals (JOBSTAGE.CHILD_STAGE))
			{
				ChangeJob ();
				ChangeStage ();

				return;
			}

			timerIndex++;

			if (NormalEvent != null)
				NormalEvent();
		}
		else if (timerIndex.Equals (3)) 
		{
			if(SpecialEvent!=null)
				SpecialEvent();

			if (nowStage.Equals (JOBSTAGE.STUDENT_STAGE) ||
			   nowStage.Equals (JOBSTAGE.UNIVERSITY_STAGE) ||
			   nowStage.Equals (JOBSTAGE.UNEMPLOYED_STAGE_0) ||
			   nowStage.Equals (JOBSTAGE.UNEMPLOYED_STAGE_1) ||
			   nowStage.Equals (JOBSTAGE.WORKER_STAGE_0) ||
			   nowStage.Equals (JOBSTAGE.WORKER_STAGE_1) ||
			   nowStage.Equals (JOBSTAGE.CHICKEN_STAGE)) {

				timerIndex++;
			} 

			else
			{
				ChangeJob ();
				ChangeStage();

				return;
			}
		}
		else if (timerIndex.Equals(4)) 
		{
			if (SectionEvent != null)
				SectionEvent();
			//
		}
		else
		{
			//ChangeJob (0 or 1);
			ChangeJob ();
			ChangeStage();
		}
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