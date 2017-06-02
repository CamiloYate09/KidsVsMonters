using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;// el daño que hace por disparo
    public float timeBetweenBullets = 0.15f;// el tiempo que tiene que pasar entre disparo y disparo
    public float range = 100f;//contiene la distancia que tiene la bala desde que sale desde el arma

    //variables con relacion a las balas del jugador

    public Slider ammoSlider;// variable de contandor de las balas que disponemos
    public int masBullets = 100;// variable de la cantidad de balas 


    public Image enemyImage; // variable con relacion al icono del enemigo 

    public Slider enemyHealthSlider;
    public Light pointLight;// varaible con referencia a la luz del disparo 


    float timer;//contar el tiempo que ha pasado desde el ultimo disparo
    Ray shootRay = new Ray();
    //Ray shootRay;//el rayo del disparo
    RaycastHit shootHit;//el resultado del objeto, con el cual la balla alla colisionado
    int shootableMask;//representar las capas o colider, con el cual la bala penetre
    //compononetes empeciales de 
    ParticleSystem gunParticles;//
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;// el tiempo que van a estar activas las luces
    int currentBullets;


    void Awake()
    {//refernecia a todos los componentes 
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        currentBullets = masBullets;//inicializacion del valor de balas que tenemos 
        StartCoroutine(ReloadAmmo()); // mandamos a llamar el metodo 
        enemyImage.enabled = false; // queremos que el icono del enemigo al iniciar este oculto 
        enemyHealthSlider.gameObject.SetActive(false);// desabilitamos el slider de vida total en el inspector 
    }

    //se crea una corrutina para no quedarnos sin balas 
    //recargar la municion 
    IEnumerator ReloadAmmo()
    {

        // el tiempo que tiene que pasar para recargar una municion

        float reloadTime = timeBetweenBullets * 2;  // tiempo calculado hay 

        while (true)
        {

            yield return new WaitForSeconds(reloadTime);
            if (currentBullets < masBullets)
            { // si tenemos menos balas de las recargamos entonces las incrementamos 

                currentBullets++;//se incrementan en una
                ammoSlider.value = currentBullets; // incrementamos la barra del slider 

            }
        }
    }

    //se crea esta corrutina para cuando se deje de disparar al enemigo se oculte el slider de vida

    IEnumerator SchuduleHideEnemyUI()
    {


        yield return new WaitForSeconds(1);// ocultar por un segundos el slider de vida del jugador

        HideEnemyUI();// mandamos a llamar al metodo de la imagen y el slider para que se oculten

    }

    void HideEnemyUI()
    {

        enemyImage.enabled = false;
        enemyHealthSlider.gameObject.SetActive(false);
    }


    void Update()
    {


        timer += Time.deltaTime;//incrementamos el tiempo que ha pasado, desde la ultima vez que se disparo 

        //"!EventSystem.current.IsPointerOverGameObject(-1)" que no estemos encima de ningun objeto, de la interfaz de ususrio UI en inspector 
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && !EventSystem.current.IsPointerOverGameObject(-1))//comprobacion si hay que disparar// entradas de mouse "Fire1
        {// esta pausado el juego timeScale != 0
            Shoot();//disparo
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();//desactivar la luz y render de efectos 
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        pointLight.enabled = false;// luz del cañon de la pistola desactivado 
    }


    void Shoot()
    {

        //comprobar el estado de balas
        if (currentBullets <= 0) return; // como no tenemos balas no disparamos 

        //si tenemos balas 
        currentBullets--; //decrementariamos las balas que tenemos 
        ammoSlider.value = currentBullets;// actualizariamos el valor de balas con el slider que tenemos a cargo 
        timer = 0f;// inicializamos la variable

        gunAudio.Play();//se reproduce el sonido de audio 

        gunLight.enabled = true;//activamos el componente de la luz
        pointLight.enabled = true;// la segunda luz del disparo 

        gunParticles.Stop();//detenemos el efecto de particulas
        gunParticles.Play();//inicializamos el efecto de particulas

        gunLine.enabled = true;//
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;//es una estrucutra ray // el origen de donde va salir la bala
        shootRay.direction = transform.forward;//de donde sale la bala y en que direccion

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))//simulacion de disparo, vamos colisionar con algun objeto
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);// cuando le disparamos al enemigo
                enemyImage.enabled = true;// cuando se le dispare al enemigo queremos que aparesca el icono de este 
                enemyImage.sprite = enemyHealth.icon;// llamamos al codigo del enemigo con su imagen asiganada en esta clase 
                enemyHealthSlider.gameObject.SetActive(true);//si hemos disparamo al enemigo queremos que este activo el slider de vida
                enemyHealthSlider.value = enemyHealth.GetHealthPercentage();// saber la vida del enemigo 
                StopCoroutine("SchuduleHideEnemyUI");//paramos la corrutina 
                StartCoroutine("SchuduleHideEnemyUI"); // volvemos a iniciar los tiempos del slider, que estan en esta clase

            }
            else
            {
                // si disparamos a otra parte del enemigo queremos que la imagen del enemigo desaparesca
                //enemyImage.enabled = false;
                //enemyHealthSlider.gameObject.SetActive(false);// Y si no disparamos al enemigo queremos que este oculto 
                HideEnemyUI(); // llamamos al metodo ya que tiene lo mismo de los dos lineas comentadas 

            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);// el disparo con la misma direccion en 100 unidades 
        }
    }
}
