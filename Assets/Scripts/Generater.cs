using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{
    // ����Ԥ�Ƽ�
    public GameObject[] itemPrefabs;

    // Player1 �� Player2 �����������ĸ����䣨GameObject��
    public GameObject player1Corner1;
    public GameObject player1Corner2;
    public GameObject player2Corner1;
    public GameObject player2Corner2;

    // ����ȷ���� Player1 ���� Player2 �Ļغ�
    public enum AttackSide
    {
        Player1,
        Player2
    }
    public AttackSide attackSide;  // ��ǰ�Ĺ�����

    // ����ÿ�غ����ɵ���������
    public int itemsToGenerate = 3;

    void Start()
    {
        GenerateItems();
        // ��ʼʱ���� 3 ������
    }

    void GenerateItems()
    {
        // ��������ϵľ����壨��ѡ�����Ƿ���Ҫ��
        ClearPreviousItems();

        InvokeRepeating("Gene", 1f, 0.05f);

    }

    // ��ȡ��ǰ�غϵ��������λ��
    Vector3 GetRandomSpawnPosition()
    {
        // ���ݵ�ǰ�غ�ѡ����Ӧ�ĳ�������Player1 �� Player2��
        if (attackSide == AttackSide.Player1)
        {
            // �� Player2 ��������������
            return new Vector3(Random.Range(player2Corner1.transform.position.x, player2Corner2.transform.position.x),
                               Random.Range(player2Corner1.transform.position.y, player2Corner2.transform.position.y),
                               0f); // Z��Ĭ��Ϊ 0
        }
        else
        {
            // �� Player1 ��������������
            return new Vector3(Random.Range(player1Corner1.transform.position.x, player1Corner2.transform.position.x),
                               Random.Range(player1Corner1.transform.position.y, player1Corner2.transform.position.y),
                               0f); // Z��Ĭ��Ϊ 0
        }
    }

    // ��������ϵľ����壨�����Ҫ�Ļ���
    void ClearPreviousItems()
    {
        GameObject[] existingItems = GameObject.FindGameObjectsWithTag("GeneratedItem");
        foreach (GameObject item in existingItems)
        {
            Destroy(item); // ɾ�����е���������
        }
    }

    // �ڻغ��л�ʱ���ø÷������������� 3 ������
    public void SwitchRound()
    {
        // �л��غ�
        attackSide = (attackSide == AttackSide.Player1) ? AttackSide.Player2 : AttackSide.Player1;

        // ����������
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
