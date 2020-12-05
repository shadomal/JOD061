using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public float speed = 8f;
    // Update is called once per frame
    private Color playerColor;
    private Material mat;
    public int life;
    public float Movespeed = 6.5f;
    public float Turnspeed = 120f;
    [Header("ARMA PLAYER ATRIBUTOS")]
    public Transform Weapon;
    public GameObject Shoot;
    override public void OnStartServer()
    {
        base.OnStartServer();
        playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }
    override public void OnStartClient()
    {

    }
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = playerColor;
    }
    void Update()
    {
        /*if (isServer)
        {
            Debug.Log("Somente no servidor");
        }
        else if (isClient) Debug.Log("Somente no servidor");
        */

        if (!isLocalPlayer) return;

        if (Input.GetKey(KeyCode.Space))
        {
            GameObject tiro = Instantiate(Shoot, Weapon.position, transform.rotation);
            NetworkServer.Spawn(tiro);
        }

        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.forward * vert * Movespeed * Time.deltaTime);
        this.transform.localRotation *= Quaternion.AngleAxis(horz * Turnspeed * Time.deltaTime, Vector3.up);
    }
}
