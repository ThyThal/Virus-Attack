using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
    }
#endif

    [SerializeField] public int current;
    [SerializeField] private int _minimun;
    [SerializeField] private int _maximum;
    [SerializeField] private Image _mask;
    [SerializeField] private Image _fill;
    [SerializeField] private Color _color;

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = current - _minimun;
        float maximumOffset = _maximum - _minimun;
        float fillAmount = currentOffset / maximumOffset;
        _mask.fillAmount = fillAmount;
        _fill.color = _color;
    }
}
