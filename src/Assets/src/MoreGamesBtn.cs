using UnityEngine;
using System.Collections;

public class MoreGamesBtn : MonoBehaviour
{
    public tk2dUIItem btn;

    private PluginsProxy _plugins;
    public PluginsProxy plugins
    {
        get
        {
            if (_plugins == null)
            {
                Debug.Log(GameObject.Find("PluginsController"));
                _plugins = GameObject.Find("PluginsController").GetComponent<PluginsProxy>();
            }

            return _plugins;
        }
    }


    void Start()
    {
        btn.OnClick += click;
    }

    void Update()
    {

    }

    void click()
    {
        plugins.moreGames();
    }


}
