namespace LIN.Access.Controllers;


public static class Outflows
{


    /// <summary>
    /// Crea una salida de inventario
    /// </summary>
    /// <param name="modelo">Modelo</param>
    public async static Task<CreateResponse> CreateAsync(OutflowDataModel modelo)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("outflow/create");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Obtiene una salida por medio del ID
    /// </summary>
    /// <param name="id">ID de la salida</param>
    public async static Task<ReadOneResponse<OutflowDataModel>> Read(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("outflow/read");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("id", $"{id}");

        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<OutflowDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



    /// <summary>
    /// Obtiene la lista de salidas asociadas a un inventario
    /// </summary>
    /// <param name="id">ID del inventario</param>
    public async static Task<ReadAllResponse<OutflowDataModel>> ReadAll(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("outflow/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("id", $"{id}");

        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<OutflowDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }


    public async static Task<ReadOneResponse<List<byte>>> InformeMonth(int contextUser, int id, int mes, int año)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("outflow/info");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("id", $"{id}");
        request.Headers.Add("month", $"{mes}");
        request.Headers.Add("year", $"{año}");
        request.Headers.Add("contextUser", $"{contextUser}");

        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<List<byte>>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



}
