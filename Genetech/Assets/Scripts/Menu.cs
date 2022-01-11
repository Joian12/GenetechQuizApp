using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;


public class Menu : MonoBehaviour
{   
    public TextMeshProUGUI playerName, playerScore;
    public GameObject category, setting;
    public GameObject login, register, start, score;
    private void Awake() {
        login.SetActive(true);
        register.SetActive(false);
        start.SetActive(false);
    }

    public void StartButton(){
        category.SetActive(true);
    }

    public void SettingsButton(){
        setting.SetActive(true);
    }

    public void SettingBack(){
        setting.SetActive(false);
    }

    public void GoToRegister(){
        register.SetActive(true);
        login.SetActive(false);
    }

    public void GoToLogIn(){
        register.SetActive(false);
        login.SetActive(true);
    }

    public void ScoreButton(){
        FirebaseController.Instance.GetOnlineScore(playerName, playerScore);
        score.SetActive(true);
        GetOnlineScore();
    }

    public void Scoreback(){
        score.SetActive(false);
    }

    public void GoToStart(){
        login.SetActive(false);
        register.SetActive(false);
        start.SetActive(true);
    }

    public void GetOnlineScore(){
      FirebaseDatabase.DefaultInstance.RootReference.Child("Player").Child(FirebaseController.Instance.user.DisplayName).Child("Score").GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
          Debug.Log("Failed");
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          Debug.Log("Success");
          playerName.text = FirebaseController.Instance.user.DisplayName;
          playerScore.text = snapshot.GetRawJsonValue();
        }
      });
    }
}
