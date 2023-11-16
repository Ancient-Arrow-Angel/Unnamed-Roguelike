using UnityEngine;

public class Slash : MonoBehaviour
{

    [SerializeField]
    float SwingTime;
    [SerializeField]
    float SwingSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * SwingSpeed);

        Destroy(gameObject, SwingTime);
    }
}