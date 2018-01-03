using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class GoalKeeper : NetworkBehaviour {

	public TextMeshProUGUI team1Text;
	public TextMeshProUGUI team2Text;
	public TextMeshProUGUI timeText;

	[SyncVar]
	public double time;

	[SyncVar]
	public int team1;

	[SyncVar]
	public int team2;


	public enum Teams
	{
		blue, orange
	}
	

	public BallController ball;

	void Start () {
		team1Text.text = team1.ToString();
		team2Text.text = team2.ToString();
		timeText.text = Time.time.ToString();
	}

	void Update () {
		RpcUpdateTime();

		timeText.text = time.ToString("F1");

	}

	[ClientRpc] 
	void RpcUpdateTime() {
		time += Time.deltaTime;
	}

	[ClientRpc]
	public void RpcUpdateScore(int teamIndex){
		//score [teamIndex]++;
		//if (!isServer) {
		//	return;
		//}

		if(teamIndex == 0) {
			team1++;
		}else if(teamIndex == 1){
			team2++;
		}
	}

	public void resetGame(){
		ball.resetBall();
		team1Text.text = team1.ToString();
		team2Text.text = team2.ToString();
		//scoreText.text = score[0] + "   00:00   " + score[1];
		Debug.Log("Team 1: " + team1 + "\n Team 2: " + team2);
	}
}
