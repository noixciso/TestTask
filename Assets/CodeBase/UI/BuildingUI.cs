using CodeBase.Building;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class BuildingUI : MonoBehaviour
    {
        public TextMeshPro text;

        public void SetText(ReasonForStopping reasonForStopping) =>
            text.text = reasonForStopping.ToString();

        public void HideText() =>
            text.gameObject.SetActive(false);

        public void ShowText() =>
            text.gameObject.SetActive(true);
    }
}