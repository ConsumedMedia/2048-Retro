using UnityEngine;
using PlayHaven;
using Chartboost;

public class PluginsProxy : MonoBehaviour 
{
    static public bool INTERNET_AVAILABLE = true;
    static public string FLURRY_KEY = "";

    private bool _isEnabled = false;
    private bool _isInited = false;
    private bool _needChartboost = false;
    private NerdFlurry _flurryProxy;

    #if UNITY_EDITOR_OSX || UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_WIN

    #elif UNITY_ANDROID


    void Start () 
    {
    _initPlugins();
    }

    void OnApplicationPause(bool pause)
    {
    if(!pause && _isInited)
    {
    PlayHavenManager.instance.OpenNotification();
    CBBinding.init();
    }
    }

    void Update()
    {
    if (Input.GetKeyUp(KeyCode.Escape)) 
    {
    if (_isInited && CBBinding.onBackPressed())
    return;
    }
    }

    #elif UNITY_IPHONE
    void Awake() 
    {
    _initPlugins();
    }

    #endif

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
    }

    public void _initPlugins()
    {
        FB.Init(OnInitComplete);
        _isEnabled = true;
        Debug.Log("C# _initPlugins");
        _isInited = true;
        _flurryProxy = new NerdFlurry();

        #if UNITY_ANDROID && !UNITY_EDITOR
        _flurryProxy.StartSession(Settings.instance.flurryAndroid);
        Debug.Log("C# CBBinding.init()");
        #elif UNITY_IPHONE && !UNITY_EDITOR
        _flurryProxy.StartSession(Settings.instance.flurryIOS);
        #endif
        #if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
        PlayHavenManager.instance.OpenNotification();
        #endif

        #if UNITY_ANDROID && !UNITY_EDITOR
        CBBinding.init();
        #elif UNITY_IPHONE && !UNITY_EDITOR
        CBBinding.init(Settings.instance.chartboostID, Settings.instance.chartboostSignature);
        #endif
        CBBinding.cacheInterstitial(null);
        CBBinding.cacheMoreApps();

        PlayHavenManager.instance.ContentPreloadRequest(Settings.instance.playhavenFullscreen);
    }

    void OnApplicationQuit()
    {
        #if UNITY_ANDROID || UNITY_IPHONE
        if (_flurryProxy != null)
            _flurryProxy.EndSession();
        #endif
        UserData.save();
    }

    public void showBanner()
    {
        if (UserData.adRemoved)
            return;

        #if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR

        if (_needChartboost && CBBinding.hasCachedInterstitial(null))
        {
        Debug.Log("C# chartboost banner");
        CBBinding.showInterstitial(null);
        }
        else
        {
        Debug.Log("C# playhaven banner");
        PlayHavenManager.instance.ContentRequest(Settings.instance.playhavenFullscreen, true);
        PlayHavenManager.instance.ContentPreloadRequest(Settings.instance.playhavenFullscreen);
        }

        if (_needChartboost)
            CBBinding.cacheInterstitial(null);

        _needChartboost = !_needChartboost;

        #endif
    }

    public void moreGames() 
    {
        if (!_isEnabled)
            return;

        Debug.Log("C# moreGames");

        if (CBBinding.hasCachedMoreApps())
        {
            CBBinding.showMoreApps();
            Debug.Log("C# moreGames chartboost");
        }

        CBBinding.cacheMoreApps();
    }
}

