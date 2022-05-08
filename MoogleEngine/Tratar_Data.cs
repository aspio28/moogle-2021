using System.Text.RegularExpressions;
namespace MoogleEngine;
public class Tratar_Data
{ 
    public static string[] Tokenizacion(string texto)
    { 
        texto = texto.ToLower();
        texto = texto.Replace('á' , 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó','o').Replace('ú', 'u');
        string[] aux = Regex.Split(texto, @"\W+");
        return aux;
    }
}
