using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController3 : MonoBehaviour, IDataPersistence
{
    private PlayerInput playerInput;
    private Rigidbody2D rb2D;
    [Header("Movimiento")]
    private float movimientoHorizontal=0f;
    [SerializeField] private float velocidadDeMovimiento;
    private bool mirandoDerecha=true;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private float coyoteTime=0.2f;
    private float contadorCoyoteTime;

    [Header("Dash")]
    [SerializeField] private float velocidadDash;
    [SerializeField] private float tiempoDash;
    private float gravedadInicial;
    private bool puedeHacerDash=true;
    private bool estaDash=false;
    private bool sePuedeMover=true;
    private bool estaDashV=false;
    private int numeroDash=1;
   
    TrailRenderer trailRenderer;

    private Vector3 respawnPoint;

    [Header("Menu")]
    public GameObject MenuInicial;

    [Header("Texto")]
    public GameObject frutas;

    string sceneName;
     Scene m_Scene;

    // Start is called before the first frame update


    private void Awake()
    {
        rb2D=GetComponent<Rigidbody2D>();
        playerInput=GetComponent<PlayerInput>();
        gravedadInicial=rb2D.gravityScale;
        trailRenderer=GetComponent<TrailRenderer>();
        
    }
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        DataPersistenceManager.instance.SaveGame();

    }

    //Colisiones del jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si colisiona con un tag Checkpoint el punto de reaparicion se actualiza
        if(collision.tag=="Checkpoint")
        {
            respawnPoint=transform.position;
        }
        //Si colisiona con un tag Muerte se llama al metodo PlayerDeath y se transporta al
        //Jugador al ultimo punto de reaparicion
        if(collision.tag=="Muerte")
        {
            //Se desactiva el trail
            trailRenderer.emitting=false;
            GameEventsManager.instance.PlayerDeath();
            transform.position=respawnPoint;   
        }
        //Si colisiona con fin se obtienen las frutas para ver si alcanza las necesarias
        //para la recompensa. si no se carga el segundo nivel. Se actualizan los datos para guardar
        if(collision.tag=="Fin")
        {
            string fruits = frutas.GetComponent<TextMeshProUGUI>().text;
            int numFruta=int.Parse(fruits);
            transform.position=Vector2.zero;
            respawnPoint=Vector2.zero;
            DataPersistenceManager.instance.SaveGame();
            if(numFruta==12)
            {
                SceneManager.LoadScene("Recompensa1");
            }
            else
            {
                SceneManager.LoadScene("Pico");
            }
            
        }
        //Si colisiona con el finjuego se actualizan los datos para guardar y se lleva a la escena
        //de estadisticas
        if(collision.tag=="FinJuego")
        {
            transform.position=Vector2.zero;
            respawnPoint=Vector2.zero;
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene("Estadisticas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Controlador para detectar si se toca el suelo
        enSuelo= Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        //Si esta en el suelo podrá hacer un dash además si ha tocado el suelo
        //Y salta se guarda que puede hacer solo un dash estando el aire
        if(enSuelo){
            puedeHacerDash=true;
            numeroDash=1;
            GetComponent<Animator>().SetBool("caida", false);
            contadorCoyoteTime=coyoteTime;
        }
        else{
            puedeHacerDash=false;
            contadorCoyoteTime-=Time.deltaTime;
        }
 
    //Logica para evitar error si se desconecta el mando o no hay
    if(InputSystem.GetDevice<Gamepad>()!=null){
        //Si en el teclado mantiene pulsado la W o en el mando la tecla con direccion hacia arriba se deshabilita el dash horizontal
        if(Keyboard.current.wKey.wasPressedThisFrame || Gamepad.current.dpad.up.wasPressedThisFrame){
        playerInput.actions.FindAction("Dash").Disable();
        }
        //Si deja de presionar estas teclas se habilita el dash
        if(Keyboard.current.wKey.wasReleasedThisFrame ||  Gamepad.current.rightTrigger.wasReleasedThisFrame){
        playerInput.actions.FindAction("Dash").Enable();
    }
    }
    //Si no hay mando se hace la logica de antes solo con teclado
    if(InputSystem.GetDevice<Gamepad>()==null){
        if(Keyboard.current.wKey.wasPressedThisFrame){
        playerInput.actions.FindAction("Dash").Disable();
        }
        if(Keyboard.current.wKey.wasReleasedThisFrame){
        playerInput.actions.FindAction("Dash").Enable();
    }
    }
        //Controlamos la animacion de caida
        if(rb2D.velocity.y<0f && !enSuelo)
        {
            GetComponent<Animator>().SetBool("caida", true);
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("corriendo", false);
            GetComponent<Animator>().SetBool("dashing", false);
            GetComponent<Animator>().SetBool("dashingV", false);
        }
        }

    private void FixedUpdate()
    {   //Si se puede mover se calcula el movimiento
        enSuelo= Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        if(sePuedeMover)
        {
           rb2D.velocity=new Vector2(movimientoHorizontal*velocidadDeMovimiento*Time.fixedDeltaTime, rb2D.velocity.y); 
          
        }
        //Controlamos la animacion de correr
        if(rb2D.velocity.x!=0f && enSuelo && !estaDash)
        {
        GetComponent<Animator>().SetBool("corriendo", true);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        }
        //Controlamos la animacion de saltar
        if(rb2D.velocity.y>0f && !enSuelo)
        {
            GetComponent<Animator>().SetBool("jump", true);
            GetComponent<Animator>().SetBool("corriendo", false);
            GetComponent<Animator>().SetBool("dashing", false);
            GetComponent<Animator>().SetBool("dashingV", false);
            GetComponent<Animator>().SetBool("caida", false);
        }
        //Giramos al personaje
        if(!mirandoDerecha && movimientoHorizontal>0f)
        {
            Girar();
        }else if(mirandoDerecha && movimientoHorizontal<0f)
        {
            Girar();
        }
    }



    public void Jump(InputAction.CallbackContext context){
        //Si se pulsa la tecla y esta en el suelo se realiza el salto con coyoteTime
        if(context.performed && contadorCoyoteTime>0f){
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
        //Si se suelta la tecla y no se ha alcanzado la altura maxima se cancela el salto
        //Esto proporciona un control en la altura que queramos en el salto
        if(context.canceled && rb2D.velocity.y>0f)
        {
            rb2D.velocity=new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            GetComponent<Animator>().SetBool("caida", true);
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("corriendo", false);
            GetComponent<Animator>().SetBool("dashing", false);
            GetComponent<Animator>().SetBool("dashingV", false);
            
            
        }
        //si se cancela el salto el tiempo coyote se pone a 0
        if(context.canceled)
        {
            contadorCoyoteTime=0f;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Obtenemos el animador e iniciamos la animacion si el personaje se mueve
        GetComponent<Animator>().SetBool("corriendo", true);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        //Se lee el valor que le llega desde el periferico
       movimientoHorizontal=context.ReadValue<Vector2>().x;
       //Si se cancela el movimiento se queda estatico
       if(context.canceled){
        GetComponent<Animator>().SetBool("corriendo", false);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        
       }
    }

    //Metodo para pausar el juego y habilitar el menu de pausa
    public void Pause(InputAction.CallbackContext context)
    {
        Time.timeScale=0f;
        MenuInicial.SetActive(true);
    }

    //Metodo para girar al personaje
    private void Girar()
    {
        mirandoDerecha=!mirandoDerecha;
        Vector3 localScale=transform.localScale;
        localScale.x*=-1;
        transform.localScale=localScale;
    }

    //Metodo para el dashHorizontal
    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && numeroDash==1 && !estaDashV)
        {
            numeroDash=0;
            StartCoroutine(Dash());
            
        }
    }


    //Metodo para el dashVertical
    public void DashVertical(InputAction.CallbackContext context)
    {
        if(context.performed && numeroDash==1 && !estaDash){
        
        numeroDash=0;
        StartCoroutine(DashV());
        }
    }

    IEnumerator Dash()
    {
        //Controlamos el tiempo que esta en dash
        float timeOut = Time.fixedTime + tiempoDash;
        //Controlamos la animacion
        GetComponent<Animator>().SetBool("corriendo", false);
        GetComponent<Animator>().SetBool("dashing", true);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        trailRenderer.emitting=true;
        sePuedeMover=false;
        puedeHacerDash=false;
        estaDash=true;
        numeroDash=0;
        //La escala de la gravedad se pone a 0
        rb2D.gravityScale=0;
        //Añadimos velocidad al personaje
        rb2D.velocity=new Vector2(velocidadDash * transform.localScale.x, 0);
        //Se controla el tiempo de dash
        while(Time.fixedTime < timeOut){
            
            yield return null;
        }
        
       //Una vez terminado el tiempo de dash se puede mover el personaje con la escala de gravedad del inicio
        sePuedeMover=true;
        estaDash=false;
        numeroDash=0;
        rb2D.gravityScale=gravedadInicial;
        //Se controla la animacion
        GetComponent<Animator>().SetBool("corriendo", false);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        trailRenderer.emitting=false;
        
    }

        IEnumerator DashV()
    {
        //Controlamos el tiempo que esta en dash
        float timeOut = Time.fixedTime + tiempoDash;
        //Controlamos la animacion
        GetComponent<Animator>().SetBool("corriendo", false);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", true);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        trailRenderer.emitting=true;
        sePuedeMover=false;
        puedeHacerDash=false;
        estaDashV=true;
        //La escala de la gravedad se pone a 0
        rb2D.gravityScale=0;
        //Añadimos velocidad al personaje
        rb2D.velocity=new Vector2(0, velocidadDash* 0.4f);
        //Se controla el tiempo de dash
        while(Time.fixedTime < timeOut){
            
            yield return null;
        }
        //Una vez terminado el tiempo de dash se puede mover el personaje con la escala de gravedad del inicio
        sePuedeMover=true;
        estaDashV=false;
        rb2D.gravityScale=gravedadInicial;
        //Se controla la animacion
        GetComponent<Animator>().SetBool("corriendo", false);
        GetComponent<Animator>().SetBool("dashing", false);
        GetComponent<Animator>().SetBool("dashingV", false);
        GetComponent<Animator>().SetBool("jump", false);
        GetComponent<Animator>().SetBool("caida", false);
        trailRenderer.emitting=false;
    }

    //Se obtienen los datos del archivo de guardado
    public void LoadData(GameData data)
    {
        this.transform.position=data.playerPosition;
        this.sceneName=data.sceneName;
        if(data.respawnPoint==Vector3.zero)
        {
            this.respawnPoint=transform.position;
        }
        else{
            this.respawnPoint=data.respawnPoint;
        }
    }

    //Se guardan los datos en el archivo de guardado
    public void SaveData(ref GameData data)
    {
        data.playerPosition=this.transform.position;
        data.sceneName=this.sceneName;
        data.respawnPoint=this.respawnPoint;
    }
}
