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
    [SerializeField] public int maximum;
    [SerializeField] private int _minimun;
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
        float maximumOffset = maximum - _minimun;
        float fillAmount = currentOffset / maximumOffset;
        _mask.fillAmount = fillAmount;
        _fill.color = _color;
    }
}
