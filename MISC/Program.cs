



Console.WriteLine(Pruebas.Saludar() + " Yael");


Console.WriteLine(Pruebas.Saludar() + " Yael ".AgregarFecha());

Console.WriteLine(Pruebas.Saludar()+ " Michel ".AgregarFecha() );




public static class Pruebas
{
    public static string Saludar() => "Hola";

    public static string AgregarFecha(this string str)
        => str + DateTime.Now.ToString();

}