using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;

    public int point;
    public TextMeshProUGUI pointText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        point = 0;
        UpdateUI();
    }

    public void AddPoint(int value)
    {
        point += value;
        Debug.Log("Point sekarang: " + point);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (pointText != null)
        {
            pointText.text = "Point: " + point.ToString();
        }
    }
}
