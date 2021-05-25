var categorycontroller = {
    init: function () {
        categorycontroller.registerEvent();
        categorycontroller.loadData();

    },
    registerEvent: function () {
        $('#addmodal').off('click').on('click', function () {
            $('#modelUpdate').modal('show');
            categorycontroller.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            categorycontroller.saveData();
        });
    },
    saveData: function () {
        var name = $('#CategoryName').val();
        
        var user = {

            CategoryName: name,
            MetaTitle: meta,
            ParId:par,       
            Id: id

        }
        $.ajax({
            url: '/ProductCate/SaveData',
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
            url: '/ProductCate/ListCategory',
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
                            CategoryName: item.CategoryName,
                            MetaTitle: item.MetaTitle,
                            ParId:item.ParId                        
                        });
                    });
                    $('#tblData').html(html);
                }
            }

        })

    }
}
categorycontroller.init();