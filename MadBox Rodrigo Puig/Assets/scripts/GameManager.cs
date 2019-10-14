using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum CameraPosition { BACK, LATERAL, CENITAL, FRONT}

    public Player player;

    CameraPosition previousCameraPos;
    CameraPosition actualCameraPos;

    [Header("scene to load when the game ends")]
    public string sceneName;

    [Header("UI")]
    public Transform goalPanel;

    [Header("Level")]
    public Transform spawnPoint;

    //Camera
    [Header("Camera")]
    public float timeToChangeTheCamera;
    public Transform backPos;
    public Transform lateralPos;
    public Transform cenitalPos;
    public Transform frontPos;

    Camera camera;
    Vector3 previousPos;
    float changeTheCameraCounter;

    bool needToChangeCameraPosition;
    bool needToRedirectCamera;

    private void Awake()
    {
        previousCameraPos = CameraPosition.BACK;
        actualCameraPos = CameraPosition.BACK;

        changeTheCameraCounter = 0;
        needToChangeCameraPosition = false;
        needToRedirectCamera = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        player.transform.position = spawnPoint.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (needToChangeCameraPosition)
        {
            changeTheCameraCounter += Time.deltaTime;

            if (previousCameraPos == CameraPosition.BACK)
            {
                if (actualCameraPos == CameraPosition.CENITAL)
                {
                    camera.transform.position = Vector3.Lerp(backPos.position, cenitalPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if(changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if (actualCameraPos == CameraPosition.LATERAL)
                {
                    camera.transform.position = Vector3.Lerp(backPos.position, lateralPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if (actualCameraPos == CameraPosition.FRONT)
                {
                    camera.transform.position = Vector3.Lerp(backPos.position, frontPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }

            }
            else if (previousCameraPos == CameraPosition.LATERAL)
            {
                changeTheCameraCounter += Time.deltaTime;

                if (actualCameraPos == CameraPosition.BACK)
                {
                    camera.transform.position = Vector3.Lerp(lateralPos.position, backPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if (actualCameraPos == CameraPosition.CENITAL)
                {
                    camera.transform.position = Vector3.Lerp(lateralPos.position, cenitalPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if (actualCameraPos == CameraPosition.FRONT)
                {
                    camera.transform.position = Vector3.Lerp(lateralPos.position, frontPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
            }
            else if (previousCameraPos == CameraPosition.CENITAL)
            {
                if (actualCameraPos == CameraPosition.BACK)
                {
                    camera.transform.position = Vector3.Lerp(cenitalPos.position, backPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if (actualCameraPos == CameraPosition.LATERAL)
                {
                    camera.transform.position = Vector3.Lerp(cenitalPos.position, lateralPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
                else if(actualCameraPos == CameraPosition.FRONT)
                {
                    camera.transform.position = Vector3.Lerp(cenitalPos.position, frontPos.position, changeTheCameraCounter / timeToChangeTheCamera);

                    if (changeTheCameraCounter >= timeToChangeTheCamera)
                    {
                        changeTheCameraCounter = 0;
                        needToChangeCameraPosition = false;
                    }
                }
            }
        }
        else if(needToRedirectCamera)
        {
            changeTheCameraCounter += Time.deltaTime;

            if(actualCameraPos == CameraPosition.BACK)
            {
                camera.transform.position = Vector3.Lerp(previousPos, backPos.position, changeTheCameraCounter / timeToChangeTheCamera);
            }
            else if(actualCameraPos == CameraPosition.CENITAL)
            {
                camera.transform.position = Vector3.Lerp(previousPos, cenitalPos.position, changeTheCameraCounter / timeToChangeTheCamera);
            }
            else if(actualCameraPos == CameraPosition.LATERAL)
            {
                camera.transform.position = Vector3.Lerp(previousPos, lateralPos.position, changeTheCameraCounter / timeToChangeTheCamera);
            }

            if (changeTheCameraCounter > timeToChangeTheCamera)
            {
                changeTheCameraCounter = 0;
                needToRedirectCamera = false;
            }
        }
        else if(!needToRedirectCamera && !needToChangeCameraPosition)
        {
            switch(actualCameraPos)
            {
                case CameraPosition.BACK:
                    camera.transform.position = Vector3.Lerp(camera.transform.position, backPos.position, 1f);
                    break;
                case CameraPosition.LATERAL:
                    camera.transform.position = Vector3.Lerp(camera.transform.position,lateralPos.position, 1f);
                    break;
                case CameraPosition.CENITAL:
                    camera.transform.position = Vector3.Lerp(camera.transform.position, cenitalPos.position, 1f);;
                    break;
            }
        }

        camera.transform.LookAt(player.transform.position);
    }

    public void SetLateralCamera()
    {
        previousCameraPos = actualCameraPos;
        actualCameraPos = CameraPosition.LATERAL;
        needToChangeCameraPosition = true;
    }

    public void SetCenitalCamera()
    {
        previousCameraPos = actualCameraPos;
        actualCameraPos = CameraPosition.CENITAL;
        needToChangeCameraPosition = true;
    }

    public void SetBackCamera()
    {
        previousCameraPos = actualCameraPos;
        actualCameraPos = CameraPosition.BACK;
        needToChangeCameraPosition = true;
    }

    public void RedirectCamera()
    {
        needToRedirectCamera = true;

        previousPos = camera.transform.position;
    }

    public void Goal()
    {
        previousCameraPos = actualCameraPos;
        actualCameraPos = CameraPosition.FRONT;
        needToChangeCameraPosition = true;

        goalPanel.gameObject.SetActive(true);
    }

    public void Reload()
    {
        player.transform.position = spawnPoint.position;

        actualCameraPos = CameraPosition.BACK;
        camera.transform.position = backPos.position;
    }

    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
