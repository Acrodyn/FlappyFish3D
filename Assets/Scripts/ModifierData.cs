using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier Data", menuName = "FishObjects/Modifier Data", order = 2)]
public class ModifierData : ScriptableObject
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private float Duration;
    [SerializeField] private float PointScale;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Properties]
    public float ModifierDuration => Duration;
    public float ModifierdPointScale => PointScale;
    // ------------------------------------------------------------------------------------------------------------------------------
}
