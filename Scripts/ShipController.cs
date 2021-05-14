using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float fwdSPeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeFwdSpeed, activeStrafeSpeed, activeHoverSPeed;
    private float fwdAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration =2f;

    public float lookRotateSpeed = 90f;
    
    private Vector2 lookInput, screenCenter, mouseDistance;
    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    [SerializeField]GameObject[] lasers;
    [SerializeField]GameObject VictoryUI;
   
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
        
        
    }
    void Update()
    {
        Movement();
        Shooting();

        if(GameObject.FindGameObjectsWithTag("Enemy").Length < 1) {
            VictoryUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Shooting()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot(true);
        }
        else{
            Shoot(false);
        }
    }

    void Shoot(bool keyPressed)
    {
        foreach (GameObject i in lasers){
            var module = i.GetComponent<ParticleSystem>().emission;
            module.enabled = keyPressed;
        }
    }

    void Movement()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeFwdSpeed = Mathf.Lerp(activeFwdSpeed, Input.GetAxisRaw("Vertical") * fwdSPeed, fwdAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSPeed = Mathf.Lerp(activeHoverSPeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeFwdSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSPeed * Time.deltaTime;
        
    }
}
