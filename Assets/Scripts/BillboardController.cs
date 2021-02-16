using UnityEngine;

public class BillboardController : MonoBehaviour
{
    void Update()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}
