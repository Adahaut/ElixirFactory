using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BuildInterface
{
    public void ConstructItem();
}

public interface IItemReceiver
{
    void ReceiveItem(BeltItem item);
    bool CanReceiveItem();
}
