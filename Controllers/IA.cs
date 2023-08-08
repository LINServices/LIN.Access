namespace LIN.Access.Controllers;


public static class IA
{

    /// <summary>
    /// Acceso a LIN Vision
    /// </summary>
    /// <param name="image">Imagen en bits</param>
    public static async Task<ReadOneResponse<ProductCategories>> IAVision(byte[] image)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("IA/image");

        // Crear HttpRequestMessage y agregar el encabezado
        string json = JsonConvert.SerializeObject(image);

        // Contenido
        StringContent content = new(json, Encoding.UTF8, "application/json");

        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<ProductCategories>>(responseBody);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }


}
