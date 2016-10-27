using UnityEngine;

public class DebugUI : MonoBehaviour
{
#if UNITY_EDITOR || DEBUG
    void Awake()
    {
        this.gameObject.SetActive(false);
    }
#endif
}
