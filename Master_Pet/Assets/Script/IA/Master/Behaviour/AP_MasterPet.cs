using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_MasterPet : MonoBehaviour, IMasterPet
{
    [SerializeField] AI_Pet pet = null;
    public Transform MasterPosition => transform;

    public bool IsValid => pet != null;
    public AI_Pet SpecificPet => pet;

    public IPet Pet
    {
        get
        {
            if (!pet) return null;
            return pet.GetComponent<IPet>();
        }
    }
}
