global using LIN.Shared.Tools;
global using LIN.Types.Enumerations;

namespace LIN.Access.Controllers;


public static class User
{


    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    public async static Task<ReadOneResponse<UserDataModel>> CreateAsync(UserDataModel modelo)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("user/create");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<UserDataModel>>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Obtiene los datos de una cuenta especifica
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
    public async static Task<ReadOneResponse<UserDataModel>> ReadOneAsync(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/read/id");

        url = Web.AddParameters(url, new(){
            {"id",id.ToString() }
        });

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<UserDataModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



    /// <summary>
    /// Obtiene los datos de una cuenta
    /// </summary>
    /// <param name="cuenta">Usuario de la cuenta</param>
    public async static Task<ReadOneResponse<UserDataModel>> ReadOneAsync(string cuenta)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/read/user");

        url = Web.AddParameters(url, new(){
            {"user", cuenta }
        });


        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);


            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<UserDataModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }




    /// <summary>
    /// Obtiene la cantidad de las ventas en un plazo determinado
    /// </summary>
    /// <param name="user">ID del usuario</param>
    /// <param name="days">Dias atras</param>
    public async static Task<ReadOneResponse<HomeDto>> TotalSales(int user, int days)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("Inventory/home");

        client.DefaultRequestHeaders.Add("id", $"{user}");
        client.DefaultRequestHeaders.Add("days", $"{days}");

        try
        {
            // Envía la solicitud
            HttpResponseMessage response = await client.GetAsync(url);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<HomeDto>>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }




    /// <summary>
    /// Obtiene la valuacion de los inventarios donde un usuario es admin
    /// </summary>
    public async static Task<ReadOneResponse<decimal>> ValueInventorys(int user)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("Inventory/valuation");

        client.DefaultRequestHeaders.Add("id", $"{user}");

        try
        {
            // Envía la solicitud
            HttpResponseMessage response = await client.GetAsync(url);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<decimal>>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Inicia una sesion
    /// </summary>
    public async static Task<ReadOneResponse<UserDataModel>> Login(string cuenta, string password)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/login");

        httpClient.DefaultRequestHeaders.Add("user", $"{cuenta}");
        httpClient.DefaultRequestHeaders.Add("password", $"{password}");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<UserDataModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Inicia una sesion por medio de un token
    /// </summary>
    public async static Task<ReadOneResponse<UserDataModel>> Login(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/LoginWithToken");


        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("token", $"{token}");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<UserDataModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Busqueda de usuarios por medio de su ID
    /// </summary>
    public async static Task<ReadAllResponse<UserDataModel>> SearhByPattern(string pattern, int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/searchByPattern");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("pattern", $"{pattern}");
        request.Headers.Add("id", $"{id}");

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<UserDataModel>>(responseBody) ?? new();


            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }






    public async static Task<ReadAllResponse<UserDataModel>> GetAll(string pattern, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("user/findAllUsers");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("pattern", $"{pattern}");
        request.Headers.Add("token", $"{token}");

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<UserDataModel>>(responseBody) ?? new();


            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }





    /// <summary>
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ResponseBase> UpdateAsync(UserDataModel modelo)
    {
        await Task.Delay(100);
        return new();
    }





    /// <summary>
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ResponseBase> ResetPassword(string key, UpdatePasswordModel modelo)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        string url = ApiServer.PathURL("account/security/password/reset");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

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







    public async static Task<ResponseBase> ResendMail(int mail, string token)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("mailID", mail.ToString());
        client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("account/security/email/resend");

        

        try
        {
            HttpRequestMessage ms = new(HttpMethod.Post, url);

            // Envía la solicitud
            HttpResponseMessage response = await client.SendAsync(ms);

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








    public async static Task<ResponseBase> Verficate(string key)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        string url = ApiServer.PathURL("account/security/email/verify");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

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
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ResponseBase> UpdatePassword(UpdatePasswordModel modelo)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("user/update/password");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

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
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ResponseBase> UpdateGender(string token, Genders genero)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("genero", $"{(int)genero}");

        string url = ApiServer.PathURL("user/update/gender");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

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
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ResponseBase> Disable(int id, string password)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("user/disable/account");
        string json = JsonConvert.SerializeObject(new UserDataModel()
        {
            ID = id,
            Contraseña = password
        });

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

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
    /// Elimna un usuario
    /// </summary>
    public async static Task<ResponseBase> DeleteAsync(UserDataModel modelo)
    {
        await Task.Delay(100);
        return new();
    }





}
