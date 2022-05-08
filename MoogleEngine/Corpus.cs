namespace MoogleEngine;
    public class Corpus
    {
        public static List<Vector_Documents> Run()
        {
            FileInfo[] files = new DirectoryInfo("../Content").GetFiles();

            List<Vector_Documents> documents = new List<Vector_Documents>();

            for(int i = 0; i < files.Length;i++)
            {
                StreamReader reader = new StreamReader(files[i].FullName);
                Vector_Documents aux = new Vector_Documents(reader.ReadToEnd(), files[i].Name, files[i].FullName);
                documents.Add(aux);
            }
            Tools.Calcular_idf(documents);
            for(int i = 0; i < documents.Count; i++)
            {
                documents[i].Purge();
            }
            for(int i = 0; i < documents.Count; i++)
            {
                documents[i].Sim_Cos();
            }
            return documents;
        } 
        public static Vector_Documents Tratar_Query(string query, Dictionary diccionario)
        {
            Vector_Documents query_Trat = new Vector_Documents(query,"Query","query"); 
            for(int i = 0; i < query_Trat.size; i++)
            {
                diccionario.Asig_Idf(query_Trat[i]);
            }  
            for(int i = 0; i < query_Trat.size; i++)
            {
                query_Trat.Purge();
            } 
            if(query_Trat[0].palabra == "")
                query_Trat.Remove(0);
            for(int i = 0; i < query_Trat.size; i++)
            {
                if(query_Trat[i].InvertFrecuency == -1)
                {
                    for(int j = 0; j < diccionario.size; j++)
                    {
                        int aux = Distancia_de_Levenshtein.LD(query_Trat[i].palabra,diccionario[j].palabra);
                        if(aux == 2) 
                        {
                            query_Trat[i].palabra = diccionario[j].palabra;
                            query_Trat.AggCambio = (query_Trat[i].pos,i);
                            query_Trat[i].InvertFrecuency = diccionario[j].InvertFrecuency;
                            query_Trat.Sim_Cos();
                            return query_Trat;
                        }
                        else if(aux == 1)
                        {
                            query_Trat[i].palabra = diccionario[j].palabra;
                            query_Trat.AggCambio = (query_Trat[i].pos,i);
                            query_Trat[i].InvertFrecuency = diccionario[j].InvertFrecuency;
                            query_Trat.Sim_Cos();
                            return query_Trat;
                        }
                    }
                }
            } 
            query_Trat.Sim_Cos();
            return query_Trat;      
        }
    }   
