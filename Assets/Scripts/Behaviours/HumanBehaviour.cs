using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    [System.NonSerialized] public HumanData humanData;

    public Transform self;
    public List<MeshRenderer> meshRenderers;

    private float timeLeftBeforeShowUp;
    private bool startingToShowUp = false;

    public void Init(HumanData humanData)
    {
        this.humanData = humanData;
        ResetTimeLeft();
        for (int i = 00; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].enabled = false;
        }
        
        startingToShowUp = false;
    }

    private void ResetTimeLeft()
    {
        timeLeftBeforeShowUp = Random.Range(humanData.spawnDelay.x, humanData.spawnDelay.y);
    }


    private void Update()
    {
        if (startingToShowUp) { return; }

        if (timeLeftBeforeShowUp > 0)
        {
            timeLeftBeforeShowUp -= Time.deltaTime;
        }
        else
        {
            startingToShowUp = true;
            StartShowingUp();
        }
    }

    private void StartShowingUp()
    {
        if (HumanManager.instance.humansPresentCount < HumanManager.instance.humanMaxCountAtSameTime)
        {
            for (int i = 00; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].enabled = true;
                meshRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            
            StartCoroutine(ShowUp());
        }
        
    }

    private IEnumerator ShowUp()
    {
        yield return new WaitForSeconds(humanData.timeToShowUpAfterWarning);
        for (int i = 00; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        HumanManager.instance.humansPresentCount++;
        StartCoroutine(GoAway());
    }


    private IEnumerator GoAway()
    {
        yield return new WaitForSeconds(Random.Range(humanData.presenceDuration.x,humanData.presenceDuration.y));
        for (int i = 00; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].enabled = false;
        }
        HumanManager.instance.humansPresentCount--;
        startingToShowUp = false;
        ResetTimeLeft();
    }


}
