using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Vector3 startLocation;
    Vector3 endLocation;
    float currentPosition;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
        startLocation = transform.localPosition;
        endLocation = startLocation + new Vector3(114, -41, 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startLocation, endLocation, currentPosition);
        if (currentPosition > 1)
        {
            Destroy(gameObject);
        }
    }
}
