// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const baseUrl = "http://localhost:5000/api/";
const baseUrlClient = "https://localhost:44372/";

function GetCategoryList() {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "categories",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $.each(result, (i, item) => {
                    $("#product-edit-category").append($("<option>", {
                        value: item["categoryId"],
                        text: item["categoryName"],
                    }));
                });
            }
        });
    });
}

function GetProductList(isUser) {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "products",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            success: (result, status, xhr) => {
                $.each(result,  (indexedDB,  value) => {
                    const id = value["productId"];
                    const name = value["productName"];
                    const unitPrice = value["unitPrice"];
                    let $row;
                    if(isUser == true){
                        $row = '<tr>' +
                            '<td>' + name + '</td>' +
                            '<td>' + value["weight"] +'</td>' +
                            '<td>' + unitPrice + '</td>' +
                            '<td>' + value["unitInStock"] + '</td>' +
                            '<td>' + value["categoryName"] + '</td>' +
                            '<a href="' + baseUrlClient + 'Products?handler=buy&id=' + id + '&name=' + name + '&price=' + unitPrice + '">Add to cart</a>';
                    }
                    else {
                        $row = '<tr>' +
                            '<td>' + name + '</td>' +
                            '<td>' + value["weight"] +'</td>' +
                            '<td>' + unitPrice + '</td>' +
                            '<td>' + value["unitInStock"] + '</td>' +
                            '<td>' + value["categoryName"] + '</td>' +
                            '<td><a href="' + baseUrlClient + 'Products/Edit?id=' + id + '">Edit</a> | ' +
                            '<a href="' + baseUrlClient + 'Products/Details?id=' + id + '">Detail</a> | ' +
                            '<a href="' + baseUrlClient + 'Products/Delete?id=' + id + '">Delete</a> | ' +
                            '<a href="' + baseUrlClient + 'Products?handler=buy&id=' + id + '&name=' + name + '&price=' + unitPrice + '">Add to cart</a>';
                    }
                    $("tbody:last").append($row);
                });
            },
            error: (xhr, status, error) => {
                console.log(error);
            },
        });
    });
}

function SearchProduct(isUser) {
    $("#btn-search-product").click(() => {
        const prodNameParam = $("#product-search-name").val();
        let query = "?$filter=contains(ProductName, '" + prodNameParam + "')";
        const prodPriceParam = parseInt($("#product-search-price").val());
        if(prodNameParam > 0) {
            query += " and UnitPrice eq " + prodPriceParam;
        }
        $("tbody").empty();
        $.ajax({
            url: baseUrl + "products" + query,
            type: "get",
            contentType: "application/json",
            dataType: "json",
            success: (result, status, xhr) => {
                $.each(result,  (indexedDB,  value) => {
                    const id = value["productId"];
                    const name = value["productName"];
                    const unitPrice = value["unitPrice"];
                    let $row;
                    if(isUser == true){
                        $row = '<tr>' +
                            '<td>' + name + '</td>' +
                            '<td>' + value["weight"] +'</td>' +
                            '<td>' + unitPrice + '</td>' +
                            '<td>' + value["unitInStock"] + '</td>' +
                            '<td>' + value["categoryName"] + '</td>' +
                            '<a href="' + baseUrlClient + 'Products?handler=buy&id=' + id + '&name=' + name + '&price=' + unitPrice + '">Add to cart</a>';
                    }
                    else {
                        $row = '<tr>' +
                            '<td>' + name + '</td>' +
                            '<td>' + value["weight"] +'</td>' +
                            '<td>' + unitPrice + '</td>' +
                            '<td>' + value["unitInStock"] + '</td>' +
                            '<td>' + value["categoryName"] + '</td>' +
                            '<td><a href="' + baseUrlClient + 'Products/Edit?id=' + id + '">Edit</a> | ' +
                            '<a href="' + baseUrlClient + 'Products/Details?id=' + id + '">Detail</a> | ' +
                            '<a href="' + baseUrlClient + 'Products/Delete?id=' + id + '">Delete</a> | ' +
                            '<a href="' + baseUrlClient + 'Products?handler=buy&id=' + id + '&name=' + name + '&price=' + unitPrice + '">Add to cart</a>';
                    }
                    $("tbody:last").append($row);
                });
            },
            error: (xhr, status, error) => {
                console.log(error);
            },
        });
    });
}

function GetProductDetail(id) {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "products/" + id,
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $("#product-name").html(result["productName"]);
                $("#product-weight").html(result["weight"]);
                $("#product-price").html(result["unitPrice"]);
                $("#product-unit").html(result["unitInStock"]);
                $("#product-category").html(result["categoryName"]);
            },
        });
    });
}

function GetProductEdit(id) {
    GetCategoryList();
    
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "products/" + id,
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $("#product-edit-name").attr("value", result["productName"]);
                $("#product-edit-weight").attr("value", result["weight"]);
                $("#product-edit-price").attr("value", result["unitPrice"]);
                $("#product-edit-unit").attr("value", result["unitInStock"]);
            }
        }); 
    });
}

function UpdateProduct(id) {
    $("#btn-update-product").click(() => {
        const category = $("#product-edit-category").val();
        const name = $("#product-edit-name").val();
        const weight = $("#product-edit-weight").val();
        const price = $("#product-edit-price").val();
        const unit = $("#product-edit-unit").val();
        
        const data = {
            "productId": id,
            "productName": name,
            "categoryId": category,
            "weight": weight,
            "unitPrice": price,
            "unitInStock": unit,
        };
        console.log(data);
        
        $.ajax({
            url: baseUrl + "products",
            type: "put",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(data),
            processData: false,
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                console.log("Update success");
            }
        });
    });
}

