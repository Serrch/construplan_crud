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

    public List<OrdenDeCompra> GetAll()
        => GetOrdenes();

    public OrdenDeCompra? GetById(int id)
        => GetOrdenes().FirstOrDefault(o => o.Id == id);

    public void Create(OrdenDeCompra orden)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        orden.Id = ordenes.Count == 0 ? 1 : ordenes.Max(o => o.Id) + 1;

        ordenes.Add(orden);
        Save(ordenes);
    }

    public void Update(OrdenDeCompra orden)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        OrdenDeCompra ordenVieja = ordenes.First(o => o.Id == orden.Id);

        ordenVieja.NumeroDeOrden = orden.NumeroDeOrden;
        ordenVieja.Proveedor = orden.Proveedor;
        ordenVieja.MontoTotal = orden.MontoTotal;
        ordenVieja.Fecha = orden.Fecha;

        Save(ordenes);
    }

    public void Delete(int id)
    {
        List<OrdenDeCompra> ordenes = GetOrdenes();
        OrdenDeCompra orden = ordenes.First(o => o.Id == id);
        ordenes.Remove(orden);
        Save(ordenes);
    }
}
