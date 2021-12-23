using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category : MonoBehaviour
{   
    public GameObject instruction, easy, hard, difficult, category;

    private void Start() {
        instruction.SetActive(false);
        easy.SetActive(false);
        hard.SetActive(false);
        difficult.SetActive(false);
        category.SetActive(false);
    }

    public void Instruction(){
        instruction.SetActive(true);
        easy.SetActive(false);
        hard.SetActive(false);
        difficult.SetActive(false);
    }

    public void Easy(){
        instruction.SetActive(false);
        easy.SetActive(true);
        hard.SetActive(false);
        difficult.SetActive(false);
    }

    public void Hard(){
        instruction.SetActive(false);
        easy.SetActive(false);
        hard.SetActive(true);
        difficult.SetActive(false);
    }

    public void Difficult(){
        instruction.SetActive(false);
        easy.SetActive(false);
        hard.SetActive(false);
        difficult.SetActive(true);
    }

    public void Back(){
        instruction.SetActive(false);
        easy.SetActive(false);
        hard.SetActive(false);
        difficult.SetActive(false);
        category.SetActive(false);
    }
}
