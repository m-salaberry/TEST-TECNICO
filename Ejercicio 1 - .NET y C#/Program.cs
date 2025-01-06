using Ejercicio_1___.NET_y_C_;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;




public class Program
{



    public static void Main(string[] args)
    {

        try
        {

            ObtenerAPI().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }

    }

    private static async Task ObtenerAPI()
    {
        try
        {
            HttpResponseMessage res = await client.GetAsync("");
            if (res.IsSuccessStatusCode)
            {
                string respuestaJson = await res.Content.ReadAsStringAsync();

                List<CatBreed> razasGatos = JsonSerializer.Deserialize<List<CatBreed>>(respuestaJson) ?? throw new ArgumentNullException("No se pudo deserializar el JSON");
                List<CatBreed> razasGatosOrdenadas = razasGatos.OrderBy(x => x.intelligence).ToList();

                int promedioAdaptabilidad = 0;

                foreach (var raza in razasGatos)
                {
                    promedioAdaptabilidad += raza.adaptability;
                }

                promedioAdaptabilidad = promedioAdaptabilidad / razasGatos.Count;

                for (int i = razasGatosOrdenadas.Count - 1; i >= razasGatosOrdenadas.Count - 10; i--)
                {
                    Console.Write("\n\nNombre: " + razasGatosOrdenadas[i].name);
                    Console.Write("\nDescripción: " + razasGatosOrdenadas[i].description);
                    Console.Write("\nPais de origen: " + razasGatosOrdenadas[i].origin);
                    Console.Write("\nInteligencia: " + razasGatosOrdenadas[i].intelligence);
                }

                Console.WriteLine($"\n\nEl promedio de adaptabilidad entre todas las razas de gatos es de: {promedioAdaptabilidad}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static HttpClient client = new()
    {
        //https://api.thecatapi.com/v1/breeds
        BaseAddress = new Uri("https://api.thecatapi.com/v1/breeds"),

    };


}