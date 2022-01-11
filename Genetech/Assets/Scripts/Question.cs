using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
public class Question : MonoBehaviour
{   
    public GameObject allQuestionnaireGO;
    public GameObject[] allQuestionnaire;
    public GameObject allQuestionsScriptGO;
    public Question[] allQuestionScript;
    public GameObject myQuestionnaire;
    public int questionNumber;
    public AnswerOption firstHint, secondHint; 
    public bool isOpen;
    public int navNum;
    public GameObject correct, wrong, locked, correctGO, wrongGO;
    public GameObject block1, block2, block3, block4;

    public TextMeshProUGUI  youGotPoints, generalScore, hints, round;
    public TextMeshProUGUI myNumber;
    public AnswerOption theAnswer;
    public CheckAnswer checkAnswer;
    public bool isHinted;
    public Button[] allButtons;
    public Button wrongFinishedButton, correctFinishedButton;
    
    public AllStats allStats;

    private void Awake() {
        navNum = questionNumber;
        myNumber = GetComponentInChildren<TextMeshProUGUI>();
        myNumber.text = (questionNumber+1).ToString();
        isOpen = (questionNumber == 0);
        locked.SetActive(!isOpen);
        isHinted = false;
        
        
        allQuestionnaire = new GameObject[20];
        for (int i = 0; i < allQuestionnaireGO.transform.childCount; i++){
            allQuestionnaire[i] = allQuestionnaireGO.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < allQuestionnaire.Length; i++){
            if(questionNumber == i)
                myQuestionnaire = allQuestionnaire[i];
        }
        allButtons = myQuestionnaire.GetComponentsInChildren<Button>();
        allQuestionsScriptGO = this.gameObject.transform.parent.gameObject;
        allQuestionScript = allQuestionsScriptGO.GetComponentsInChildren<Question>();
        wrongFinishedButton = myQuestionnaire.transform.Find("WrongAnswer").gameObject.GetComponentInChildren<Button>();
        correctFinishedButton = myQuestionnaire.transform.Find("CorrectAnswer").gameObject.GetComponentInChildren<Button>();
        InitializeButton();
        block1 = myQuestionnaire.transform.Find("Option_1").gameObject.transform.Find("Block").gameObject;
        block2 = myQuestionnaire.transform.Find("Option_1 (1)").gameObject.transform.Find("Block (1)").gameObject;
        block3 = myQuestionnaire.transform.Find("Option_1 (2)").gameObject.transform.Find("Block (2)").gameObject;
        block4 = myQuestionnaire.transform.Find("Option_1 (3)").gameObject.transform.Find("Block (3)").gameObject;
        wrongGO =  myQuestionnaire.transform.Find("WrongAnswer").gameObject;
        correctGO = myQuestionnaire.transform.Find("CorrectAnswer").gameObject;
        block1.SetActive(false);
        block2.SetActive(false);
        block3.SetActive(false);
        block4.SetActive(false);
        allStats = AllStats.instance;
    }

    public void InitializeButton(){
        allButtons[0].onClick.AddListener(ClickHint);
        allButtons[1].onClick.AddListener(A);
        allButtons[2].onClick.AddListener(B);
        allButtons[3].onClick.AddListener(C);
        allButtons[4].onClick.AddListener(D);
        allButtons[5].onClick.AddListener(CheckLeft);
        allButtons[6].onClick.AddListener(CheckRight);
        wrongFinishedButton.onClick.AddListener(FinishedQuestion);
        correctFinishedButton.onClick.AddListener(FinishedQuestion);
    }

    public void SaveScore(){
        Debug.Log("Saved");
        FirebaseDatabase.DefaultInstance.RootReference.Child("Player").Child(FirebaseController.Instance.user.DisplayName).Child("Score").SetValueAsync(allStats.Score);//SetValueAsync(AllStats.instance.Score.ToString());
    }

    public void OpenQuestion(){
        if(!isOpen)
            return;
        generalScore.text = (AllStats.instance.Score).ToString();
        myQuestionnaire.SetActive(true);
        round.text = questionNumber.ToString() +"/20";
        wrongGO.SetActive(false);
        correctGO.SetActive(false);
        HintAnswer();
    }
    
    public void ClickHint(){
        if(AllStats.instance.Hints <= 0)
            return;
        
        if(!isHinted){
            hints.text = (AllStats.instance.Hints -= 1).ToString();
            TriggerHints();
            isHinted = true;
        }
    }

    public void HintAnswer(){
        if(isHinted){
            TriggerHints();
            isHinted = true;
        }
    }

    public void A(){
        AnswerOption optionHere = AnswerOption.a;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else
            WrongAnswer();
    }

    public void B(){
        AnswerOption optionHere = AnswerOption.b;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else
            WrongAnswer();
    }

    public void C(){
        AnswerOption optionHere = AnswerOption.c;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else
            WrongAnswer();
    }

    public void D(){
        AnswerOption optionHere = AnswerOption.d;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else
            WrongAnswer();
    }

    public void CorrectAnswer(){
        wrongGO.SetActive(false);
        correctGO.SetActive(true);
        checkAnswer = CheckAnswer.correct;
    }

    public void WrongAnswer(){
        wrongGO.SetActive(true);
        correctGO.SetActive(false);
        checkAnswer = CheckAnswer.wrong;
    }
    
   
    private void ResetStats(){
        AllStats.instance.Hints = 4;
        SaveScore();
    }

    public void FinishedQuestion(){
        
        if(checkAnswer == CheckAnswer.correct){
            correct.SetActive(true);
            wrong.SetActive(false);
            generalScore.text = (AllStats.instance.Score += 50).ToString();
            wrongGO.SetActive(false);
            correctGO.SetActive(false);
            if(questionNumber == 19)
            {
                OpenNextQuestion();
                CloseQuestionnaire();
                ResetStats();
            }
                
            OpenNextQuestion();
            CloseQuestionnaire();
        }else{
            correct.SetActive(false);
            wrong.SetActive(true);
            wrongGO.SetActive(false);
            correctGO.SetActive(false);
            if(questionNumber == 19){
                OpenNextQuestion();
                CloseQuestionnaire();
                ResetStats();
            }
            OpenNextQuestion();
            CloseQuestionnaire();
            
        }
    }

    public void OpenNextQuestion(){
        if(questionNumber != 19)
        {
             allQuestionScript[navNum+1].isOpen = true;
            allQuestionScript[navNum+1].locked.SetActive(false);
        }  
    }

    public void CloseQuestionnaire(){
        myQuestionnaire.SetActive(false);
    }
    
    public void CheckLeft(){
        if(navNum > 0)
            navNum--;
    }

    public void CheckRight(){
        if(navNum < 19)
            navNum++;
    }

    public void TriggerHints(){
        if(firstHint == AnswerOption.a || secondHint == AnswerOption.a)
                block1.SetActive(true);
            if(firstHint == AnswerOption.b || secondHint == AnswerOption.b)
                block2.SetActive(true);
            if(firstHint == AnswerOption.c || secondHint == AnswerOption.c)
                block3.SetActive(true);
            if(firstHint == AnswerOption.d || secondHint == AnswerOption.d)
                block4.SetActive(true);
    }

}

public enum AnswerOption
{
    a,b,c,d
}

public enum CheckAnswer{
    correct, wrong
}
