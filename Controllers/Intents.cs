namespace LIN.Access.Controllers;


public static class Intents
{


    /// <summary>
    /// Obtiene los intentos de Passkey que no han sido aceptados
    /// </summary>
    /// <param name="contextDevice">ID Contexto</param>
    /// <param name="account">ID de la cuenta</param>
    public async static Task<ReadAllResponse<PasskeyIntentDataModel>> ReadAll(string contextDevice, string account)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("intents");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("contextDevice", $"{contextDevice}");
        request.Headers.Add("user", $"{account}");

        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<PasskeyIntentDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }


}
