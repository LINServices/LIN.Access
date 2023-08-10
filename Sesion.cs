global using LIN.Shared.Models;
global using LIN.Types.Responses;
global using Newtonsoft.Json;
global using System;
global using System.Net.Http;
global using System.Text;
global using System.Threading.Tasks;
global using LIN.Shared.Enumerations;
global using Microsoft.AspNetCore.SignalR.Client;
global using LIN.Modules;

namespace LIN.Access;


public sealed class Sesion
{

    /// <summary>
    /// Token de la sesion
    /// </summary>
    public string Token { get; set; }   


    /// <summary>
    /// Genera una sesion artificial
    /// </summary>
    /// <param name="model">Modelo del usuario</param>
    /// <param name="token">Token de acceso</param>
    public static void GenerateSesion(UserDataModel model, string token)
    {
        Instance.Informacion = model;
        Instance.Token = token;
        IsOpen = true;
    }


    /// <summary>
    /// Informacion del usuario
    /// </summary>
    public UserDataModel Informacion { get; private set; }



    /// <summary>
    /// Si la sesion es activa
    /// </summary>
    public static bool IsOpen { get; set; } = false;


    /// <summary>
    /// Recarga o inicia una sesion
    /// </summary>
    public static async Task<(Sesion? Sesion, Responses Response)> LoginWith(string username, string password, dynamic platform, bool priv = false)
    {

        // Cierra la sesion Actual
        CloseSesion();
        
        // Validacion de user
        var response = await Controllers.User.Login(username, password);

        if (response.Response != Responses.Success)
            return (null, response.Response);

        // Datos de la instancia
        Instance.Informacion = response.Model;
        Instance.Token = response.Token;

        IsOpen = true;

        return (Instance, Responses.Success);

    }



    /// <summary>
    /// Recarga o inicia una sesion
    /// </summary>
    public static async Task<(Sesion? Sesion, Responses Response)> LoginWith(string token, dynamic platform)
    {

        // Cierra la sesion Actual
        CloseSesion();

        // Validacion de user
        var response = await Controllers.User.Login(token);

        if (response.Response != Responses.Success)
            return (null, response.Response);


        // Datos de la instancia
        Instance.Informacion = response.Model;
        Instance.Token = response.Token;

        IsOpen = true;

        return (Instance, Responses.Success);

    }





    /// <summary>
    /// Cierra la sesion
    /// </summary>
    public static void CloseSesion()
    {
        IsOpen = false;
        Instance.Informacion = new();
    }






    //==================== Singletong ====================//


    private readonly static Sesion _instance = new();

    private Sesion()
    {
        this.Informacion = new();
        Token = "";
    }


    public static Sesion Instance => _instance;
}
