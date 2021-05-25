var newsscontroller = {
    init: function () {
        newsscontroller.registerEvent();
        newsscontroller.loadData();

    },
    registerEvent: function () {
        $('#addmodal').off('click').on('click', function () {
            $('#modelUpdate').modal('show');
            newscontroller.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            newscontroller.saveData();
        });
    },
    saveData: function () {
        var name = $('#CategoryName').val();

        var user = {

            CategoryName: name,
            MetaTitle: meta,
            ParId: par,
            Id: id

        }
        $.ajax({
            url: '/News/SaveData',
            data:
            {
                strcate: JSON.stringify(user)
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (status == true) {

                    alert('Update Success');
                    $('#modelUpdate').modal('hide');
                    categorycontroller.loadData();
                }
                else {
                    alert(response.Message);
                }
            },
            error: function (err) {
                Console.log(err);

            }
        })

    },
    resetForm: function () {
        $('#Id').val('0');
        $('#CategoryName').val('');
        $('#MetaTitle').val('');
        $('#ParId').val(0);



    },
    loadData: function () {
        $.ajax({
            url: '/Newss/ListNews',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Id: item.Id,
                            Title: item.Title,
                            Detail: item.Detail,
                            Photo: item.Photo,
                            Date: item.Date
                            
                        });
                    });
                    $('#tblData').html(html);
                }
            }

        })

    }
}
newsscontroller.init();