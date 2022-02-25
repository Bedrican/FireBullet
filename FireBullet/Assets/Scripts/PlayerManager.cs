using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region PlayerStats
    
    private float playerSpeed;

    private float playerAngularSpeed;

    #endregion
    private Vector3 rotateVector;
    private Vector3 firstPos;
    private Vector3 endPos;
    private Rigidbody rb;
    public LayerMask enemyLayers;
    public  Transform attackPoint;
    public static Transform target;
    public float nextAttackTime = 0.0f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveAndRotate();
        nextAttackTime += Time.deltaTime;
        if (nextAttackTime > 0.1f)
        {
            Attack();
            nextAttackTime = 0;
        }
        
        
    }
    
    public void InitializePlayer() {
        gameObject.transform.position = new Vector3(0,1.5f,-23);
        gameObject.transform.eulerAngles = Vector3.zero;
        playerSpeed = DataManager.Instance._GameDesignSO.playerSpeed;
        playerAngularSpeed = DataManager.Instance._GameDesignSO.playerAngularSpeed;
    }
    void MoveAndRotate() {
        
        /*float zMove,xMove, orientation;
        zMove = Input.GetAxis("Vertical");
        orientation = Input.GetAxis("Horizontal");
        rotateVector = new Vector3(0, orientation * playerAngularSpeed * Time.deltaTime);
        gameObject.transform.Translate(new Vector3(0,0,zMove*playerSpeed*Time.deltaTime));
        gameObject.transform.Rotate(rotateVector);*/
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Moved)
            {
                rb.MovePosition(new Vector3(touch.deltaPosition.x*kat,0,touch.deltaPosition.y*kat));
            }*/
        if(Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            float farkX = endPos.x - firstPos.x;
            float farkZ = endPos.y - firstPos.y;
            
            transform.Translate(farkX*Time.deltaTime/50,0,farkZ*Time.deltaTime/50);
        }
        if(Input.GetMouseButtonUp(0))
        {
            firstPos = Vector3.zero;
            endPos = Vector3.zero;
        }
        
        
    }

    private void Attack()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, DataManager.Instance._GameDesignSO.attackRange, enemyLayers);
        /*if (hitEnemies == null)
        {
            return;
        }
        else*/
        
            foreach (Collider enemy in hitEnemies)
            {
                /*float rangeBetween;
                rangeBetween = Mathf.Abs((transform.position - enemy.transform.position).magnitude);
                enemy.GetComponent<SimpleEnemyMove>().GetDamage(DataManager.Instance._GameDesignSO.bulletDamage);
                Debug.Log(enemy.GetComponent<SimpleEnemyMove>().health);*/
                if (enemy.TryGetComponent<SimpleEnemyMove>(out SimpleEnemyMove simpleEnemyMove))
                {
                    target = enemy.transform;
                    GameObject bulletObj = PoolManager.Instance.GetBulletFromPool();
                    bulletObj.SetActive(true);
                    bulletObj.GetComponent<FireBullet>().MoveBullet(attackPoint.transform, enemy.transform , Vector3.forward);
                }
            }
        
        
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, DataManager.Instance._GameDesignSO.attackRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerDeath();
        }
    }
}
