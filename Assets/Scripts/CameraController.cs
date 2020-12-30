using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Canvas canvas;
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(3f, 2f);
    public bool isLeft;
    private Transform player;
    private int lastX;
    public GameObject MaskIndikator;
    Image image;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
        MaskIndikator = GameObject.Find("Mask_indikator");
        image = MaskIndikator.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("maski");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (flag)
            {
                image.sprite = Resources.Load<Sprite>("maski");
                flag = !flag;
            }
            else
            {
                image.sprite = Resources.Load<Sprite>("maski2");
                flag = !flag;
            }
        }

        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (isLeft)         
            {
                 target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);        
            }
             else
              {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
             }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;


        }
    }

    public void FindPlayer( bool playerIsLeft) {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);        
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }

    }
}
