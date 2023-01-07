using UnityEngine;
using UnityEngine.UI;


public class ResManager : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(Result);
    }

    public void Result(int value)
    {

        switch (value)
        {

            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(640, 360, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 3:
                Screen.SetResolution(640, 360, false);
                break;
        }

        Debug.Log("nowResolutionType" + value);

    }
}
