using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    private ActionFactory actionFac;

    public ItemFactory()
    {
        actionFac = new ActionFactory();
    }
    public ItemFactory(ActionFactory actionFac)
    {
        this.actionFac = actionFac;
    }

    public GameObject Create(int index)
    {
        GameObject gameObject = new GameObject();
        //일단 Equipment만 만든다.

        switch (index / 10000)
        {
            //Weapon
            case 1:
                { 

                }
                break;
            case 2:
                { }
                break;
        }


        return gameObject;
    }
}
