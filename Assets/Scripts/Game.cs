using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    private RectTransform PowerBar;
    private Camera camera;
    private GameObject Retry;
    private Vector3 offset;

    public int power_speed;
    public float direction_speed;
    private bool isFire;
    private bool isDirection;
    private int clickCount;
    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        PowerBar = GameObject.Find("Power").GetComponent<RectTransform>();
        Retry = GameObject.Find("Retry");

        clickCount = 0;
        isFire = false;
        isDirection = false;
        Retry.SetActive(false);

        offset = camera.transform.position - transform.position;
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1)
                isDirection = true;
            if (clickCount == 2)
            {
                isFire = true;
                Retry.SetActive(true);
                Throw(PowerBar.anchoredPosition.y);
            }
        }

        if (!isDirection && !isFire)
        {
            if (transform.position.x < -4 || transform.position.x > 4)
                direction_speed *= -1;

            transform.position += new Vector3(direction_speed, 0, 0);
        }

        if (!isFire && isDirection)
        {
            if (PowerBar.anchoredPosition.y >= -50 && PowerBar.anchoredPosition.y <= 350)
            {
                if (PowerBar.anchoredPosition.y > 300 || PowerBar.anchoredPosition.y < 10)
                    power_speed *= -1;

                PowerBar.anchoredPosition += new Vector2(0, power_speed);
            }
        }

        if (isFire && isDirection)
            camera.transform.position = transform.position + offset;
    }
    private void Throw(float power)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.back * 20000 * power * Time.deltaTime);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
}
