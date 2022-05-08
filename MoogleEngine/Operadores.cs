namespace MoogleEngine;
public class Operadores
{
    private (bool,string) Desprecio = (false,"");
    private (bool,string) Obligacion = (false,"");
    private (bool,string,string) Cercania = (false,"","");
    private (bool,string,int) importancia = (false,"",0);
    public Operadores(string query)
    {
        //solo funciona para un operador de cada tipo por query
        query = query.Replace('á' , 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó','o').Replace('ú', 'u');
        for(int i = 0; i < query.Length; i++)
        {
            if(query[i] == '!')
            {
                this.Desprecio.Item1 = true;
                this.Desprecio.Item2 = query.Substring(i+1, Tools.Distancia(query, i+1));
            }
            if(query[i] == '^') 
            {
                this.Obligacion.Item1 = true;
                this.Obligacion.Item2 = query.Substring(i+1, Tools.Distancia(query, i+1));
            }
            if(query[i] == '~' && i > 2 && (char.IsLetter(query[i-2]))) 
            {
                this.Cercania.Item1 = true;
                this.Cercania.Item2 = query.Substring(i-1 - Tools.DistaciaInversa(query, i-1), Tools.DistaciaInversa(query, i-1));
                this.Cercania.Item3 = query.Substring(i+2, Tools.Distancia(query, i+1));

            }
            if(query[i] == '*') 
            {
                this.importancia.Item1 = true;
                for(;;i++)
                {
                    if(query[i] == '*') this.importancia.Item3++;
                    else
                    {
                        this.importancia.Item2 = query.Substring(i, Tools.Distancia(query, i));
                        break;
                    }
                }
            }
        }
    }
    public bool Hay
    {
        get
        {
            if(this.Desprecio.Item1 || this.Obligacion.Item1 || this.Cercania.Item1 || this.importancia.Item1)
                return true;
            return false;
        }
    }
    public void AppOperadores(List<Vector_Documents> documents, float[] scores)
    {
        (int,int) aux = (-1,-1);
        //paso por cada documento viendo que operadores hay
        for(int i = 0; i < documents.Count; i++)
        {
            if(this.Desprecio.Item1 && Tools.find(documents[i], this.Desprecio.Item2) && this.Desprecio.Item1) 
                scores[i] = 0;

            if(this.Obligacion.Item1 && !Tools.find(documents[i], this.Obligacion.Item2)) 
                scores[i] = 0;

            if(this.importancia.Item1 && Tools.find(documents[i], this.importancia.Item2)) 
                scores[i] = scores[i] * (this.importancia.Item3 + 1);
            
            if(this.Cercania.Item1)
            {
               //busco la posicion de las dos palabras y reviso si estan
               aux = Tools.EstaDoble(documents[i], this.Cercania.Item2, this.Cercania.Item3);
               if(aux.Item1 != -1 && aux.Item2 != -1)
               {
                    int distancia = CalcularDis(documents[i], aux);
                    if(distancia > 50 && distancia < 100) scores[i] += scores[i]/10;
                    else if(distancia > 10) scores[i] += scores[i];
                    else if(distancia > 5) scores[i] += 2*scores[i];
                    else if(distancia > 1) scores[i] += 3*scores[i];
                    else scores[i] += 5*scores[i];
               }
           }
        }
    } 
    private static int CalcularDis(Vector_Documents document, (int,int) pos)
    {
        //obtengo las listas de las posiciones
        List<int> uno = document[pos.Item1].ArrPos;
        List<int> dos = document[pos.Item2].ArrPos;
        int distancia = int.MaxValue;
        int i = 0;
        int j = 0;
        //voy por cada lista al mismo tiempo calculando la distancia y quedandome con la menor
        while(i < uno.Count && j < dos.Count)
        {
            if((uno[i] > dos[j]) && distancia > uno[i] - dos[j]) 
            {
                distancia = uno[i] - dos[j];
                j++;
            }
            else if((dos[j] > uno[i]) && distancia > dos[j] - uno[i])
            {
                distancia = dos[j] - uno[i];
                i++;
            }
            else if(i > j) j++;
            
            else i++;
        }
        return distancia;
    }
}