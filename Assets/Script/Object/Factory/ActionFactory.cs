using Unity.VisualScripting;
using UnityEngine;

public class ActionFactory
{
    public Action CreateAction(int index)
    {
        GameObject actionObject = new GameObject();

        Action action = null;
        switch(index / 10000)
        {
            //WeaponAction
            case 1:
                {
                    WeaponActionStruct tempStruct = new WeaponActionStruct();
                    //여기에 데이터 불러오기
                    //tempStruct = ~~~~~;

                    WeaponAttackAction temp = actionObject.AddComponent<WeaponAttackAction>();
                    action = temp;

                    temp.priority = tempStruct.Priority;
                    temp.coolTime = tempStruct.CoolTime;
                    temp.activeRadius = tempStruct.ActiveRadius;
                    temp.activeDuration = tempStruct.ActiveDuration;

                    int mask = 0;
                    for(int i = 0; i < tempStruct.LayerMask.Length; ++i)
                    {
                        mask += 1 << tempStruct.LayerMask[i];
                    }
                    temp.targetMask = mask;

                    temp.hitBoxPrefabs = new HitBox[tempStruct.HitBox_Index.Length];
                    //HitBoxFactory hitFac = new HitBoxFactory();
                    for (int i = 0; i < tempStruct.HitBox_Index.Length; ++i)
                    {
                        //GameObject hitbox = hitFac.CreateHitBox(HitBox_Index[i]);
                        //hitbox.transform.SetParent(actionObject, false);
                        //temp.hitBoxPrefabs[i] = hitbox;
                        //hitbox.SetActive(false);
                    }

                }
                break;
        }

        if (action != null)
            return action;
        else
            return null;
    }
}
