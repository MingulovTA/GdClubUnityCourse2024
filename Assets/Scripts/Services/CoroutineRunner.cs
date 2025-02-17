using System.Collections;
using UnityEngine;

//Todo Refucktor this...
public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _runner;
    private static bool _inited;

    public static CoroutineRunner Instance
    {
        get
        {
            if (!_inited)
                Init();
            return _runner;
        }
    }

    public Coroutine Run(IEnumerator coroutine)
    {
        return _runner.StartCoroutine(coroutine);
    }

    public void Stop(Coroutine unmuteChecker)
    {
        _runner.StopCoroutine(unmuteChecker);
    }
    
    private static void Init()
    {
        GameObject gm = new GameObject();
        DontDestroyOnLoad(gm);
        gm.name = "CoroutineRunner";
        _runner = gm.AddComponent<CoroutineRunner>();
        _inited = true;
    }
}
