using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Questions",fileName = "Question")]
public class QuizQuestions : ScriptableObject
{
   [TextArea(2,6)] 
    [SerializeField]string Question="Enter questions here";
    [SerializeField] string[]answer=new string[4];
    [SerializeField] int correctanswerIndex;
    public string GetQuestion(){
        return Question;
    }
    public int GetCorrectAnswerIndex(){
        return correctanswerIndex;
    }
    public string GetAnswer(int index){
        return answer[index];
    }
}
