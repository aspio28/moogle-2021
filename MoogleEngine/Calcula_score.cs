namespace MoogleEngine;
    public class Calcula_score
    {
        public static float[] Similitud(List<Vector_Documents> documents, Vector_Documents query)
        {
            float[] scores = new float[documents.Count];
            float ProductoParcial = 0;

            for(int i = 0; i < documents.Count; i++)
            {
                for(int j = 0; j < query.size; j++)
                {
                    for(int k = 0; k < documents[i].size; k++)
                    {
                        if(query[j].palabra == documents[i][k].palabra)
                            ProductoParcial += documents[i][k].peso_TF_idf * query[j].peso_TF_idf;
                    }    
                }
                scores[i] = (float)(ProductoParcial/(documents[i].SC * query.SC));
                ProductoParcial = 0;
            }
            return scores;
        }
    }