function CreateProduct() {
    $("#btn-create-product").click(() => {
        const category = parseInt($("#product-edit-category").val());
        const name = $("#product-edit-name").val();
        const weight = $("#product-edit-weight").val();
        const price = parseInt($("#product-edit-price").val());
        const unit = parseInt($("#product-edit-unit").val());
        
        const data = {
            "productName": name,
            "categoryId": category,
            "weight": weight,
            "unitPrice": price,
            "unitInStock": unit,
        };
        console.log(data);
        
        $.ajax({
            url: baseUrl + "products",
            type: "post",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(data),
            processData: false,
            error: (xhr, status, error) => {
                console.log("Error");
                console.log(error);
            },
            success: (result, status, xhr) => {
                console.log("Create success");
            }
        });
    });
}

function DeleteProduct(id) {
    $("#btn-delete-product").click(() => {
        $.ajax({
            url: baseUrl + "products/" + id,
            type: "delete",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                console.log("Delete success");
            }
        });
    });
}

function GetOrderList() {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "orders",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: {},
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $.each(result,  (index,  value) => {
                    const id = value["orderId"];
                    let $row = '<tr>' +
                        '<td>' + value["orderId"] + '</td>' +
                        '<td>' + value["orderedDate"] + '</td>' +
                        '<td>' + value["requiredDate"] +'</td>' +
                        '<td>' + value["shippedDate"] + '</td>' +
                        '<td>' + value["freight"] + '</td>' +
                        '<td>' + value["user"]["email"] + '</td>' +
                        '<td><a href="' + baseUrlClient + 'Orders/Details?id=' + id + '">Details</a>';                    ;
                    $("tbody:last").append($row);
                });
            }
        });
    });
}

function GetOrdersByUserId(id) {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "orders",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: {
                "userId": id,
            },
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $.each(result,  (index,  value) => {
                    const id = value["orderId"];
                    let $row = '<tr>' +
                        '<td>' + value["orderId"] + '</td>' +
                        '<td>' + value["orderedDate"] + '</td>' +
                        '<td>' + value["requiredDate"] +'</td>' +
                        '<td>' + value["shippedDate"] + '</td>' +
                        '<td>' + value["freight"] + '</td>' +
                        '<td>' + value["user"]["email"] + '</td>' +
                        '<td><a href="' + baseUrlClient + 'Orders/Details?id=' + id + '">Details</a>';
                    $("tbody:last").append($row);
                });
            }
        });
    });
}

function GetOrderDetail(id) {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "orders/" + id,
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $("#order-orderedDate").html(result["orderedDate"]);
                $("#order-requiredDate").html(result["requiredDate"]);
                $("#order-shippedDate").html(result["shippedDate"]);
                $("#order-freight").html(result["freight"]);
                $("#order-user").html(result["user"]["email"]);
                $.each(result["orderDetails"], (index, value) => {
                    let $row = '<tr>' +
                        '<td>' + value["productName"] + '</td>' +
                        '<td>' + value["unitPrice"] +'</td>' +
                        '<td>' + value["quantity"] + '</td>' +
                        '<td>' + value["discount"] + '</td>';
                        $("tbody:last").append($row);
                });
            },
        });
    });
}

function CreateOrder() {
    $("#btn-create-order").click(() => {
        const orderedDate = $("#order-edit-orderedDate").val();
        const requiredDate = $("#order-edit-requiredDate").val();
        const shippedDate = $("#order-edit-shippedDate").val();
        const freight = parseInt($("#order-edit-freight").val());
        const discount = parseInt($("#order-edit-discount").val());
        const user = $("#order-edit-user").val();
        
        let details = [];
        $("tbody").find("tr").each((i, row) => {
            let productId;
            let quantity;
            let price;
            $(row).find("td").each((j, column) => {
                if(j == 0) {
                    productId = parseInt($(column).text());
                }
                if(j == 2) {
                    quantity = parseInt($(column).text());
                }
                if(j == 3) {
                    price = parseInt($(column).text());
                }
            });
            details.push({
                "productId": productId,
                "unitPrice": price,
                "quantity": quantity,
                "discount": discount,
            });
        });
        
        const data = {
            "userId": user,
            "orderedDate": orderedDate,
            "requiredDate": requiredDate,
            "shippedDate": shippedDate,
            "freight": freight,
            "orderDetails": details,
        };
        
        $.ajax({
            url: baseUrl + "orders",
            type: "post",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(data),
            processData: false,
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                console.log("Create order successfully");
            },
        });
    });
}

function GetUsers() {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "users",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $.each(result, (i, item) => {
                    $("#order-edit-user").append($("<option>", {
                        value: item["id"],
                        text: item["email"],
                    }));
                });
            },
        }); 
    });
}

function GetStatistics() {
    $("#btn-report").click(() => {
        $.ajax({
            url: baseUrl + "orders/statistics",
            type: "get",
            contentType: "application/json",
            dataType: "json",
            data: {
                "startDate": $("#startDate").val(),
                "endDate": $("#endDate").val(),
            },
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $.each(result, (i, item) => {
                    let $row = '<tr>' +
                        '<td>' + item["date"] + '</td>' +
                        '<td>' + item["total"] + '</td>';
                    $("tbody:last").append($row);
                });
            }
        });
    });
}

function GetUserDetail(id) {
    $(document).ready(() => {
        $.ajax({
            url: baseUrl + "users/" + id,
            type: "get",
            contentType: "application/json",
            dataType: "json",
            error: (xhr, status, error) => {
                console.log(error);
            },
            success: (result, status, xhr) => {
                $("#user-id").html(result["id"]);
                $("#user-email").html(result["email"]);
                $("#user-password").html(result["passwordHash"]);
            },
        });
    });
}