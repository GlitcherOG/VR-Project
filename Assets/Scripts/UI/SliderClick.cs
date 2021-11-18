using UnityEngine;
using UnityEngine.UI;

public class SliderClick : MonoBehaviour
{
    public Button increaseVol, decreaseVol;
    private Slider volumeSlider;
    [SerializeField] private float volumeLevel;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
    }

    public void IncreaseVolume()
    {
        volumeSlider.value += volumeLevel;
    }

    public void DecreaseVolume()
    {
        volumeSlider.value -= volumeLevel;
    }
}
