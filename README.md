# FDV-MovimientoRectilineo-2627

## Ejercicio 1

1. Crear un script que mueva el objeto hacia un punto fijo que se marque como objetivo utilizando el método Translate de la clase Transform. El objetivo debe ser una variable pública, de esta forma conseguimos manipularla en el inspector y ver el efecto de distintos valores en las coordenadas. Utilizar this.transform.Translate(goal) en el start, solo se mueve una vez. Experimentar las siguientes opciones:

    1. Añadir this.transform.Translate(goal); al Update e ir multiplicando goal = goal * 0.5f; de esta forma darás saltos más pequeños cada vez. ¿Qué sucede si pones la instrucción  goal = goal * 0.5f; en el Start?. Qué sucede si en el Update incluyes goal = goal * 2.0f;
    2.  Configurar la coordenada Y del Objetivo en 0.
    3.   Poner al Objetivo una coordenada Y distinta de cero.
    4.  Modificar el script para que el objeto despegue del suelo y vuele como un avión.

Al utilizar el `Translate` en el `Start`, efectivamente el objeto se moverá una sola vez hacia el objetivo definido por la variable pública `goal`. Esto significa que al iniciar el juego, el objeto se desplazará inmediatamente a la posición especificada por `goal`, pero no continuará moviéndose después de eso.

Sin embargo, si se coloca `this.transform.Translate(goal);` dentro del `Update`, el objeto se moverá continuamente hacia el objetivo en cada frame del juego. Esto permitirá que el objeto se acerque progresivamente al objetivo, y si se multiplica `goal` por 0.5f en cada actualización, los movimientos serán cada vez más pequeños, acercándose más lentamente al objetivo.

Si configuramos la coordenada Y del objetivo en 0, el objeto se moverá únicamente en el plano XZ, permaneciendo a nivel del suelo.

Por otro lado, si se establece una coordenada Y del objetivo distinta de cero, el objeto se desplazará hacia arriba o hacia abajo dependiendo del valor de Y, lo que permitirá simular un movimiento vertical.

![Ejercicio1](Media/Ejercicio-1.gif)

## Ejercicio 2

2. El Objetivo no es un objetivo propiamente dicho, sino una dirección en la que queremos movernos. La información relevante de un vector es la dirección. Los vectores normalizados, conservan la misma dirección pero su escala no afecta al movimiento. Se debe conseguir un movimiento consistente de forma que la escala no afecte a la traslación. Del mismo modo, se debe conseguir que el recorrido realizado por el personaje entre un frame y otro no tenga aberraciones espacio-temporales. Para ello se debe considerar la relación entre la velocidad, el espacio y el tiempo. Por otra parte, el tiempo que transcurre entre un frame y otro se obtiene con: Time.deltaTime. En este ejercicio se pretende dotar de esa consistencia al movimiento que hace el personaje, para ello:

    1. Sustituir la dirección del movimiento por su equivalente normalizada. Esto se consigue con el método normalized de la clase Vector3: this.transform.Translate(goal.normalized);
    2. Con el vector normalizado, lo podemos multiplicar por un valor de velocidad para determinar cómo de rápido va el personaje. public float speed = 0.1f this.transform.Translate(goal.normalized*speed)
    3. A pesar de que esas velocidades puedan parecer ahora que son consistentes, no lo son, porque dependen de la velocidad a la que se produzca el Update. El tiempo entre dos Updates del mismo objeto no es necesariamente siempre el mismo, con lo que se pueden tener inconsistencias en la velocidad, y a pesar de que en aplicaciones con poca complejidad no lo notemos, se debe usar: this.transform.Translate(goal.normalized * speed*Time.deltaTime); para suavizar el movimiento ya que Time.deltaTime es el tiempo que ha pasado desde el último frame. 

Al utilizar `goal.normalized`, se asegura que el objeto se mueva en la dirección correcta sin importar la magnitud del vector `goal`. Esto significa que el objeto siempre se moverá hacia el objetivo, pero la distancia recorrida en cada frame será consistente.

Por ello, se modifica el funcionamiento al introducir el parámetro `speed` que teniendo el vector ya normalizado, nos permitirá determinar la velocidad a la que se mueve el objeto. Al multiplicar el vector normalizado por `speed` con un valor bajo como `0.1f`, se obtiene un movimiento consistente y controlado bastante menos errático.

Al añadir el factor de `Time.deltaTime`, se asegura que el movimiento del objeto sea independiente de la velocidad de fotogramas del juego. Esto significa que, sin importar cuán rápido o lento se ejecute el juego, el objeto se moverá a una velocidad constante hacia el objetivo.

![Ejercicio2](Media/Ejercicio-2.gif)

## Ejercicio 3

