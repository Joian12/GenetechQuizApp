using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{   
    public GameObject category, setting;
    private void Start() {
        
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
}
