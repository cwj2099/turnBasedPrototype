using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random1 : MonoBehaviour
{
    public float sx;
    public float sy;
    public float dx;
    public float dy;
    // Start is called before the first frame update
    void Start()
    {
        sx = Random.Range(transform.localScale.x, sx);
        sy = Random.Range(transform.localScale.y, sy);
        dx = Random.Range(transform.position.x, transform.position.x+dx);
        dy = Random.Range(transform.position.y, transform.position.y+dy);
        transform.position = new Vector3(dx, dy, transform.position.z);
        transform.localScale = new Vector3(sx, sy, transform.localScale.z);
    }

}
