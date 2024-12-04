using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    
    [SerializeField] private bool is_finished;

    private Image _image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_finished)
        {
            Debug.Log("Is finished");
            _image.enabled = true;
        }
        else
        {
            Debug.Log("Is not finished");
            _image.enabled = false;
        }
    }
}
