using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_ErikaShootSystem : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab = null;
    [SerializeField] Transform spawnPoint = null;

    [SerializeField] AP_FightSystem fightSystem = null;
    GameObject currentArrow = null;
    public bool IsValid => arrowPrefab && spawnPoint && fightSystem;

    private void Start()
    {
        fightSystem.OnAtk += CreateArrow;
    }

    void CreateArrow()
    {
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
    }
    public void ShootArrow()
    {
        currentArrow.GetComponent<AP_ArrowBehaviour>().SetTarget(fightSystem.AtkTarget);
    }
}