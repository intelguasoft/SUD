function savePurchase() {

    debugger;

    //data
    let data = $("#Data").val();

    //cliente
    let customer = $("#Customer").val();

    //valor
    let value = $("#Value").val();

    let token = $('input[name="__RequestVerificationToken"]').val();
    let tokenAddr = $('form[action="/Purchases/Create"] input[name="__RequestVerificationToken"]').val();
    let headers = {};
    let headersAddr = {};
    headers['__RequestVerificationToken'] = token;
    headersAddr['__RequestVerificationToken'] = tokenAddr;

    // Save the purchase
    let url = "/Purchases/Create";

    $.ajax({
        url: url,
        type: "POST",
        datatype: "json",
        headers: headersAddr,
        data: { Id: 0, Data: data, Customer: customer, Value: value, __RequestVerificationToken: token },
        success: function (data) {
            if (data.Resultado > 0) {
                debugger;
                listarItems(data.Resultado);
            }
        }
    });
}

function listarItems(purchaseId) {

    let url = "/Purchases/ListItems";

    $.ajax({
        url: url,
        type: "GET",
        data: { id: purchaseId },
        datatype: "html",
        headers: headersAddr,
        success: function (data) {
            let listItems = $("#listItems");
            listItems.empty();
            listItems.show();
            listItems.html(data);

        }
    });
}