using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    Vector2 dir;
    [SerializeField] float speed;

    public Fish fishType;

    private void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;

        if(transform.position.y > GameManager.gameManager.divingManager.topLeft.position.y) 
        {
            Destroy(gameObject);
        }
        if (transform.position.x > GameManager.gameManager.divingManager.bottomRight.position.x)
        {
            Destroy(gameObject);
        }
    }

    public void SetDir(Vector2 newDir) 
    {
        dir = newDir;
    }

    void OnDestroy()
    {
        GameManager.gameManager.divingManager.fishBehaviours.Remove(this); 
    }
}
