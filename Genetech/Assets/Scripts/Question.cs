using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : MonoBehaviour
{   
    public int questionNumber;
    public bool isOpen;
    public Question TEST;
    public AllStats allStats;
    public GameObject correct, wrong, locked, questionnaire, allQuestion, correctGO, wrongGO;
    public TextMeshProUGUI question, option1, option2, option3, option4, wrongAnswerText, solutionForWrong;
    public TextMeshProUGUI  points, hints, anotherOne;
    public Button a, b, c, d, nextWrong, nextCorrect;
    public AnswerOption theAnswer;
    public CheckAnswer checkAnswer;
    public Question[] allQuestions;
    [TextArea]
    public string question_, option1_, option2_, option3_, option4_, solutionForWrong_;
    private void Start() {
        question = questionnaire.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        option1 = questionnaire.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        option2 = questionnaire.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();
        option3 = questionnaire.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
        option4 = questionnaire.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>();
        allQuestions = FindObjectsOfType<Question>();
        isOpen = (questionNumber == 1);
        questionnaire.SetActive(false);
    }

    public void OpenQuestion(){
        if(!isOpen)
            return;
        questionnaire.SetActive(true);
        wrongGO.SetActive(true);
        correctGO.SetActive(true);
        question.text = question_;
        option1.text = option1_;
        option2.text = option2_;
        option3.text = option3_;
        option4.text = option4_;
        a.onClick.AddListener(A);
        b.onClick.AddListener(B);
        c.onClick.AddListener(C);
        d.onClick.AddListener(D);
        InitializeNextButtons(); 
    }

    public void A(){
        AnswerOption optionHere = AnswerOption.a;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else{
            WrongAnswer(option1_);
        }
    }

    public void B(){
        AnswerOption optionHere = AnswerOption.b;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else{
            WrongAnswer(option2_);
        }
    }

    public void C(){
        AnswerOption optionHere = AnswerOption.c;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else{
            WrongAnswer(option3_);
        }
    }

    public void D(){
        AnswerOption optionHere = AnswerOption.d;
        if(theAnswer == optionHere){
            CorrectAnswer();
        }else{
            WrongAnswer(option4_);
        }
    }

    public void CorrectAnswer(){
        wrongGO.SetActive(false);
        correctGO.SetActive(true);
        questionnaire.SetActive(false);
        checkAnswer = CheckAnswer.correct;
       
    }
    
    public void WrongAnswer(string wrongAns){
        wrongGO.SetActive(true);
        correctGO.SetActive(false);
        questionnaire.SetActive(false);
        solutionForWrong.text = solutionForWrong_;
        wrongAnswerText.text = wrongAns;
        checkAnswer = CheckAnswer.wrong;
    }

    public void FinishedQuestion(){
        
        if(checkAnswer == CheckAnswer.correct){
            Debug.Log("Clicking");
            correct.SetActive(true);
            wrong.SetActive(false);
            questionnaire.SetActive(false);
            RemoveNextButtons();
            wrongGO.SetActive(false);
            correctGO.SetActive(false);
            CheckNextQuestion();

        }else{
            correct.SetActive(false);
            wrong.SetActive(true);
            questionnaire.SetActive(false);
            RemoveNextButtons();
            wrongGO.SetActive(false);
            correctGO.SetActive(false);
            CheckNextQuestion();
        }
    }

    public void CheckNextQuestion(){
        int num = questionNumber+1;
        for (int i = 0; i < allQuestions.Length; i++)
        {
            if(num == allQuestions[i].questionNumber){
                allQuestions[i].locked.SetActive(false);
                allQuestions[i].isOpen = true;
            }       
        }
    }

    private void InitializeNextButtons(){
        nextCorrect.onClick.AddListener(FinishedQuestion);
        nextWrong.onClick.AddListener(FinishedQuestion);
    }

    private void RemoveNextButtons(){
        nextCorrect.onClick.RemoveListener(FinishedQuestion);
        nextWrong.onClick.RemoveListener(FinishedQuestion);
    }
}

public enum AnswerOption
{
    a,b,c,d
}

public enum CheckAnswer{
    correct, wrong
}
