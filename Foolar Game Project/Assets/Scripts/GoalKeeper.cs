using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {

	public enum Teams
	{
		blue, orange
	}
	public int[] score;
	public GameObject ballPrefab;

	void Start () {
		score = new int[2];
	}

	void Update () {
		
	}

	public void updateScore(int teamIndex){
		score [teamIndex]++;
	}

	public void resetGame(){

	}
}
