// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// [CreateAssetMenu(fileName = "ScoreEvents", menuName = "SOs/ScoreEvents")]
// public class ScoreEvents : ScriptableObject {
//
//     static float scoreCount = 0;
//
//     public static void SetScore(float scoreToSet)
//     {
//         scoreCount = scoreToSet;
//     }
//
//     public static void AddScore(float scoreToAdd)
//     {
//         scoreCount += scoreToAdd;
//         PlayerLevel.Instance.IncreaseXP((int)scoreToAdd);
//     }
//
//     public static float GetScore()
//     {
//         return scoreCount;
//     }
// }
