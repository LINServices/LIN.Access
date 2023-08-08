namespace LIN.Access.Hubs;


public sealed class PasskeyHub
{

    //======== Eventos ========//


    /// <summary>
    /// Recive un intento (Admin)
    /// </summary>
    public event EventHandler<PasskeyIntentDataModel>? OnRecieveIntentAdmin;



    /// <summary>
    /// Recibe un intento (Client)
    /// </summary>
    public event EventHandler<PasskeyIntentDataModel>? OnRecieveResponse;



    //======== Propiedades ========//


    /// <summary>
    /// Conexion del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Obtiene el ID de usuario asignado este dispositivo
    /// </summary>
    public string ID
    {
        get
        {
            return HubConnection?.ConnectionId ?? string.Empty;
        }
    }



    /// <summary>
    /// Usuario 
    /// </summary>
    public string Account { get; set; }


    /// <summary>
    /// Si es una sesion de Admin
    /// </summary>
    public bool IsAdmin { get; set; }



    /// <summary>
    /// Constructor de un HUB
    /// </summary>
    public PasskeyHub(string account, bool isAdmin = false)
    {
        this.Account = account;
        this.IsAdmin = isAdmin;
    }


    /// <summary>
    /// Reconecta la conexion
    /// </summary>
    public async void Reconnect()
    {
        await Suscribe();
    }



    /// <summary>
    /// Cierra la conexion
    /// </summary>
    public async void Disconet()
    {
        try
        {
            if (HubConnection != null)
                await HubConnection.StopAsync();
            
        }
        catch
        {
        }

    }



    /// <summary>
    /// Conecta el Hub
    /// </summary>
    public async Task Suscribe()
    {
        try
        {
            // Crea la conexion al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl(ApiServer.PathURL("realtime/passkey"))
                 .WithAutomaticReconnect()
                 .Build();


            // Recibe un intento Admin
            HubConnection.On<PasskeyIntentDataModel>("newintent", (pass) =>
            {
                OnRecieveIntentAdmin?.Invoke(null, pass);
            });


            // Recibe una respuesta (Cliente)
            HubConnection.On<PasskeyIntentDataModel>("recieveresponse", (pass) =>
            {
                OnRecieveResponse?.Invoke(null, pass);
            });


            // Inicia la conexion
            await HubConnection.StartAsync();


            if (IsAdmin)
            {
                // Suscribe al grupo
                await HubConnection.InvokeAsync("JoinAdmin", Account);
            }



        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error HUB Account: " + ex);
        }

    }





    public async void SendIntent(PasskeyIntentDataModel intent)
    {
        try
        {
            await HubConnection!.InvokeAsync("JoinIntent", intent);
        }
        catch
        {

        }
    }


    public async void SendStatus(PasskeyIntentDataModel intent)
    {
        try
        {
            await HubConnection!.InvokeAsync("ReceiveRequest", intent);
        }
        catch
        {

        }
    }


    //==== Disparadores ====//





}
