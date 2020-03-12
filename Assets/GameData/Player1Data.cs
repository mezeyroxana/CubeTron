using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player1Data", menuName = "Tron/Player1Data")]
public class Player1Data : ScriptableObject
{
    public string playerName;
    public Material color;
    public int score;
}
