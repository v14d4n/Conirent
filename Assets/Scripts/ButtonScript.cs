using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isPushed { get; set; }
    public Vector3 oldPos;

    private void Start()
    {
        oldPos = transform.position;
    }
}
