$(document).ready(function () {
    const tabla = $('#tablaOrdenDeCompra');

    if (tabla.length) {
        tabla.DataTable({
            responsive: true,
            language: {
                url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json",
                paginate: {
                    previous: "<i class='bi bi-chevron-left'></i>",
                    next: "<i class='bi bi-chevron-right'></i>"
                }
            },
            pageLength: 5
        });
    }
});


setTimeout(() => {
    document.querySelectorAll('.alert').forEach(alert => {
        let bsAlert = new bootstrap.Alert(alert);
        bsAlert.close();
    });
}, 3000);
