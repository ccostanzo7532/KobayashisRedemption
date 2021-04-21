using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayer : MonoBehaviour
{
    public Collider2D player;
    private void Awake()
    {
        player = Player.Instance.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player, true);
    }
}
