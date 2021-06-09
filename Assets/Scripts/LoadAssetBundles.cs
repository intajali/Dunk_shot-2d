using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class LoadAssetBundles : MonoBehaviour
{
    public RawImage image;

    public string bundlePath = "avatar/avatar1.png";
    public string objectToLoad = "avatar1";

    // Start is called before the first frame update
    void Start()
    {
        bundlePath = "avatar/avatar1.png";
        string filePath = Application.streamingAssetsPath + "/AssetBundles/avatar";
       // filePath = System.IO.Path.Combine(filePath, bundlePath);

        //Debug.Log("Path :: "+filePath);
        // StartCoroutine(LoadAssets(filePath));

        DirectoryInfo info = new DirectoryInfo(filePath);
        List<string> localFileNameList = info.GetFiles().Select(t => Path.GetFullPath(t.FullName)).ToList();


        foreach (string item in localFileNameList)
        {
            //Debug.Log(item);
        }
       // StartCoroutine(DownloadAvatars(localFileNameList[0]));
    }

    //IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
    //{
    //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
    //    filePath = System.IO.Path.Combine(filePath, bundlePath);

    //    var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
    //    yield return assetBundleCreateRequest;

    //    AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

    //    AssetBundleRequest asset = asseBundle.LoadAssetAsync<Texture2D>(objectNameToLoad);
    //    yield return asset;

    //    Texture2D loadedAsset = asset.asset as Texture2D;

    //    image.texture = loadedAsset;
    //}


    IEnumerator LoadAssets(string url)
    {
        //using (var www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        //{
        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.LogError(www.error);
        //    }
        //    else
        //    {
        //        if (www.isDone)
        //        {
        //            Debug.Log("LoadAssets :: success :: ");
        //            //AssetBundle asseBundle = DownloadHandlerAssetBundle.GetContent(www);
        //            //AssetBundleRequest asset = asseBundle.LoadAssetAsync<Texture2D>(objectToLoad);
        //            //yield return asset;

        //            //Texture2D loadedAsset = asset.asset as Texture2D;

        //            //image.texture = loadedAsset;
        //        }
        //    }
        //}

        using (var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.LogError("www.isDone :: Download Successfully .");
                   
                }
            }
        }
    }

    IEnumerator DownloadAvatars(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Download Successfully");
            //Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.RGB24, false);
            //www.LoadImageIntoTexture(texture);

        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
