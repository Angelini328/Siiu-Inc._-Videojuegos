using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour
{

    

    public float jumpv = 0.4f;
    public float altura_salto = 10;
    public Rigidbody rb;

    public int Velocidad = 0;
    public float giro= 0;
    public float Horizontal = 0;
    public float vertical = 0;
    public float limite_x = 0;
    public float limite_z = 0;
        
    bool esta_en_suelo;
    public int vida = 0;
    public Transform reaparicion;
    public Text Msg_perdiste;
    public Text continuar;
    bool muerte;
    bool ganar;


    // Start is called before the first frame update
    void Start()
    {
        Msg_perdiste.enabled = false;
        continuar.enabled = false;
        esta_en_suelo = true;
        rb = GetComponent<Rigidbody>();
        muerte = false;
        ganar = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(muerte == true){
            dead();
        }

        if(ganar == true){
            ganaste();
        }


        if (Input.GetKeyDown(KeyCode.Space) && esta_en_suelo == true){
            Jump();
        }

        Horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * Velocidad * vertical);
        transform.Translate(Vector3.right * Time.deltaTime * giro * Horizontal);

        if(vida >0){
            Debug.Log(vida);

        if (transform.position.x < -limite_x || transform.position.x > limite_x ){
            vida = vida -1;
            this.transform.position = reaparicion.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //transform.position = new Vector3(-limite_x, transform.position.y, transform.position.z);
        }
        
        if (transform.position.z < -limite_z || transform.position.z > limite_z ){
            vida = vida -1;
            this.transform.position = reaparicion.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //transform.position = new Vector3(transform.position.x, transform.position.y, -limite_z);
        }
        } else{
            Msg_perdiste.enabled = true;
                continuar.enabled = true;
            if (Input.GetKeyDown(KeyCode.T)){
            Msg_perdiste.enabled = false;
            continuar.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        }
        void dead(){
        muerte = false;
        vida = vida - 1;
        this.transform.position = reaparicion.transform.position;
        }
        void ganaste(){
        ganar = false;
        Destroy(gameObject);
    }
    }
    public void Jump(){
        esta_en_suelo = false;
        rb.AddForce(0, altura_salto, 0, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Suelo")){
            esta_en_suelo = true;
        }
        if(other.gameObject.CompareTag("muerte")){
            muerte = true;
        }
        if(other.gameObject.CompareTag("ganar")){
            ganar = true;
    
    }
    }

}
