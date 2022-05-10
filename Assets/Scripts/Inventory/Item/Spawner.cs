﻿using DG.Tweening;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Throw Spawn(IItem item, Vector3 position)
    {
        var spawned = Instantiate(item.Info.Throw);
        var scale = spawned.transform.localScale;
        spawned.transform.localScale = new Vector3(0, 0, 0);
        spawned.transform.position = position;
        spawned.transform.DOScale(scale, 0.15f);
        return spawned;
    }

    public Throw Spawn(IItem item, Vector3 position, Transform parent)
    {
        if (item.Info.Throw == null)
            return null;
        var spawned = Instantiate(item.Info.Throw, parent);
        var scale = spawned.transform.localScale;
        spawned.transform.localScale = new Vector3(0, 0, 0);
        spawned.transform.position = position;
        if (item.Info.Type == ItemType.Wood)
            scale /= 4f;
        spawned.transform.DOScale(scale, 0.15f);
        return spawned;
    }
}