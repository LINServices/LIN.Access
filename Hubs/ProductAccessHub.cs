namespace LIN.Access.Hubs;


public class ProductAccessHub
{


    /// <summary>
    /// Conexion del HUB
    /// </summary>
    private protected HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Evento ON
    /// </summary>
    public event EventHandler<ProductDataTransfer>? On;



    /// <summary>
    /// Evento ON
    /// </summary>
    public event EventHandler<ProductDataTransfer>? OnUpdate;



    /// <summary>
    /// Evento ON
    /// </summary>
    public event EventHandler<InflowDataModel>? OnReciveInflow;


    /// <summary>
    /// Evento ON
    /// </summary>
    public event EventHandler<int>? OnDeleteProducto;


    /// <summary>
    /// Evento ON
    /// </summary>
    public event EventHandler<OutflowDataModel>? OnReciveOutflow;





    /// <summary>
    /// Envía el evento
    /// </summary>
    /// <param name="contenido"></param>
    private protected void SendEvent(ProductDataTransfer contenido)
    {
        On?.Invoke(null, contenido);
    }


    private protected void SendUpdate(ProductDataTransfer contenido)
    {
        OnUpdate?.Invoke(null, contenido);
    }


    /// <summary>
    /// Envía el evento
    /// </summary>
    /// <param name="contenido"></param>
    private protected void SendEvent(int contenido)
    {
        OnDeleteProducto?.Invoke(null, contenido);
    }




    /// <summary>
    /// Envía el evento
    /// </summary>
    /// <param name="contenido"></param>
    private protected void SendEvent(InflowDataModel contenido)
    {
        OnReciveInflow?.Invoke(null, contenido);
    }


    /// <summary>
    /// Envía el evento
    /// </summary>
    /// <param name="contenido"></param>
    private protected void SendEvent(OutflowDataModel contenido)
    {
        OnReciveOutflow?.Invoke(null, contenido);
    }



    /// <summary>
    /// ID del inventario
    /// </summary>
    public int Inventario { get; set; }




    /// <summary>
    /// Nuevo HUB de productos
    /// </summary>
    /// <param name="inventario">ID del inventario</param>
    public ProductAccessHub(int inventario)
    {
        this.Inventario = inventario;
        Suscribe();
    }



    /// <summary>
    /// Suscribe
    /// </summary>
    private async void Suscribe()
    {
        try
        {
            // Crea la conexion al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl(LIN.Access.ApiServer.PathURL("realtime/productos"))
                 .WithAutomaticReconnect()
                 .Build();


            // Evento cuando algo cambie
            HubConnection.On<ProductDataTransfer>("SendProduct", SendEvent);


            // Evento cuando algo cambie
            HubConnection.On<ProductDataTransfer>("UpdateProduct", SendUpdate);


            // Evento cuando algo cambie
            HubConnection.On<InflowDataModel>("SendInflow", SendEvent);

            // Evento cuando algo cambie
            HubConnection.On<int>("DeleteProducto", SendEvent);


            // Evento cuando algo cambie
            HubConnection.On<OutflowDataModel>("SendOutflow", SendEvent);


            // Inicia la conexion
            await HubConnection.StartAsync();

            // Suscribe al grupo
            await HubConnection.InvokeAsync("JoinGroup", $"{Inventario}");
        }
        catch {

        }

    }



    /// <summary>
    /// Envía la actualizacion de un modelo
    /// </summary>
    /// <param name="inventario">ID del inventario</param>
    /// <param name="productID">ID del nuevo producto</param>
    public async void SendAddModel(int inventario, int productID)
    {
        try
        {
            await HubConnection!.InvokeAsync("AddProduct", $"{inventario}", productID);
        }
        catch {

        }

    }


    public async void DeleteProducto(int inventario, int productID)
    {
        try
        {
            await HubConnection!.InvokeAsync("RemoveProducto", $"{inventario}", productID);
        }
        catch
        {

        }

    }



    public async void UpdateProduct(int inventario, int productID)
    {
        try
        {
            await HubConnection!.InvokeAsync("UpdateProduct", $"{inventario}", productID);
        }
        catch
        {

        }

    }





    /// <summary>
    /// Envía la actualizacion de una entrada
    /// </summary>
    public async void SendAddModelInflow(int inventario, int inflowID)
    {
        try
        {
            await HubConnection!.InvokeAsync("AddEntrada", $"{inventario}", inflowID);
        }
        catch
        {

        }

    }


    public async void SendAddModelOutflow(int inventario, int outflowID)
    {
        try
        {
            await HubConnection!.InvokeAsync("AddSalida", $"{inventario}", outflowID);
        }
        catch
        {

        }

    }




}
