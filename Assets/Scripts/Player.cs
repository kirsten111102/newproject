using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 10;
    public string gender = "Male";
    void Start()
    {
        Debug.Log("Player started");
        Debug.Log("Player level: " + level);
        Debug.Log("Player gender: " + gender);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
