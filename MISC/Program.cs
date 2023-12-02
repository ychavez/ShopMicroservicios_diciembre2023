

//Console.WriteLine("Hola");
//Console.WriteLine(Pruebas.PrimerNoRepetidoFunc("Hola"));

//Console.WriteLine("abuela");
//Console.WriteLine(Pruebas.PrimerNoRepetidoFunc("abuela"));

var SaludoFunc = (string x) => "H";

Predicate<string> predicado = x => true;

Func<string,bool> predicado2 = x => true;

Action<string, bool> accion = (x, y) => Console.WriteLine("Hola desde el action");

var saludoAct = (string x, bool y) => Console.WriteLine("Hola desde el action con inferencua") ;





accion += Pruebas.AccionVacia;

accion("Hola", true);


//saludoAct("Hola", true);







//Console.WriteLine(Pruebas.Saludo("Yael", SaludoFunc) );

//Console.WriteLine(Pruebas.Saludar() + " Yael");


//Console.WriteLine(Pruebas.Saludar() + " Yael ".AgregarFecha());

//Console.WriteLine(Pruebas.Saludar() + " Michel ".AgregarFecha());




public static class Pruebas
{
    public static string Saludar() => "Hola";

    public static string AgregarFecha(this string str)
        => str + DateTime.Now.ToString();

    //Hola
    //Mama
    //Abuela
   public static char PrimerNoRepetido(string palabra)
    {
        return palabra.ToArray()
            .GroupBy(x => x)
            .Where(x => x.Count() == 1)
            .Select(x => x.Key)
            .FirstOrDefault();
    }

    /// <summary>
    /// Action<string, bool> accion
    /// </summary>
    /// 
    public static void AccionVacia(string palabra, bool evaluacion) {

        Console.WriteLine("Hola desde la accion vacia");
    }



    public static Func<string,char> PrimerNoRepetidoFunc
        = (s) => s.ToArray()
            .GroupBy(x => x)
            .Where(x => x.Count() == 1)
            .Select(x => x.Key)
            .FirstOrDefault();


    public static string Saludo(string nombre,Func<string,string> funcion) 
    {
 

        return $"Hola {nombre} y su letra es la {funcion}";

    }
}