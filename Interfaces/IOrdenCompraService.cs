using construplan_examen.Models;

namespace construplan_examen.Interfaces
{
    public interface IOrdenCompraService
    {
        List<OrdenDeCompra> GetAll();
        OrdenDeCompra? GetById(int id);
        void Create(OrdenDeCompra orden);
        void Update(OrdenDeCompra orden);
        void Delete(int id);
    }

}
