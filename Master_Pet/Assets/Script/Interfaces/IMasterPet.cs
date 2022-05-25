using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMasterPet
{
    Transform MasterPosition { get; }
    IPet Pet { get; }
}
