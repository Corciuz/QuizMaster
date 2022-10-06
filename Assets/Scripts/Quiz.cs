using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("QuizQuestions")]
    [SerializeField]TextMeshProUGUI questionText;
    [SerializeField]List<QuizQuestions> question=new List<QuizQuestions>();
     QuizQuestions currentQuestions;
    [Header("Answers")]
    [SerializeField] GameObject [] answerButtons;
    int correctAnswer;
    bool isAnsweredEarly;
    [Header("Button Colors")]
    [SerializeField]Sprite defaultAnswerSprite;
    [SerializeField]Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField]Image timerImage;
    Timer timer;
    [Header("Scoring")]
    [SerializeField]TextMeshProUGUI scoreText;
    ScoreKeeper ScoreKeeper;
    [Header("ProgressBar")]
    [SerializeField]Slider progressBar;

    public bool isComplete;
    
    void Awake()
    {
        timer=FindObjectOfType<Timer>();
    ScoreKeeper=FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue=question.Count;
        progressBar.value=0;
    }
    void Update(){
        timerImage.fillAmount=timer.fillFraction;
        
       if(timer.loadNextQuestion){
        if(progressBar.value==progressBar.maxValue){
        isComplete=true;
        return;
        }
        isAnsweredEarly=false;
        GetNextQuestion();
        timer.loadNextQuestion=false;
        
       }else if(!isAnsweredEarly&&!timer.isAnsweringQuestion){
        
        NotAnswered();
      
        
       }
    }
    public void OnAnswerSelected(int index){
        isAnsweredEarly=true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text="score:"+ScoreKeeper.clculateScore()+"%";
        
    }
    void NotAnswered(){
        Image buttonImage;
         questionText.text="Correct Answer was :"+currentQuestions.GetAnswer(currentQuestions.GetCorrectAnswerIndex());
            buttonImage=answerButtons[currentQuestions.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
            scoreText.text="score:"+ScoreKeeper.clculateScore()+"%";
    }
    void DisplayAnswer(int index){
Image buttonImage;
        if(index==currentQuestions.GetCorrectAnswerIndex()){
        questionText.text="Correct!";
        buttonImage=answerButtons[index].GetComponent<Image>();
        buttonImage.sprite=correctAnswerSprite;
        ScoreKeeper.IncrementCorrectAnswers();
        }else{
            questionText.text="Wrong \n Correct Answer was :"+currentQuestions.GetAnswer(currentQuestions.GetCorrectAnswerIndex());
            buttonImage=answerButtons[currentQuestions.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
        }
    }

    void DisplayQuestion(){
questionText.text=currentQuestions.GetQuestion();

       for(int x=0;x<answerButtons.Length;x++){
     TextMeshProUGUI buttonText=answerButtons[x].GetComponentInChildren<TextMeshProUGUI>();
       buttonText.text=currentQuestions.GetAnswer(x);
       }
    }
    void SetButtonState(bool State){

        for(int x=0;x<answerButtons.Length;x++){
            Button button=answerButtons[x].GetComponent<Button>();
            button.interactable=State;
        }
    }


    void GetNextQuestion(){ 
     if(question.Count>0){
        SetButtonState(true);
        
        if(progressBar.value!=0){
        SetDefultSprite();

        }
        GetRandomQuestion();
        DisplayQuestion();
        
        progressBar.value++;
        ScoreKeeper.IncrementQuestionsSeen();
     }
        
       
    }
    void GetRandomQuestion(){
        int index=Random.Range(0,question.Count);
        currentQuestions=question[index];
        if(question.Contains(currentQuestions)){
        question.Remove(currentQuestions);

        }
    }


    void SetDefultSprite(){
    Image ButtonImage;
     ButtonImage=answerButtons[currentQuestions.GetCorrectAnswerIndex()].GetComponent<Image>();
     ButtonImage.sprite=defaultAnswerSprite;
    }
}

