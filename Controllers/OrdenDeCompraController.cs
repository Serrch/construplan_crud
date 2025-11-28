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
        return View(_service.GetAll());
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

        _service.Create(model.Orden);

        TempData["SuccessMessage"] = "Orden creada correctamente.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        OrdenDeCompra? orden = _service.GetById(id);

        if (orden == null)
        {
            TempData["ErrorMessage"] = "Orden no encontrada.";
            return RedirectToAction("Index");
        }

        return View(new OrdenDeCompraViewModel
        {
            Orden = orden,
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

        _service.Update(model.Orden);

        TempData["SuccessMessage"] = "Orden editada correctamente.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        TempData["SuccessMessage"] = "Orden eliminada correctamente.";

        return RedirectToAction("Index");
    }
}
