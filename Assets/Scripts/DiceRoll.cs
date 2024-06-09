using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public int RollDice(int sides)
    {
        return Random.Range(1, sides + 1);
    }
}