3. En lugar de seguir usando una dirección como objetivo, vamos a movernos ahora hacia una verdadera posición objetivo. Lo agregaremos como un campo público en la clase para poder configurarlo desde el Inspector. También agregaremos un campo para configurar la velocidad del personaje desde el propio Inspector. Aunque queramos desplazarnos hacia un punto en el espacio, el método Translate debe recibir la dirección del movimiento. La dirección que une dos puntos se obtiene restando el más lejano al más cercano. Por último, si el personaje no está encarando el objetivo (podría incluso estar de espaldas a él), el desplazamiento será suave pero la orientación de su malla no será consistente. Por esta razón será necesario rotarlo de forma que su eje z local (forward) apunte hacia el objetivo. La función LookAt del Transform nos ayudará con esto. En este caso, por tanto, para movernos hacia un punto en el espacio que configuramos a una velocidad dada: 
    1. Hacemos el objetivo una variable pública public Transform goal y añadimos un public float speed = 1.0f. 
    2. Giramos al personaje para lograr que su movimiento sea hacia delante utilizando this.transform.LookAt(goal.position) en el Start para que gire primero y luego se mueva. 
    3. La dirección en la que nos tenemos que mover viene determinada por la diferencia entre la posición del objetivo y nuestra posición:
     Vector3 direction = goal.position - this.transform.position;
    4. this.transform.Translate(direction.normalized * speed * Time.deltaTime)
    5. Si lo ejecutamos en este momento, como la orientación del personaje va a cambiar, el translate no va a funcionar correctamente, el vector direction se ha calculado con el sistema de referencia alineado con el mundo. Al rotar al personaje cambiamos ese sistema de referencia, por tanto, habrá que hacer el cambio a la dirección. Los ejes del personaje y el mundo no están alineados. El movimiento se debe hacer de forma relativa al sistema de referencia del mundo.
    **this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World).**

Tras realizar los cambios propuestos, el código del ejercicio 3 quedaría de la siguiente manera:

```csharp
public class Ejercicio3 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;

    void Start()
    {
        this.transform.LookAt(goal.position);
    }

    void Update()
    {
        direction = goal.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
```

Esto permite el movimiento del objeto hacia el objetivo de manera consistente y suave, asegurando que la orientación del objeto esté alineada con la dirección del movimiento.

![Ejercicio3](Media/Ejercicio-3.gif)

## Ejercicio 4

4. Añadir Debug.DrawRay(this.transform.position,direction,Color.red) para depuración para comprobar que la dirección está correctamente calculada. 

En este ejercicio, (debido a que por algún motivo el Debug.DrawRay no se veía en la escena), decidí utilizar un line renderer que apunta a la misma dirección que el vector forward del objeto, para comprobar que la dirección está correctamente calculada. El código del ejercicio 4 quedaría de la siguiente manera:

```csharp
public class Ejercicio4 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;
    private LineRenderer lineRenderer;

    void Start()
    {
        this.transform.LookAt(goal.position);

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        direction = goal.position - this.transform.position;
        Vector3 lineEnd = this.transform.position + this.transform.forward * 10f;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, lineEnd);
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
```

![Ejercicio4](Media/Ejercicio-4.gif)

## Ejercicio 5

5. Agregar un cubo en la escena que hará de objetivo, que debe ser movido por teclado, con las teclas de flechas. Sobre la escena que has trabajado ubica un personaje que va a seguir al cubo. 

    1. Crear un script que haga que el personaje siga al cubo continuamente sin aplicar simulación física.
    2. Agregar un campo público que permita graduar la velocidad del movimiento desde el inspector de objetos.
    3. Utilizar la tecla de espaciado para incrementar la velocidad del desplazamiento en el tiempo de juego.

En este escenario, tenemos esta vez dos cubos, el rojo se encarga de seguir al verde, que es el que se mueve con las flechas del teclado. El código del ejercicio 5 para el cubo rojo sería una versión bastante simplificada vista en los ejercicios anteriores:

```csharp
public class Ejercicio5_1 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;

    void Update()
    {
        this.transform.LookAt(goal.position);
        direction = goal.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
```

Mientras que el código del cubo verde que se mueve con las flechas del teclado sería el siguiente:

```csharp
public class Ejercicio5_2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boostMultiplier = 2f;

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.Space) ? speed * boostMultiplier : speed;

        Vector3 movement = new Vector3(
            horizontalMovement,
            0f,
            verticalMovement
        );

        transform.Translate(movement * currentSpeed * Time.deltaTime);
    }
}
```

Debido a que estamos utilizando el antiguo sistema de Input, el Input Manager tuvo que ser modificado a utilizar el mismo por motivos de simplicidad. Como se puede ver, al utilizar las flechas del teclado el cubo verde se moverá en el plano XZ, y al presionar la barra espaciadora, la velocidad de movimiento se incrementará mientras se mantenga presionada. El cubo rojo seguirá al cubo verde de manera consistente, ajustando su orientación y movimiento hacia la posición del cubo verde.

