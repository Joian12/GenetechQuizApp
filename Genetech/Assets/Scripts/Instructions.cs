using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject mainInstruction, instructionFinished, correct;
    public GameObject easy, hard, difficult;

    public void OpenInstructionQuestion(){
        instructionFinished.SetActive(true);
    }

    public void Finished(){
        correct.SetActive(true);
        instructionFinished.SetActive(false);
    }

    public void Back(){
        mainInstruction.SetActive(false);
        easy.SetActive(false);
        hard.SetActive(false);
        difficult.SetActive(false);
    }
}
