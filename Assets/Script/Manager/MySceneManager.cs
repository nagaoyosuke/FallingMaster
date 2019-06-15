using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager> {

	public static void GoTitle(){
		SceneManager.LoadScene("Title");
	}

    public static void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public static void GoMenu()
    {
        SceneManager.LoadScene("Menu");
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
		SceneManager.LoadScene("Endless");
	}

	public static void GoEndless2(){
		SceneManager.LoadScene("Endless2");
	}

	public static void GoEndlessReady(){
		SceneManager.LoadScene("EndlessReady");
	}

	public static void GoEndlessResult(){
		SceneManager.LoadScene("EndlessResult");
	}

	public static void GoEndlessNakane(){
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
