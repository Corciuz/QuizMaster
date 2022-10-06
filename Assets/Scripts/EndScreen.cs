using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI FinalText;
    ScoreKeeper ScoreKeeper;
    void Awake()
    {
        ScoreKeeper=FindObjectOfType<ScoreKeeper>();
    }
public void ShowFinalScore(){
    FinalText.text="Congratulations!\nYou got a score of "+ 
    ScoreKeeper.clculateScore()+" %";
}
    
}
