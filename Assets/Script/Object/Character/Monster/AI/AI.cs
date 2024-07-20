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
    protected TileChargeManager tileChargeManager;
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    //return false if not add to OList
    //return true if add to OList
    ///<summary>
    ///OList�� ��带 �߰��ϴ� �Լ�
    ///</summary>
    ///<returns>true : OList�� �߰� ����, false : OList�� �߰� ����</returns>
    private bool AddNodeToOList(Dictionary<Vector3Int, Node> CList, Dictionary<Vector3Int, Node> OList, Vector3Int key, Vector3Int targetCoor, Node parent)
    {
        if (CList.ContainsKey(key))
        {
            return false;
        }

        if(key == targetCoor && !OList.ContainsKey(key))
        {
            OList.Add(key, new Node(key, 0, 0, parent.NodeID));
            return false;
        }

        if (TileManager.Instance.HasTile(key))
        {
            if (TileManager.Instance.availablePlaces[key].available || tileChargeManager.previousTilePos.Contains(key))
            {
                float gscore = 1 + parent.GScore;
                float hscore = Mathf.Abs(targetCoor.x - key.x) + Mathf.Abs(targetCoor.y - key.y);
                float fscore = gscore + hscore;
                if (OList.ContainsKey(key))
                {
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
                        gscore,
                        hscore,
                        parent.NodeID));
                }
            }
        }
        return true;
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    ///<summary>
    ///���������� ���ӿ�����Ʈ�� �켱������ �����Ͽ� Ÿ���� �����ϴ� �Լ�
    ///�������� ��ǥ�� �켱������ ���� �������� ��ȯ�ϵ� ���� ã�� �� �ִ� ��ǥ�� ��ȯ
    ///</summary>
    ///<param name="targetMask">Ÿ���� �� ���̾� ����ũ</param>
    ///<param name="Radius">����</param>
    public GameObject TargetSelect(LayerMask targetMask, float Radius)
    {
        //Scan Targets
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, Radius, targetMask);
        if (targets.Length <= 0) { return null; }

        int bestTarget = -1;
        float max = -1.0f;

        IGetPriority getValue;
        float computedVal = 0;
        for (int i = 0; i < targets.Length; ++i)
        {
            //Compute Best Target
            getValue = targets[i].GetComponentInParent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;

            if(!this.PathFinding(this.transform.position, targets[i].transform.position, out Vector2[] path))
            {
                continue;
            }

            if (max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
            else if(Mathf.Approximately(max, computedVal))
            {
                if (((Vector2)targets[bestTarget].transform.position - (Vector2)transform.position).sqrMagnitude
                    - ((Vector2)targets[i].transform.position - (Vector2)transform.position).sqrMagnitude
                    > 0)
                {
                    max = computedVal;
                    bestTarget = i;
                }
            }
        }
        
        if(bestTarget < 0)
        {
            Debug.Log("AI.TargetSelect.if(bestTarget < 0)");
            return null;
        }
        
        return targets[bestTarget].gameObject;
    }

    public bool PathFinding(Vector2 startPos, Vector2 targetPos, out Vector2[] path)
    {
        Vector3Int targetCoor = TileManager.Instance.GetTileCoordinates(targetPos);
        Vector3Int startCoor = TileManager.Instance.GetTileCoordinates(startPos);

        if(targetCoor.z < 0 || startCoor.z < 0)
        {
            Debug.Log("AI.PathFinding.Not Available Pos");
            path = null;
            return false;
        }

        //use A* Algorisim
        //http://www.gisdeveloper.co.kr/?p=3897
        //�޸���ƽ �Լ��� ����ź ���Ͻ�
        //H(x) = |target.x - node.x| + |target.y - node.y|
        Dictionary<Vector3Int, Node>OList = new Dictionary<Vector3Int, Node>();
        Dictionary<Vector3Int, Node>CList = new Dictionary<Vector3Int, Node>();

        CList.Add(startCoor, new Node(startCoor, 0, 0, startCoor));

        //repeat 5 times
        for (int i = 0; i < 5; ++i)
        {
            //Add Node to Open Node List
            foreach(Node node in CList.Values)
            {
                Vector3Int temp;
                bool[] arrows = new bool[4]{ false, false, false, false};

                //Add Right Angle nodes
                temp = node.NodeID + Vector3Int.up;
                arrows[0] = AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.right;
                arrows[1] = AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.down;
                arrows[2] = AddNodeToOList(CList, OList, temp, targetCoor, node);

                temp = node.NodeID + Vector3Int.left;
                arrows[3] = AddNodeToOList(CList, OList, temp, targetCoor, node);

                //for add diagonal
                //�밢�� �߰�
                if (arrows[0] && arrows[1])
                {
                    temp = node.NodeID + Vector3Int.up + Vector3Int.right;
                    AddNodeToOList(CList, OList, temp, targetCoor, node);
                }

                if (arrows[1] && arrows[2])
                {
                    temp = node.NodeID + Vector3Int.down + Vector3Int.right;
                    AddNodeToOList(CList, OList, temp, targetCoor, node);
                }

                if (arrows[2] && arrows[3])
                {
                    temp = node.NodeID + Vector3Int.down + Vector3Int.left;
                    AddNodeToOList(CList, OList, temp, targetCoor, node);
                }

                if (arrows[3] && arrows[0])
                {
                    temp = node.NodeID + Vector3Int.up + Vector3Int.left;
                    AddNodeToOList(CList, OList, temp, targetCoor, node);
                }
            }

            //find minimum FScore Node
            float min = float.MaxValue;
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
                }
            }

            //Remove node in OList, the nodes are inserted in CList
            foreach(Vector3Int nodeID in CList.Keys)
            {
                if (OList.ContainsKey(nodeID))
                {
                    OList.Remove(nodeID);
                }
            }

            //find Path
            if (CList.ContainsKey(targetCoor))
                break;
        }


        //if cannot find Path
        if (!CList.ContainsKey(targetCoor))
        {
            /*
            Debug.Log("AI.PathFinding.Cannot Find Path");
            Debug.Log(startCoor);
            Debug.Log(targetCoor);
            Debug.Log("OList");
            foreach (Node node in OList.Values)
            {
                Debug.Log($"node id = {node.NodeID}\n" +
                    $"node fscore = {node.FScore}\n" +
                    $"node gscore = {node.GScore}\n" +
                    $"node hscore = {node.HScore}\n" +
                    $"node parent node = {node.ParentNode}");
            }
            Debug.Log("CList");
            foreach (Node node in CList.Values)
            {
                Debug.Log($"node id = {node.NodeID}\n" +
                    $"node fscore = {node.FScore}\n" +
                    $"node gscore = {node.GScore}\n" +
                    $"node hscore = {node.HScore}\n" +
                    $"node parent node = {node.ParentNode}");
            }
            */
            CList.Clear();
            OList.Clear();

            path = null;
            return false;
        }
        else
        {
            List<Vector2> reversePath = new List<Vector2>();

            Vector3Int temp = targetCoor;

            //for now, save coordinate point, because world pos saved in coordinate
            //later, need to translate coordinate pos to world pos
            //20 times repeat for safety
            for(int i = 0; i < 20 && temp != startCoor; ++i)
            {
                temp = CList[temp].ParentNode;
                reversePath.Add(TileManager.Instance.GetWorldPosCenterOfCell((Vector2Int)temp));
            }

            if (reversePath.Count > 1)
            {
                path = new Vector2[reversePath.Count - 1];
                //not to save startPos, i condition is not i < reversePath.Count
                //but i < reversePath.Count - 1
                //if i < reversePath.Count, path will save start pos
                for (int i = 0, j = reversePath.Count - 2; i < reversePath.Count - 1; ++i, --j)
                {
                    path[i] = reversePath[j];
                }
            }
            else
            {
                path = new Vector2[1];
                path[0] = TileManager.Instance.GetWorldPosCenterOfCell((Vector2Int)targetCoor);
            }
            /*
            //for debug
            Debug.Log("Path");
            for(int i = 0; i < path.Length; ++i)
            {
                Debug.Log(path[i]);
            }
            Debug.Log("reversePath");
            for(int i = 0; i < reversePath.Count; ++i)
            {
                Debug.Log(reversePath[i]);
            }
            Debug.Log("OList");
            foreach (Node node in OList.Values)
            {
                Debug.Log($"node id = {node.NodeID}\n" +
                    $"node fscore = {node.FScore}\n" +
                    $"node gscore = {node.GScore}\n" +
                    $"node hscore = {node.HScore}\n" +
                    $"node parent node = {node.ParentNode}");
            }
            Debug.Log("CList");
            foreach (Node node in CList.Values)
            {
                Debug.Log($"node id = {node.NodeID}\n" +
                    $"node fscore = {node.FScore}\n" +
                    $"node gscore = {node.GScore}\n" +
                    $"node hscore = {node.HScore}\n" +
                    $"node parent node = {node.ParentNode}");
            }

            Debug.Log($"targetCoor = {targetCoor}, startCoor = {startCoor}");
            Debug.Log($"targetPos = {targetPos}, startPos = {startPos}");
            */
            CList.Clear();
            OList.Clear();
            return true;
        }
        
    }
    public Action SelectAction(Action[] actions)
    {
        int bestTarget = 0;
        float max = 0;
        bool isSelected = false;

        IGetPriority getValue;
        float computedVal;

        for (int i = 0; i < actions.Length; ++i)
        {
            //Compute Best Target
            if (!actions[i].available)
                continue;

            getValue = actions[i].GetComponent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;

            if (max < computedVal)
            {
                isSelected = true;
                max = computedVal;
                bestTarget = i;
            }
        }

        if (isSelected)
        {
            return actions[bestTarget];
        }
        else
        {
            return null;
        }
        
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
        tileChargeManager = GetComponentInParent<TileChargeManager>();
    }
    #endregion
}