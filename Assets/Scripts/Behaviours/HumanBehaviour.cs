using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public Transform self;
    public SkinnedMeshRenderer meshRenderer;
    public Animator animator;

    public float moveSpeed;

    public Transform headSocket;
    private float startAngle;

    private float actualRadius;
    private HumanPath humanPath;

    private List<Vector3> realPathPoints;
    private float pathIndex;

    private float distanceTravelled;
    private float time;
    private bool walking = true;

    private List<PropBehaviour> propsAttachedToHead;



    /*public void Init(HumanPath humanPath)
    {
        self.position = new Vector3(humanPath.points[0].x,0, humanPath.points[0].y);
        this.humanPath = humanPath;
        realPathPoints = new List<Vector3>();
        for (int j = 0; j < humanPath.points.Count; j++)
        {
            for (int i = 0; i <= humanPath.bezierDivisions; i++)
            {
                float t = i / (float)humanPath.bezierDivisions;
                Vector3 point;
                if (j < humanPath.points.Count - 1)
                {

                    point = CalculateCubicBezierPoint(t, new Vector3(humanPath.points[j].x,0,humanPath.points[j].y), new Vector3(humanPath.bezierHandleBeforePoints[j].x,0, humanPath.bezierHandleBeforePoints[j].y), new Vector3(humanPath.bezierHandleAfterPoints[j].x,0, humanPath.bezierHandleAfterPoints[j].y), new Vector3(humanPath.points[j + 1].x,0, humanPath.points[j + 1].y));
                }
                else
                {
                    point = CalculateCubicBezierPoint(t, new Vector3(humanPath.points[j].x,0,humanPath.points[j].y), new Vector3(humanPath.bezierHandleBeforePoints[j].x,0, humanPath.bezierHandleBeforePoints[j].y), new Vector3(humanPath.bezierHandleAfterPoints[j].x,0, humanPath.bezierHandleAfterPoints[j].y), new Vector3(humanPath.points[0].x,0, humanPath.points[0].y));

                }
                realPathPoints.Add(point);
            }
        }

        Vector3 lastPoint=Vector3.up;
        for (int i = 0; i < realPathPoints.Count; i++)
        {
            if (realPathPoints[i] == lastPoint)
            {
                realPathPoints.RemoveAt(i);
                i--;
                continue;
            }
            lastPoint = realPathPoints[i];
        }

        realPathPoints.RemoveAt(realPathPoints.Count - 1);
    }*/



    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }


    public void Init(float distanceFromTable)
    {
        if (propsAttachedToHead != null)
        {
            for (int i = 0; i < propsAttachedToHead.Count; i++)
            {
                Destroy(propsAttachedToHead[i].gameObject);
            }
        }

        propsAttachedToHead = new List<PropBehaviour>();

        startAngle = Random.Range(0, Mathf.PI * 2);

        meshRenderer.material = HumanManager.instance.humanMaterials[Random.Range(0, HumanManager.instance.humanMaterials.Count)];

        actualRadius = TableBehaviour.instance.meshFilter.sharedMesh.bounds.size.x / 2 * TableBehaviour.instance.meshFilter.transform.localScale.x + distanceFromTable;
        self.position = TableBehaviour.instance.self.position + new Vector3(Mathf.Cos(startAngle), 0, Mathf.Sin(startAngle)) * actualRadius;
        animator.SetBool("Moving", true);
        walking = true;

        
    }


    private void Update()
    {
        if (!walking) { return; }
        time += Time.deltaTime;

        if (time * moveSpeed >= Mathf.PI * 2)
        {
            time -= Mathf.PI * 2 / moveSpeed;
        }
        float angle = startAngle + time * moveSpeed;
        self.position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * actualRadius;
        self.eulerAngles = new Vector3(self.eulerAngles.x, -angle / (2 * Mathf.PI) * 360-90, self.eulerAngles.z);
    }

    /*private void FollowPath(float distanceRemaining)
    {
        int currentIndex=(int)pathIndex;
        float sign = Mathf.Sign(distanceRemaining);
        int nextIndex=(int)pathIndex+ 1* (int)sign;


        if (sign <0 && nextIndex < 0)
        {
            nextIndex = realPathPoints.Count - 1;
        }else if (sign >0 && nextIndex > realPathPoints.Count - 1)
        {
            nextIndex = 0;
        }


        
        float distanceForNextPoint = (realPathPoints[nextIndex] - realPathPoints[currentIndex]).magnitude;
        


        float diff = distanceRemaining / distanceForNextPoint;
        
        if (Mathf.Abs(diff) > 1)
        {
            pathIndex = nextIndex;
            FollowPath(distanceRemaining-distanceForNextPoint*sign);
        }
        else
        {
            pathIndex += diff;
        }
    }


    private void Update()
    {
        FollowPath(Time.deltaTime * moveSpeed);
        

        if (pathIndex > realPathPoints.Count - 1)
        {
            if (pathIndex < realPathPoints.Count)
            {
                self.position = Vector3.Lerp(realPathPoints[(int)pathIndex], realPathPoints[0], pathIndex - (int)pathIndex);
            }
            else
            {
                pathIndex -= realPathPoints.Count;
            }
        }
        else if(pathIndex<0)
        {
            if (pathIndex > -1)
            {
                self.position = Vector3.Lerp(realPathPoints[0], realPathPoints[realPathPoints.Count-1], 1-(pathIndex - ((int)pathIndex-1)));
            }
            else
            {
                pathIndex += realPathPoints.Count;
            }
        }
        else
        {
            self.position = Vector3.Lerp(realPathPoints[(int)pathIndex], realPathPoints[(int)pathIndex + 1], pathIndex - (int)pathIndex);
        }
    }*/



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            CatchProp(other.GetComponent<PropBehaviour>());
        }
    }


    private void CatchProp(PropBehaviour prop)
    {
        //play catch anim
        //ReputPropOnTable(prop);

        if (propsAttachedToHead.Contains(prop))
        {
            return;
        }

        print("prop catched");
        
        prop.self.parent=headSocket;
        
        prop.self.localEulerAngles =new Vector3(-180,0,0);
        prop.self.localPosition = new Vector3(0, 0, -GetHeadPileHeight());
        prop.rigidBody.useGravity = false;
        prop.rigidBody.velocity = Vector3.zero;
        prop.rigidBody.angularVelocity = Vector3.zero;
        prop.attached = true;
        propsAttachedToHead.Add(prop);
        PropManager.instance.activeProps.Remove(prop);
        Debug.Log("zbeub",propsAttachedToHead[0].gameObject);
        PropManager.instance.propsCatched++;

        CatBehaviour.instance.bubbleImage.sprite = CatManager.instance.bubbleSprites[Random.Range(0, CatManager.instance.bubbleSprites.Count)];
        CatBehaviour.instance.bubbleImage.gameObject.SetActive(true);
        StartCoroutine(HideBubble());

        GameManager.instance.CheckGameOver();
    }

    private IEnumerator HideBubble()
    {
        yield return new WaitForSeconds(1);
        CatBehaviour.instance.bubbleImage.gameObject.SetActive(false);
    }


    private float GetHeadPileHeight()
    {
        float height = 0;
        for (int i = 0; i < propsAttachedToHead.Count; i++)
        {
            height += propsAttachedToHead[i].meshRenderer.bounds.size.y * propsAttachedToHead[i].self.localScale.y/2;
        }
        print(height);
        return height;
    }




    private void ReputPropOnTable(PropBehaviour prop)
    {
        prop.self.position = prop.initialPosition;
        prop.self.rotation = prop.initialRotation;
        prop.rigidBody.velocity = Vector3.zero;
        prop.rigidBody.angularVelocity = Vector3.zero;
    }


    public void StopWalking()
    {
        walking = false;
        animator.SetBool("Moving", false);
    }

}