![Ejercicio5](Media/Ejercicio-5.gif)

## Ejercicio 6

6. Como solución alternativa al ejercicio 5, en este ejercicio se trabaja el Movimiento rectilíneo hacia el objetivo haciendo avanzar al personaje siempre en línea recta hacia adelante.  Para ello, el personaje debe rotar hacia el objetivo y luego avanzar en la dirección forward. En este caso hay  que destacar que el método Translate de la clase Transform tiene dos formas de realizar la traslación. Esto lo podemos resolver rotando al personaje hacia su objetivo (LookAt) y trasladándolo en el eje forward, respecto al sistema de referencia local, lo que corresponde al valor por defecto del parámetro de Translate: relativeTo.

```csharp
    // Move the object forward along its z axis 1 unit/second.
    transform.Translate(Vector3.forward * Time.deltaTime);
```

    1. Realizar un script que gire al personaje hacia su objetivo para llegar hasta él desplazándose sobre su vector forward local.

Para este ejercicio, simplemente se tuvo que modificar el script de movimiento del cubo rojo para que en lugar de moverse hacia el objetivo utilizando la dirección calculada, se moviera hacia adelante en su propio sistema de referencia local. El código del ejercicio 6 quedaría de la siguiente manera:

```csharp
public class Ejercicio6 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;

    void Update()
    {
        this.transform.LookAt(goal.position);
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
```

![Ejercicio6](Media/Ejercicio-6.gif)

## Ejercicio 7

7. Cuando ejecutamos el script, el personaje calcula la dirección hacia el objetivo y se mueve hacia él, pero no puede dejar de moverse y se produce jittering. La razón es que todavía estamos dentro del bucle, calculando la dirección y moviéndonos hacia él. En la mayoría de los casos no vamos a conseguir que nuestro personaje se mueva a la posición exacta del objetivo, con lo que continuamente oscila en torno a esa posición. En el último frame, el paso speed * Time.deltaTime será mayor que la distancia que falta, así que el personaje se pasa un poco. En el frame siguiente, el objetivo queda detrás: LookAt lo gira 180° y avanza otra vez, pasándose de nuevo. Se repite cada frame: el personaje vibra sobre el objetivo y además da la vuelta constantemente, lo que se nota mucho. Por eso, debemos tener algún cálculo del tipo de rango de tolerancia. Incluimos una variable global pública, public float accuracy = 0.01f; y una condición if(direction.magnitude > accuracy). Aún con el accuracy, el personaje puede hacer jitter si la velocidad es muy alta.

    1. Controlar el jittering utilizando la magnitud de la dirección.
    2. Dado que la dirección nos la da la separación entre el objetivo y el personaje, también podemos controlar el jittering utilizando la distancia entre los dos puntos.
    3. Usar MoveTowards, que no se pasa nunca: si el paso es mayor que lo que falta, deja el objeto justo en el destino. 

En ambos casos, el jittering desaparece por completo y el movimiento del personaje hacia el objetivo es suave y consistente. El código del ejercicio 7 eliminando el jittering manualmente quedaría de la siguiente manera:

```csharp
public class Ejercicio7_1 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    public float accuracy = 0.01f;
    private Vector3 direction;

    void Update()
    {
        direction = goal.position - this.transform.position;
        if (direction.magnitude > accuracy)
        {
            this.transform.LookAt(goal.position);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
```

Mientras que el código del ejercicio 7 utilizando `MoveTowards` para eliminar el jittering quedaría de la siguiente manera:

```csharp
using UnityEngine;

public class Ejercicio7_2 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;

    void Update()
    {
        this.transform.LookAt(goal.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, goal.position, speed * Time.deltaTime);
    }
}
```

![Ejercicio7_1](Media/Ejercicio-7_1.gif)
![Ejercicio7_2](Media/Ejercicio-7_2.gif)

## Ejercicio 8

En este ejercicio se trabaja el Movimiento rectilíneo haciendo avanzar al personaje siempre en línea recta hacia adelante introduciendo una mejora. El uso de la función LookAt hace que el personaje gire instantáneamente hacia el objetivo, provocando cambios bruscos. Se aconseja realizar una transición suave a lo largo de diferentes frames. Para ello, en lugar de computar una rotación del ángulo necesario, se realizan sucesivas rotaciones donde el ángulo en cada frame viene dado por los valores intermedios al interpolar la dirección original y la final. Para esto utilizaremos la función Slerp de la clase Quaternion:

```csharp
Quaternion.Slerp(Vector3 from, Vector3 to, float t);
```

Para este ejercicio, simplemente reemplazamos el `LookAt` por la interpolación esférica utilizando `Quaternion.Slerp`. Esto permite que el objeto gire suavemente hacia el objetivo en lugar de hacerlo de manera instantánea. El código del ejercicio 8 quedaría de la siguiente manera:

