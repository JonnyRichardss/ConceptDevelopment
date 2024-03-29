using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStashController : MonoBehaviour
{
    public int foodAmount = 0;

    // Method to deposit food into the stash
    public void DepositFood(int amount)
    {
        foodAmount += amount;
        GameManager.instance.Resources.TryApplyEffect(new Effect(0, -1, 0, 0, 0, 0));
        Debug.Log("Deposited " + amount + " food. Current food amount: " + foodAmount);
    }

    // Method to withdraw food from the stash
    public bool WithdrawFood(int amount)
    {
        if (foodAmount >= amount)
        {
            foodAmount -= amount;
            GameManager.instance.Resources.TryApplyEffect(new Effect(0, 1, 0, 0, 0, 0));
            Debug.Log("Withdrawn " + amount + " food. Current food amount: " + foodAmount);
            return true; // Withdrawal successful
        }
        else
        {
            Debug.Log("Not enough food in the stash.");
            return false; // Withdrawal failed due to insufficient food
        }
    }

    // Method to get the current food amount in the stash
    public int GetCurrentFoodAmount()
    {
        return foodAmount;
    }

}
