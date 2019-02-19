using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void GoTitle(){
		VSave.isVersus = false;
		ESave.isEndless = false;
		SceneManager.LoadScene("Title");
	}

	public static void GoLobby(){
		SceneManager.LoadScene("Lobby");
	}

	public static void GoSilentcave(){
		SceneManager.LoadScene("Silent Cave");
	}

	public static void GoTutorial(){
		SceneManager.LoadScene("Tutorial");
	}

	public static void GoResult(){
		SceneManager.LoadScene("Result");
	}

	public static void GoVersusReady(){
		SceneManager.LoadScene("VersusReady");
	}

	public static void GoVersus(){
		SceneManager.LoadScene("Versus");
	}

	public static void GoVersusTime(){
		SceneManager.LoadScene("VersusTime");
	}

	public static void GoVersusScore(){
		SceneManager.LoadScene("VersusScore");
	}

	public static void GoVersusResult(){
		SceneManager.LoadScene("VersusResult");
	}

	public static void GoVersusResult2(){
		SceneManager.LoadScene("VersusResult2");
	}

	public static void GoEndless(){
		ESave.isEndless = true;
		ESave.isResult = false;
		SceneManager.LoadScene("Endless");
	}

	public static void GoEndless2(){
		ESave.isEndless = true;
		ESave.isResult = false;
		SceneManager.LoadScene("Endless2");
	}

	public static void GoEndlessReady(){
		SceneManager.LoadScene("EndlessReady");
	}

	public static void GoEndlessResult(){
		ESave.isResult = true;
		SceneManager.LoadScene("EndlessResult");
	}

	public static void GoEndlessNakane(){
		ESave.isResult = true;
		SceneManager.LoadScene("Nakane");
	}

	public static void GoEndlessOver(){
		SceneManager.LoadScene("EndlessOver");
	}

	public static void GoEndlessStore(){
		SceneManager.LoadScene("EndlessStore");
	}

	public static void GoModeSelect(){
		SceneManager.LoadScene("ModeSelect");
	}

	public static void GoCredit(){
		SceneManager.LoadScene("Credit");
	}

	public static void GoExplanatoryText(){
		SceneManager.LoadScene("ExplanatoryText");
	}

}
