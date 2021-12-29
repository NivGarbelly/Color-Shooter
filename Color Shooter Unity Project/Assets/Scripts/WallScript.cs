using UnityEngine;

public class WallScript : MonoBehaviour
{
    private enum ObjColor
    {
        red = 0,
        green = 1,
        blue = 2
    };
    [SerializeField] private ObjColor currentColor;
    private void Awake()
    {
        switch (currentColor)
        {
            case ObjColor.red:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.red);
                break;
            case ObjColor.blue:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.blue);
                break;
            case ObjColor.green:
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.green);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullets"))
        {
            if (other.gameObject.GetComponents<Renderer>() != null)
            {
                var otheRenderer = other.GetComponent<Renderer>();
                var cubeRenderer = GetComponent<Renderer>();
                if (otheRenderer.material.GetColor("_BaseColor") ==cubeRenderer.material.GetColor("_BaseColor"))
                {
                   gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.black);
                   Collider col = GetComponent<Collider>();
                   col.enabled = false;
                }
            }
        }
    }
}
