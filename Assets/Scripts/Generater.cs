using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{
    // 物体预制件
    public GameObject[] itemPrefabs;

    // Player1 和 Player2 的生成区域四个角落（GameObject）
    public GameObject player1Corner1;
    public GameObject player1Corner2;
    public GameObject player2Corner1;
    public GameObject player2Corner2;

    // 用来确定是 Player1 还是 Player2 的回合
    public enum AttackSide
    {
        Player1,
        Player2
    }
    public AttackSide attackSide;  // 当前的攻击方

    // 控制每回合生成的物体数量
    public int itemsToGenerate = 3;

    void Start()
    {
        GenerateItems();
        // 初始时生成 3 个物体
    }

    void GenerateItems()
    {
        // 清除场地上的旧物体（可选，看是否需要）
        ClearPreviousItems();

        InvokeRepeating("Gene", 1f, 0.05f);

    }

    // 获取当前回合的随机生成位置
    Vector3 GetRandomSpawnPosition()
    {
        // 根据当前回合选择相应的场地区域（Player1 或 Player2）
        if (attackSide == AttackSide.Player1)
        {
            // 在 Player2 的区域生成物体
            return new Vector3(Random.Range(player2Corner1.transform.position.x, player2Corner2.transform.position.x),
                               Random.Range(player2Corner1.transform.position.y, player2Corner2.transform.position.y),
                               0f); // Z轴默认为 0
        }
        else
        {
            // 在 Player1 的区域生成物体
            return new Vector3(Random.Range(player1Corner1.transform.position.x, player1Corner2.transform.position.x),
                               Random.Range(player1Corner1.transform.position.y, player1Corner2.transform.position.y),
                               0f); // Z轴默认为 0
        }
    }

    // 清除场地上的旧物体（如果需要的话）
    void ClearPreviousItems()
    {
        GameObject[] existingItems = GameObject.FindGameObjectsWithTag("GeneratedItem");
        foreach (GameObject item in existingItems)
        {
            Destroy(item); // 删除现有的生成物体
        }
    }

    // 在回合切换时调用该方法来重新生成 3 个物体
    public void SwitchRound()
    {
        // 切换回合
        attackSide = (attackSide == AttackSide.Player1) ? AttackSide.Player2 : AttackSide.Player1;

        // 生成新物体
        GenerateItems();
    }

    public void Gene()
    {
        float rand = Random.Range(0, 1000);
        if (rand <= 8)
        {
            Debug.Log("Gene bubble");
            GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
