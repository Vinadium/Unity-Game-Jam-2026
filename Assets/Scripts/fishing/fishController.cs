using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishController : MonoBehaviour
{
    enum State {Swimming, Interested, Picking, Hooked};

    [Header("Swimming")]
    [SerializeField] float swimSpeed = 1.5f;
    [SerializeField] float waypointReachDist = 0.2f;

    [Header("Hook interaction")]
    [SerializeField] float noticeRange = 2f;
    [SerializeField] float pickRange = 0.4f;
    [SerializeField] int minPicks = 2;
    [SerializeField] int maxPicks = 5;
    [SerializeField] float pickInterval = 0.5f;
    [SerializeField, Range(0f, 1f)] float biteChance = 0.5f;
    [SerializeField] float loseInterestCooldown = 2f;

    [Header("References")]
    [SerializeField] hookMovement hook;
    [SerializeField] reelMinigame minigame;

    [Header("Data")]
    [SerializeField] fishData data;

    public float weight;
    public int scaleValue;

    public fishData Data => data;

    Vector2 minBounds, maxBounds;
    Vector2 waypoint;

    State state = State.Swimming;
    float ignoreHookUntil;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (hook == null) hook = FindAnyObjectByType<hookMovement>();
        if (minigame == null) minigame = FindAnyObjectByType<reelMinigame>(FindObjectsInactive.Include);

        minBounds = hook.minBounds;
        maxBounds = hook.maxBounds;
        pickNewWaypoint();
    }

    public void Initialize(fishData fishData, float rolledWeight)
    {
        data = fishData;
        weight = rolledWeight;
        if (data != null)
        {
            swimSpeed = data.swimSpeed;
            biteChance = data.biteChance;
            scaleValue = data.scaleValueFor(weight);
        }
    }

    void Update()
    {
        switch (state)
        {
            case State.Swimming: swimTick(); checkForHook(); break;
            case State.Interested: approachHook(); break;
        }
    }

    void swimTick()
    {
        moveToward(waypoint, swimSpeed);
        if (Vector2.Distance(transform.position, waypoint) < waypointReachDist)
            pickNewWaypoint();
    }

    void pickNewWaypoint()
    {
        waypoint = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );
    }

    void checkForHook()
    {
        if (hook == null || hook.isBusy) return;
        if (Time.time < ignoreHookUntil) return;
        if (Vector2.Distance(transform.position, hook.transform.position) < noticeRange)
            state = State.Interested;
    }

    void approachHook()
    {
        if (hook == null || hook.isBusy) { state = State.Swimming; return; }
        
        moveToward(hook.transform.position, swimSpeed);
        if (Vector2.Distance(transform.position, hook.transform.position) < pickRange)
        {
            state = State.Picking;
            StartCoroutine(pickRoutine());
        }
    }

    IEnumerator pickRoutine()
    {
        int picks = Random.Range(minPicks, maxPicks + 1);
        for (int i = 0; i<picks; i++)
        {
            // wiggle animation
            Debug.Log("The fish picks");
            yield return new WaitForSeconds(pickInterval);
            if (hook==null || hook.isBusy) { state = State.Swimming; yield break; }
        }

        if (Random.value <= biteChance) bite();
        else loseInterest();
    }

    void bite()
    {
        if (minigame == null || hook.isBusy) { loseInterest(); return;}
        Debug.Log("The fish bite the hook!");
        state = State.Hooked;
        hook.isBusy = true;
        minigame.startMinigame(this);
    }

    void loseInterest()
    {
        ignoreHookUntil = Time.time + loseInterestCooldown;
        state = State.Swimming;
        pickNewWaypoint();
    }

    public void onReeledIn()
    {
       hook.isBusy = false;

       if (data != null)
        {
            compendium.Instance?.recordCatch(data.fishName, weight);

            FishData caughtFish = new FishData
            {
                fishName = data.fishName,
                fishSprite = data.sprite,
                skeletonSprite = data.skeletonSprite,
                scalesBeforeSkeleton = scaleValue
            };
            fishInventory.Instance?.addFish(caughtFish);
        }
        Destroy(gameObject);
    }

    public void onEscape()
    {
        hook.isBusy = false;
        loseInterest();
    }

    void moveToward(Vector2 target, float speed)
    {
        Vector2 next = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (sr != null && Mathf.Abs(next.x - transform.position.x) > 0.001f)
            sr.flipX = next.x < transform.position.x;
        transform.position = next;
    }
}