```csharp
public class Ejercicio8 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    public float rotationSpeed = 2f;
    private Vector3 direction;

    void Update()
    {
        direction = goal.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation, 
            Quaternion.LookRotation(direction), 
            rotationSpeed * Time.deltaTime
        );
        this.transform.position = Vector3.MoveTowards(this.transform.position, goal.position, speed * Time.deltaTime);
    }
}
```

![Ejercicio8](Media/Ejercicio-8.gif)

## Ejercicio 9

9. En esta sección se trabaja un sistema básico de Waypoints. Se debe crear un circuito en una escena con la colección de puntos que conforman el circuito. Cada punto del circuito será un objeto 3D al que se le asigne la etiqueta “waypoint”. También se agregará un objeto personaje que será el que recorra los objetivos. Este objeto debe implementar el script con la mecánica de recorrido del circuito. Para ello, debe recuperar la referencia a cada uno de los objetivo y realizar los desplazamientos de un objetivo a otro aplicando el trabajo anterior. En la lógica se debe incluir la gestión de obtener quién es el siguiente objetivo.

Para este ejercicio se editó la escena para incluir un circuito compuesto por 4 cilindros que tendrán la etiqueta "Waypoint" y un cubo rojo que será el personaje que recorrerá el circuito. Al principio de la ejecución, el cubo recoge todos los waypoints y los almacena en un array, para luego ir recorriéndolos uno a uno, seleccionando el índice del siguiente waypoint a recorrer. El código del ejercicio 9 quedaría de la siguiente manera:

```csharp
public class Ejercicio9 : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;
    private Vector3 direction;
    private GameObject[] waypoints;
    private GameObject nextWaypoint;

    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        System.Array.Reverse(waypoints);
        nextWaypoint = waypoints.Length > 0 ? waypoints[0] : null;
    }

    void Update()
    {
        MoveToNextWaypoint(nextWaypoint.transform);

        if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) < 0.1f)
        {
            int currentIndex = System.Array.IndexOf(waypoints, nextWaypoint);
            int nextIndex = (currentIndex + 1) % waypoints.Length;
            nextWaypoint = waypoints[nextIndex];
        }
    }

    void MoveToNextWaypoint(Transform goal)
    {
        direction = goal.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation, 
            Quaternion.LookRotation(direction), 
            rotationSpeed * Time.deltaTime
        );
        this.transform.position = Vector3.MoveTowards(this.transform.position, goal.position, speed * Time.deltaTime);
    }
}
```

![Ejercicio9](Media/Ejercicio-9.gif)

## Ejercicio 10

10. En esta sección se trabaja con el sistema de Waypoints de Unity. Para ello debes importar como asset en el proyecto la carpeta Utility. Configura el circuito, agrega el objetivo que debe perseguir el personaje y añade al personaje que recorrerá el circuito el script WaypointProgressTracker. Finalmente agrega un script al personaje que lo haga perseguir al objetivo. El sistema moverá el objetivo alejándolo del personaje moviéndose de un punto a otro del circuito. El personaje intenta perseguir al objetivo con nuestro script, por tanto, está “obligando” al objetivo a ir de un punto a otro a la par que lo persigue.

    1. Crea el circuito. Un objeto vacío con el componente WaypointCircuit.
    2. Crea los waypoints como hijos. Objetos vacíos dentro del circuito colocados donde quieras que pase el personaje. El orden en la jerarquía es el orden del recorrido. Pon su altura a la del personaje para que la ruta no vaya por el suelo ni flote.
    3. Asígnalos. En el Inspector del circuito, pulsa Assign using all child objects. Cada vez que añadas, borres o reordenes un hijo, vuelve a pulsarlo.
    4. Elige recta o curva. Con Smooth Route activado la ruta es una curva que pasa por todos los puntos; desactivado, son tramos rectos. En la vista Scene verás la ruta dibujada en amarillo (con los Gizmos activados). La ruta es cerrada: del último punto vuelve al primero.
    5. Crea el objeto auxiliar. Un objeto vacío llamado Target, fuera del personaje (no como hijo, o se movería con él). Para verlo mientras pruebas, puedes ponerle una esfera pequeña sin collider. 
    6. Configura el tracker en el personaje. Añade WaypointProgressTracker, arrastra el circuito al campo Circuit y Target al campo Target. 
    7. Añade el script que persigue al target porque el tracker solo avanza cuando el personaje avanza.

Para este ejercicio se siguieron los pasos expuestos para crear la escena con el circuito adecuado y se reutilizó el script del ejercicio 8 para hacer funcionar al cubo rojo para que persiga al `target` que se mueve a lo largo del circuito.

![Ejercicio10](Media/Ejercicio-10.gif)