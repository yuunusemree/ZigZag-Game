
using UnityEngine;

public class WallMaker : MonoBehaviour
{
    public Transform lastWall;
    public GameObject wallPrefab;
    Vector3 lastPos;
    Camera cam;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastWall.position;
        player = FindObjectOfType<PlayerController>();
        cam = Camera.main;
        InvokeRepeating("CreateWalls", 1, 0.1f);
    }

    private void CreateWalls()
    {
        float distance = Vector3.Distance(lastPos, player.transform.position);
        if (distance > cam.orthographicSize * 2) return;
        
        Vector3 newPos = Vector3.zero;
        int rand = Random.Range(0, 11);
        if (rand<=5)
            newPos = new Vector3(lastPos.x - 0.707f, lastPos.y, lastPos.z + 0.707f);
        
        else
            newPos = new Vector3(lastPos.x + 0.707f, lastPos.y, lastPos.z + 0.707f);

        GameObject newBlock = Instantiate(wallPrefab, newPos, Quaternion.Euler(0, 45, 0), transform);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand % 3 == 2);
        lastPos = newBlock.transform.position;
    }
}
