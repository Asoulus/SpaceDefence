using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public int transitionTime = 1000;

    public List<CanvasGroup> canvases = new List<CanvasGroup>();

    [SerializeField]
    private GameObject _blackScreen = null;

    private void Start()
    {
        _blackScreen.SetActive(false);
    }

    public async void PrepareForLoading(string sceneName)
    {
        if (canvases.Count > 0)
        {
            foreach (var c in canvases)
            {
                c.alpha = 0;
                c.blocksRaycasts = false;
                c.interactable = false;
            }
        }

        _blackScreen.SetActive(true);

        await Task.Delay(transitionTime);

        LoadScene(sceneName);
    }

    public async void PrepareForReloading()
    {
        if (canvases.Count > 0)
        {
            foreach (var c in canvases)
            {
                c.alpha = 0;
                c.blocksRaycasts = false;
                c.interactable = false;
            }
        }

        _blackScreen.SetActive(true);

        await Task.Delay(transitionTime);

        ReloadScene();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator DelayLoading(string _lvlName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(_lvlName, LoadSceneMode.Single);
    }
}
