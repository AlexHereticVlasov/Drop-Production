using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Opening : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        SceneManager.LoadScene(1);
    }
}
