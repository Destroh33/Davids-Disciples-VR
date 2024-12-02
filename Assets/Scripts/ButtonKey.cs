using UnityEngine;
using UnityEngine.UI;
public class ButtonKey : MonoBehaviour
{
    [SerializeField] public KeyCode key;
    public bool OptionsUp = false;
    [SerializeField] private GameObject optionsMenu; 
    
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (!OptionsUp)
            {
                if (optionsMenu != null)
                {
                    optionsMenu.SetActive(true); 
                    Cursor.lockState = CursorLockMode.None;
                }
                GetComponent<Button>().onClick.Invoke();
                OptionsUp = true;
            }
            else
            {
                if (optionsMenu != null)
                {
                    optionsMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
                OptionsUp = false;
            }
        }
    }
}
