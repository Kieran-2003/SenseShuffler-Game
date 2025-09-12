using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    public SpriteRenderer Sr;
    public GameObject player;
    public int triggerDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,player.transform.position) < triggerDistance && InputManager.Instance.isMoving)
        {
            Sr.color = Color.red;
        }
        else
        {
            Sr.color = Color.purple;
        }
    }
}
