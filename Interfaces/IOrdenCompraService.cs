using construplan_examen.Models;

namespace construplan_examen.Interfaces
{
    public interface IOrdenCompraService
    {
        ServiceResult<List<OrdenDeCompra>> GetAll();
        ServiceResult<OrdenDeCompra> GetById(int id);
        ServiceResult<OrdenDeCompra> Create(OrdenDeCompra orden);
        ServiceResult<OrdenDeCompra> Update(OrdenDeCompra orden);
        ServiceResult<OrdenDeCompra> Delete(int id);
    }

}
