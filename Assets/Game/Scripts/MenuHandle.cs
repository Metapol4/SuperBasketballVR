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
        m_Camera = Camera.main;
        CreateMenu();
    }

    void CreateMenu()
    {
        if (MenuPrefab)
        {
            MenuPrefab.SetActive(false);
            m_MenuInstance = Instantiate(MenuPrefab);
        }
    }

    protected virtual void OnEnable()
    {
        m_MenuButton.action.Enable();
        m_MenuButton.action.performed += Press;
    }

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
    
   public virtual void OnEnabledUI(bool bIsEnabled)
    {
        if (bIsEnabled)
        {
            Transform camTransform = m_Camera.transform;
            Vector3 camLoc = camTransform.position;

            //Menu Position Logic
            var camRot = camTransform.rotation;
            camRot.z = 0f;
            
            Vector3 finalDirection = (camRot * Vector3.forward).normalized;
            Vector3 targetPos = camLoc + finalDirection * MenuDistance;
            m_MenuInstance.transform.position = targetPos;

            //Menu Rotation Logic
            Vector3 directionToPlayer = m_MenuInstance.transform.position - camLoc;
            m_MenuInstance.transform.rotation = Quaternion.LookRotation(directionToPlayer );
          
            m_MenuInstance.SetActive(true);
            OnMenuShowUp.Invoke();
        }
        else
        {
            m_MenuInstance.SetActive(false);
            OnMenuHide.Invoke();
        }
    }
}