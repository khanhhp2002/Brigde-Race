using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PhaseController _phaseController;

    public PhaseController PhaseController => _phaseController;
}
