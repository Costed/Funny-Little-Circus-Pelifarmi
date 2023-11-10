using UnityEngine;
using System;

public class KeyManager : MonoBehaviour
{
    KeyType collectedKeys;

    Key lastCollectedKey;

    //public event Action<KeyType> OnKeyCollected;

    public void CollectKey(Key key)
    {
        collectedKeys |= key.Type;
        lastCollectedKey = key;
        //OnKeyCollected?.Invoke(key.Type);
    }

    public void CommitKeyPickup()
    {
        lastCollectedKey = null;
    }

    public void ResetKeyToCheckpoint()
    {
        if (lastCollectedKey != null)
        {
            collectedKeys &= ~lastCollectedKey.Type;
            lastCollectedKey.Reset();
        }
    }

    public bool HasKeyOfType(KeyType keyType) => collectedKeys.HasFlag(keyType);
}

[Flags]
public enum KeyType
{
    None = 0,
    Ticket = 1,
    StorageRoom1 = 2,
    Backroom = 4,
    StorageRoom2 = 8,
    Exit = 16
}
