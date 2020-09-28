using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerachange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Button;
    public GameObject CameraC;
    public GameObject CameraC2;
    public GameObject player;
    private void Start()
    {
        Button.SetActive(false);
        CameraC2.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Button.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Button.SetActive(false);
    }
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            player.SetActive(false);
            CameraC2.SetActive(true);
           
        }
        if (Button.activeSelf&&Input.GetKeyDown(KeyCode.O))
        {
            CameraC2.SetActive(false);
           
            player.SetActive(true);
        }
    }
}
