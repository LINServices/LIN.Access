namespace LIN.Access.Controllers;


public class Api
{


    
    public async static Task<ReadAllResponse<PaisDataModel>> GetPaises()
    {

        // Url del servicio 
        string url = ApiServer.PathURL("api/pais");



        // Ejecucion
        try
        {

            // Envía la solicitud
            var response = await new HttpClient().GetAsync(url);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<PaisDataModel>>(responseContent) ?? new();

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }




}
