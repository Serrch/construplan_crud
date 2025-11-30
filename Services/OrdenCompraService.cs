using construplan_examen.Interfaces;
using construplan_examen.Models;
using construplan_examen.Utils;

namespace construplan_examen.Services;
public class OrdenService : IOrdenCompraService
{
    private readonly IHttpContextAccessor _http;

    public OrdenService(IHttpContextAccessor http)
    {
        _http = http;
    }

    public ServiceResult<List<OrdenDeCompra>> GetAll()
    {
        List<OrdenDeCompra> listaOrdenes = GetOrdenes();

        if(listaOrdenes.Count == 0) return ServiceResult<List<OrdenDeCompra>>.FailWithData(listaOrdenes, "Lista de ordenes vacia", ErrorType.NotFound);
        
        return ServiceResult<List<OrdenDeCompra>>.Ok(listaOrdenes,"Lista de ordenes consultada exitosamente");
    }
        

    public ServiceResult<OrdenDeCompra> GetById(int id)
    {
        OrdenDeCompra? orden = GetOrdenes().FirstOrDefault(o => o.Id == id);

        if(orden == null) return ServiceResult<OrdenDeCompra>.FailIdNotFound("Orden de compra", id);

        return ServiceResult<OrdenDeCompra>.OkFinded(orden, "Orden de compra");
    }

    public ServiceResult<OrdenDeCompra> Create(OrdenDeCompra orden)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        orden.Id = ordenes.Count == 0 ? 1 : ordenes.Max(o => o.Id) + 1;

        ordenes.Add(orden);
        Save(ordenes);

        return ServiceResult<OrdenDeCompra>.OkAction(orden, "guardar", "Orden de compra");

    }

    public ServiceResult<OrdenDeCompra> Update(OrdenDeCompra orden)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        OrdenDeCompra? ordenVieja = ordenes.FirstOrDefault(o => o.Id == orden.Id);

        if (ordenVieja == null) return ServiceResult<OrdenDeCompra>.FailIdNotFound("Orden de compra", orden.Id);

        ordenVieja.NumeroDeOrden = orden.NumeroDeOrden;
        ordenVieja.Proveedor = orden.Proveedor;
        ordenVieja.MontoTotal = orden.MontoTotal;
        ordenVieja.Fecha = orden.Fecha;

        Save(ordenes);
        return ServiceResult<OrdenDeCompra>.OkAction("editar", "Orden de compra");
    }

    public ServiceResult<OrdenDeCompra> Delete(int id)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        OrdenDeCompra? orden = ordenes.FirstOrDefault(o => o.Id == id);

        if (orden == null) return ServiceResult<OrdenDeCompra>.FailIdNotFound("Orden de compra", id);

        ordenes.Remove(orden);
        Save(ordenes);
        return ServiceResult<OrdenDeCompra>.OkAction("borrar", "Orden de compra");
    }
    private List<OrdenDeCompra> GetOrdenes()
    {
        List<OrdenDeCompra>? listaOrdenes = _http.HttpContext!.Session.GetObject<List<OrdenDeCompra>>("ordenes");

        if (listaOrdenes == null) return new List<OrdenDeCompra>();

        return listaOrdenes;
    }

    private void Save(List<OrdenDeCompra> ordenes)
    {
        _http.HttpContext!.Session.SetObject("ordenes", ordenes);
    }

}
