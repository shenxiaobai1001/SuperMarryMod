using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrickController : MonoBehaviour
{
    public List<BoxCollider2D> boxCollider2Ds;
    public BoxCollider2D collider2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteorite"))
        {
            Sound.PlaySound("smb_breakblock");
            collider2D.enabled = false;
            OnChangeAllBox(true);
        }
    }

    void OnChangeAllBox(bool show)
    {
        for (int i = 0; i < boxCollider2Ds.Count; i++)
        {
            boxCollider2Ds[i].enabled = show;
        }
    }
}
