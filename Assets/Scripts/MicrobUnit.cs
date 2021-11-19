using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobUnit : Unit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MicrobUnit unit) && _isDrugging == true)
        {
            ChangePrefab(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MicrobUnit unit) && _isDrugging == true)
        {
            ChangePrefab(collision.gameObject);
        }
    }
}
