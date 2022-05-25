using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Master : MonoBehaviour, IMasterPet
{
    [SerializeField] AI_Pet pet = null;

    public Transform MasterPosition => transform;

    public IPet Pet
    {
        get
        {
            if (!pet) return null;
            return pet.GetComponent<IPet>();
        }
    }

}
