using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Snake Snake;
    public Text Score;

    public void Update()
    {
        Score.text = $"Score : {Snake.GetSnakeLength().ToString()}";
    }
}
