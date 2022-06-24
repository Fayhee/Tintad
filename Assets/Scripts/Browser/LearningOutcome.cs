using UnityEngine;

[System.Serializable]

public class LearningOutcome


{
    public string name; 
    public int negInteractions = 0; 
    public int posInteractions = 0; 
    public Dialogue warning; 
    public Dialogue approval; 
}
