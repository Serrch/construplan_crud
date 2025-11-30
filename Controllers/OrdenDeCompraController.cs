using construplan_examen.Interfaces;
using construplan_examen.Models;
using Microsoft.AspNetCore.Mvc;

public class OrdenDeCompraController : Controller
{
    private readonly IOrdenCompraService _service;

    public OrdenDeCompraController(IOrdenCompraService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        ServiceResult<List<OrdenDeCompra>> result = _service.GetAll();

        return View(result.Data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        OrdenDeCompraViewModel ordenViewModel = new OrdenDeCompraViewModel()
        {
            Orden = new OrdenDeCompra(),
            FormAction = "Create",
        };
        return View(ordenViewModel);
    }

    [HttpPost]
    public IActionResult Create(OrdenDeCompraViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Los datos ingresados no son validos.";
            return View(model);
        }

        ServiceResult<OrdenDeCompra> result = _service.Create(model.Orden);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }

        TempData["SuccessMessage"] = result.Message;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ServiceResult<OrdenDeCompra> result = _service.GetById(id);

        if (result.Data == null || result.Success == false)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Index");
        }

        return View(new OrdenDeCompraViewModel
        {
            Orden = result.Data,
            FormAction = "Edit"
        });
    }

    [HttpPost]
    public IActionResult Edit(OrdenDeCompraViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Los datos ingresados no son validos.";
            model.FormAction = "Edit";
            return View(model);
        }

        ServiceResult<OrdenDeCompra> result = _service.Update(model.Orden);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            model.FormAction = "Edit";
            return View(model);
        }

        TempData["SuccessMessage"] = result.Message;
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        ServiceResult<OrdenDeCompra> result = _service.Delete(id);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Index");
        }

        TempData["SuccessMessage"] = "Orden eliminada correctamente.";

        return RedirectToAction("Index");
    }
}
