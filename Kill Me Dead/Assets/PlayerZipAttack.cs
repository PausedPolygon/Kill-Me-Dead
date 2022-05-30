using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZipAttack : MonoBehaviour
{
    [SerializeField] LayerMask enemyCollider;
    [SerializeField] float zipRange = 20f;
    [SerializeField] bool hasEnemy;
    [SerializeField] float zipSpeed = 10f;
    [SerializeField] float travelPercent;
    [SerializeField] Transform playerViewObject;
    [SerializeField] PlayerDot playerDot;

    Transform enemy;
    bool isZipping;

    PlayerMovement playerMovement;
    MeleeAttack meleeAttack;
    PlayerView playerView;
    PlayerStateHandler playerStateHandler;
    PlayerHealth playerHealth;

    private void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttack = GetComponent<MeleeAttack>();
        playerView = GetComponent<PlayerView>();
        playerStateHandler = GetComponent<PlayerStateHandler>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start() 
    {
        hasEnemy = false;
        isZipping = false;    
    }

    private void Update()
    {
        Ray ray = new Ray(playerViewObject.transform.position, playerViewObject.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, zipRange, enemyCollider))
        {
            hasEnemy = true;
            if (playerStateHandler.canZip)
            {
                playerDot.ChangeDot(hasEnemy);
            }
            enemy = hitInfo.transform;
        }

        else
        {
            hasEnemy = false;
            if (playerStateHandler.canZip)
            {
                playerDot.ChangeDot(hasEnemy);
            }
            enemy = null;
        }
    }

    public void Zip()
    {
        if (playerStateHandler.canZip)
        {
            if (hasEnemy)
            {
                if (!isZipping && enemy != null)
                    StartCoroutine(ZipMovement());
                    //playerView.isViewActive = false;
            }
        }
    }

    private IEnumerator ZipMovement()
    {
        playerHealth.canTakeDamage = false;
        Transform enemyTransform = enemy.transform;
        isZipping = true;
        Vector3 currentPosition = transform.position;
        Vector3 zippedPosition = enemyTransform.GetComponent<EnemyAI>().zipPosition.position;
        travelPercent = 0f;

        while (travelPercent <= 1)
        {
            travelPercent += Time.deltaTime * zipSpeed;
            transform.position = Vector3.Lerp(currentPosition, zippedPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        meleeAttack.PerformMeleeAttack();
        playerMovement.Jump();

        yield return new WaitForSeconds(0.5f);
        playerHealth.canTakeDamage = true;
        isZipping = false;
        // playerView.isViewActive = false;
        // playerViewObject.LookAt(enemyTransform);
        // playerView.isViewActive = true;
    }

    private void OnDrawGizmos() 
    {
        Debug.DrawRay(playerViewObject.transform.position, playerViewObject.transform.forward * zipRange, Color.red);
    }
}