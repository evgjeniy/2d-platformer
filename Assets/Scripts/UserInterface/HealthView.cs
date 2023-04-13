using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class HealthView : MonoBehaviour
    {
        public Slider barImage;
        public Text text;

        public void UpdateView(float value)
        {
            barImage.value = value / 100;
            text.text = $"{value}%";
        }
    }
}
