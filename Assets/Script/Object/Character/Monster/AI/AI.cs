using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using Unity.VisualScripting;

public class AI : MonoBehaviour
{
    public struct Node
    {
        public Vector3Int NodeID;
        //FScore = GScore + HScore
        public float FScore
        {
            get { return GScore + HScore; }
        }
        //Cost of Node
        public float GScore;
        //HScore is linear distance of this Node to target Node
        public float HScore;
        public Vector3Int ParentNode;

        public Node(Vector3Int NodeID, float GScore, float HScore, Vector3Int ParentNode)
        {
            this.NodeID = NodeID;
            this.GScore = GScore;
            this.HScore = HScore;
            this.ParentNode = ParentNode;
        }
    }

    #region Properties
    #region Private
    #endregion
    #region Protected
    protected bool isWave;
    protected Monster owner;
    #endregion
    #region Public
    public TileManager tileManager;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    private void AddNodeToOList(Dictionary<Vector3Int, Node> CList, Dictionary<Vector3Int, Node> OList, Vector3Int key, Vector3Int targetCoor, Node parent)
    {
        if (CList.ContainsKey(key))
        {
            return;
        }

        if (tileManager.HasTile(key))
        {
            if (tileManager.availablePlaces[key].available)
            {
                if(OList.ContainsKey(key))
                {
                    float gscore = 1 + parent.GScore;
                    float hscore = Mathf.Pow(targetCoor.x - key.x, 2) + Mathf.Pow(targetCoor.y - key.y, 2);
                    float fscore = gscore + hscore;
                    if (fscore < OList[key].FScore)
                    {
                        Node temp = new Node(key, gscore, hscore, parent.NodeID);
                        OList[key] = temp;
                    }
                }
                else
                {
                    OList.Add(key, new Node(
                        key,
                        1 + parent.GScore,
                        Mathf.Pow(targetCoor.x - key.x, 2) + Mathf.Pow(targetCoor.y - key.y, 2),
                        parent.NodeID));
                }
            }
        }
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    public GameObject TargetSelect(LayerMask targetMask, float Radius)
    {
        //Scan Targets
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, Radius, targetMask);
        if (targets.Length <= 0) { return null; }

        int bestTarget = 0;
        float max = 0;

        IGetPriority getValue;
        float computedVal;
        for (int i = 0; i < targets.Length; ++i)
        {
            //Compute Best Target
            getValue = targets[i].GetComponentInParent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;

            if (max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
        }

        getValue = targets[bestTarget].GetComponentInParent<IGetPriority>();
        //find target but, that can not become target
        if (getValue != null)
        {
            if (getValue.GetPriority() < 0)
            {
                return null;
            }
        }
        else { return null; }

        return targets[bestTarget].gameObject;
    }

    public bool PathFinding(Vector2 startPos, Vector2 targetPos ,out Vector2[] path)
    {
        Vector3Int targetCoor = tileManager.GetTileCoordinates(targetPos);
        Vector3Int startCoor = tileManager.GetTileCoordinates(startPos);

        if(targetCoor.z < 0 || startCoor.z < 0)
        {
            path = null;
            return false;
        }

        //use A* Algorisim
        //http://www.gisdeveloper.co.kr/?p=3897
        //휴리스틱 함수는 두 지점 사이의 거리를 제곱근 말고 제곱한 상태 그대로
        //H(x) = (target.x - node.x)^2 + (target.y - node.y)^2
        Dictionary<Vector3Int, Node>OList = new Dictionary<Vector3Int, Node>();
        Dictionary<Vector3Int, Node>CList = new Dictionary<Vector3Int, Node>();

        CList.Add(startCoor, new Node(startCoor, 0, 0, Vector3Int.back));

        //repeat 20 times
        for(int i = 0; i < 20; ++i)
        {
            //Add Node to Open Node List
            foreach(Node node in CList.Values)
            {
                Vector3Int temp;

                //Add nodes clockwise
                temp = node.NodeID + Vector3Int.up;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.up + Vector3Int.right;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.right;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.down + Vector3Int.right;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.down;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.down + Vector3Int.left;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.left;
                AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.up + Vector3Int.left;
                AddNodeToOList(CList, OList, temp, targetCoor, node);
            }

            //find minimum FScore Node
            float min = 0;
            foreach (Node node in OList.Values)
            {
                if (min > node.FScore)
                    min = node.FScore;
            }

            //Add minimum node to CList
            foreach (Node node in OList.Values)
            {
                if (Mathf.Approximately(node.FScore, min))
                {
                    CList.Add(node.NodeID, node);
                    OList.Remove(node.NodeID);
                }
            }

            //find Path
            if (CList.ContainsKey(targetCoor))
                break;
        }


        //if cannot find Path
        if (!CList.ContainsKey(targetCoor))
        {
            CList.Clear();
            OList.Clear();

            path = null;
            return false;
        }
        else
        {
            List<Vector2> reversePath = new List<Vector2>();

            Vector3Int temp = targetCoor;
            while(temp != startCoor)
            {
                temp = CList[targetCoor].ParentNode;
                reversePath.Add(new Vector2(temp.x, temp.y));
            }

            path = new Vector2[reversePath.Count];
            for (int i = 0, j = reversePath.Count - 1; i < reversePath.Count; ++i, --j)
            {
                path[i] = reversePath[j];
            }


            CList.Clear();
            OList.Clear();
            return true;
        }
        
    }
    public Action SelectAction(Action[] actions)
    {
        int bestTarget = 0;
        float max = 0;

        IGetPriority getValue;
        float computedVal;

        for (int i = 0; i < actions.Length; ++i)
        {
            //Compute Best Target
            getValue = actions[i].GetComponent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;

            if (max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
        }

        return actions[bestTarget];
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        owner = GetComponentInParent<Monster>();
    }
    #endregion
}