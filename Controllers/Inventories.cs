namespace LIN.Access.Controllers;


public static class Inventories
{


    /// <summary>
    /// Crea un nuevo inventario
    /// </summary>
    /// <param name="modelo">Modelo del inventario</param>
    /// <param name="id">ID del usuario</param>
    public async static Task<CreateResponse> Create(InventoryDataModel modelo, int id)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("inventory/create");
        string json = JsonConvert.SerializeObject(modelo);


        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            content.Headers.Add("userID", $"{id}");

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
    /// Obtiene los inventarios que están asociados a una cuenta
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
    public async static Task<ReadAllResponse<InventoryDataModel>> ReadAll(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("inventory/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("id", $"{id}");

        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<InventoryDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



    /// <summary>
    /// Cambia el estado de un usuario en un inventario
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="estado">Nuevo estado</param>
    public async static Task<ResponseBase> UpdateState(int id, InventoryAccessState estado)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("Inventory/access/update/state");


        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");


            content.Headers.Add("id", $"{id}");
            content.Headers.Add("estado", $"{(int)estado}");

            // Envía la solicitud
            HttpResponseMessage response = await client.PutAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Actualiza el rol de un usuario en un inventario
    /// </summary>
    /// <param name="id">ID del inventario</param>
    /// <param name="rol">Nuevo rol</param>
    /// <param name="token">Token de acceso</param>
    public async static Task<ResponseBase> UpdateRol(int id, InventoryRols rol, string token)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("Inventory/update/rol");


        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");


            content.Headers.Add("accessID", $"{id}");
            content.Headers.Add("newRol", $"{(int)rol}");
            content.Headers.Add("token", $"{token}");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }





















    /// <summary>
    /// Nuevas invitaciones para un inventario
    /// </summary>
    /// <param name="modelo">Modelo del inventario con los nuevos accesos</param>
    /// <param name="token">Token de sesion</param>
    public async static Task<ResponseBase> GenerateInvitaciones(InventoryDataModel modelo, string token)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("Inventory/access/newinvitacion");
        string json = JsonConvert.SerializeObject(modelo);


        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");


            content.Headers.Add("token", $"{token}");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }







    /// <summary>
    /// Obtiene las invitaciones que un usuario tiena
    /// /// </summary>
    /// <param name="id">ID de la cuenta</param>
    public async static Task<ReadAllResponse<Notificacion>> ReadNotifications(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("Inventory/access/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("id", $"{id}");

        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<Notificacion>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }








    /// <summary>
    /// Obtiene los inregrantes de un inventario
    /// </summary>
    /// <param name="inv">ID del inventario</param>
    /// <param name="user">ID del usuario que esta consultando</param>
    public async static Task<ReadAllResponse<IntegrantDataModel>> GetIntegrants(int inv, int user)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("Inventory/access/members");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("inventario", $"{inv}");
        request.Headers.Add("usuario", $"{user}");


        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<IntegrantDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



    /// <summary>
    /// Elimina a alguien de el inventario
    /// </summary>
    /// <param name="inv">ID del inventario</param>
    /// <param name="user">ID del usuario que va a ser eliminado</param>
    /// <param name="inv">ID del usuario que va a realizar la accion</param>
    public async static Task<ResponseBase> DeleteSomeOne(int inv, int user, int me)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("Inventory/access/delete/One");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Add("inventario", $"{inv}");
        request.Headers.Add("usuario", $"{user}");
        request.Headers.Add("me", $"{me}");


        try
        {
            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseBody);

            return obj ?? new();


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }


}
