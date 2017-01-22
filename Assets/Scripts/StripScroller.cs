using UnityEngine;
using System.Collections;

public class StripScroller : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject[] backgrounds;

    private Player player;
    private int currbackground = 0;
    private int numBackgrounds;
    private float backgroundLength;

    void Start()
    {
        player = playerObj.GetComponent<Player>();
        numBackgrounds = backgrounds.Length;
        backgroundLength = backgrounds[0].GetComponent<Renderer>().bounds.size.y;
    }
    void Update()
    {
        float displacement = player.yDisplacement;

        int middle = (currbackground + 1) % numBackgrounds;

        if(playerObj.transform.position.y < backgrounds[middle].transform.position.y)
        {
            backgrounds[currbackground].transform.Translate(0, (numBackgrounds -1) * -backgroundLength, 0);
            currbackground = (currbackground + 1) % numBackgrounds;
        }

    }
}