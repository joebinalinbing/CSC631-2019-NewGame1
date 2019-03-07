﻿using UnityEngine;

public static class EnemyType {
    public const string TestEnemy = "TestEnemy";
};

public class EnemyGroupDescription
{
    public string enemyType;
    public GameAgentStats stats;
    public float attackVariance;
    public float healthVariance;
    public float rangeVariance;
    public float speedVariance;

    public int quantityOfEnemyInGroup;

    public bool randomNumberOfEnemies;
    public int minNumberOfEnemiesInGroup;
    public int maxNumberOfEnemiesInGroup;

    public float powerLevel = 0;

    // Variance is based on a percentage from 0 to 1 (1 = 100%)
    // The stat can be potentially rasied to any percetage, but cannot fall below 50% the original stat
    public EnemyGroupDescription(string enemyType, GameAgentStats stats, int quantityOfEnemyInGroup, 
                                float attackVariance = 0f,  float healthVariance = 0f,
                                float rangeVariance = 0f, float speedVariance = 0f,
                                bool randomNumberOfEnemies = false, 
                                int minNumberOfEnemiesInGroup = -1, int maxNumberOfEnemiesInGroup = -1) {
        this.enemyType = enemyType;
        this.stats = stats;
        this.attackVariance = attackVariance;
        this.healthVariance = healthVariance;
        this.rangeVariance = rangeVariance;
        this.speedVariance = speedVariance;
        this.quantityOfEnemyInGroup = quantityOfEnemyInGroup;
        this.randomNumberOfEnemies = randomNumberOfEnemies;
        this.minNumberOfEnemiesInGroup = minNumberOfEnemiesInGroup;
        this.maxNumberOfEnemiesInGroup = maxNumberOfEnemiesInGroup;

        CalculatePowerLevel();
    }

    public float GetAttackWithVariance() {
        return Mathf.Max(stats.attack / 2, (stats.attack + (Random.Range(-attackVariance, attackVariance) * stats.attack)));
    }

    public float GetHealthWithVariance() {
        return Mathf.Max(stats.health / 2, (stats.health + (Random.Range(-healthVariance, healthVariance) * stats.health)));
    }

    public float GetRangeWithVariance() {
        return Mathf.Max(stats.range / 2, (stats.range + (Random.Range(-rangeVariance, rangeVariance) * stats.range)));
    }

    public float GetSpeedWithVariance() {
        return Mathf.Max(stats.speed / 2, (stats.speed + (Random.Range(-speedVariance, speedVariance) * stats.speed)));
    }

    public float GetPowerLevel() {
        return powerLevel;
    }

    private void CalculatePowerLevel() {
        powerLevel += stats.attack * stats.attack;
        powerLevel += stats.health * stats.health;
        powerLevel *= Mathf.Sqrt(stats.range);
        powerLevel *= Mathf.Sqrt(stats.speed);
    }
}
