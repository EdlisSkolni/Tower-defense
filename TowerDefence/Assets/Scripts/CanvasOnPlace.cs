using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOnPlace : MonoBehaviour
{
    private Camera cam;

    [HeaderAttribute("Fill in")]
    public RawImage[] ímages;
    public Canvas[] canvases;
    // Start is called before the first frame update
    void Start()
    {
        invisible();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Canvas c in canvases)
        {
            c.transform.LookAt(cam.transform.position);
        }
    }

    public void invisible()
    {
        foreach(RawImage image in ímages)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void visible()
    {
        foreach (RawImage image in ímages)
        {
            image.gameObject.SetActive(true);
        }
    }
}
