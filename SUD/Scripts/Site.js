$(document).ready(function () {
    $('#miTablaVentaDetalle').DataTable({
        "ajax": {
            "url": "/SaleDetails/getSaleDetails",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion", "autoWidth": true },
            { "data": "Precio", "autoWidth": true },


            {
                "data": function (data, type, row, meta) {
                    return '<a class="btn btn-warning fa fa-pencil-square-o" href="../SaleDetails/Edit/' + data.SaleDetailId + '"></a><a class="btn btn-info fa fa-info-circle" href="../SaleDetails/Details/' + data.SaleDetailId + '" ></a><a class="btn btn-danger fa fa-trash" href="../SaleDetails/Delete/' + data.SaleDetailId + '" ></a>';


                }
                , "autoWidth": true
            }

        ]
    });

    
    $('#miTablaCliente').DataTable({
        "processing": true,
        "serverSide": true,
        "paging": true,
            
            "ajax": {
                "url": "/clients/getClients",
                "type": "POST",
                "datatype": "json",
                
                
                
            },
            "columns": [
                { "data": "FirstNameContact", "autoWidth": true },
                { "data": "LastNameContact", "autoWidth": true },
                { "data": "ComertialName", "autoWidth": true },


                {
                    "data": function (data, type, row, meta) {
                        return '<a class="btn btn-warning fa fa-pencil-square-o" href="../Clients/Edit/' + data.ClientId + '"></a><a class="btn btn-info fa fa-info-circle" href="../Clients/Details/' + data.ClientId + '" ></a><a class="btn btn-danger fa fa-trash" href="../Clients/Delete/' + data.ClientId + '" ></a>';


                    }
                    , "autoWidth": true
                }

            ]
        });
})