using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTest : MonoBehaviour
{
    FactoryDIContainer container;

    public int itemIdx;


    public int actionIdx;
    public int hitboxIdx;
    public int affectIdx;


    public FactoryTest()
    {
        container = new FactoryDIContainer();
    }


    [ContextMenu("Item")]
    public void CreateItem()
    {
        container.itemFac.Create(itemIdx);
    }

    [ContextMenu("action")]
    public void CreateAction()
    {
        container.actionFac.Create(actionIdx);
    }

    [ContextMenu("hitBox")]
    public void CreateHitBox()
    {
        container.hitBoxFac.Create(hitboxIdx);
    }

    [ContextMenu("affect")]
    public void CreateAffect()
    {
        container.affectFac.Create(affectIdx);
    }
}
