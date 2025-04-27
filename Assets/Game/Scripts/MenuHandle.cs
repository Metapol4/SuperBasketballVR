using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuHandle : MonoBehaviour
{
    [SerializeField] InputActionReference m_MenuButton;

    [SerializeField] [Range(0f, 5f)] private float MenuDistance = 2.5f;
    [SerializeField] [Range(0f, 5f)] private float OffsetHeightWithPlayer = 0f;
    [SerializeField] GameObject MenuPrefab;


    [SerializeField] UnityEvent OnMenuShowUp = new UnityEvent();
    [SerializeField] UnityEvent OnMenuHide = new UnityEvent();

////////////////////////////////////////////////////////////////////////
    Camera m_Camera;
    bool bIsOpen;
    GameObject m_MenuInstance;

////////////////////////////////////////////////////////////////////////


    private void Awake()
    {
        bIsOpen = false;
        //Cache the camera to reduce call of get camera
        m_Camera = Camera.main;
        CreateMenu();
    }

 
    void CreateMenu()
    {
        if (MenuPrefab)
        {
            MenuPrefab.SetActive(false);
            //Create menu base and cache it for future use 
            m_MenuInstance = Instantiate(MenuPrefab);
        }
    }

    //Event trigger when the menu is visible
    protected virtual void OnEnable()
    {
        m_MenuButton.action.Enable();
        m_MenuButton.action.performed += Press;
    }

    //Event trigger when the menu is not visible
    protected virtual void OnDisable()
    {
        m_MenuButton.action.Disable();
        m_MenuButton.action.performed -= Press;
    }

    protected virtual void Press(InputAction.CallbackContext context)
    {
        bIsOpen = !bIsOpen;
        OnEnabledUI(bIsOpen);
    }

    //this is called when the menu is opened or closed to make the UI face the player in the correct direction
    public virtual void OnEnabledUI(bool bIsEnabled)
    {
        if (bIsEnabled)
        {
            Transform camTransform = m_Camera.transform;
            Vector3 camLoc = camTransform.position;

            //Menu Position Logic
            var camRot = camTransform.rotation;
            camRot.z = 0f;
            //Calculate and apply distance to the menu with the player
            Vector3 finalDirection = (camRot * Vector3.forward).normalized;
            Vector3 targetPos = camLoc + finalDirection * MenuDistance;
            m_MenuInstance.transform.position = targetPos;

            //Calculate and apply the rotation to the menu
            Vector3 directionToPlayer = m_MenuInstance.transform.position - camLoc;
            m_MenuInstance.transform.rotation = Quaternion.LookRotation(directionToPlayer);

            //finnaly open the menu
            m_MenuInstance.SetActive(true);
            OnMenuShowUp.Invoke();
        }
        else
        {
            //mean the menu is closed
            m_MenuInstance.SetActive(false);
            OnMenuHide.Invoke();
        }
    }
}