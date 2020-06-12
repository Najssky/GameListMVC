var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Games/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "gameTitle", "width": "20%" },
            { "data": "gameGenre", "width": "20%" },
            { "data": "gameYear", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Games/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edytuj
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/Games/Delete?id='+${data})>
                            Usuń
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "Nie znaleziono danych"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Jesteś pewny?",
        text: "Raz usunięte dane są nieprzywracalne",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}