//let table = new DataTable('#myTable');

var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({

        "ajax":
        {
            "url": "/Admin/Product/AllProduct"
        },
        "columns":
            [
                { "data": "id" },
                { "data": "name" },
                { "data": 'description' },
                { "data": 'price' },
                { "data": 'imageURL' },
                { "data": 'category.name' },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                       <a href="/Admin/Product/CreateUpdate?id=${data}" ><i class="bi bi-pencil-square"></i></a>
                       <a onClick=RemoveProduct("/Admin/Product/Delete?id=${data}")><i class="bi bi-trash"></i></a>
                        `
                    }
                }
            ]

    });
});

function RemoveProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data)
                {
                    if (data.success) {
                        dtable.ajax.reload();
                            toastr.success(data.message);
                        }
                        else {
                            toastr.error(data.message);
                        }
                    }
                })
        }
    
    })
}
