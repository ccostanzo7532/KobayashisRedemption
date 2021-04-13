using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float speed;
    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 nextPos;
    public Transform plat;
    public Transform pos2Tran;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = plat.transform.localPosition;
        pos2 = pos2Tran.localPosition;
        nextPos = pos2;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlat();
    }
    public void MovePlat()
    {
        plat.localPosition = Vector3.MoveTowards(plat.localPosition, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(plat.localPosition, nextPos) <= 0.1)
        {
            ChangePos();
        }
    }
    public void ChangePos()
    {
        nextPos = nextPos != pos1 ? pos1 : pos2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(plat);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
