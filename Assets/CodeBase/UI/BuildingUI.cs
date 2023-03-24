using CodeBase.Building;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class BuildingUI : MonoBehaviour
    {
        public TextMeshPro Text;

        public void SetText(ReasonForStopping reasonForStopping) =>
            Text.text = reasonForStopping.ToString();

        public void HideText() =>
            Text.gameObject.SetActive(false);

        public void ShowText() =>
            Text.gameObject.SetActive(true);
    }
}
