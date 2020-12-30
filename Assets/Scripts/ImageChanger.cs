using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    //index of current image
    public int CurrentImageNumber;

    //names of images in Resources folder
    public string[] ImageNames = new string[] { "maski_8", "maski_9" };

    void Start()
    {
        //load CurrentImageNumber from disk
        CurrentImageNumber = PlayerPrefs.GetInt("CurrentImageNumber");
        //show image
        ShowImage();
    }

    private void ShowImage()
    {
        //get name of image for CurrentImageNumber
        var name = ImageNames[CurrentImageNumber];
        //set image to Image component
        GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
    }

    // Go to next CurrentImageNumber and show image
    // this method is handler of button click
    public void GoToNextImage()
    {
        //next CurrentImageNumber
        CurrentImageNumber = (CurrentImageNumber + 1) % ImageNames.Length;
        //show image
        ShowImage();
        //save CurrentImageNumber to disk
        PlayerPrefs.SetInt("CurrentImageNumber", CurrentImageNumber);
    }
}