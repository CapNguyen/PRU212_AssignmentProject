using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] private float chance = 1f;

    bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void CheckDrop()
    {
        if (isQuitting) return;
        if(Random.value < chance)
        {
            GameObject dropItem = Instantiate(dropItemPrefab);
            dropItem.transform.position = transform.position;
        }
    }
}
