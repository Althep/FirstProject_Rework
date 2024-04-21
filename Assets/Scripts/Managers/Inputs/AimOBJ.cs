using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOBJ : MonoBehaviour
{
    List<GameObject> targets = new List<GameObject>();
    GameObject playerObj;
    [SerializeField]LivingEntity targetEntity;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);
        playerObj = GameManager.instance.playerObj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveAimOBJ(Vector2 nextPos)
    {
        transform.position = nextPos;
    }
    public void SetTargets(GameObject collisionObj)
    {
        targets.Add(collisionObj);
    }
    public void FindTargetEntity()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].TryGetComponent<LivingEntity>(out targetEntity);
            Debug.Log(targetEntity);
        }
    }
    public void RemoveTargets()
    {
        targets.Clear();
    }
    public void AimOBjPositionReset()
    {
        transform.position = playerObj.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetTargets(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveTargets();
    }
    
}
