using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FMod
{
    public class SaveScreenshotToPC : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Use dedicated camera")]
        [SerializeField]
        Camera screenShotcamera;
        [SerializeField]
        int renderDepth = 32;
        [SerializeField]
        RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;
        [SerializeField]
        TextureFormat textureFormat = TextureFormat.RGB24;

        [Header("Use mutiple camera")]
        [SerializeField]
        bool useMultipleCamera = true;

        [Space]
        [SerializeField]
        string folderPath;

        [Space]
        [SerializeField]
        KeyCode hotKey = KeyCode.Space;

        RenderTexture renderTexture;

        bool isTakingScreenshot = false;

        [field: System.NonSerialized]
        public string PredefinedSavePath { get; set; } = null;

        public void LateUpdate()
        {
            if (Input.GetKeyDown(hotKey))
            {
                TakeScreenshotAndSave();
            }
        }

        [ContextMenu("TakeScreenshotAndSave")]
        public void TakeScreenshotAndSave()
        {
            ///
            if (isTakingScreenshot)
            {
                return;
            }

            ///
            if (!gameObject.activeInHierarchy)
            {
                gameObject.transform.SetParent(null);
                gameObject.SetActive(true);
            }

            ///
            StartCoroutine(useMultipleCamera ? MultiCamerasTakeScreenshotAndSaveAsync() : TakeScreenshotAndSaveAsync());
        }

        [ContextMenu("Unity_TakeScreenshotAndSave")]
        public void Unity_TakeScreenshotAndSave()
        {
            string filePath = folderPath + "/" + System.DateTime.Now.Ticks.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(filePath);

            ///
            Debug.LogFormat("Saved screen shot: {0}", filePath);
        }

        IEnumerator TakeScreenshotAndSaveAsync()
        {
            ///
            isTakingScreenshot = true;

            ///
            string filePath = GetSavePath();

            ///
            yield return new WaitForEndOfFrame();

            ///
            int resX = Screen.width;
            int resY = Screen.height;

            // Render
            renderTexture = new RenderTexture(resX, resY, renderDepth, renderTextureFormat);
            screenShotcamera.targetTexture = renderTexture;
            screenShotcamera.Render();

            // Read pixel
            Texture2D capturedScreenshot = new Texture2D(renderTexture.width, renderTexture.height, textureFormat, false);
            RenderTexture.active = renderTexture;
            capturedScreenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            capturedScreenshot.Apply(false);

            // Encode and save
            byte[] bytes = capturedScreenshot.EncodeToPNG();
            Object.Destroy(capturedScreenshot);
            File.WriteAllBytes(filePath, bytes);

            ///
            Debug.LogFormat("Saved screen shot: {0}", filePath);

            ///            
            RenderTexture.active = null;
            screenShotcamera.targetTexture = null;
            renderTexture.Release();

            ///
            isTakingScreenshot = false;
        }

        IEnumerator MultiCamerasTakeScreenshotAndSaveAsync()
        {
            ///
            isTakingScreenshot = true;

            ///
            string filePath = GetSavePath();

            ///
            int resX = Screen.width;
            int resY = Screen.height;

            ///
            yield return new WaitForEndOfFrame();

            // Read pixel
            Texture2D capturedScreenshot = new Texture2D(resX, resY, textureFormat, false);
            capturedScreenshot.ReadPixels(new Rect(0, 0, resX, resY), 0, 0);
            capturedScreenshot.Apply(false);

            // Encode and save
            byte[] bytes = capturedScreenshot.EncodeToPNG();
            Object.Destroy(capturedScreenshot);
            File.WriteAllBytes(filePath, bytes);

            ///
            Debug.LogFormat("Saved screen shot: {0}", filePath);

            ///
            isTakingScreenshot = false;
        }

        private string GetSavePath()
        {
            if (string.IsNullOrWhiteSpace(PredefinedSavePath))
            {
                return folderPath + "/" + System.DateTime.Now.Ticks.ToString() + ".png";
            }
            else
            {
                return PredefinedSavePath;
            }
        }
#endif
    }

}