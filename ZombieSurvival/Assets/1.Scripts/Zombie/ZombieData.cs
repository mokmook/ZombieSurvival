
using UnityEngine;
[CreateAssetMenu(menuName ="Scriptable/ZombieData",fileName ="ZomboeData")]
public class ZombieData : ScriptableObject
{
    [SerializeField] Texture2D texture2D;

    public float health=100f;

    public float damage=20f;

    public float  speed=2f;

    public Color skinColor=Color.white;

    
}

