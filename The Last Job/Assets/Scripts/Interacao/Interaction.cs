using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Local local;
}

[System.Serializable]
public enum Local
{
    table,
    floor,
    door,
    taser
}