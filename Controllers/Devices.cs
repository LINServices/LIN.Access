namespace LIN.Access.Controllers;


public static class Devices
{


    /// <summary>
    /// Obtiene la lista de dispositivos asociados a una cuenta
    /// </summary>
    /// <param name="contextDevice">ID Del HUB Local</param>
    /// <param name="account">ID de la cuenta</param>
    public async static Task<ReadAllResponse<DeviceModel>> ReadAll(string contextDevice, int account)
    {

        // url
        string url = ApiServer.PathURL("devices");

        // Crear HttpClient
        var httpClient = new HttpClient();

        // Headers
        httpClient.DefaultRequestHeaders.Add("contextDevice", $"{contextDevice}");
        httpClient.DefaultRequestHeaders.Add("user", $"{account}");

        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // Leer la respuesta
            string responseBody = await response.Content.ReadAsStringAsync();

            // Objecto respuesta
            var obj = JsonConvert.DeserializeObject<ReadAllResponse<DeviceModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }


}
