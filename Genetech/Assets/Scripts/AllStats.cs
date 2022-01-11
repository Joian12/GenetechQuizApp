using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStats : MonoBehaviour
{  
    public static AllStats instance;
    public int Hints;
    public int Score;
    private void Awake() {
        instance = this;
        Hints = 4;
        Score = 0;
    }

    public bool CheckHints(){
        return (Hints < 10);
    }   


    public void CheckScore(){

    }
}
